

$(document).ready(function () {

    $(".btnCreate").click(function () {

        $("#conteudoModal").load("/Debts/DebtCreate/",

            function () {
                $('#myModal').modal("show")
                console.log();
            }

        )


    });

});

$(document).ready(function () {
    debugger
    $(".btnDelete").click(function () {
        
        var id = $(this).data("value")

        $("#conteudoModal").load("/Debts/RemoveDebt/" + id,

            function () {
                $('#myModal').modal("show")
                console.log();
            }

        )


    });

});

$(document).ready(function () {
    debugger
    $(".btnFuncao").click(function () {

        alert("eai")

    });

});
