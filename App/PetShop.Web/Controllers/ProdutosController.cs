using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class ProdutosController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}