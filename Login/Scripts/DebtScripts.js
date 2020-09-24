$(document).ready(function () {

    $(".btnReceita").click(function () {
        $("#conteudoModal").load("/Debts/AdicionarReceita/",

            function () {
                $('#myModal').show();
                console.log();
            }

        )


    });

});

$(document).ready(function () {

    $(".btnCreate").click(function () {
        $("#conteudoModal").load("/Debts/DebtCreate/",

            function () {
                $('#myModal').show()
                console.log();
            }

        )


    });

});

$(document).ready(function () {

    $(".btnOtReceitas").click(function () {

       

        $("#conteudoModal").load("/Debts/LerOutrasReceitas/" ,

            function () {
                $('#myModal').show()
                console.log();
            }

        )


    });

});


$(document).ready(function () {

    $(".btnAddOtReceitas").click(function () {



        $("#conteudoModal").load("/Debts/AdicionarOutrasReceitas/",

            function () {
                $('#myModal').show()
                console.log();
            }

        )


    });

});

$(document).ready(function () {

    $(".btnDelete").click(function () {

        var id = $(this).data("value")

        $("#conteudoModal").load("/Debts/RemoveDebt/" + id,

            function () {
                $('#myModal').show()
                console.log();
            }

        )

       
    });
});