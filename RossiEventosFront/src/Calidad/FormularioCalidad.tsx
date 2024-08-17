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
import { urlCalidad } from '../utils/endpoints';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import MostrarErrores from '../utils/MostrarErrores';
import { calidadCreacionDTO, calidadDTO, deleteCalidadDTO } from './calidad.modulo';

export default function FormularioCalidad() {
    let emptyCalidad: calidadDTO = {
        id: 0,
        codigo: '', nombre: '',
        descripcion: ''
    };

    const [errores, setErrores] = useState<string[]>([]);
    const [calidades, setCalidades] = useState<calidadDTO[]>([]);
    const [productDialog, setCalidadDialog] = useState<boolean>(false);
    const [deleteCalidadDialog, setDeleteCalidadDialog] = useState<boolean>(false);
    const [deleteCalidadesDialog, setDeleteCalidadesDialog] = useState<boolean>(false);
    const [calidad, setCalidad] = useState<calidadDTO>(emptyCalidad);
    const [selectedCalidades, setSelectedCalidades] = useState<calidadDTO[]>([]);
    const [submitted, setSubmitted] = useState<boolean>(false);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const toast = useRef<Toast>(null);
    const dt = useRef<DataTable<calidadDTO[]>>(null);


    useEffect(() => {
        CargarCalidades()
    }, [])


    function CargarCalidades() {
        const url = `${urlCalidad}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<calidadDTO[]>) => {
                setCalidades(respuesta.data);
            })
    }

    async function BorrarCalidades(id: number) {
        try {
            await axios.delete(`${urlCalidad}/${id}`);
            CargarCalidades();
        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    async function DeleteRangoCalidades(calidades: deleteCalidadDTO[]) {
        try {
            await axios.delete(`${urlCalidad}`, { data: calidades })
            CargarCalidades();
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    async function editarCalidad(calidadEditar: calidadDTO, id: number) {
        try {
            await axios.put(`${urlCalidad}/${id}`, calidadEditar)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function crearCalidad(calidad: calidadCreacionDTO) {
        try {
            await axios.post(`${urlCalidad}/crear`, calidad)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    async function getCalidad(id: number) {
        axios.get(`${urlCalidad}/${id}`)
            .then((respuesta: AxiosResponse<calidadDTO>) => {
                setCalidad(respuesta.data);
            });
    }

    const openNew = () => {
        setCalidad(emptyCalidad);
        setSubmitted(false);
        setCalidadDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setCalidadDialog(false);
    };

    const hideDeleteCalidadDialog = () => {
        setDeleteCalidadDialog(false);
    };

    const hideDeleteCalidadsDialog = () => {
        setDeleteCalidadesDialog(false);
    };

    const saveCalidad = () => {
        setSubmitted(true);

        if (calidad.codigo.trim()) {
            let _calidads = [...calidades];
            let _calidad = { ...calidad };

            if (calidad.id) {
                const index = findIndexById(calidad.id.toString());

                _calidads[index] = _calidad;
                editarCalidad(_calidad, _calidad.id)
                toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se actualizó exitosamente.', life: 3000 });
            } else {
                // _product.image = 'product-placeholder.svg';
                _calidad.id = parseInt(createId());
                crearCalidad(_calidad)
                _calidads.push(_calidad);
                toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Se creó exitosamente.', life: 3000 });
            }

            setCalidades(_calidads);
            setCalidadDialog(false);
            setCalidad(emptyCalidad);
        }
    };

    const editCalidad = (calidad: calidadDTO) => {
        const calidadLoc = { ...calidad };
        getCalidad(calidadLoc.id);
        setCalidadDialog(true);
    };

    const confirmDeleteCalidad = (calidad: calidadDTO) => {
        setCalidad(calidad);
        setDeleteCalidadDialog(true);
    };

    const deleteCalidad = () => {
        let _calidads = calidades.filter((val) => val.id !== calidad.id);
        BorrarCalidades(calidad.id);
        setCalidades(_calidads);
        setDeleteCalidadDialog(false);
        setCalidad(emptyCalidad);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'Fue borrado exitosamente.', life: 3000 });
    };

    const findIndexById = (id: string) => {
        let index = -1;

        for (let i = 0; i < calidades.length; i++) {
            if (calidades[i].id === parseInt(id)) {
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
        setDeleteCalidadesDialog(true);
    };

    const deleteSelectedCalidads = () => {
        let _calidads = calidades.filter((val) => !selectedCalidades.includes(val));
        setCalidades(_calidads);
        setDeleteCalidadesDialog(false);
        setSelectedCalidades([]);
        DeleteRangoCalidades(selectedCalidades);
        toast.current?.show({ severity: 'success', summary: 'Exitoso', detail: 'El rango fue borrado exitosamente.', life: 3000 });
    };

    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...calidad };

        // @ts-ignore
        _product[name] = val;

        setCalidad(_product);
    };

    const onInputTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>, name: string) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...calidad };

        // @ts-ignore
        _product[name] = val;

        setCalidad(_product);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button className="button-secundario-guardar" label="Nuevo" severity="success" onClick={openNew} />
                <Button className="button-secundario-cancelar" label="Borrar" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedCalidades || !selectedCalidades.length} />
            </div>
        );
    };

    const rightToolbarTemplate = () => {
        return <Button label="Export" icon="pi pi-upload" className="p-button-help" onClick={exportCSV} />;
    };

    const actionBodyTemplate = (rowData: calidadDTO) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => editCalidad(rowData)} />
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteCalidad(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Calidades</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" placeholder="Buscar..." onInput={(e) => { const target = e.target as HTMLInputElement; setGlobalFilter(target.value); }} />
            </IconField>
        </div>
    );
    const calidadDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="Cancelar" outlined onClick={hideDialog} />
            <Button className="button-secundario-guardar" label="Guardar" onClick={saveCalidad} />
        </React.Fragment>
    );
    const deleteCalidadDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteCalidadDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteCalidad} />
        </React.Fragment>
    );
    const deleteCalidadsDialogFooter = (
        <React.Fragment>
            <Button className="button-secundario-cancelar" label="No" outlined onClick={hideDeleteCalidadsDialog} />
            <Button className="button-secundario-guardar" label="Yes" severity="danger" onClick={deleteSelectedCalidads} />
        </React.Fragment>
    );

    return (
        <div>
            <MostrarErrores errores={errores} />
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
                <DataTable ref={dt} value={calidades} selection={selectedCalidades}
                    onSelectionChange={(e) => {
                        if (Array.isArray(e.value)) {
                            setSelectedCalidades(e.value);
                        }
                    }}
                    dataKey="id" paginator rows={5} rowsPerPageOptions={[5, 10, 25]}
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    currentPageReportTemplate="Mostrando {first} de {last} de un total de {totalRecords} calidades" globalFilter={globalFilter} header={header}
                    selectionMode="multiple"
                >
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="codigo" header="Código" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column field="nombre" header="Nombre" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={productDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Detalle de tipo de calidad" modal className="p-fluid" footer={calidadDialogFooter} onHide={hideDialog}>
                <hr className="violet-line" />
                <div className="field">
                    <label htmlFor="codigo" className="font-bold">
                        Código
                    </label>
                    <InputText id="codigo" value={calidad.codigo} onChange={(e) => onInputChange(e, 'codigo')} required autoFocus className={classNames({ 'p-invalid': submitted && !calidad.codigo })} />
                    {submitted && !calidad.codigo && <small className="p-error">El código es requerido.</small>}
                </div>
                <div className="field">
                    <label htmlFor="nombre" className="font-bold">
                        Nombre
                    </label>
                    <InputText id="nombre" value={calidad.nombre} onChange={(e) => onInputChange(e, 'nombre')} required autoFocus className={classNames({ 'p-invalid': submitted && !calidad.nombre })} />
                </div>
                <div className="field">
                    <label htmlFor="descripcion" className="font-bold">
                        Descripcion
                    </label>
                    <InputTextarea id="descripcion" value={calidad.descripcion} onChange={(e: ChangeEvent<HTMLTextAreaElement>) => onInputTextAreaChange(e, 'descripcion')} required rows={3} cols={20} />
                </div>
                <hr className="violet-line" />
            </Dialog>

            <Dialog visible={deleteCalidadDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteCalidadDialogFooter} onHide={hideDeleteCalidadDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {calidad && (
                        <span>
                            ¿Quieres borrar el tipo de calidad <b>{calidad.codigo}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteCalidadesDialog} style={{ width: '32rem' }}
                breakpoints={{ '960px': '75vw', '641px': '90vw' }}
                header="Confirma"
                modal footer={deleteCalidadsDialogFooter} onHide={hideDeleteCalidadsDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {calidad && <span>¿Quieres borrar los tipos de calidades seleccionados?</span>}
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