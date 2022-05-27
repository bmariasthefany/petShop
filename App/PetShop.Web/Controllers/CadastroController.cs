using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class CadastroController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        #region Usuários

        private const string _senhaPadrao = "{@127;@188}";

        [Authorize]
        public ActionResult Usuario()
        {
            ViewBag.SenhaPadrao = _senhaPadrao;
            return View(UsuarioModel.RecuperarLista());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(short Id)
        {
            return Json(UsuarioModel.RecuperarPeloId(Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(short Id)
        {
            return Json(UsuarioModel.ExcluirPeloId(Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var IdSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    if (model.Senha == _senhaPadrao)
                    {
                        model.Senha = "";
                    }

                    var Id = model.Salvar();
                    if (Id > 0)
                    {
                        IdSalvo = Id.ToString();
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

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = IdSalvo });
        }

        #endregion

        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }

        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        
    }
}