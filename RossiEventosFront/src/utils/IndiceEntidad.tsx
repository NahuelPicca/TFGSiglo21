import { ReactElement, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Button from "./Button";
import axios, { AxiosResponse } from "axios";
import confirmar from "./Confirmar";
import Paginacion from "./Paginacion";
import ListadoGenerico from "./ListadoGenerico";

export default function IndiceEntidad<T>(props: indiceEntidadProps<T>) {

    const [entidades, setEntidades] = useState<T[]>();
    const [totalDePaginas, setTotalDePaginas] = useState(1);
    const [recordsPorPagina, setRecordsPorPagina] = useState(5);
    const [pagActual, setPagActual] = useState(1);

    // Para instalar los Router de React hay que correr el 
    //comando en la terminal "npm i react-router-dom@6.21"
    //6.21 es la versión del router
    //Instalar los Types de la slibrería anteriormente mencionada
    //con el comando "npm i --save-dev @types/react-router-dom"

    //Para que funcione pasar los parametros desde el front al back, 
    //tiene que ser el mismo nombre de las variables, si no, 
    // no se actualiza los valores y quedan por defecto lo cual no
    //funcionan.
    useEffect(() => {
        cargarDatos();
    }, [pagActual, recordsPorPagina]);

    function cargarDatos() {
        //Axios se encarga de conectarse con la api
        axios.get(props.url, {
            params: { pagActual, recordsPorPagina }
        })
            .then((respuesta: AxiosResponse<T[]>) => {
                const totalDeRegistros =
                    parseInt(respuesta.headers['cantidadtotalregistros'], 10)
                setTotalDePaginas(Math.ceil(totalDeRegistros / recordsPorPagina));
                setEntidades(respuesta.data);
            })
    }

    async function borrar(id: number) {
        try {
            await axios.delete(`${props.url}/${id}`);
            cargarDatos();

        } catch (error: any) {
            console.log(error.response.data);
        }
    }

    const botones = (urlEditar: string, id: number) =>
        <>
            <Link className="btn btn-success" to={urlEditar}>
                Editar
            </Link>
            <Button
                onClick={() => confirmar(() => borrar(id))}
                className="btn btn-danger">
                Borrar
            </Button>
        </>

    return (
        <>
            <h3>{props.titulo}</h3>
            {props.urlCrear ? <Link className="btn btn-primary" to={props.urlCrear}>
                Crear {props.nombreEntidad}
            </Link> : null}
            <div className="form-group" style={{ width: '150px' }}>
                <label>Registros por página:</label>
                <select
                    className="form-control"
                    defaultValue={10}
                    onChange={e => {
                        setPagActual(1);
                        setRecordsPorPagina(parseInt(e.currentTarget.value, 10))
                    }}>
                    <option value={5}>5</option>
                    <option value={10}>10</option>
                    <option value={25}>25</option>
                    <option value={50}>50</option>
                </select>
            </div>
            <div className="form-group" >
                <Paginacion cantidadTotalDePaginas={totalDePaginas}
                    paginaActual={pagActual} onChange={nuevaPag => setPagActual(nuevaPag)} />
            </div>
            <ListadoGenerico listado={entidades}>
                <table className="table table-striped">
                    {props.children(entidades!, botones)}
                </table>
            </ListadoGenerico>
        </>
    )
}

interface indiceEntidadProps<T> {
    url: string;
    urlCrear?: string; // El simbolo ? significa opcional o null. 
    children(entidades: T[],
        botones: (urlEditar: string, id: number) => ReactElement): ReactElement;
    titulo: string;
    nombreEntidad?: string;
}