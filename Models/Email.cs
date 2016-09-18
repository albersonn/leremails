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
            return $@"{{
    {{ ID: {ID} }},
    {{ SUBJECT: {Subject} }},
    {{ TO: {To} }},
    {{ CATEGORIES: {Categories} }},
    {{ CC: {Cc} }},
    {{ DATA: {Data} }},
    {{ HORA: {Hora} }},
    {{ FROM: {From} }},
    {{ BODY: {Comeco(Body, 10)}}}
}}";
        }

        private string Comeco(string str, int tamanho)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (str.Length <= tamanho)
                return str;
            return str.Substring(0, tamanho) + "...";
        }
    }
}
