using MVC_Q5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_Q5.Models.Interface
{
    interface IProductRepository
    {
        List<Product> GetAll();
        List<SelectListItem> GetLocaleList(List<String>  defaultLocale);
    }
}
