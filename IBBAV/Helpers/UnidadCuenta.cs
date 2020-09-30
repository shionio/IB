using IBBAV.WsIbsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBBAV.Helpers
{
    public class UnidadCuenta
    {
        private string sPetroField;

        IbaConsDet data = new IbaConsDet();

        public UnidadCuenta()
        {
        }

        public string SPetro(string disponible)
        {           
            Decimal Calc = Convert.ToDecimal(disponible) / 8000000m; //Convert.ToDecimal(petro);
            sPetroField = Calc.ToString();
            return sPetroField;
        }

    }
}