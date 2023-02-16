using LaptopMVCEntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;


namespace LaptopMVCEntityFramework.Models.ViewModels
{
    public class BrandLaptopsDisplayViewModel
    {
        LaptopDBContext context = new LaptopDBContext();

        public IEnumerable<IGrouping<string, Laptop>> LaptopsByBrand { get; set; }
        public List<Brand> BrandsWithNoLaptops { get; set; }


        public BrandLaptopsDisplayViewModel()
        {
            LaptopsByBrand = context.Laptops.GroupBy(laptop => laptop.Brand.Name);

            List<string> brandsWithLaptop = new(); 

            //creates a list of all brands that have laptops
            foreach(var group in LaptopsByBrand)
            {
                brandsWithLaptop.Add(group.Key);
            }

            //creates a list of all the brands that arent present in the other grouping because they dont have any laptops associated to them
            BrandsWithNoLaptops = context.Brands.Where(brand => !brandsWithLaptop.Contains(brand.Name)).ToList();
        }
    }
}
