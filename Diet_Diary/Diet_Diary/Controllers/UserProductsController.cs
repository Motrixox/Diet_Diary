using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Diet_Diary.Models;
using Diet_Diary.Services;

namespace Diet_Diary.Controllers
{
    public class UserProductsController : Controller
    {
        private readonly ProductsService _productsService;
        private readonly UserProductsService _userProductsService;

        public UserProductsController(ProductsService productsService, UserProductsService userProductsService)
        {
            _productsService = productsService;
            _userProductsService = userProductsService;
        }

        public ActionResult Index()
        {
            var categories = _productsService.GetCategories();

            return View(categories);
        }

        public ActionResult Category(string category)
        {
            var products = _productsService.GetByCategory(category);

            return View(products);
        }

        [HttpPost]
        public ActionResult AddProduct(string productName, string inputWeight, string username)
        {
            UserProduct product = new UserProduct();
            //int parsedWeight = 0;
            product.Name = productName;
            product.Username = username;
            if (Int32.TryParse(inputWeight, out int parsedWeight))
            {
                product.Weight = parsedWeight;
            }
            else
            {
                throw new FormatException("Field cannot be empty!"); // replace with validation
            }
            //product.Amount = Int32.Parse(amount);


            _userProductsService.Create(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
