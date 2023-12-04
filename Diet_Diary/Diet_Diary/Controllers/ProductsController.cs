using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diet_Diary.Services;
using Diet_Diary.Models;

namespace Diet_Diary.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }


        // GET: ProductsController
        public ActionResult Index()
        {
            var products = _productsService.Get();

            return View(products);
        }
    }
}
