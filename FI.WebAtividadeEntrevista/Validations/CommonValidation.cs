using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAtividadeEntrevista.Validations
{
    public static class CommonValidation
    {
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            cpf = Regex.Replace(cpf, @"[^\d]", "");

            var digitosCpf = cpf.ToCharArray().Select(s => CharUnicodeInfo.GetDecimalDigitValue(s)).ToList();

            return (ValidarPrimeiroDigitoVerificadorCpf(digitosCpf) && ValidarSegundoDigitoVerificadorCpf(digitosCpf));
        }

        private static bool ValidarPrimeiroDigitoVerificadorCpf(List<int> digitosCpf)
        {
            var total = 0;
            var indiceDigitos = 0;
            
            for (var i = 10; i >= 2; i--)
            {
                var resultado = digitosCpf[indiceDigitos] * i;
                total = total + resultado;

                indiceDigitos = indiceDigitos + 1;
            }

            var modulo = total % 11;

            var digitoCalculado = 11 - modulo;

            if (digitoCalculado > 9) 
                digitoCalculado = 0;

            return digitoCalculado == digitosCpf[9];
        }

        private static bool ValidarSegundoDigitoVerificadorCpf(List<int> digitosCpf)
        {
            var total = 0;
            var indiceDigitos = 0;

            for (var i = 11; i >= 2; i--)
            {
                var resultado = digitosCpf[indiceDigitos] * i;
                total = total + resultado;

                indiceDigitos = indiceDigitos + 1;
            }

            var modulo = total % 11;

            var digitoCalculado = 11 - modulo;

            if (digitoCalculado > 9)
                digitoCalculado = 0;

            return digitoCalculado == digitosCpf[10];
        }
    }
}