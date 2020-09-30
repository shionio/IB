using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace IBBAV.Entidades.Notificaciones
{
    public class ValidaRangoIP
    {

        public static bool ValidaRangoIPV6(string ipDir, string dirRed, string cidr)
        {
            string rango = dirRed + cidr;
            IPNetwork ipnetwork = IPNetwork.Parse(rango);
            IPAddress ipaddress = IPAddress.Parse(ipDir);
            IPAddress ipaddress2 = IPAddress.Parse("fe80::202:b3ff:fe1e:1");

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos Ignoramos la advertencia del compilador
            bool contains = IPNetwork.Contains(ipnetwork, ipaddress);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            return contains;
        }

        public static bool ValidaRangoIPV4(string ipDir, string dirRed, string cidr)
        {
            string rango = dirRed + cidr;
            IPNetwork ipnetwork = IPNetwork.Parse(rango);
            IPAddress ipaddress = IPAddress.Parse(ipDir);
            IPAddress ipaddress2 = IPAddress.Parse("192.168.0.200");

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            bool contains = IPNetwork.Contains(ipnetwork, ipaddress);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            return contains;
        }
    }
}