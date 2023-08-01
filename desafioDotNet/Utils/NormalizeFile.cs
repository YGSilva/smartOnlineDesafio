using desafioDotNet.Models;
using System.Globalization;
using desafioDotNet.Enums;

namespace desafioDotNet.Utils {
    public class NormalizeFile {
        public List<RegisterModel> GetRegistration(string content) {
            var registers = new List<RegisterModel>();

            var lines = content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines) {
                var register = new RegisterModel();

                register.Tipo = (TransactionType)Enum.Parse(typeof(TransactionType), line.Substring(0, 1));
                register.Data = DateConverter(line.Substring(1, 8));
                
                register.Valor = double.Parse(line.Substring(9, 10)) / 100;

                string cpf = line.Substring(19, 11);
                if (IsCpfValid(cpf))
                    register.Cpf = cpf;
                else 
                    register.Cpf = cpf + " = CPF INVALIDO!";

                register.Cartao = line.Substring(30, 12);
                register.DonoLoja = line.Substring(42, 14);
                register.NomeLoja = line.Substring(56, 18);

                registers.Add(register);
            }

            return registers;
        }

        public static bool IsCpfValid(string cpf) {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11) {
                return false;
            }

            if (cpf.Distinct().Count() == 1) {
                return false;
            }

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma1 = cpf
                .Take(9)
                .Select((digit, index) => int.Parse(digit.ToString()) * multiplicadores1[index])
                .Sum();

            int resto1 = soma1 % 11;
            int digitoVerificador1 = resto1 < 2 ? 0 : 11 - resto1;


            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma2 = cpf
                .Take(10)
                .Select((digit, index) => int.Parse(digit.ToString()) * multiplicadores2[index])
                .Sum();

            int resto2 = soma2 % 11;
            int digitoVerificador2 = resto2 < 2 ? 0 : 11 - resto2;


            return cpf.EndsWith(digitoVerificador1.ToString() + digitoVerificador2.ToString());
        }

        public static DateTime DateConverter(string dateString) {
            int year = int.Parse(dateString.Substring(0, 4));
            int month = int.Parse(dateString.Substring(4, 2));
            int day = int.Parse(dateString.Substring(6, 2));

            if (month > 12 || month < 0)
                return new DateTime();

            if (day < 1 || day > 31)
                return new DateTime();

            DateTime data = new DateTime(year, month, day, 0, 0, 0);
            DateTime realData = TimeZoneInfo.ConvertTimeToUtc(data);
            return realData;
            
        }
    }
}