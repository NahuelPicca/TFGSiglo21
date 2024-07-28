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
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import axios, { AxiosResponse } from 'axios';
import { urlCategoria, urlTipoProducto } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { creacionTipoProductoDTO, deleteTipoProductoDTO, tipoProductoDTO } from './tipoProducto.modulo';
import { categoriaDTO } from '../Categoria/categoria.modulo';
import { Dropdown } from 'primereact/dropdown';

export default function FormularioTipoProducto() {
    let emptyTipoProducto: tipoProductoDTO = {
        id: 0,
        nombre: '',
        descripcion: '',
        categoriaId: 0
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [tiposProductos, setTiposProductos] = useState<tipoProductoDTO[]>([]);
    const [tipoProductoDialog, setTipoProductoDialog] = useState<boolean>(false);
    const [deleteTipoProductoDialog, setDeleteTipoProductoDialog] = useState<boolean>(false);
    const [deleteTiposProductosDialog, setDeleteTiposProductosDialog] = useState<boolean>(false);
    const [tipoProducto, setTipoProducto] = useState<tipoProductoDTO>(emptyTipoProducto);
    const [selectedTiposProductos, setSelectedTiposProductos] = useState<tipoProductoDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const [categorias, setCategorias] = useState<categoriaDTO[]>([]);
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<tipoProductoDTO[]>>(null);


    useEffect(() => {
        CargarTiposProductos()
    }, [])

    useEffect(() => {
        const url = `${urlCategoria}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<categoriaDTO[]>) => {
                setCategorias(respuesta.data);
            })
    }, [])

    function CargarTiposProductos() {
        const url = `${urlTipoProducto}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<tipoProductoDTO[]>) => {
                setTiposProductos(respuesta.data);
            })
    }

    async function BorrarTipoProducto(id: number) {
        try {
            await axios.delete(`${urlTipoProducto}/${id}`);
            CargarTiposProductos();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoTiposProductos(tiposProductos: deleteTipoProductoDTO[]) {
        try {
            await axios.delete(`${urlTipoProducto}`, { data: tiposProductos })
            CargarTiposProductos();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarTipoProducto(tipoProductoEditar: tipoProductoDTO, id: number) {
        try {
            await axios.put(`${urlTipoProducto}/${id}`, tipoProductoEditar)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearTipoProducto(tipoProducto: creacionTipoProductoDTO) {
        try {
            await axios.post(`${urlTipoProducto}/crear`, tipoProducto)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getTipoProducto(id: number) {
        axios.get(`${urlTipoProducto}/${id}`)
            .then((respuesta: AxiosResponse<tipoProductoDTO>) => {
                setTipoProducto(respuesta.data);
            });
    }

    const openNew = () => {
        setTipoProducto(emptyTipoProducto);
        setSubmitted(false);
        setTipoProductoDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setTipoProductoDialog(false);
    };

    const hideDeleteTipoProdDialog = () => {
        setDeleteTipoProductoDialog(false);
    };

    const hideDeleteTipoProductoDialog = () => {
        setDeleteTiposProductosDialog(false);
    };

    const saveTipoProducto = () => {
        setSubmitted(true);
        let _tiposProductos = [...tiposProductos];
        let _tipoProducto = { ...tipoProducto };

        if (tipoProducto.id) {
            const index = findIndexById(tipoProducto.id.toString());

            _tiposProductos[index] = _tipoProducto;
            editarTipoProducto(_tipoProducto, _tipoProducto.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se actualizó exitosamente.', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            _tipoProducto.id = parseInt(createId());
            crearTipoProducto(_tipoProducto)
            _tiposProductos.push(_tipoProducto);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se creó exitosamente.', life: 3000 });
        }
        setTiposProductos(_tiposProductos);
        setTipoProductoDialog(false);
        setTipoProducto(emptyTipoProducto);
    };

    const editTipoProducto = (tipoProducto: tipoProductoDTO) => {
        const tipoProductoLoc = { ...tipoProducto };
        getTipoProducto(tipoProductoLoc.id);
        setTipoProductoDialog(true);
    };

    const confirmDeleteTipoProducto = (tipoProducto: tipoProductoDTO) => {
        setTipoProducto(tipoProducto);
        setDeleteTipoProductoDialog(true);
    };

    const deleteTipoProducto = () => {
        let _tipoProducto = tiposProductos.filter((val) => val.id !== tipoProducto.id);
        BorrarTipoProducto(tipoProducto.id);
        setTiposProductos(_tipoProducto);
        setDeleteTipoProductoDialog(false);
        setTipoProducto(emptyTipoProducto);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Fue borrado exitosamente.', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < tiposProductos.length; i++) {
            if (tiposProductos[i].id === parseInt(id)) {
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
        setDeleteTiposProductosDialog(true);
    };

    const deleteSelectedTiposProductos = () => {
        let _categorias = tiposProductos.filter((val) => !selectedTiposProductos.includes(val));
        setTiposProductos(_categorias);
        setDeleteTiposProductosDialog(false);
        setSelectedTiposProductos([]);
        DeleteRangoTiposProductos(selectedTiposProductos);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'El rango fue borrado exitosamente.', life: 3000 });
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _tipoProd = { ...tipoProducto };

        // @ts-ignore
        _tipoProd[name] = val;

        setTipoProducto(_tipoProd);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _tipoProd = { ...tipoProducto };

        // @ts-ignore
        _tipoProd[name] = val;

        setTipoProducto(_tipoProd);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="Nuevo" icon="pi pi-plus" severity="success" onClick={openNew} />
                <Button label="Borrar" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedTiposProductos || !selectedTiposProductos.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: tipoProductoDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editTipoProducto(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteTipoProducto(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Listado de tipos de productos</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const tipoProdDialogFooter = (
        <React.Fragment>
            <Button label="Cancelar" icon="pi pi-times" outlined onClick={hideDialog} />
            <Button label="Guardar" icon="pi pi-check" onClick={saveTipoProducto} />
        </React.Fragment>
    );
    const deleteTipoProdDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteTipoProdDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteTipoProducto} />
        </React.Fragment>
    );
    const deleteTiposProductosDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteTipoProductoDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteSelectedTiposProductos} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate} right={rightToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={tiposProductos} selection={selectedTiposProductos}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedTiposProductos(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={10} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} tipo de producto" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="nombre" header="Nombre" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="descripcion" header="Descripcion" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={tipoProductoDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle de tipo de producto" modal className="p-fluid" footer={tipoProdDialogFooter} onHide={hideDialog}>
                <div className="field">
                    <label htmlFor="nombre" className="font-bold">
                        Nombre
                    </label>
                    <InputText id="nombre" value={tipoProducto.nombre} onChange={(e) => onInputChange(e, 'nombre')} required autoFocus className={classNames({ 'p-invalid': submitted && !tipoProducto.nombre })} />
                </div>
                <div className="field">
                    <label htmlFor="descripcion" className="font-bold">
                        Descripcion
                    </label>
                    <InputTextarea id="descripcion" value={tipoProducto.descripcion} onChange={(e: ChangeEvent<HTMLTextAreaElement>) => onInputTextAreaChange(e, 'descripcion')} required rows={3} cols={20} />
                </div>
                <div className="field col">
                    <label htmlFor="categoriaId" className="font-bold">
                        Categoría
                    </label>
                    <Dropdown id="categoriaId" onChange={(e: any) => onInputChange(e, 'categoriaId')}
                        options={categorias} optionLabel="nombre" optionValue='id' placeholder="Seleccione la categoría del tipo de producto"
                        value={tipoProducto.categoriaId}
                    />
                </div>
            </Dialog>

            <Dialog visible={deleteTipoProductoDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteTipoProdDialogFooter} onHide={hideDeleteTipoProdDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {tipoProducto && (
                        <span>
                            ¿Quieres borrar el tipo <b>{tipoProducto.nombre}</b> seleccionado?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteTiposProductosDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteTiposProductosDialogFooter} onHide={hideDeleteTipoProductoDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {tipoProducto && <span>¿Quieres borrar los tipos seleccionados?</span>}
                </div>
            </Dialog>
        </div >
    );
}