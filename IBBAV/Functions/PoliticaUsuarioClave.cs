using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace IBBAV.Functions
{
    public class PoliticaUsuarioClave
    {
        public static string SIMBOLOS_ACEPTADOS;

        private static Regex RegexUserName;

        private static Regex RegexRespuesta;

        static PoliticaUsuarioClave()
        {
            PoliticaUsuarioClave.SIMBOLOS_ACEPTADOS = "+|*|.|,|{|}|_|(|)|?|¿|/|&|%|$|#|!|\"|°|=|'|¨|~|[|]|-|:|;|<|>]+";
            PoliticaUsuarioClave.RegexUserName = new Regex(string.Concat("^[0-9a-zA-Z", PoliticaUsuarioClave.SIMBOLOS_ACEPTADOS, "]+$"));
            PoliticaUsuarioClave.RegexRespuesta = new Regex(string.Concat("^[0-9a-zA-Z", PoliticaUsuarioClave.SIMBOLOS_ACEPTADOS, "]+$"));
        }

        public static void validaPoliticaClave(string userName, string clave, string confirmacionClave)
        {
            string usrName = userName.Trim().ToLower(); //usrName
            string passUpper = clave.Trim().ToUpper();      // pass
            string confirm = confirmacionClave.Trim().ToUpper(); //confirm
            string passLow = passUpper.ToLower();            //passUpper
            string claveLower = clave.ToLower();              //passLower           

            if (passUpper.Equals(string.Empty))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("La clave de usuario es requerida");
            }
            if (usrName.Equals(passLow))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave es inválida. La clave no debe ser igual a su usuario");
            }
            if ((passUpper.Length < 8 ? true : passUpper.Length > 10))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("La clave de usuario debe tener entre 8 y 10 caracteres");
            }
            if ((new Regex("(([a-z]|[A-Z]){5})|(([0-9]){5})")).IsMatch(passUpper))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave es inválida. La clave no debe contener más de cuatro (4) carácteres númericos o alfabéticos consecutivos");
            }
            if ((new Regex("([a-z|ñ|A-Z|Ñ|0-9])\\1{3}")).IsMatch(passUpper))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave es inválida. La clave no debe contener más de tres (3) carácteres iguales consecutivos");
            }
            if (((new Regex("[a-z|A-Z|ñ|Ñ]")).Matches(clave).Count < 4 || (new Regex("[0-9]")).Matches(clave).Count < 3 ? true : (new Regex(string.Concat("[", PoliticaUsuarioClave.SIMBOLOS_ACEPTADOS, "]"))).Matches(clave).Count < 1))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave es inválida. La clave debe tener una longitud de 9 caracteres, con cuatro (4) caracteres alfabéticos, cuatro (4) numéricos y un (1) carácter especial");
            }
            if (confirm.Equals(string.Empty))
            {
                throw new Exception("La confirmación de clave de usuario es requerida");
            }
            if (!passUpper.Equals(confirm))
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("La clave y la confirmación no son iguales");
            }
            if (claveLower.IndexOf("agri") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("cola") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("vene") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("uela") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("banc") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("anco") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (claveLower.IndexOf("bav") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("Su clave no debe contener las siglas BAV");
            }
        }

        public static void ValidaPoliticaPreguntaDesafio(string preguntaDesafio, string respuestaPreguntaDesafio)
        {
            string str = preguntaDesafio.Trim();
            string str1 = respuestaPreguntaDesafio.Trim();
            if (str.Length == 0)
            {
                throw new PoliticaUsuarioClave.ErrorPreguntaDesafioException("La pregunta desafío es requerida");
            }
            if (str.Length < 3)
            {
                throw new PoliticaUsuarioClave.ErrorPreguntaDesafioException("La pregunta de desafío debe tener al menos 3 carácteres");
            }
            if (str1.Length == 0)
            {
                throw new PoliticaUsuarioClave.ErrorPreguntaDesafioException("La respuesta a pregunta de desafío es requerida");
            }
            if (str1.Length < 6)
            {
                throw new PoliticaUsuarioClave.ErrorPreguntaDesafioException("La respuesta a pregunta de desafío debe tener al menos 6 carácteres");
            }
            if (!PoliticaUsuarioClave.RegexRespuesta.IsMatch(str1))
            {
                throw new PoliticaUsuarioClave.ErrorPreguntaDesafioException("La respuesta debe ser una sóla palabra y no debe contener carácteres especiales");
            }
        }

        public static void validaPoliticaUsuario(string userName)
        {
            string str = userName.Trim();
            string lower = str.ToLower();

            if (str.Equals(string.Empty))
            {
                throw new PoliticaUsuarioClave.ErrorUsuarioException("El nombre de usuario es requerido");
            }
            if ((!PoliticaUsuarioClave.RegexUserName.IsMatch(str) || str.Length < 6 ? true : str.Length > 10))
            {
                throw new PoliticaUsuarioClave.ErrorUsuarioException("El nombre de usuario debe tener entre 6 y 10 letras y/o números, sin espacios, comas ni puntos");
            }
            if (lower.IndexOf("bav") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorUsuarioException("El nombre de usuario no debe contener las siglas BAV");
            }
            if ((lower.IndexOf("banco") > -1 || lower.IndexOf("agricola") > -1 ? true : lower.IndexOf("bav") > -1))
            {
                throw new PoliticaUsuarioClave.ErrorUsuarioException("El nombre de usuario no debe contener el nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("agri") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("cola") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("vene") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("uela") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("banc") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("anco") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener porciones del nombre Banco Agrícola de Venezuela");
            }
            if (lower.IndexOf("bav") > -1)
            {
                throw new PoliticaUsuarioClave.ErrorClaveException("El nombre de usuario no debe contener las siglas BAV");
            }
        }

        public class ErrorClaveException : Exception
        {
            public ErrorClaveException(string mensaje) : base(mensaje)
            {
            }
        }

        public class ErrorPreguntaDesafioException : Exception
        {
            public ErrorPreguntaDesafioException(string mensaje) : base(mensaje)
            {
            }
        }

        public class ErrorUsuarioException : Exception
        {
            public ErrorUsuarioException(string mensaje) : base(mensaje)
            {
            }
        }
    }
}