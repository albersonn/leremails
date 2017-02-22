using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerEmail.Models
{
    [Table("TB_EMAIL")]
    class Email
    {
        [Key]
        public int ID { get; set; }

        [Column("SUBJECT")]
        public string Subject { get; set; }

        [Column("TO")]
        public string To { get; set; }

        [Column("CATEGORIES")]
        public string Categories { get; set; }

        [Column("CC")]
        public string Cc { get; set; }

        [Column("DATA")] //103
        public string Data { get; set; }

        [Column("HORA")] //108
        public string Hora { get; set; }

        [Column("FROM")]
        public string From { get; set; }

        [Column("BODY")]
        public string Body { get; set; }

        public override string ToString()
        {

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(ID), ID) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Subject).ToUpper(), Subject) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(To).ToUpper(), To) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Categories).ToUpper(), Categories) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Cc).ToUpper(), Cc) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Data).ToUpper(), Data) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Hora).ToUpper(), Hora) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(From).ToUpper(), From) + "}");
            stringBuilder.AppendLine("{" + ChaveValor(nameof(Body).ToUpper(), Comeco(Body, 10)) + "}");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        private static string ChaveValor(string chave, object valor) => string.Format("{0}: {1}", chave, valor);

        private static string Comeco(string str, int tamanho)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (str.Length <= tamanho)
                return str;
            return str.Substring(0, tamanho) + "...";
        }
    }
}
