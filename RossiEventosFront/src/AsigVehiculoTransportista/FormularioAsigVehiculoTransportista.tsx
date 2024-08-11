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
import { urlAsignacion, urlTransportista, urlVehiculo } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { asigVehiculoTransportistaCreacionDTO, asigVehiculoTransportistaDTO, deleteAsigVehiculoTransportistaDTO } from './asigVehiculoTransportista.modulo';
import { vehiculoDTO } from '../Vehiculo/vehiculo.modulo';
import { Dropdown } from 'primereact/dropdown';
import { transportistaDTO } from '../Transportista/transportista.modulo';

export default function FormularioAsigVehiculoTransportista() {
    let emptyAsignacion: asigVehiculoTransportistaDTO = {
        id: 0,
        licencia: '', modelo: '',
        patente: '', nombreTransportista: '',
        apellidoTransportista: '',
        transportitaId: 0, vehiculoId: 0
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [asignaciones, setAsignaciones] = useState<asigVehiculoTransportistaDTO[]>([]);
    const [asignacionDialog, setAsignacionDialog] = useState<boolean>(false);
    const [deleteAsignacionDialog, setDeleteAsignacionDialog] = useState<boolean>(false);
    const [deleteAsignacionesDialog, setDeleteAsignacionesDialog] = useState<boolean>(false);
    const [asignacion, setAsignacion] = useState<asigVehiculoTransportistaDTO>(emptyAsignacion);
    const [selectedAsignaciones, setSelectedAsignaciones] = useState<asigVehiculoTransportistaDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const [vehiculos, setVehiculos] = useState<vehiculoDTO[]>([]);
    const [transportistas, setTransportistas] = useState<transportistaDTO[]>([]);
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<asigVehiculoTransportistaDTO[]>>(null);

    useEffect(() => {
        CargarVehiculos()
        CargarTransportistas()
        CargarAsignaciones()
    })

    function CargarAsignaciones() {
        const url = `${urlAsignacion}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<asigVehiculoTransportistaDTO[]>) => {
                setAsignaciones(respuesta.data);
            })
    }

    function CargarVehiculos() {
        const url = `${urlVehiculo}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<vehiculoDTO[]>) => {
                setVehiculos(respuesta.data);
            })
    }

    function CargarTransportistas() {
        const url = `${urlTransportista}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<transportistaDTO[]>) => {
                setTransportistas(respuesta.data);
            })
    }

    async function BorrarAsignaciones(id: number) {
        try {
            await axios.delete(`${urlAsignacion}/${id}`);
            CargarAsignaciones();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoAsignaciones(asignaciones: deleteAsigVehiculoTransportistaDTO[]) {
        try {
            await axios.delete(`${urlAsignacion}`, { data: asignaciones })
            CargarAsignaciones();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarAsignacion(asignacionEditar: asigVehiculoTransportistaDTO, id: number) {
        try {
            await axios.put(`${urlAsignacion}/${id}`, asignacionEditar)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearAsignacion(asignacion: asigVehiculoTransportistaCreacionDTO) {
        try {
            await axios.post(`${urlAsignacion}/crear`, asignacion)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getAsignacion(id: number) {
        axios.get(`${urlAsignacion}/${id}`)
            .then((respuesta: AxiosResponse<asigVehiculoTransportistaDTO>) => {
                setAsignacion(respuesta.data);
            });
    }

    const openNew = () => {
        setAsignacion(emptyAsignacion);
        setSubmitted(false);
        setAsignacionDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setAsignacionDialog(false);
    };

    const hideDeleteAsignacionDialog = () => {
        setDeleteAsignacionDialog(false);
    };

    const hideDeleteAsignacionesDialog = () => {
        setDeleteAsignacionesDialog(false);
    };

    const saveAsignacion = () => {
        setSubmitted(true);

        let _asignacioness = [...asignaciones];
        let _asignacion = { ...asignacion };

        if (asignacion.id) {
            const index = findIndexById(asignacion.id.toString());

            _asignacioness[index] = _asignacion;
            editarAsignacion(_asignacion, _asignacion.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se actualizó exitosamente.', life: 3000 });
        } else {
            _asignacion.id = parseInt(createId());
            crearAsignacion(_asignacion)
            _asignacioness.push(_asignacion);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se creó exitosamente.', life: 3000 });
        }

        setAsignaciones(_asignacioness);
        setAsignacionDialog(false);
        setAsignacion(emptyAsignacion);
    };

    const editAsignacion = (asignacion: asigVehiculoTransportistaDTO) => {
        const asigLoc = { ...asignacion };
        getAsignacion(asigLoc.id);
        setAsignacionDialog(true);
    };

    const confirmDeleteAsignacion = (asignacion: asigVehiculoTransportistaDTO) => {
        setAsignacion(asignacion);
        setDeleteAsignacionDialog(true);
    };

    const deleteAsignacion = () => {
        let _asignacionss = asignaciones.filter((val) => val.id !== asignacion.id);
        BorrarAsignaciones(asignacion.id);
        setAsignaciones(_asignacionss);
        setDeleteAsignacionDialog(false);
        setAsignacion(emptyAsignacion);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Fue borrado exitosamente.', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < asignaciones.length; i++) {
            if (asignaciones[i].id === parseInt(id)) {
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
        setDeleteAsignacionesDialog(true);
    };

    const deleteSelectedAsignacioness = () => {
        let _asignaciones = asignaciones.filter((val) => !selectedAsignaciones.includes(val));
        setAsignaciones(_asignaciones);
        setDeleteAsignacionesDialog(false);
        setSelectedAsignaciones([]);
        DeleteRangoAsignaciones(selectedAsignaciones);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'El rango fue borrado exitosamente.', life: 3000 });
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _asignacion = { ...asignacion };

        // @ts-ignore
        _asignacion[name] = val;

        setAsignacion(_asignacion);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _asignacion = { ...asignacion };

        // @ts-ignore
        _asignacion[name] = val;

        setAsignacion(_asignacion);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="Nuevo" icon="pi pi-plus" severity="success" onClick={openNew} />
                <Button label="Borrar" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedAsignaciones || !selectedAsignaciones.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: asigVehiculoTransportistaDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editAsignacion(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteAsignacion(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Vinculación de vehículo y transportista </h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const asignacionDialogFooter = (
        <React.Fragment>
            <Button label="Cancelar" icon="pi pi-times" outlined onClick={hideDialog} />
            <Button label="Guardar" icon="pi pi-check" onClick={saveAsignacion} />
        </React.Fragment>
    );
    const deleteAsignacionDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteAsignacionDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteAsignacion} />
        </React.Fragment>
    );
    const deleteAsignacionsDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteAsignacionesDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteSelectedAsignacioness} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={asignaciones} selection={selectedAsignaciones}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedAsignaciones(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} registros" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="patente" header="Patente" sortable style={{ minWidth: '5rem' }}></Column>
                    <Column field="modelo" header="Modelo" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="apellidoTransportista" header="Apellido" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="nombreTransportista" header="Nombre" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="licencia" header="Licencia" sortable style={{ minWidth: '3rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={asignacionDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle de vinculación" modal className="p-fluid" footer={asignacionDialogFooter} onHide={hideDialog}>
                <div className="field">
                    <label htmlFor="vehículo" className="font-bold">
                        Vehículo
                    </label>
                    <Dropdown id="vehiculoId" onChange={(e: any) => onInputChange(e, 'vehiculoId')}
                        options={vehiculos} optionLabel="patenteModelo" optionValue='id' placeholder="Seleccione el vehículo"
                        value={asignacion.vehiculoId}
                    />
                </div>
                <div className="field">
                    <label htmlFor="transportista" className="font-bold">
                        Transportista
                    </label>
                    <Dropdown id="transportitaId" onChange={(e: any) => onInputChange(e, 'transportitaId')}
                        options={transportistas} optionLabel="apellidoNombreLicencia" optionValue='id' placeholder="Seleccione el transportista"
                        value={asignacion.transportitaId}
                    />
                </div>
            </Dialog>

            <Dialog visible={deleteAsignacionDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteAsignacionDialogFooter} onHide={hideDeleteAsignacionDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {asignacion && (
                        <span>
                            ¿Quieres borrar la asignación seleccionada?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteAsignacionesDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteAsignacionsDialogFooter} onHide={hideDeleteAsignacionesDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {asignacion && <span>¿Quieres borrar las asignaciones seleccionados?</span>}
                </div>
            </Dialog>
        </div >
    );
}