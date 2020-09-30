using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IBBAV.Helpers;
using IBBAV.Entidades;
using System.Net;
using IBBAV.Entidades.Notificaciones;
using System.Numerics;

namespace IBBAV.Entidades
{
    class RangoIP
    {
        private string dirRed;
        private string cdir;
        private DataTable rangoIP;

        public DataTable RangoIpNacional
        {
            get
            {
                return this.rangoIP;
            }
            set
            {
                this.rangoIP = value;
            }
        }

        public RangoIP()
        {

        }

        public DataTable getRangoIP(DataRow dr)
        {
            string str;
            string str1;
            return rangoIP;
        }

        public bool ValidaConexion(string dirIP)
        {
            bool flag = false;
            IPNetwork ipnetwork = IPNetwork.Parse(dirIP);
            string familiaIP = ipnetwork.AddressFamily.ToString();

            DataSet ds = HelperNotificacionIBP.IpNacional();
            rangoIP = ds.Tables[0];
            
            if (familiaIP == "InterNetwork")
            {
                for (int i = 0; i < rangoIP.Rows.Count; i++)
                {
                    this.dirRed = rangoIP.Rows[i][0].ToString();
                    this.cdir = rangoIP.Rows[i][1].ToString();
                    if (ValidaRangoIP.ValidaRangoIPV4(dirIP, dirRed, cdir))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            else if (familiaIP == "InterNetworkV6")
            {
                for (int i = 0; i < rangoIP.Rows.Count; i++)
                {
                    this.dirRed = rangoIP.Rows[i][0].ToString();
                    this.cdir = rangoIP.Rows[i][1].ToString();
                    if (ValidaRangoIP.ValidaRangoIPV6(dirIP, dirRed, cdir))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return flag;
        }

        public string StringToInt(string ipDir)
        {
            IPAddress ipaddres = IPAddress.Parse(ipDir);
            BigInteger ipDirDec = IPNetwork.ToBigInteger(ipaddres);
            return ipDirDec.ToString();
        }

    }
}
