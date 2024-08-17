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
import { urlCategoria } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { categoriaDTO, creacionCategoriaDTO, deleteCategoriaDTO } from './categoria.modulo';

export default function FormularioCategoria() {
    let emptyCategoria: categoriaDTO = {
        id: 0,
        nombre: '',
        descripcion: ''
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [categorias, setCategorias] = useState<categoriaDTO[]>([]);
    const [categoriaDialog, setCategoriaDialog] = useState<boolean>(false);
    const [deleteCategoriaDialog, setDeleteCategoriaDialog] = useState<boolean>(false);
    const [deleteCategoriasDialog, setDeleteCategoriasDialog] = useState<boolean>(false);
    const [categoria, setCategoria] = useState<categoriaDTO>(emptyCategoria);
    const [selectedCategorias, setSelectedCategorias] = useState<categoriaDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<categoriaDTO[]>>(null);


    useEffect(() => {
        CargarCategorias()
    }, [])


    function CargarCategorias() {
        const url = `${urlCategoria}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<categoriaDTO[]>) => {
                setCategorias(respuesta.data);
            })
    }

    async function BorrarCategoria(id: number) {
        try {
            await axios.delete(`${urlCategoria}/${id}`);
            CargarCategorias();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoCategorias(categorias: deleteCategoriaDTO[]) {
        try {
            await axios.delete(`${urlCategoria}`, { data: categorias })
            CargarCategorias();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarCategoria(categoriaEditar: categoriaDTO, id: number) {
        try {
            await axios.put(`${urlCategoria}/${id}`, categoriaEditar)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearCategoria(categoria: creacionCategoriaDTO) {
        try {
            await axios.post(`${urlCategoria}/crear`, categoria)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getCategoria(id: number) {
        axios.get(`${urlCategoria}/${id}`)
            .then((respuesta: AxiosResponse<categoriaDTO>) => {
                setCategoria(respuesta.data);
            });
    }

    const openNew = () => {
        setCategoria(emptyCategoria);
        setSubmitted(false);
        setCategoriaDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setCategoriaDialog(false);
    };

    const hideDeleteCategoriaDialog = () => {
        setDeleteCategoriaDialog(false);
    };

    const hideDeleteCategoriasDialog = () => {
        setDeleteCategoriasDialog(false);
    };

    const saveCategoria = () => {
        setSubmitted(true);
        let _categorias = [...categorias];
        let _categoria = { ...categoria };

        if (categoria.id) {
            const index = findIndexById(categoria.id.toString());

            _categorias[index] = _categoria;
            editarCategoria(_categoria, _categoria.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se actualizó exitosamente.', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            _categoria.id = parseInt(createId());
            crearCategoria(_categoria)
            _categorias.push(_categoria);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se creó exitosamente.', life: 3000 });
        }
        setCategorias(_categorias);
        setCategoriaDialog(false);
        setCategoria(emptyCategoria);
    };

    const editCategoria = (categoria: categoriaDTO) => {
        const categoriaLoc = { ...categoria };
        getCategoria(categoriaLoc.id);
        setCategoriaDialog(true);
    };

    const confirmDeleteCategoria = (categoria: categoriaDTO) => {
        setCategoria(categoria);
        setDeleteCategoriaDialog(true);
    };

    const deleteCategoria = () => {
        let _categorias = categorias.filter((val) => val.id !== categoria.id);
        BorrarCategoria(categoria.id);
        setCategorias(_categorias);
        setDeleteCategoriaDialog(false);
        setCategoria(emptyCategoria);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Fue borrado exitosamente.', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < categorias.length; i++) {
            if (categorias[i].id === parseInt(id)) {
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
        setDeleteCategoriasDialog(true);
    };

    const deleteSelectedCategorias = () => {
        let _categorias = categorias.filter((val) => !selectedCategorias.includes(val));
        setCategorias(_categorias);
        setDeleteCategoriasDialog(false);
        setSelectedCategorias([]);
        DeleteRangoCategorias(selectedCategorias);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'El rango fue borrado exitosamente.', life: 3000 });
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...categoria };

        // @ts-ignore
        _product[name] = val;

        setCategoria(_product);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _categoria = { ...categoria };

        // @ts-ignore
        _categoria[name] = val;

        setCategoria(_categoria);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button className="button-secundario-guardar" label="Nuevo" severity="success" onClick={openNew} />
                <Button className="button-secundario-cancelar" label="Borrar" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedCategorias || !selectedCategorias.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: categoriaDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editCategoria(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteCategoria(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Categorías</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const categoriaDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideDialog} />
            <Button className="button-secundario-guardar" label="Guardar" onClick={saveCategoria} />
        </React.Fragment>
    );
    const deleteCategoriaDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteCategoriaDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteCategoria} />
        </React.Fragment>
    );
    const deleteCategoriasDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteCategoriasDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteSelectedCategorias} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={categorias} selection={selectedCategorias}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedCategorias(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} categorías" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="nombre" header="Nombre" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="descripcion" header="Descripcion" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={categoriaDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle de categoría" modal className="p-fluid" footer={categoriaDialogFooter} onHide={hideDialog}>
                <hr className="violet-line" />
                <div className="field">
                    <label htmlFor="nombre" className="font-bold">
                        Nombre
                    </label>
                    <InputText id="nombre" value={categoria.nombre} onChange={(e) => onInputChange(e, 'nombre')} required autoFocus className={classNames({ 'p-invalid': submitted && !categoria.nombre })} />
                </div>
                <div className="field">
                    <label htmlFor="descripcion" className="font-bold">
                        Descripcion
                    </label>
                    <InputTextarea id="descripcion" value={categoria.descripcion} onChange={(e: ChangeEvent<HTMLTextAreaElement>) => onInputTextAreaChange(e, 'descripcion')} required rows={3} cols={20} />
                </div>
                <hr className="violet-line" />
            </Dialog>

            <Dialog visible={deleteCategoriaDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteCategoriaDialogFooter} onHide={hideDeleteCategoriaDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {categoria && (
                        <span>
                            ¿Quieres borrar la categoría <b>{categoria.nombre}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteCategoriasDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteCategoriasDialogFooter} onHide={hideDeleteCategoriasDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {categoria && <span>¿Quieres borrar las categorias seleccionadas?</span>}
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