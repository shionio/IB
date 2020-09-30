using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using IBBAV;
using System.Web;
using System.Net;

namespace IBBAV.Helpers
{
    public class HelperRangoIP
    {
        private static IBBAV.WsNotif_IBP.WsNotif_IBP_asmx ws
        {
            get
            {
                return new IBBAV.WsNotif_IBP.WsNotif_IBP_asmx();
            }
        }

        public HelperRangoIP()
        {

        }

        public static DataSet IpNacional()
        {
            bool flag;
            DataSet dataSet = null;
            DataTable item = null;
            try
            {
                dataSet = HelperRangoIP.ws.ValidaConexion();
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
    }
}