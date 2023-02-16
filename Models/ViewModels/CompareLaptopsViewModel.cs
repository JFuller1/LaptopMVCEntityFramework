using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using LaptopMVCEntityFramework.Data;

namespace LaptopMVCEntityFramework.Models.ViewModels
{
    public class CompareLaptopsViewModel
    {
        LaptopDBContext context = new LaptopDBContext();

        public List<SelectListItem> Laptops = new();
        public Laptop LaptopOne;
        public Laptop LaptopTwo;

        [Display(Name = "Laptop Model One")]
        public string LaptopOneString { get; set; }

        [Display(Name = "Laptop Model Two")]
        public string LaptopTwoString { get; set; }

        //used for generating the drop down
        public CompareLaptopsViewModel()
        {
            foreach(Laptop laptop in context.Laptops)
            {
                Laptops.Add(new SelectListItem { Value = laptop.Model, Text = laptop.Model });
            }
        }
    }
}
