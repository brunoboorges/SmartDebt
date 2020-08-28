using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models.Enums
{
    public enum DebtStatus : int
    {
        //AINDA FALTA MUITO TEMPO PARA SER PAGO : BLUE
        Distant = 0,
        //TEMPO MODERADO PARA PAGAMENTO : YELLOW
        Default = 1,
        //ESTA MUITO PERTO PARA O PAGAMENTO : RED
        Close = 2,
        //PAGAMENTO FOI CANCELADO : PURPLE
        Cancel = 3

    }
}