function SalvarDepartamento() {
    debugger;


    //Name
    var name = $("#Name").val();




    var token = $('input[name="_RequestVerificationToken"]').val();
    var tokenadr = $('form[action="Departments/Create"] input [name="_RequestVerificationToken"]').val();
    var header = {};
    var headersadr = {};
    header['_RequestVerificationToken'] = token;
    headersadr['_RequestVerificationToken'] = tokenadr;

    var url = "/Departments/Create"

    $.ajax({
        url: url
        , type: "POST"
        , dataType: "json"
        , headers: headersadr
        , data: { Id: 0, Nome: name, _RequestVerificationToken:token }
        , sucess: function (data) {
            if (data.Resultado > 0) {
                debugger;
            }
        }



    });





}