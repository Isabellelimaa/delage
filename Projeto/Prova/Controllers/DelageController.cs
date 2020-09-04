using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prova.Controllers
{
    public class DelageController : Controller
    {
     
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //setando a entidade em uma viewbag para usar em todas as telas
            Entities context = new Entities();
            ViewBag.Entidade = context.Entidade.FirstOrDefault();
            ViewBag.Contador = context.Produto.Count();
        }
    }
}