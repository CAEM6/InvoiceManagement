function GetView(url, content) {
    $.ajax({
        type: "GET",
        cache: false,
        url: url,
    }).done(function (data) {
        $('#' + content).html(data).ready();
    }).fail(function () {

    });
}

function GetResponse(response) {
    if (response.error != true) {
        swal("", "Registro guardado con exito!", "success");
    }
    else {
        swal("", response.message, "error");
    }
}