import React, { useState, useEffect, useRef, ChangeEvent } from 'react';
import { classNames } from 'primereact/utils';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Toast } from 'primereact/toast';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { InputTextarea } from 'primereact/inputtextarea';
import { IconField } from 'primereact/iconfield';
import { InputIcon } from 'primereact/inputicon';
import { InputNumber, InputNumberValueChangeEvent } from 'primereact/inputnumber';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import axios, { AxiosResponse } from 'axios';
import { urlCalidad, urlProducto, urlTipoProducto } from '../utils/endpoints';
import { creacionProductoDTO, deleteProductoDTO, productoDTO, productoEditarDto } from './producto.modulo';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { Calendar } from 'primereact/calendar';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { FormEvent } from 'primereact/ts-helpers';
import { convertirProductoAFormData, convertirProductoAFormDataProductoDTO } from './baseFormDataProducto';
import { Dropdown } from 'primereact/dropdown';
import { tipoProductoDTO } from '../TipoProducto/tipoProducto.modulo';
import { calidadDTO } from '../Calidad/calidad.modulo';
import { convierteFormatoFecha } from '../utils/Comunes';

export default function FormularioProducto() {
    let emptyProduct: productoDTO = {
        id: 0,
        codigo: '',
        marca: '',
        descripcion: '',
        precio: 0,
        anio: new Date(),
        habilitado: false,
        poster1: '',
        poster2: '',
        poster3: '',
        codigoCalidad: '',
        calidadId: 0,
        tipoProductoId: 0
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [products, setProducts] = useState<productoDTO[]>([]);
    const [productDialog, setProductDialog] = useState<boolean>(false);
    const [deleteProductDialog, setDeleteProductDialog] = useState<boolean>(false);
    const [deleteProductsDialog, setDeleteProductsDialog] = useState<boolean>(false);
    const [product, setProduct] = useState<productoDTO>(emptyProduct);
    const [selectedProducts, setSelectedProducts] = useState<productoDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<productoDTO[]>>(null);
    const [fecha, setFecha] = useState<Date>(new Date("10/10/2000"));
    const [calidades, setCalidades] = useState<calidadDTO[]>([]);
    const [tiposProductos, setTiposProductos] = useState<tipoProductoDTO[]>([]);

    useEffect(() => {
        try {
            CargarProductos();
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }, [])

    useEffect(() => {
        const url = `${urlCalidad}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<calidadDTO[]>) => {
                setCalidades(respuesta.data);
            })
    }, [])

    useEffect(() => {
        const url = `${urlTipoProducto}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<tipoProductoDTO[]>) => {
                setTiposProductos(respuesta.data);
            })
    }, [])

    function CargarProductos() {
        const url = `${urlProducto}`;
        axios.get(url)
            .then((respuesta: AxiosResponse<productoDTO[]>) => {
                setProducts(respuesta.data);
            });
    }

    async function BorrarProducto(id: number) {
        try {
            await axios.delete(`${urlProducto}/${id}`);
            CargarProductos();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoProductos(productos: deleteProductoDTO[]) {
        try {
            await axios.delete(`${urlProducto}`, { data: productos })
            CargarProductos();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarProducto(productoEditar: productoDTO, id: number) {
        try {
            const formData = convertirProductoAFormDataProductoDTO(productoEditar);
            await axios({
                method: 'put',
                url: `${urlProducto}/${id}`,
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' }
            });
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearProducto(producto: creacionProductoDTO) {
        try {
            const formData = convertirProductoAFormData(producto);
            await axios({
                method: 'post',
                url: `${urlProducto}/crear`,
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' }
            })
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getProducto(id: number) {
        axios.get(`${urlProducto}/${id}`)
            .then((respuesta: AxiosResponse<productoEditarDto>) => {
                setProduct(respuesta.data);
            });
    }

    const formatCurrency = (value: number) => {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    };

    const openNew = () => {
        setProduct(emptyProduct);
        setSubmitted(false);
        setProductDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setProductDialog(false);
    };

    const hideDeleteProductDialog = () => {
        setDeleteProductDialog(false);
    };

    const hideDeleteProductsDialog = () => {
        setDeleteProductsDialog(false);
    };

    const saveProduct = () => {
        setSubmitted(true);

        if (product.codigo.trim()) {
            let _products = [...products];
            let _product = { ...product };

            if (product.id) {
                const index = findIndexById(product.id.toString());

                _products[index] = _product;
                editarProducto(_product, _product.id)
                toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Producto actualizado', life: 3000 });
            } else {
                // _product.image = 'product-placeholder.svg';
                _product.id = parseInt(createId());
                crearProducto(_product)
                _products.push(_product);
                toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Producto creado', life: 3000 });
            }

            setProducts(_products);
            setProductDialog(false);
            setProduct(emptyProduct);
        }
    };

    const editProduct = (product: productoDTO) => {
        const producto = { ...product };
        getProducto(producto.id);
        setFecha(convierteFormatoFecha(product.anio));
        setProductDialog(true);
    };

    const confirmDeleteProduct = (product: productoDTO) => {
        setProduct(product);
        setDeleteProductDialog(true);
    };

    const deleteProduct = () => {
        let _products = products.filter((val) => val.id !== product.id);
        BorrarProducto(product.id);
        setProducts(_products);
        setDeleteProductDialog(false);
        setProduct(emptyProduct);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Producto borrado', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < products.length; i++) {
            if (products[i].id === parseInt(id)) {
                index = i;
                break;
            }
        }

        return index;
    };

    const createId = (): string => {
        let id = '';
        let chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

        for (let i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }

        return id;
    };

    const exportCSV = () => {
        dt.current?.exportCSV();
    };

    const confirmDeleteSelected = () => {
        setDeleteProductsDialog(true);
    };

    const deleteSelectedProducts = () => {
        let _products = products.filter((val) => !selectedProducts.includes(val));
        setProducts(_products);
        setDeleteProductsDialog(false);
        setSelectedProducts([]);
        DeleteRangoProductos(selectedProducts);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Productos Borrados', life: 3000 });
    };

    const onCheckBoxChangeHabilitado = (e: CheckboxChangeEvent) => {
        let _product = { ...product };
        _product['habilitado'] = e.checked;
        setProduct(_product);
    };

    const onChangeCalendar = (e: FormEvent) => {
        let _product = { ...product };
        const val = (e.target && e.target.value) || new Date();
        _product['anio'] = convierteFormatoFecha(val);
        setProduct(_product);
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...product };

        // @ts-ignore
        _product[name] = val;

        setProduct(_product);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...product };

        // @ts-ignore
        _product[name] = val;

        setProduct(_product);
    };

    const onInputNumberChange = (e: InputNumberValueChangeEvent, name: string) => {
        const val = e.value ?? 0;
        let _product = { ...product };

        // @ts-ignore
        _product[name] = val;

        setProduct(_product);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button className="button-secundario-guardar" label="Nuevo" severity="success" onClick={openNew} />
                <Button className="button-secundario-cancelar" label="Borrar" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedProducts || !selectedProducts.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const priceBodyTemplate = (rowData: productoDTO) => {
        return formatCurrency(rowData.precio);
    };

    const actionBodyTemplate = (rowData: productoDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editProduct(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteProduct(rowData)} />
            </React.Fragment>
        );
    };

    const formatDate = (value: any) => {
        return value.toLocaleDateString('en-US', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
        });
    };

    const dateBodyTemplate = (rowData: any) => {
        return formatDate(rowData.date);
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Productos</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const productDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideDialog} />
            <Button className="button-secundario-guardar" label="Guardar" onClick={saveProduct} />
        </React.Fragment>
    );
    const deleteProductDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteProduct} />
        </React.Fragment>
    );
    const deleteProductsDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductsDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteSelectedProducts} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={products} selection={selectedProducts}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedProducts(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} productos" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="codigo" header="Código" sortable style={{ minWidth: '6rem' }}></Column>
                    <Column field="descripcion" header="Descripción" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="marca" header="Marca" sortable style={{ minWidth: '10rem' }}></Column>
                    <Column field="precio" header="Precio" body={priceBodyTemplate} sortable style={{ minWidth: '6rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={productDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle del producto" modal className="p-fluid" footer={productDialogFooter} onHide={hideDialog}>
                <hr className="violet-line" />
                <div className="field">
                    <label htmlFor="codigo" className="font-bold">
                        Código
                    </label>
                    <InputText id="codigo" value={product.codigo} onChange={(e) => onInputChange(e, 'codigo')} required autoFocus className={classNames({ 'p-invalid': submitted && !product.codigo })} />
                    {submitted && !product.codigo && <small className="p-error">El código es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="descripcion" className="font-bold">
                        Descripcion
                    </label>
                    <InputTextarea id="descripcion" value={product.descripcion} onChange={(e: ChangeEvent<HTMLTextAreaElement>) => onInputTextAreaChange(e, 'descripcion')} required rows={3} cols={20} />
                </div>
                <div className="field">
                    <label htmlFor="calidad" className="font-bold">
                        Calidad del producto
                    </label>
                    <Dropdown id="calidadId" onChange={(e: any) => onInputChange(e, 'calidadId')}
                        options={calidades} optionLabel="nombre" optionValue='id' placeholder="Seleccione la calidad del producto"
                        value={product.calidadId}
                    />
                </div>
                <div className="field">
                    <label htmlFor="tipoProductos" className="font-bold">
                        Tipos de Productos
                    </label>
                    <Dropdown id="tipoProductoId" onChange={(e: any) => onInputChange(e, 'tipoProductoId')}
                        options={tiposProductos} optionLabel="nombre" optionValue='id' placeholder="Seleccione el tipo de producto"
                        value={product.tipoProductoId}
                    />
                </div>
                <div className="field">
                    <label htmlFor="marca" className="font-bold">
                        Marca
                    </label>
                    <InputText id="marca" value={product.marca} onChange={(e) => onInputChange(e, 'marca')} required autoFocus className={classNames({ 'p-invalid': submitted && !product.marca })} />
                    {submitted && !product.marca && <small className="p-error">La marca es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="anio" className="font-bold">
                        Año
                    </label>
                    <Calendar id="anio" value={fecha} readOnlyInput onChange={(e) => onChangeCalendar(e)} dateFormat="dd/mm/yy" />
                    {submitted && !product.anio && <small className="p-error">El año es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="precio" className="font-bold">
                        Precio
                    </label>
                    <InputNumber id="precio" value={product.precio} onValueChange={(e) => onInputNumberChange(e, 'precio')} mode="currency" currency="USD" locale="en-US" />
                </div>
                <div className="field" style={{ marginTop: "10px", marginBottom: "10px" }}>
                    <label htmlFor="habilitado" className="font-bold" style={{ marginRight: '5px' }}>
                        Habilitado
                    </label>
                    <Checkbox name="habilitado" onChange={e => onCheckBoxChangeHabilitado(e)} checked={product.habilitado} />
                </div>
                <hr className="violet-line" />
            </Dialog>

            <Dialog visible={deleteProductDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {product && (
                        <span>
                            ¿Quieres borrar el producto <b>{product.codigo}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteProductsDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteProductsDialogFooter} onHide={hideDeleteProductsDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {product && <span>¿Quieres borrar los productos seleccionados?</span>}
                </div>
            </Dialog>
            <br></br>
            <br></br>
            <br></br>
            <br></br>
            <br></br>
        </div >
    );
}