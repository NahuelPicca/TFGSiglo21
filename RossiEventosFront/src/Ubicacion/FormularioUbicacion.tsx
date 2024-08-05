import React, { useState, useEffect, useRef } from 'react';
import { classNames } from 'primereact/utils';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Toast } from 'primereact/toast';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { IconField } from 'primereact/iconfield';
import { InputIcon } from 'primereact/inputicon';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import axios, { AxiosResponse } from 'axios';
import { urlUbicacion } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { creacionUbicacionDTO, deleteUbicacionDTO, ubicacionDTO } from './ubicacion.modulo';

export default function FormularioUbicacion() {
    let emptyUbicacion: ubicacionDTO = {
        id: 0,
        codigo: '',
        nombre: '',
        columna: '',
        estante: '',
        fila: '',
        habilitado: true
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [ubicaciones, setUbicaciones] = useState<ubicacionDTO[]>([]);
    const [ubicacioneDialog, setUbicacioneDialog] = useState<boolean>(false);
    const [deleteUbicacionDialog, setDeleteUbicacionDialog] = useState<boolean>(false);
    const [deleteUbicacionesDialog, setDeleteUbicacionesDialog] = useState<boolean>(false);
    const [ubicacion, setUbicacione] = useState<ubicacionDTO>(emptyUbicacion);
    const [selectedUbicaciones, setSelectedUbicaciones] = useState<ubicacionDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<ubicacionDTO[]>>(null);

    useEffect(() => {
        try {
            CargarUbicaciones();
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }, [])

    useEffect(() => {
        const url = `${urlUbicacion}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<ubicacionDTO[]>) => {
                setUbicaciones(respuesta.data);
            })
    }, [])

    function CargarUbicaciones() {
        const url = `${urlUbicacion}`;
        axios.get(url)
            .then((respuesta: AxiosResponse<ubicacionDTO[]>) => {
                setUbicaciones(respuesta.data);
            });
    }

    async function BorrarUbicacion(id: number) {
        try {
            await axios.delete(`${urlUbicacion}/${id}`);
            CargarUbicaciones();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoUbicaciones(ubicaciones: deleteUbicacionDTO[]) {
        try {
            await axios.delete(`${urlUbicacion}`, { data: ubicaciones })
            CargarUbicaciones();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function EditarUbicacion(ubicacion: ubicacionDTO, id: number) {
        try {
            await axios.put(`${urlUbicacion}/${id}`, ubicacion)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function CrearUbicacion(ubicacion: creacionUbicacionDTO) {
        try {
            await axios.post(`${urlUbicacion}/crear`, ubicacion)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function GetUbicacion(id: number) {
        axios.get(`${urlUbicacion}/${id}`)
            .then((respuesta: AxiosResponse<ubicacionDTO>) => {
                setUbicacione(respuesta.data);
            });
    }

    const openNew = () => {
        setUbicacione(emptyUbicacion);
        setSubmitted(false);
        setUbicacioneDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setUbicacioneDialog(false);
    };

    const hideDeleteUbicacionDialog = () => {
        setDeleteUbicacionDialog(false);
    };

    const hideDeleteUbicacionesDialog = () => {
        setDeleteUbicacionesDialog(false);
    };

    const saveUbicacion = () => {
        setSubmitted(true);
        let _ubicaciones = [...ubicaciones];
        let _ubicacion = { ...ubicacion };

        if (ubicacion.id) {
            const index = findIndexById(ubicacion.id.toString());

            _ubicaciones[index] = _ubicacion;
            EditarUbicacion(_ubicacion, _ubicacion.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Ubicación actualizado', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            _ubicacion.id = parseInt(createId());
            CrearUbicacion(_ubicacion)
            _ubicaciones.push(_ubicacion);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Ubicación creado', life: 3000 });
        }
        setUbicaciones(_ubicaciones);
        setUbicacioneDialog(false);
        setUbicacione(emptyUbicacion);
    };

    const editUbicacion = (ubicacion: ubicacionDTO) => {
        const ubicacionLocal = { ...ubicacion };
        GetUbicacion(ubicacionLocal.id);
        setUbicacioneDialog(true);
    };

    const confirmDeleteUbicacion = (ubicacion: ubicacionDTO) => {
        setUbicacione(ubicacion);
        setDeleteUbicacionDialog(true);
    };

    const deleteUbicacion = () => {
        let _depositos = ubicaciones.filter((val) => val.id !== ubicacion.id);
        BorrarUbicacion(ubicacion.id);
        setUbicaciones(_depositos);
        setDeleteUbicacionDialog(false);
        setUbicacione(emptyUbicacion);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Depósito borrado', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < ubicaciones.length; i++) {
            if (ubicaciones[i].id === parseInt(id)) {
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
        setDeleteUbicacionesDialog(true);
    };

    const deleteSelectedUbicaciones = () => {
        let _ubicaciones = ubicaciones.filter((val) => !selectedUbicaciones.includes(val));
        setUbicaciones(_ubicaciones);
        setDeleteUbicacionesDialog(false);
        setSelectedUbicaciones([]);
        DeleteRangoUbicaciones(selectedUbicaciones);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Ubicaciones borrados', life: 3000 });
    };

    const onCheckBoxChangeHabilitado = (e: CheckboxChangeEvent) => {
        let _ubicacion = { ...ubicacion };
        // @ts-ignore
        _ubicacion['habilitado'] = e.checked;
        setUbicacione(_ubicacion);
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _ubicacion = { ...ubicacion };

        // @ts-ignore
        _ubicacion[name] = val;

        setUbicacione(_ubicacion);
    };

    const verifiedBodyTemplate = (rowData: ubicacionDTO) => {
        return <i className={classNames('pi', { 'true-icon pi-check-circle': rowData.habilitado, 'false-icon pi-times-circle': !rowData.habilitado })}></i>;
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="Nuevo" icon="pi pi-plus" severity="success" onClick={openNew} />
                <Button label="Borrar" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedUbicaciones || !selectedUbicaciones.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: ubicacionDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editUbicacion(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteUbicacion(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Ubicaciones</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const ubicacionDialogFooter = (
        <React.Fragment>
            <Button label="Cancelar" icon="pi pi-times" outlined onClick={hideDialog} />
            <Button label="Guardar" icon="pi pi-check" onClick={saveUbicacion} />
        </React.Fragment>
    );
    const deleteUbicacionDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteUbicacionDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteUbicacion} />
        </React.Fragment>
    );
    const deleteUbicacionesDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteUbicacionesDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteSelectedUbicaciones} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={ubicaciones} selection={selectedUbicaciones}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedUbicaciones(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={10} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} ubicaciones" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="codigo" header="Codigo" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column field="nombre" header="Nombre" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="habilitado" header="Habilitado" body={verifiedBodyTemplate} dataType="boolean" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>
            <Dialog visible={ubicacioneDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle de ubicación" modal className="p-fluid" footer={ubicacionDialogFooter} onHide={hideDialog}>
                <div className="field">
                    <label htmlFor="codigo" className="font-bold">
                        Código
                    </label>
                    <InputText id="codigo" value={ubicacion.codigo} onChange={(e) => onInputChange(e, 'codigo')} required autoFocus className={classNames({ 'p-invalid': submitted && !ubicacion.codigo })} />
                    {submitted && !ubicacion.codigo && <small className="p-error">El código es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="nombre" className="font-bold">
                        Nombre
                    </label>
                    <InputText id="nombre" value={ubicacion.nombre} onChange={(e) => onInputChange(e, 'nombre')} required autoFocus className={classNames({ 'p-invalid': submitted && !ubicacion.nombre })} />
                    {submitted && !ubicacion.nombre && <small className="p-error">El nombre es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="columna" className="font-bold">
                        Columna
                    </label>
                    <InputText id="columna" value={ubicacion.columna} onChange={(e) => onInputChange(e, 'columna')} required autoFocus className={classNames({ 'p-invalid': submitted && !ubicacion.columna })} />
                </div>
                <div className="field">
                    <label htmlFor="estante" className="font-bold">
                        Estante
                    </label>
                    <InputText id="estante" value={ubicacion.estante} onChange={(e) => onInputChange(e, 'estante')} required autoFocus className={classNames({ 'p-invalid': submitted && !ubicacion.estante })} />
                </div>
                <div className="field">
                    <label htmlFor="fila" className="font-bold">
                        Fila
                    </label>
                    <InputText id="fila" value={ubicacion.fila} onChange={(e) => onInputChange(e, 'fila')} required autoFocus className={classNames({ 'p-invalid': submitted && !ubicacion.fila })} />
                </div>
                <div className="field co">
                    <label htmlFor="habilitado" className="font-bold">
                        Habilitado
                    </label>
                    <Checkbox name="habilitado" onChange={e => onCheckBoxChangeHabilitado(e)} checked={ubicacion.habilitado} />
                </div>
            </Dialog>

            <Dialog visible={deleteUbicacionDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteUbicacionDialogFooter} onHide={hideDeleteUbicacionDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {ubicacion && (
                        <span>
                            ¿Quieres borrar la ubicación con el código <b>{ubicacion.codigo}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteUbicacionesDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteUbicacionesDialogFooter} onHide={hideDeleteUbicacionesDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {ubicacion && <span>¿Quieres borrar las ubicaciones seleccionadas?</span>}
                </div>
            </Dialog>
        </div >
    );
}