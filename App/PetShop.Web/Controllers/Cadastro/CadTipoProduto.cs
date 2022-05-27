using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class CadTipoProdutoController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;
        
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20}, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = TipoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = TipoProdutoModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult TipoProdutoPagina(int pagina, int tamPag)
        {
            var lista = TipoProdutoModel.RecuperarLista(pagina, tamPag);
            return Json(lista);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarTipoProduto(short CodTipoProduto)
        {
            return Json(TipoProdutoModel.RecuperarPeloId(CodTipoProduto));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirTipoProduto(short CodTipoProduto)
        {
            return Json(TipoProdutoModel.ExcluirPeloId(CodTipoProduto));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarTipoProduto(TipoProdutoModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var CodTipoProdutoSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var CodTipoProduto = model.Salvar();
                    if (CodTipoProduto > 0)
                    {
                        CodTipoProdutoSalvo = CodTipoProduto.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, CodTipoProdutoSalvo = CodTipoProdutoSalvo });
        }
        
    }
}