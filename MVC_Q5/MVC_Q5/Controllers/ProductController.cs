using MVC_Q5.Models.Interface;
using MVC_Q5.Models.Repository;
using MVC_Q5.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Q5.Controllers
{
    public class ProductController : Controller
    {
        private int _pageSize = 5;
        private IProductRepository _productRepository;
        private ProductViewModel _product;
        public List<String> DefaultLocale = new List<string>() { "US", "DE", "CA", "ES", "FR", "JP" };
        public ProductController()
        {
            _productRepository = new ProductRepository();
            _product = new ProductViewModel();

        }
        // GET: Product
        public ActionResult Index(int page = 1)
        {
            ViewBag.DefaultLocale = _productRepository.GetLocaleList(DefaultLocale);
            List<Product> productList = _productRepository.GetAll();
            _product.ProductList = productList.ToPagedList(page, _pageSize);
            return View(_product);
        }
        [HttpPost]
        public ActionResult Index(ProductViewModel data, int page = 1)
        {
            ViewBag.DefaultLocale = _productRepository.GetLocaleList(DefaultLocale);
            _product.InputModel = data.InputModel;
            List<Product> productList = _productRepository.GetAll();
            List<Product> result = DataFilter(productList, data.InputModel);
            _product.ProductList = result.ToPagedList(page, _pageSize);
            return View(_product);
        }
        
        public List<Product> DataFilter(List<Product> dataList, InputModel inputModel)
        {
            List<Product> result = dataList;
            if (inputModel.Locale != "0")
            {
                result = result.Where(x => x.Locale == inputModel.Locale).ToList();
            }
            if (!String.IsNullOrEmpty(inputModel.Product_Name))
            {
                result = result.Where(x => x.Product_Name.ToLower().Contains(inputModel.Product_Name.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(inputModel.Price))
            {
                string[] price = inputModel.Price.Split('~');
                double minPrice, maxPrice;
                if (price.Length == 1)
                {
                    double.TryParse(price[0], out minPrice);
                    result = result.Where(x => x.Price > minPrice).ToList();
                } else
                {
                    double.TryParse(price[0], out minPrice);
                    double.TryParse(price[1], out maxPrice);
                    result = result.Where(x => x.Price > minPrice && x.Price < maxPrice).ToList();
                }
            }
            return result;
        }
    }
}