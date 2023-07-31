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

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

                register.Tipo = (TransactionType)Enum.Parse(typeof(TransactionType), line.Substring(0, 1));

                int year = int.Parse(line.Substring(1, 4));
                int month = int.Parse(line.Substring(5, 2));  
                int day = int.Parse(line.Substring(7, 2));
                if (month == 70)
                    month = 1;

                DateTime data = new DateTime(year, month, day, 0, 0, 0);
                DateTime realData = TimeZoneInfo.ConvertTimeToUtc(data, tz);

                register.Data = realData;
                register.Valor = double.Parse(line.Substring(9, 10)) / 100;
                register.Cpf = line.Substring(19, 11);
                register.Cartao = line.Substring(30, 12);
                register.DonoLoja = line.Substring(42, 14);
                register.NomeLoja = line.Substring(56, 18);

                registers.Add(register);
            }

            return registers;
        }

    }
}