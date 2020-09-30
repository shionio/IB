using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using IBBAV;
using IBBAV.Entidades.Notificaciones;

namespace IBBAV.Helpers
{
    public class HelperNotificacionIBP
    {
        private static IBBAV.WsNotif_IBP.WsNotif_IBP_asmx ws
        {
            get
            {
                return new IBBAV.WsNotif_IBP.WsNotif_IBP_asmx();
            }
        }

        public HelperNotificacionIBP()
        {
        }

        public static bool VerificaNotificacion(long nAF_Id)
        {
            bool flag = false;
            try
            {
                flag = (HelperNotificacionIBP.ws.NotificacionVerify(nAF_Id));

            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }

        public static string RestriccionVerify(long nAF_Id)
        {
            string str = "";
            try
            {
                str = (HelperNotificacionIBP.ws.RestriccionVerify(nAF_Id));

            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return str;
        }

        public static bool ResticcionAdd(long AF_Id, string dirIP, string decIP,string TipoIns, string NumIns)
        {
            bool flag = false;
            try
            {
                flag = (HelperNotificacionIBP.ws.RestriccionAdd(AF_Id, dirIP, decIP, TipoIns, NumIns));

            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }

        public static bool NotificacionAdd(long AF_Id, string inicio, string fin, string destino, string TipoIns, string NumIns)
        {
            bool flag = false;
            
            try
            {
                flag = HelperNotificacionIBP.ws.NotificacionAdd(AF_Id, inicio, fin, destino,TipoIns,NumIns);
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }

        public static bool DestinosAdd(long nAF_Id, DateTime inicio, DateTime fin, string pais)
        {
            bool flag = false;
            try
            {
                flag = HelperNotificacionIBP.ws.DestinoAdd(nAF_Id, inicio, fin, pais);
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }

        public static bool DestinosDel(long nAF_Id, long destino)
        {
            bool flag = false;
            try
            {
                flag = HelperNotificacionIBP.ws.DestinoDel(nAF_Id, destino);
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }

        public static bool ValidaDestinos(long nAF_Id)
        {
            bool flag = false;

            try
            {
                flag = HelperNotificacionIBP.ws.ValidaDestPeriodo(nAF_Id);
            }
            catch (WebException webExecption)
            {
                throw new IBException(webExecption.Message, "SQLIB");
            }

            return flag;
        }

        public static DataSet IpNacional()
        {
            bool flag;
            DataSet dataSet = null;
            DataTable item = null;
            try
            {
                dataSet = HelperNotificacionIBP.ws.ValidaConexion();
                if (dataSet.Tables.Count != 0)
                {
                    item = dataSet.Tables[0];
                    if (item.Rows.Count == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
                    }
                    if (flag)
                    {
                        throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                    }

                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return dataSet;
        }

        public static List<IP_Nacional> GetListIpNacional()
        {
            List<IP_Nacional> iPs = new List<IP_Nacional>();
            DataSet dataSet;
            try
            {
                dataSet = HelperNotificacionIBP.ws.ValidaConexion();
                if (dataSet.Tables.Count != 0)
                {
                    DataTable item = dataSet.Tables[0];
                    if (item.Rows.Count != 0)
                    {
                        if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
                        {
                            throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                        }
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            iPs.Add(IP_Nacional.GetIP_Nacional(row));
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return iPs;
        }

        public static List<Paises> GetListaPaises()
        {
            List<Paises> pais = new List<Paises>();
            try
            {
                DataSet dataSet = HelperNotificacionIBP.ws.ListaPaises();
                if (dataSet.Tables.Count != 0)
                {
                    DataTable item = dataSet.Tables[0];
                    if (item.Rows.Count != 0)
                    {
                        if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
                        {
                            throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                        }
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            pais.Add(Paises.getNewPais(row));
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return pais;
        }

        public static Paises GetPais(long codPais)
        {
            Paises newPais = null;
            try
            {
                DataSet dataSet = HelperNotificacionIBP.ws.ListaPaises();

                if (dataSet.Tables.Count != 0)
                {
                    DataTable item = dataSet.Tables[0];
                    if (item.Rows.Count != 0)
                    {
                        if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
                        {
                            throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                        }
                        foreach (DataRow row in item.Rows)
                        {
                            newPais = Paises.getNewPais(row);
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return newPais;
        }

        public static DataSet DestinoByNotificacion(long nAF_Id)
        {
            bool flag;
            DataSet dataSet = null;
            DataTable item = null;
            try
            {
                dataSet = HelperNotificacionIBP.ws.DetalleNotificacion(nAF_Id);
                if (dataSet.Tables.Count != 0)
                {
                    item = dataSet.Tables[0];
                    if (item.Rows.Count == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
                    }
                    if (flag)
                    {
                        throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                    }

                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return dataSet;
        }

        public static DataTable DestinoTable(long nAF_Id)
        {
            bool flag;
            DataSet dataSet = null;
            DataTable item = null;
            try
            {
                dataSet = HelperNotificacionIBP.ws.DetalleNotificacion(nAF_Id);
                if (dataSet.Tables.Count != 0)
                {
                    item = dataSet.Tables[0];
                    if (item.Rows.Count == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
                    }
                    if (flag)
                    {
                        throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                    }

                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return item;
        }

        public static DataSet AfiliadoFavoritosGetByAfiliado(long nAF_Id)
        {
            bool flag;
            DataSet dataSet = null;
            try
            {
                dataSet = HelperNotificacionIBP.ws.DetalleNotificacion(nAF_Id);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable item = dataSet.Tables[0];
                    if (item.Rows.Count == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
                    }
                    if (flag)
                    {
                        throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
                    }
                }
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return dataSet;
        }

        public static bool NotificacionVencida(long nAF_Id)
        {
            bool flag = false;
            try
            {
                flag = HelperNotificacionIBP.ws.NotificacionExpiry(nAF_Id);
            }
            catch (WebException webException)
            {
                throw new IBException(webException.Message, "SQLIB");
            }
            return flag;
        }
    }
}