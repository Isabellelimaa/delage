using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prova.Models
{
    public class Entidade
    {
        [Key]
        public int idEntidade { get; set; }
        public string fantasia { get; set; }

        public int? idEndereco { get; set; }
        [ForeignKey("idEndereco")]
        public virtual Endereco Endereco { get; set; }
    }

    public class Produto
    {
        public Produto()
        {}

        public Produto(int idImportacao, int codProduto, string codBarrasProduto, string descProduto) 
        {
            this.idImportacao = idImportacao;
            this.codProduto = codProduto;
            this.codBarrasProduto = codBarrasProduto;
            this.descProduto = descProduto;
        }
        
        [Key]
        public int idProduto { get; set; }
        [ForeignKey("Importacao")]
        public int? idImportacao { get; set; }

        public int codProduto { get; set; }
        [Required]
        public string codBarrasProduto { get; set; }
        [Required]
        public string descProduto { get; set; }
        public virtual Importacao Importacao { get; set; }

    }

    public class Importacao
    {
        public Importacao()
        {
            this.dataRgst = DateTime.Now;
        }

        [Key]
        public int idImportacao { get; set; }
        public DateTime dataRgst { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}