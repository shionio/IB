using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBBAV.Functions
{
    public class FechasCalc
    {
          
        public FechasCalc()
        {

        }

         public static double DiferenciaAños(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs(fechaDesde.Year - fechaHasta.Year);
        }

        public static double DiferenciaMeses(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }

        public static double DiferenciaDias(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan diferencia;
            diferencia = fechaDesde - fechaHasta;

            return Math.Abs(diferencia.Days);
        }

        public static double DiferenciaHoras(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan diferencia;
            diferencia = fechaDesde - fechaHasta;

            return Math.Abs(diferencia.Hours);
        }

        public static double DiferenciaMinutos(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan diferencia;
            diferencia = fechaDesde - fechaHasta;

            return Math.Abs(diferencia.Minutes);
        }

        public static double DiferenciaSegundos(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan diferencia;
            diferencia = fechaDesde - fechaHasta;

            return Math.Abs(diferencia.Seconds);
        }

        public static double DiferenciaMiliSegundos(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan diferencia;
            diferencia = fechaDesde - fechaHasta;

            return Math.Abs(diferencia.Milliseconds);
        }

    }
}