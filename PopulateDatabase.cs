using LaptopMVCEntityFramework.Models;
using LaptopMVCEntityFramework.Data;
namespace LaptopMVCEntityFramework
{
    public class PopulateDatabase
    {
        public static void Populate()
        {
            using LaptopDBContext context = new LaptopDBContext();

            if(context.Brands == null) 
            {
                // BRANDS

                Brand gigaCorp = new Brand()
                {
                    Name = "GigaCorp"
                };
                context.Brands.Add(gigaCorp);

                Brand techNova = new Brand()
                {
                    Name = "TechNova"
                };
                context.Brands.Add(techNova);

                Brand quantumSoft = new Brand()
                {
                    Name = "QuantumSoft"
                };
                context.Brands.Add(quantumSoft);

                Brand innoVatek = new Brand()
                {
                    Name = "InnoVatek"
                };
                context.Brands.Add(innoVatek);

                Brand nexTech = new Brand()
                {
                    Name = "NexTech"
                };
                context.Brands.Add(nexTech);

                // LAPTOPS

                Laptop GigaBook = new Laptop()
                {
                    Model = "GigaBook",
                    Year = 2022,
                    Brand = gigaCorp,
                    BrandId = gigaCorp.Id,
                    Price = 1099.99M
                };
                context.Laptops.Add(GigaBook);

                Laptop NovaBook = new Laptop()
                {
                    Model = "NovaBook Pro",
                    Year = 2022,
                    Brand = techNova,
                    BrandId = techNova.Id,
                    Price = 1599.99M
                };
                context.Laptops.Add(NovaBook);

                Laptop QuantumNote = new Laptop()
                {
                    Model = "QuantumNote Ultra",
                    Year = 2022,
                    Brand = quantumSoft,
                    BrandId = quantumSoft.Id,
                    Price = 1299.99M
                };
                context.Laptops.Add(QuantumNote);

                Laptop InnoBook = new Laptop()
                {
                    Model = "InnoBook Pro 15",
                    Year = 2022,
                    Brand = innoVatek,
                    BrandId = innoVatek.Id,
                    Price = 1799.99M
                };
                context.Laptops.Add(InnoBook);

                Laptop NexBook = new Laptop()
                {
                    Model = "NexBook Air",
                    Year = 2022,
                    Brand = nexTech,
                    BrandId = nexTech.Id,
                    Price = 1199.99M
                };
                context.Laptops.Add(NexBook);

                context.SaveChanges();
            }
        }
    }
}
