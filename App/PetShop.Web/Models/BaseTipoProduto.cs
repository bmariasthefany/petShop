using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class BaseTipoProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Valor { get; set; }
        public bool Ativo { get; set; }
    }
}