function inicializarFormularioTransacciones(urlObtenerCategorias) {
    $("#TipoOperacionId").change(async function () {
        //console.log('inicio filtro')
        const valorSeleccionado = $(this).val();

        const respuesta = await fetch(urlObtenerCategorias, {
            method: 'POST',
            body: valorSeleccionado,
            headers: {
                'Content-Type': 'application/json'
            }
        });
        //console.log('respuesta fetch..')
        const json = await respuesta.json();

        //console.log('respuesta json..')
        console.log(json)
        const opciones = json.map(categoria => `<option value=${categoria.value}> ${categoria.text} </option>`);
        $("#CategoriaId").html(opciones)
    })
}