using MVC_Q5.Models.Interface;
using MVC_Q5.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Q5.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAll()
        {
            String PATH = String.Format(@"{0}\Content\data.json", System.AppDomain.CurrentDomain.BaseDirectory);
            string json = System.IO.File.ReadAllText(PATH);
            string str = json.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
        public List<SelectListItem> GetLocaleList(List<String> defaultLocale)
        {
            List<SelectListItem> localeList = new List<SelectListItem>();
            localeList.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "0" });
            localeList.AddRange( defaultLocale.Select(s => new SelectListItem()
            {
                Text = s,
                Value = s,
            }).ToList() );
            return localeList;
        }
    }
}