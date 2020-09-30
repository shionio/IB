using System;
using System.Net;
using System.Web.Services.Protocols;
using IBBAV.Entidades.Extraefectivo;
using IBBAV.WsExtraefectivo;
using IBBAV.Functions;
using static IBBAV.Entidades.Extraefectivo.LineaExtracredito;
using Functions;

namespace IBBAV.Helpers
{
    public class HelperExtracredito : ExtraCreditoService
    {
        // Aquí invoco cada método del webservice
        private const string CODSISTEMA = "IBS";
        private const string CODSISTEMAIB = "SQLIB";


        private static IBBAV.WsExtraefectivo.ExtraCreditoService ws
        {
            get
            {
                return new IBBAV.WsExtraefectivo.ExtraCreditoService();
            }
        }

        public HelperExtracredito()
        {
        }

        public static bool tieneTarjeta(String cedula)
        {
            bool flag = false;
            //int i = HelperExtracredito.ws.tarjetasL2(cedula).Length;
            TarjetaConsulta linea = new TarjetaConsulta();
            TarjetaConsulta[] resultados;

            using (ExtraCreditoService extraCreditoService = new ExtraCreditoService())
            {
                extraCreditoService.Timeout = 20000;
                try
                {
                    String[] con = extraCreditoService.consultaTarjetas(cedula);
                    resultados = new TarjetaConsulta[con.Length];//agregar el tamaño según numero de tdc del cliente
                    String[] tarjeta;

                    if (!con[0].ToString().Equals("999"))
                    {
                        return true;
                    }
                    if (resultados == null)
                    {
                        throw new IBException(9998, "SQLIB");
                    }
                }
                catch (WebException webException)
                {
                    throw new IBException(9997, "SQLIB");
                }
                catch (SoapException soapException)
                {
                    throw new IBException(9997, "SQLIB");
                }
            }
            return flag;
        }

        // Invoco el método que devuelve una consulta de las TDC disponibles para extra efectivo
        public static TarjetaConsulta[] consulta(String cedula)
        {

            //int i = HelperExtracredito.ws.tarjetasL2(cedula).Length;
            TarjetaConsulta linea = new TarjetaConsulta();
            TarjetaConsulta[] resultados;

            using (ExtraCreditoService extraCreditoService = new ExtraCreditoService())
            {
                extraCreditoService.Timeout = 20000;
                try
                {
                    String[] con = extraCreditoService.consultaTarjetas(cedula);
                    resultados = new TarjetaConsulta[con.Length];//agregar el tamaño según numero de tdc del cliente
                    String[] tarjeta;

                    if (!con[0].ToString().Equals("999"))
                    {
                        for (int i = 0; i < con.Length; i++)
                        {
                            tarjeta = con[i].Split('*');
                            string numTarjeta = tarjeta[0];
                            string marcaTarjeta = tarjeta[1];
                            string montoTarjeta = tarjeta[2];

                            //int nroNum2 = Convert.ToInt32(montoTarjeta);
                            //montoTarjeta = (nroNum2 / 100m).ToString("N2");

                            TarjetaConsulta linea0 = new TarjetaConsulta()
                            {
                                codRespuesta = "000",
                                descRespuesta = "OK",
                                disponible = Formatos.formatoMontoAgrDecimal(montoTarjeta),
                                //disponible = "10000",
                                marca = marcaTarjeta,
                                numero = numTarjeta,
                                numeroTDC = numTarjeta,
                                saldo = Formatos.formatoMontoAgrDecimal(montoTarjeta),
                                //saldo = "20000",
                                Key = CryptoUtils.EncryptMD5(numTarjeta),
                            };

                            resultados[i] = linea0;
                        }

                        return resultados;

                    }

                    if (resultados == null)
                    {
                        throw new IBException(9998, "SQLIB");
                    }
                }
                catch (WebException webException)
                {
                    throw new IBException(9997, "SQLIB");
                }
                catch (SoapException soapException)
                {
                    throw new IBException(9997, "SQLIB");
                }
            }

            return resultados;
        }


        // Invoco el metodo que envia una solicitud de extra efectivo
        public static LineaExtracredito consultaCuotas(string cedula, string cuenta, string tarjeta, string monto)
        {
            LineaExtracredito lineaExtra = new LineaExtracredito();

            using (ExtraCreditoService extraCreditoService = new ExtraCreditoService())
            {
                extraCreditoService.Timeout = 20000;
                try
                {
                    monto = monto.Replace(".", "");//se le quita los formatos de miles al monto
                    monto = monto.Replace(",", "");//se le quita los formatos de decimales al monto

                    string val = extraCreditoService.consultarCuotas(cedula, cuenta, tarjeta, monto);
                    val = "000*2306*2035*2000*2000";
                    string[] resultado = val.Split('*');

                    if (resultado[0].ToString().Equals("000"))
                    {
                        
                        string Cod = resultado[0];
                        string Desc = "OK";
                        string cuota1 = resultado[1];
                        string cuota2 = resultado[2];
                        string cuota3 = resultado[3];
                        string cuota4 = resultado[4];
                        
                        LineaExtracredito cuotas = new LineaExtracredito()
                        {
                            respuestaCod = Cod,
                            respuestaDesc = Desc,
                            cuota6 = cuota1,
                            cuota12 = cuota2,
                            cuota24 = cuota3,
                            cuota36 = cuota4
                        };

                        lineaExtra = cuotas;
                        
                        return lineaExtra;
                    }
                    else
                    {

                        string Cod = resultado[0];
                        string Desc = resultado[1];

                        LineaExtracredito aprobado = new LineaExtracredito()
                        {
                            respuestaCod = Cod,
                            respuestaDesc = Desc
                        };

                        return aprobado;

                    }
                    
                }
                catch (WebException webException)
                {
                    string Cod = "333";
                    string Desc = "ERROR INTERNO";

                    LineaExtracredito aprobado = new LineaExtracredito()
                    {
                        respuestaCod = Cod,
                        respuestaDesc = Desc
                    };

                    return aprobado;
                    throw new IBException(9997, "SQLIB");
                }
                catch (SoapException soapException)
                {
                    string Cod = "333";
                    string Desc = "ERROR INTERNO";

                    LineaExtracredito aprobado = new LineaExtracredito()
                    {
                        respuestaCod = Cod,
                        respuestaDesc = Desc
                    };

                    return aprobado;
                    throw new IBException(9997, "SQLIB");
                }
            }
            return lineaExtra;
        }

        // Invoco el metodo que recibe la respuesta a una solicitud de extra efectivo
        public static LineaExtracredito solicAprobacion(string cedula, string cuenta, string tarjeta, string monto, string cuotas, string montoCuota)
        {
            LineaExtracredito lineaExtra = new LineaExtracredito();

            using (ExtraCreditoService extraCreditoService = new ExtraCreditoService())
            {
                extraCreditoService.Timeout = 20000;
                try
                {
                    string val = extraCreditoService.aprobacionPago(cedula, cuenta, tarjeta, monto, cuotas, montoCuota);
                    val = "000*44541234*";
                    string[] resultado = val.Split('*');

                    if (resultado[0].ToString().Equals("000"))
                    {
                        
                            string Cod = resultado[0];
                            string Desc = "Aprobado";
                            string referencia = resultado[1];

                            LineaExtracredito aprobado = new LineaExtracredito()
                            {
                                respuestaCod = Cod,
                                respuestaDesc = Desc,
                                referencia = referencia
                            };

                            lineaExtra = aprobado;

                        

                        return lineaExtra;
                    }
                    else
                    {
                        
                            string Cod = resultado[0];
                            string Desc = resultado[1];

                            LineaExtracredito aprobado = new LineaExtracredito()
                            {
                                respuestaCod = Cod,
                                respuestaDesc = Desc
                            };

                            lineaExtra = aprobado;

                      

                        return lineaExtra;
                    }
                    if (lineaExtra == null)
                    {
                        throw new IBException(9998, "SQLIB");
                    }
                }
                catch (WebException webException)
                {
                    string Cod = "333";
                    string Desc = "ERROR INTERNO";

                    LineaExtracredito aprobado = new LineaExtracredito()
                    {
                        respuestaCod = Cod,
                        respuestaDesc = Desc
                    };

                    return aprobado;
                    throw new IBException(9997, "SQLIB");
                }
                catch (SoapException soapException)
                {
                    string Cod = "333";
                    string Desc = "ERROR INTERNO";

                    LineaExtracredito aprobado = new LineaExtracredito()
                    {
                        respuestaCod = Cod,
                        respuestaDesc = Desc
                    };

                    return aprobado;
                    throw new IBException(9997, "SQLIB");
                }
            }
            //return lineaExtra;
        }
    }
}