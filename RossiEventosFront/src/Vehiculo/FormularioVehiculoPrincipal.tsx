import React, { useState, useEffect, useRef } from 'react';
import { classNames } from 'primereact/utils';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Toast } from 'primereact/toast';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { IconField } from 'primereact/iconfield';
import { InputIcon } from 'primereact/inputicon';
import { InputNumberValueChangeEvent } from 'primereact/inputnumber';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import axios, { AxiosResponse } from 'axios';
import { urlTitular, urlVehiculo } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { Calendar } from 'primereact/calendar';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { FormEvent } from 'primereact/ts-helpers';
import { Dropdown } from 'primereact/dropdown';
import { convierteFormatoFecha } from '../utils/Comunes';
import { creacionVehiculoDTO, deleteVehiculoDTO, vehiculoDTO } from './vehiculo.modulo';
import { titularDTO } from '../Titular/titular.modulo';

export default function FormularioVehiculo() {
    let emptyVehiculo: vehiculoDTO = {
        id: 0,
        patente: '',
        marca: '',
        modelo: '',
        fechaVencPoliza: new Date(),
        nroPoliza: '',
        titularId: 0,
        habilitado: false,
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [vehiculos, setVehiculos] = useState<vehiculoDTO[]>([]);
    const [vehiculoDialog, setVehiculoDialog] = useState<boolean>(false);
    const [deleteVehiculoDialog, setDeleteVehiculoDialog] = useState<boolean>(false);
    const [deleteVehiculosDialog, setDeleteVehiculosDialog] = useState<boolean>(false);
    const [vehiculo, setVehiculo] = useState<vehiculoDTO>(emptyVehiculo);
    const [selectedVehiculos, setSelectedVehiculos] = useState<vehiculoDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<vehiculoDTO[]>>(null);
    const [fecha, setFecha] = useState<Date>(new Date("10/10/2000"));
    const [titulares, setTitulares] = useState<titularDTO[]>([]);

    useEffect(() => {
        try {
            CargarVehiculos();
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }, [])

    useEffect(() => {
        const url = `${urlTitular}/combo`
        axios.get(url)
            .then((respuesta: AxiosResponse<titularDTO[]>) => {
                setTitulares(respuesta.data);
            })
    }, [])

    function CargarVehiculos() {
        const url = `${urlVehiculo}`;
        axios.get(url)
            .then((respuesta: AxiosResponse<vehiculoDTO[]>) => {
                setVehiculos(respuesta.data);
            });
    }

    async function BorrarVehiculo(id: number) {
        try {
            await axios.delete(`${urlVehiculo}/${id}`);
            CargarVehiculos();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoVehiculos(vehiculos: deleteVehiculoDTO[]) {
        try {
            await axios.delete(`${urlVehiculo}`, { data: vehiculos })
            CargarVehiculos();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function EditarVehiculo(vehiculo: vehiculoDTO, id: number) {
        try {
            await axios.put(`${urlVehiculo}/${id}`, vehiculo)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function CrearVehiculo(vehiculo: creacionVehiculoDTO) {
        try {
            await axios.post(`${urlVehiculo}/crear`, vehiculo)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function GetVehiculo(id: number) {
        axios.get(`${urlVehiculo}/${id}`)
            .then((respuesta: AxiosResponse<vehiculoDTO>) => {
                setVehiculo(respuesta.data);
            });
    }

    const formatCurrency = (value: number) => {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    };

    const openNew = () => {
        setVehiculo(emptyVehiculo);
        setSubmitted(false);
        setVehiculoDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setVehiculoDialog(false);
    };

    const hideDeleteVehiculoDialog = () => {
        setDeleteVehiculoDialog(false);
    };

    const hideDeleteVehiculosDialog = () => {
        setDeleteVehiculosDialog(false);
    };

    const saveVehiculo = () => {
        setSubmitted(true);

        let _vehiculosLocal = [...vehiculos];
        let vehiculoLoc = { ...vehiculo };

        if (vehiculo.id) {
            const index = findIndexById(vehiculo.id.toString());

            _vehiculosLocal[index] = vehiculoLoc;
            EditarVehiculo(vehiculoLoc, vehiculoLoc.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Vehículo actualizado', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            vehiculoLoc.id = parseInt(createId());
            CrearVehiculo(vehiculoLoc)
            _vehiculosLocal.push(vehiculoLoc);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Vehículo creado', life: 3000 });
        }
        setVehiculos(_vehiculosLocal);
        setVehiculoDialog(false);
        setVehiculo(emptyVehiculo);
    };

    const editVehiculo = (vehiculo: vehiculoDTO) => {
        const vehiculoLocal = { ...vehiculo };
        GetVehiculo(vehiculoLocal.id);
        setFecha(convierteFormatoFecha(vehiculo.fechaVencPoliza));
        setVehiculoDialog(true);
    };

    const confirmDeleteVehiculo = (vehiculo: vehiculoDTO) => {
        setVehiculo(vehiculo);
        setDeleteVehiculoDialog(true);
    };

    const deleteVehiculo = () => {
        let _vehiculos = vehiculos.filter((val) => val.id !== vehiculo.id);
        BorrarVehiculo(vehiculo.id);
        setVehiculos(_vehiculos);
        setDeleteVehiculoDialog(false);
        setVehiculo(emptyVehiculo);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Vehículo borrado', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < vehiculos.length; i++) {
            if (vehiculos[i].id === parseInt(id)) {
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
        setDeleteVehiculosDialog(true);
    };

    const deleteSelectedVehiculos = () => {
        let _vehiculos = vehiculos.filter((val) => !selectedVehiculos.includes(val));
        setVehiculos(_vehiculos);
        setDeleteVehiculosDialog(false);
        setSelectedVehiculos([]);
        DeleteRangoVehiculos(selectedVehiculos);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Vehículos Borrados', life: 3000 });
    };

    const onCheckBoxChangeHabilitado = (e: CheckboxChangeEvent) => {
        let _product = { ...vehiculo };
        _product['habilitado'] = e.checked;
        setVehiculo(_product);
    };

    const onChangeCalendar = (e: FormEvent) => {
        let _vehiculo = { ...vehiculo };
        const val = (e.target && e.target.value) || new Date();
        _vehiculo['fechaVencPoliza'] = convierteFormatoFecha(val);
        setVehiculo(_vehiculo);
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...vehiculo };

        // @ts-ignore
        _product[name] = val;

        setVehiculo(_product);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _vehiculo = { ...vehiculo };

        // @ts-ignore
        _vehiculo[name] = val;

        setVehiculo(_vehiculo);
    };

    const onInputNumberChange = (e: InputNumberValueChangeEvent, name: string) => {
        const val = e.value ?? 0;
        let _product = { ...vehiculo };

        // @ts-ignore
        _product[name] = val;

        setVehiculo(_product);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="Nuevo" icon="pi pi-plus" severity="success" onClick={openNew} />
                <Button label="Borrar" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedVehiculos || !selectedVehiculos.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: vehiculoDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editVehiculo(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteVehiculo(rowData)} />
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
            <h4 className="m-0">Vehículos</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const vehiculoDialogFooter = (
        <React.Fragment>
            <Button label="Cancelar" icon="pi pi-times" outlined onClick={hideDialog} />
            <Button label="Guardar" icon="pi pi-check" onClick={saveVehiculo} />
        </React.Fragment>
    );
    const vehiculoProductDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteVehiculoDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteVehiculo} />
        </React.Fragment>
    );
    const deleteVehiculosDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteVehiculosDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteSelectedVehiculos} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={vehiculos} selection={selectedVehiculos}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedVehiculos(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} vehiculos" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="patente" header="Patente" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column field="marca" header="Marca" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="modelo" header="Modelo" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={vehiculoDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle del vehiculo" modal className="p-fluid" footer={vehiculoDialogFooter} onHide={hideDialog}>
                <div className="field">
                    <label htmlFor="patente" className="font-bold">
                        Patente
                    </label>
                    <InputText id="patente" value={vehiculo.patente} onChange={(e) => onInputChange(e, 'patente')} required autoFocus className={classNames({ 'p-invalid': submitted && !vehiculo.patente })} />
                    {submitted && !vehiculo.patente && <small className="p-error">La patente es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="marca" className="font-bold">
                        Marca
                    </label>
                    <InputText id="marca" value={vehiculo.marca} onChange={(e) => onInputChange(e, 'marca')} required autoFocus className={classNames({ 'p-invalid': submitted && !vehiculo.marca })} />
                    {submitted && !vehiculo.marca && <small className="p-error">La marca es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="modelo" className="font-bold">
                        Modelo
                    </label>
                    <InputText id="modelo" value={vehiculo.modelo} onChange={(e) => onInputChange(e, 'modelo')} required autoFocus className={classNames({ 'p-invalid': submitted && !vehiculo.modelo })} />
                    {submitted && !vehiculo.modelo && <small className="p-error">La modelo es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="fechaVencPoliza" className="font-bold">
                        Fecha vencimiento de póliza
                    </label>
                    <Calendar id="fechaVencPoliza" value={fecha} readOnlyInput onChange={(e) => onChangeCalendar(e)} dateFormat="dd/mm/yy" />
                    {submitted && !vehiculo.fechaVencPoliza && <small className="p-error">El fecha de vencimiento de la póliza es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="nroPoliza" className="font-bold">
                        Nro de póliza
                    </label>
                    <InputText id="nroPoliza" value={vehiculo.nroPoliza} onChange={(e) => onInputChange(e, 'nroPoliza')} required autoFocus className={classNames({ 'p-invalid': submitted && !vehiculo.nroPoliza })} />
                    {submitted && !vehiculo.nroPoliza && <small className="p-error">El número de póliza es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="titularId" className="font-bold">
                        Titular
                    </label>
                    <Dropdown id="titularId" onChange={(e: any) => onInputChange(e, 'titularId')}
                        options={titulares} optionLabel={"cuitApellidoNombre"} optionValue='id' placeholder="Seleccione el titular"
                        value={vehiculo.titularId}
                    />
                </div>
                <div className="field co">
                    <label htmlFor="habilitado" className="font-bold">
                        Habilitado
                    </label>
                    <Checkbox name="habilitado" onChange={e => onCheckBoxChangeHabilitado(e)} checked={vehiculo.habilitado} />
                </div>
            </Dialog>

            <Dialog visible={deleteVehiculoDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={vehiculoProductDialogFooter} onHide={hideDeleteVehiculoDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {vehiculo && (
                        <span>
                            ¿Quieres borrar el vehículo <b>{vehiculo.patente}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteVehiculosDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteVehiculosDialogFooter} onHide={hideDeleteVehiculosDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {vehiculo && <span>¿Quieres borrar los vehiculos seleccionados?</span>}
                </div>
            </Dialog>
        </div >
    );
}