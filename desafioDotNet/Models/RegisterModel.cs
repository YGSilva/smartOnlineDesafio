using desafioDotNet.Enums;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace desafioDotNet.Models {

    public class RegisterModel {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public TransactionType Tipo { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public String Cpf { get; set; }
        public String Cartao { get; set; }
        public String DonoLoja { get; set; }
        public String NomeLoja { get; set; }
    }
}
