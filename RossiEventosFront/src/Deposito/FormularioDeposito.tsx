import React, { useState, useEffect, useRef, ChangeEvent } from 'react';
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
import { urlDeposito } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { creacionDepositoDTO, deleteDepositoDTO, depositoDTO } from './deposito.modulo';
import { InputTextarea } from 'primereact/inputtextarea';

export default function FormularioDeposito() {
    let emptyDeposito: depositoDTO = {
        id: 0,
        codigo: '',
        descripcion: '',
        direccion: '',
        localidad: '',
        provincia: '',
        habilitado: true
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [depositos, setDepositos] = useState<depositoDTO[]>([]);
    const [depositoDialog, setDepositoDialog] = useState<boolean>(false);
    const [deleteDepositoDialog, setDeleteDepositoDialog] = useState<boolean>(false);
    const [deleteDepositosDialog, setDeleteDepositosDialog] = useState<boolean>(false);
    const [deposito, setDeposito] = useState<depositoDTO>(emptyDeposito);
    const [selectedDepositos, setSelectedDepositos] = useState<depositoDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<depositoDTO[]>>(null);

    useEffect(() => {
        try {
            CargarDepositos();
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }, [])

    useEffect(() => {
        const url = `${urlDeposito}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<depositoDTO[]>) => {
                setDepositos(respuesta.data);
            })
    }, [])

    function CargarDepositos() {
        const url = `${urlDeposito}`;
        axios.get(url)
            .then((respuesta: AxiosResponse<depositoDTO[]>) => {
                setDepositos(respuesta.data);
            });
    }

    async function BorrarDeposito(id: number) {
        try {
            await axios.delete(`${urlDeposito}/${id}`);
            CargarDepositos();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoDepositos(depositos: deleteDepositoDTO[]) {
        try {
            await axios.delete(`${urlDeposito}`, { data: depositos })
            CargarDepositos();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarDeposito(deposito: depositoDTO, id: number) {
        try {
            await axios.put(`${urlDeposito}/${id}`, deposito)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearDeposito(deposito: creacionDepositoDTO) {
        try {
            await axios.post(`${urlDeposito}/crear`, deposito)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getDeposito(id: number) {
        axios.get(`${urlDeposito}/${id}`)
            .then((respuesta: AxiosResponse<depositoDTO>) => {
                setDeposito(respuesta.data);
            });
    }

    const openNew = () => {
        setDeposito(emptyDeposito);
        setSubmitted(false);
        setDepositoDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setDepositoDialog(false);
    };

    const hideDeleteProductDialog = () => {
        setDeleteDepositoDialog(false);
    };

    const hideDeleteProductsDialog = () => {
        setDeleteDepositosDialog(false);
    };

    const saveDeposito = () => {
        setSubmitted(true);
        let _depositos = [...depositos];
        let _deposito = { ...deposito };

        if (deposito.id) {
            const index = findIndexById(deposito.id.toString());

            _depositos[index] = _deposito;
            editarDeposito(_deposito, _deposito.id)
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Depósito actualizado', life: 3000 });
        } else {
            // _product.image = 'product-placeholder.svg';
            _deposito.id = parseInt(createId());
            crearDeposito(_deposito)
            _depositos.push(_deposito);
            toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Depósito creado', life: 3000 });
        }
        setDepositos(_depositos);
        setDepositoDialog(false);
        setDeposito(emptyDeposito);
    };

    const editDeposito = (deposito: depositoDTO) => {
        const depositoLocal = { ...deposito };
        getDeposito(depositoLocal.id);
        setDepositoDialog(true);
    };

    const confirmDeleteDeposito = (deposito: depositoDTO) => {
        setDeposito(deposito);
        setDeleteDepositoDialog(true);
    };

    const deleteDeposito = () => {
        let _depositos = depositos.filter((val) => val.id !== deposito.id);
        BorrarDeposito(deposito.id);
        setDepositos(_depositos);
        setDeleteDepositoDialog(false);
        setDeposito(emptyDeposito);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Depósito borrado', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < depositos.length; i++) {
            if (depositos[i].id === parseInt(id)) {
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
        setDeleteDepositosDialog(true);
    };

    const deleteSelectedDepositos = () => {
        let _transportistas = depositos.filter((val) => !selectedDepositos.includes(val));
        setDepositos(_transportistas);
        setDeleteDepositosDialog(false);
        setSelectedDepositos([]);
        DeleteRangoDepositos(selectedDepositos);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Depositos borrados', life: 3000 });
    };

    const onCheckBoxChangeHabilitado = (e: CheckboxChangeEvent) => {
        let _deposito = { ...deposito };
        // @ts-ignore
        _deposito['habilitado'] = e.checked;
        setDeposito(_deposito);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _deposito = { ...deposito };
        // @ts-ignore
        _deposito[name] = val;
        setDeposito(_deposito);
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _transportista = { ...deposito };

        // @ts-ignore
        _transportista[name] = val;

        setDeposito(_transportista);
    };


    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="Nuevo" icon="pi pi-plus" severity="success" onClick={openNew} />
                <Button label="Borrar" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedDepositos || !selectedDepositos.length} />
            </div>
        );
    };

    // const rightToolbarTemplate = () => {
    //     return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    // };

    const actionBodyTemplate = (rowData: depositoDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editDeposito(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteDeposito(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Depositos</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const depositoDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideDialog} />
            <Button className="button-secundario-guardar" label="Guardar" onClick={saveDeposito} />
        </React.Fragment>
    );
    const deleteDepositoDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteDeposito} />
        </React.Fragment>
    );
    const deleteDepositosDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteProductsDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteSelectedDepositos} />
        </React.Fragment>
    );

    return (
        <>
            <div>
                <MostrarErrores errores={errores} />
                <Toast ref={toast} />
                <div className="card">
                    <Toolbar className="mb-4" left={leftToolbarTemplate}> </Toolbar>
                    <DataTable ref={dt} value={depositos} selection={selectedDepositos}
                        onSelectionChange={(e) => {
                            if (Array.isArray(e.value)) {
                                setSelectedDepositos(e.value);
                            }
                        }}
                        dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                        currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} depositos" globalFilter={globalFilter} header={header}
                        selectionMode="multiple"
                    >
                        <Column selectionMode="multiple" exportable={false}></Column>
                        <Column field="codigo" header="Codigo" sortable style={{ minWidth: '12rem' }}></Column>
                        <Column field="localidad" header="Localidad" sortable style={{ minWidth: '16rem' }}></Column>
                        <Column field="provincia" header="Provincia" sortable style={{ minWidth: '16rem' }}></Column>
                        <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                    </DataTable>
                </div>
                <Dialog visible={depositoDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle del deposito" modal className="p-fluid" footer={depositoDialogFooter} onHide={hideDialog}>
                    <hr className="violet-line" />
                    <div className="field">
                        <label htmlFor="codigo" className="font-bold">
                            Código
                        </label>
                        <InputText id="codigo" value={deposito.codigo} onChange={(e) => onInputChange(e, 'codigo')} required autoFocus className={classNames({ 'p-invalid': submitted && !deposito.codigo })} />
                        {submitted && !deposito.codigo && <small className="p-error">El código es requerido.</small>}
                    </div>
                    <div className="field">
                        <label htmlFor="descripcion" className="font-bold">
                            Descripción
                        </label>
                        <InputTextarea id="descripcion" value={deposito.descripcion} onChange={(e: ChangeEvent<HTMLTextAreaElement>) => onInputTextAreaChange(e, 'descripcion')} required autoFocus className={classNames({ 'p-invalid': submitted && !deposito.descripcion })} />
                    </div>
                    <div className="field">
                        <label htmlFor="direccion" className="font-bold">
                            Dirección
                        </label>
                        <InputText id="direccion" value={deposito.direccion} onChange={(e) => onInputChange(e, 'direccion')} required autoFocus className={classNames({ 'p-invalid': submitted && !deposito.direccion })} />
                    </div>
                    <div className="field">
                        <label htmlFor="localidad" className="font-bold">
                            Localidad
                        </label>
                        <InputText id="localidad" value={deposito.localidad} onChange={(e) => onInputChange(e, 'localidad')} required autoFocus className={classNames({ 'p-invalid': submitted && !deposito.localidad })} />
                    </div>
                    <div className="field">
                        <label htmlFor="provincia" className="font-bold">
                            Provincia
                        </label>
                        <InputText id="provincia" value={deposito.provincia} onChange={(e) => onInputChange(e, 'provincia')} required autoFocus className={classNames({ 'p-invalid': submitted && !deposito.provincia })} />
                    </div>
                    <div className="field" style={{ marginTop: "10px", marginBottom: "10px" }}>
                        <label htmlFor="habilitado" className="font-bold" style={{ marginRight: '5px' }}>
                            Habilitado
                        </label>
                        <Checkbox name="habilitado" onChange={e => onCheckBoxChangeHabilitado(e)} checked={deposito.habilitado} />
                    </div>
                    <hr className="violet-line" />
                </Dialog>

                <Dialog visible={deleteDepositoDialog} style={{ width: '32rem' }}
                    breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                    header="Confirma"
                    modal footer={deleteDepositoDialogFooter} onHide={hideDeleteProductDialog}>
                    <div className="confirmation-content">
                        <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                        {deposito && (
                            <span>
                                ¿Quieres borrar el depósito con el código <b>{deposito.codigo}</b>?
                            </span>
                        )}
                    </div>
                </Dialog>

                <Dialog visible={deleteDepositosDialog} style={{ width: '32rem' }}
                    breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                    header="Confirma"
                    modal footer={deleteDepositosDialogFooter} onHide={hideDeleteProductsDialog}>
                    <div className="confirmation-content">
                        <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                        {deposito && <span>¿Quieres borrar los depositos seleccionados?</span>}
                    </div>
                </Dialog>
            </div >
        </>
    );
}   