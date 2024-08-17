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
import { urlTransportista } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { Calendar } from 'primereact/calendar';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { FormEvent } from 'primereact/ts-helpers';
import { creacionTransportistaDTO, deleteTransportistaDTO, transportistaDTO } from './transportista.modulo';
import { convierteFormatoFecha } from '../utils/Comunes';
import QRCode from 'react-qr-code';
import PrintQRCode from '../utils/PrintQRCode';

export default function FormularioTransportista() {
    let emptyProduct: transportistaDTO = {
        id: 0,
        apellido: '',
        nombre: '',
        nroDni: '',
        direccion: '',
        cuit: '',
        localidad: '',
        telefono: '',
        codigoPostal: '',
        email: '',
        fechaNacimiento: new Date(),
        fechaVencLicencia: new Date(),
        licencia: '',
        habilitado: true,
        apellidoNombreLicencia: ''
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [transportistas, setTransportistas] = useState<transportistaDTO[]>([]);
    const [transportistaDialog, setTransportistaDialog] = useState<boolean>(false);
    const [deleteTransportistaDialog, setDeleteTransportistaDialog] = useState<boolean>(false);
    const [deleteTransportistasDialog, setDeleteTranspotistasDialog] = useState<boolean>(false);
    const [transportista, setTransportista] = useState<transportistaDTO>(emptyProduct);
    const [selectedTransportistas, setSelectedTransportistas] = useState<transportistaDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<transportistaDTO[]>>(null);
    const [fechaNacimiento, setFechaNacimiento] = useState<Date>(new Date("10/10/2000"));
    const [fechaVencLicencia, setFechaVencLicencia] = useState<Date>(new Date("10/10/2025"));
    const [identificaQrDialog, setIdentificaQrDialog] = useState<boolean>(false);
    const [textoQr, setTextoQr] = useState<string>('');


    useEffect(() => {
        try {
            CargarTransportistas();
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }, [])

    useEffect(() => {
        const url = `${urlTransportista}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<transportistaDTO[]>) => {
                setTransportistas(respuesta.data);
            })
    }, [])

    function CargarTransportistas() {
        const url = `${urlTransportista}`;
        axios.get(url)
            .then((respuesta: AxiosResponse<transportistaDTO[]>) => {
                setTransportistas(respuesta.data);
            });
    }

    async function BorrarTransportista(id: number) {
        try {
            await axios.delete(`${urlTransportista}/${id}`);
            CargarTransportistas();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoTransportistas(transportistas: deleteTransportistaDTO[]) {
        try {
            await axios.delete(`${urlTransportista}`, { data: transportistas })
            CargarTransportistas();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarTransportista(transportista: transportistaDTO, id: number) {
        try {
            await axios.put(`${urlTransportista}/${id}`, transportista)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearTransportista(transportista: creacionTransportistaDTO) {
        try {
            await axios.post(`${urlTransportista}/crear`, transportista)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getTransportista(id?: number) {
        axios.get(`${urlTransportista}/${id}`)
            .then((respuesta: AxiosResponse<transportistaDTO>) => {
                setTransportista(respuesta.data);
            });
    }

    const openNew = () => {
        setTransportista(emptyProduct);
        setSubmitted(false);
        setTransportistaDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setTransportistaDialog(false);
    };

    const hideIdentificacionQrDialog = () => {
        //setSubmitted(false);
        setIdentificaQrDialog(false);
    };

    const hideDeleteProductDialog = () => {
        setDeleteTransportistaDialog(false);
    };

    const hideDeleteProductsDialog = () => {
        setDeleteTranspotistasDialog(false);
    };

    const saveTransportista = () => {
        setSubmitted(true);
        let _transportistas = [...transportistas];
        let _transportista = { ...transportista };

        if (transportista.id) {
            const index = findIndexById(transportista.id.toString());

            _transportistas[index] = _transportista;
            editarTransportista(_transportista, _transportista.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Transportista actualizado', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            _transportista.id = parseInt(createId());
            crearTransportista(_transportista)
            _transportistas.push(_transportista);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Transportista creado', life: 3000 });
        }
        setTransportistas(_transportistas);
        setTransportistaDialog(false);
        setTransportista(emptyProduct);
    };

    const editTransportista = (transportista: transportistaDTO) => {
        const transportistaLocal = { ...transportista };
        getTransportista(transportistaLocal.id);
        setFechaNacimiento(convierteFormatoFecha(transportistaLocal.fechaNacimiento));
        setFechaVencLicencia(convierteFormatoFecha(transportistaLocal.fechaVencLicencia));
        setTransportistaDialog(true);
    };

    const confirmDeleteTransportista = (transportista: transportistaDTO) => {
        setTransportista(transportista);
        setDeleteTransportistaDialog(true);
    };

    const deleteTransportista = () => {
        let _transportistas = transportistas.filter((val) => val.id !== transportista.id);
        BorrarTransportista(transportista.id);
        setTransportistas(_transportistas);
        setDeleteTransportistaDialog(false);
        setTransportista(emptyProduct);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Transportista borrado', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < transportistas.length; i++) {
            if (transportistas[i].id === parseInt(id)) {
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


    const IdentificaQr = () => {
        setTextoQr(textoQrLoc(transportista))
        setIdentificaQrDialog(true);
    };

    const textoQrLoc = (transportista: transportistaDTO) => {
        var texto = ''
        texto = `
Hola, llegó tu pedido!!
Acompañado del conductor ${transportista.nombre}
(${transportista.nroDni.toString()}).

Que disfrutes tu evento!`
        return texto;
    };

    const confirmDeleteSelected = () => {
        setDeleteTranspotistasDialog(true);
    };

    const deleteSelectedTransportistas = () => {
        let _transportistas = transportistas.filter((val) => !selectedTransportistas.includes(val));
        setTransportistas(_transportistas);
        setDeleteTranspotistasDialog(false);
        setSelectedTransportistas([]);
        DeleteRangoTransportistas(selectedTransportistas);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Transportistas Borrados', life: 3000 });
    };

    const onCheckBoxChangeHabilitado = (e: CheckboxChangeEvent) => {
        let _product = { ...transportista };
        _product['habilitado'] = e.checked;
        setTransportista(_product);
    };

    const onChangeCalendarFechaNacimiento = (e: FormEvent) => {
        let _transportista = { ...transportista };
        const val = (e.target && e.target.value) || new Date();
        _transportista['fechaNacimiento'] = convierteFormatoFecha(val);
        setTransportista(_transportista);
    };

    const onChangeCalendarFechaVenciLicencia = (e: FormEvent) => {
        let _transportista = { ...transportista };
        const val = (e.target && e.target.value) || new Date();
        _transportista['fechaVencLicencia'] = convierteFormatoFecha(val);
        setTransportista(_transportista);
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _transportista = { ...transportista };

        // @ts-ignore
        _transportista[name] = val;

        setTransportista(_transportista);
    };

    const handlePrint = () => {
        const printWindow = window.open('', '', 'height=600,width=800');
        if (printWindow) {
            printWindow.document.open();
            printWindow.document.write(`
                <html>
                    <head>
                        <title>Imprimir QR desde Rossi Eventos App</title>
                        <style>
                            body {text - align: center; font-family: Arial, sans-serif; }
                            #qr-code {margin: 20px; }
                        </style>
                    </head>
                    <body>
                        <h1>QR de ${transportista.apellido}, ${transportista.nombre}</h1>
                        <div id="qr-code">
                            ${document.getElementById('qr-code-to-print')?.innerHTML || ''}
                        </div>
                        <script>
                            window.onload = function() {
                                window.print();
                            }
                        </script>
                    </body>
                </html>
            `);
            printWindow.document.close();
        }
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button className="button-secundario-guardar" label="Nuevo" severity="success" onClick={openNew} />
                <Button className="button-secundario-cancelar" label="Borrar" severity="danger"
                    onClick={confirmDeleteSelected}
                    disabled={!selectedTransportistas || !selectedTransportistas.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button className="button-adicionales" label="Identidad Qr"
            disabled={!selectedTransportistas || !selectedTransportistas.length}
            onClick={IdentificaQr} />;
    };

    const actionBodyTemplate = (rowData: transportistaDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editTransportista(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteTransportista(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Transportistas</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const transportistaDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideDialog} />
            <Button className="button-secundario-guardar" label="Guardar" onClick={saveTransportista} />
        </React.Fragment>
    );

    const identificacionQrDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideIdentificacionQrDialog} />
            <Button className="button-secundario-guardar" label="Imprimir" onClick={handlePrint} />
        </React.Fragment>
    );

    const deleteProductDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteTransportista} />
        </React.Fragment>
    );
    const deleteProductsDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductsDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteSelectedTransportistas} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate} right={rightToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={transportistas} selection={selectedTransportistas}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedTransportistas(e.value);
                            getTransportista(e.value[0].id);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} transportistas" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="nombre" header="Nombre" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column field="apellido" header="Apellido" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="nroDni" header="Dni" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={transportistaDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle del transportista" modal className="p-fluid" footer={transportistaDialogFooter} onHide={hideDialog}>
                <hr className="violet-line" />
                <div className="field">
                    <label htmlFor="nombre" className="font-bold">
                        Nombre
                    </label>
                    <InputText id="nombre" value={transportista.nombre} onChange={(e) => onInputChange(e, 'nombre')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.nombre })} />
                    {submitted && !transportista.nombre && <small className="p-error">El nombre es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="apellido" className="font-bold">
                        Apellido
                    </label>
                    <InputText id="apellido" value={transportista.apellido} onChange={(e) => onInputChange(e, 'apellido')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.apellido })} />
                </div>
                <div className="field">
                    <label htmlFor="nroDni" className="font-bold">
                        Dni
                    </label>
                    <InputText id="nroDni" keyfilter="int" value={transportista.nroDni} onChange={(e) => onInputChange(e, 'nroDni')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.nroDni })} />
                </div>
                <div className="field">
                    <label htmlFor="cuit" className="font-bold">
                        Cuit
                    </label>
                    <InputText id="cuit" value={transportista.cuit} keyfilter="int" onChange={(e) => onInputChange(e, 'cuit')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.cuit })} />
                </div>
                <div className="field">
                    <label htmlFor="direccion" className="font-bold">
                        Dirección
                    </label>
                    <InputText id="direccion" value={transportista.direccion} onChange={(e) => onInputChange(e, 'direccion')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.direccion })} />
                </div>
                <div className="field">
                    <label htmlFor="telefono" className="font-bold">
                        Teléfono
                    </label>
                    <InputText id="telefono" value={transportista.telefono} keyfilter="int" onChange={(e) => onInputChange(e, 'telefono')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.telefono })} />
                </div>
                <div className="field">
                    <label htmlFor="codigoPostal" className="font-bold">
                        Código Postal
                    </label>
                    <InputText id="codigoPostal" keyfilter="int" value={transportista.codigoPostal} onChange={(e) => onInputChange(e, 'codigoPostal')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.codigoPostal })} />
                </div>
                <div className="field">
                    <label htmlFor="localidad" className="font-bold">
                        Localidad
                    </label>
                    <InputText id="localidad" value={transportista.localidad} onChange={(e) => onInputChange(e, 'localidad')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.localidad })} />
                </div>
                <div className="field">
                    <label htmlFor="fechaNacimiento" className="font-bold">
                        Fecha de Nacimiento
                    </label>
                    <Calendar id="fechaNacimiento" value={fechaNacimiento} readOnlyInput onChange={(e) => onChangeCalendarFechaNacimiento(e)} dateFormat="dd/mm/yy" />
                    {submitted && !transportista.fechaNacimiento && <small className="p-error">La fecha de nacimiento es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="fechaVencLicencia" className="font-bold">
                        Fecha de Vencimiento de la Licencia
                    </label>
                    <Calendar id="fechaVencLicencia" value={fechaVencLicencia} readOnlyInput onChange={(e) => onChangeCalendarFechaVenciLicencia(e)} dateFormat="dd/mm/yy" />
                    {submitted && !transportista.fechaVencLicencia && <small className="p-error">La fecha de vencimiento de la licencia es requerida.</small>}
                </div>
                <div className="field">
                    <label htmlFor="licencia" className="font-bold">
                        Licencia
                    </label>
                    <InputText id="licencia" value={transportista.licencia} onChange={(e) => onInputChange(e, 'licencia')} required autoFocus className={classNames({ 'p-invalid': submitted && !transportista.licencia })} />
                </div>
                <div className="field" style={{ marginTop: "10px", marginBottom: "10px" }}>
                    <label htmlFor="habilitado" className="font-bold" style={{ marginRight: '5px' }}>
                        Habilitado
                    </label>
                    <Checkbox name="habilitado" onChange={e => onCheckBoxChangeHabilitado(e)} checked={transportista.habilitado} />
                </div>
                <hr className="violet-line" />
            </Dialog>

            <Dialog visible={deleteTransportistaDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {transportista && (
                        <span>
                            ¿Quieres borrar el transportista con el Dni <b>{transportista.nroDni}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteTransportistasDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteProductsDialogFooter} onHide={hideDeleteProductsDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {transportista && <span>¿Quieres borrar los transportista seleccionados?</span>}
                </div>
            </Dialog>

            <Dialog visible={identificaQrDialog} style={{ width: '19em' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="QR del transportista" modal className="p-fluid"
                footer={identificacionQrDialogFooter} onHide={hideIdentificacionQrDialog}>
                <hr className="violet-line" />
                {textoQr ?
                    <>
                        <QRCode value={textoQr} />
                        <PrintQRCode value={textoQr} />
                    </>
                    : <p>No hay datos para mostrar el QR</p>}

                <hr className="violet-line" />
            </Dialog>
            <br></br>
            <br></br>
            <br></br>
            <br></br>
            <br></br>
        </div >
    );
}