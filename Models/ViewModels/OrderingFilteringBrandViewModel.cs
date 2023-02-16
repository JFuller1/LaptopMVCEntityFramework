using LaptopMVCEntityFramework.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LaptopMVCEntityFramework.Models.ViewModels
{
    public class OrderingFilteringBrandViewModel
    {
        LaptopDBContext context = new LaptopDBContext();

        public List<SelectListItem> BrandOptions = new();

        [Display(Name = "Brand")]
        public string BrandName{ get; set; }

        //options year (ascending/descending) & price (ascending/descending)
        public List<SelectListItem> Ordering = new();

        [Display(Name = "Order")]
        public string OrderType { get; set; }

        //options below/above year/price
        public List<SelectListItem> Filtering = new();

        [Display(Name = "Filter")]
        public string FilterType { get; set; }

        public int? FilterInput { get; set; }

        public List<Laptop> Laptops = new();

        public string NoResultMsg { get; set; }

        public OrderingFilteringBrandViewModel()
        {
            BrandOptions.Add(new SelectListItem { Value = "allBrands", Text = "All Brands" });
            foreach (Brand brand in context.Brands)
            {
                BrandOptions.Add(new SelectListItem { Value = brand.Name, Text = brand.Name });
            }

            Ordering = new()
            {
                new SelectListItem { Value = "yearAscending", Text = "Year ascending" },
                new SelectListItem { Value = "yearDescending", Text = "Year descending" },
                new SelectListItem { Value = "priceAscending", Text = "Price ascending" },
                new SelectListItem { Value = "priceDescending", Text = "Price descending" }
            };

            Filtering = new()
            {
                new SelectListItem { Value = "aboveYear", Text = "Above Year" },
                new SelectListItem { Value = "belowYear", Text = "Below Year" },
                new SelectListItem { Value = "abovePrice", Text = "Above Price" },
                new SelectListItem { Value = "belowPrice", Text = "Below Price" }
            };
        }
    }
}
