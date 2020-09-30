using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace IBBAV.ws
{
    [ScriptService]
    [ToolboxItem(false)]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Data : WebService
    {
        public Data()
        {
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public ResponseWs<bool> ActualizarSesion(string sesion)
        {
            ResponseWs<bool> responseW = new ResponseWs<bool>();
            try
            {
                if (HelperSession.SA_GetSession(sesion).SES_CodStatus.Equals("A"))
                {
                    responseW.Resultado = "OK";
                }
                else
                {
                    responseW.Resultado = "FAIL";
                }
            }
            catch (Exception exception)
            {
                responseW.Resultado = "FAIL";
            }
            return responseW;
        }

        [WebMethod]
        public ResponseWs<List<FavoritosAfiliado>> MenuFavoritos(string sesion)
        {
            ResponseWs<List<FavoritosAfiliado>> responseW = new ResponseWs<List<FavoritosAfiliado>>();
            SessionAfiliado sessionAfiliado = HelperSession.SA_GetSession(sesion);
            if (sessionAfiliado != null)
            {
                responseW.CodError = 0;
                responseW.Resultado = "OK";
                responseW.Data = new List<FavoritosAfiliado>();
                foreach (DataRow row in HelperFavorito.AfiliadoFavoritosGetByAfiliado(sessionAfiliado.AF_Id).Tables[0].Rows)
                {
                    responseW.Data.Add(FavoritosAfiliado.getNewFavoritosAfiliado(row));
                }
            }
            else
            {
                responseW.CodError = 1;
                responseW.Resultado = "FAIL";
            }
            return responseW;
        }


        [WebMethod]
        public ResponseWs<List<DetallesViaje>> DetallesNotificacion(string sesion)
        {
            ResponseWs<List<DetallesViaje>> responseW = new ResponseWs<List<DetallesViaje>>();
            SessionAfiliado sessionAfiliado = HelperSession.SA_GetSession(sesion);
            if (sessionAfiliado != null)
            {
                responseW.CodError = 0;
                responseW.Resultado = "OK";
                responseW.Data = new List<DetallesViaje>();
                foreach (DataRow row in HelperNotificacionIBP.DestinoByNotificacion(sessionAfiliado.AF_Id).Tables[0].Rows)
                {
                    responseW.Data.Add(DetallesViaje.getDetalles(row));
                }
            }
            else
            {
                responseW.CodError = 1;
                responseW.Resultado = "FAIL";
            }
            return responseW;
        }

        [WebMethod]
        public ResponseWs<List<Movimientos>> Movimientos(string sesion, string cta, string tipo, DateTime di, DateTime df)
        {
            HelperSession.SA_GetSession(sesion);
            ResponseWs<List<Movimientos>> responseW = new ResponseWs<List<Movimientos>>()
            {
                CodError = 0,
                Resultado = "OK",
                Data = new List<Movimientos>()
            };
            Movimientos movimiento = new Movimientos();
            return responseW;
        }
    }
}