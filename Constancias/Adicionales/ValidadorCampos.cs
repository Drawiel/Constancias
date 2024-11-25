using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Constancias.Adicionales {
    public class ValidadorCampos {
        public static bool EsContraseniaValida(string password) {
            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"\d"))
                return false;

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]"))
                return false;

            return true;
        }

        public static bool EsCorreoValido(string email) {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
