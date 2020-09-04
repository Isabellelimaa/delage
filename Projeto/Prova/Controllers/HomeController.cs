using Prova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Prova.Controllers
{
    public class HomeController : DelageController
    {
        private Entities context = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Produtos(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodProdSortParm = sortOrder == "cod_produto" ? "cod_produto_desc" : "cod_produto";
            ViewBag.CodBarrasSortParm = sortOrder == "cod_barras" ? "cod_barras_desc" : "cod_barras";
            ViewBag.DescProdSortParm = String.IsNullOrEmpty(sortOrder) ? "desc_produto_desc" : "";
            if (page == null)
            {
                page = 1;
            }
            
            var produtos = await context.Produto.OrderBy(ob => ob.codProduto).ToListAsync();

            switch (sortOrder)
            {
                case "desc_produto_desc":
                    produtos = produtos.OrderByDescending(ob => ob.descProduto).ToList();
                    break;
                case "cod_barras":
                    produtos = produtos.OrderBy(s => s.codBarrasProduto).ToList();
                    break;
                case "cod_barras_desc":
                    produtos = produtos.OrderByDescending(s => s.codBarrasProduto).ToList();
                    break;
                case "cod_produto":
                    produtos = produtos.OrderBy(s => s.codProduto).ToList();
                    break;
                case "cod_produto_desc":
                    produtos = produtos.OrderByDescending(s => s.codProduto).ToList();
                    break;
                default:
                    produtos = produtos.OrderBy(s => s.descProduto).ToList();
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(produtos.ToPagedList(pageNumber, pageSize));
            //return View(produtos);
        }

        [HttpPost]
        public async Task<ActionResult> EnviarProdutos(HttpPostedFileBase postedFile)
        {
            var importacao = new Importacao();
            context.Importacao.Add(importacao);
            await context.SaveChangesAsync();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(postedFile.InputStream))
            {
                while (!reader.EndOfStream)
                {
                    var result = reader.ReadLine();
                    var produtoSplited = result.Split(';');
                    var produto = new Produto(importacao.idImportacao, Convert.ToInt32(produtoSplited[0]), produtoSplited[1], produtoSplited[2]);
                    context.Produto.Add(produto);
                }
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}