import './SelectorMultiple.css';
export default function SelectorMultiple(props: selectorMultipleProps) {

    function seleccionar(item: selectorMultipleModel) {
        const seleccionados = [...props.seleccionados, item];
        const noSeleccionados = props.noSeleccionados.filter(valor => valor !== item);
        props.onChange(seleccionados, noSeleccionados);
    }

    function deSeleccionar(item: selectorMultipleModel) {
        const seleccionados = props.seleccionados.filter(v => v !== item);
        const noSeleccionados = [...props.noSeleccionados, item];
        props.onChange(seleccionados, noSeleccionados);
    }

    function seleccionarTodo() {
        const seleccionados = [...props.seleccionados, ...props.noSeleccionados];
        const noSeleccionados: selectorMultipleModel[] = [];
        props.onChange(seleccionados, noSeleccionados);
    }

    function deSeleccionarTodo() {
        const noSeleccionados = [...props.noSeleccionados, ...props.seleccionados];
        const seleccionados: selectorMultipleModel[] = [];
        props.onChange(seleccionados, noSeleccionados);
    }

    return (
        <div className="selector-multiple">
            <ul>
                {props.noSeleccionados.map(item =>
                    <li key={item.llave}
                        onClick={() => { seleccionar(item) }}>{item.valor}</li>)}
            </ul>
            <div className="selector-multiple-botones">
                <button type="button" onClick={seleccionarTodo}>{'>>'}</button>
                <button type="button" onClick={deSeleccionarTodo}>{'<<'}</button>
            </div>
            <ul>
                {props.seleccionados.map(i =>
                    <li key={i.llave}
                        onClick={() => { deSeleccionar(i) }}>{i.valor}</li>)}
            </ul>
        </div>
    )
}

interface selectorMultipleProps {
    seleccionados: selectorMultipleModel[];
    noSeleccionados: selectorMultipleModel[];
    onChange(seleccionados: selectorMultipleModel[],
        noSeleccionados: selectorMultipleModel[]): void
}

export interface selectorMultipleModel {
    llave: number;
    valor: string;
}