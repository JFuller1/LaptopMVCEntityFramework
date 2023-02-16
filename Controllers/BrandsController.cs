using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopMVCEntityFramework.Data;
using LaptopMVCEntityFramework.Models;
using LaptopMVCEntityFramework.Models.ViewModels;

namespace LaptopMVCEntityFramework.Controllers
{
    public class BrandsController : Controller
    {
        private readonly LaptopDBContext _context;

        public BrandsController(LaptopDBContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
              return View(await _context.Brands.ToListAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'LaptopDBContext.Brands'  is null.");
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
          return _context.Brands.Any(e => e.Id == id);
        }

        public IActionResult BrandFilteringOrdering()
        {
            return View(new OrderingFilteringBrandViewModel());
        }

        [HttpPost]
        public IActionResult BrandFilteringOrdering(OrderingFilteringBrandViewModel orderingFilteringBrandVm)
        {
            //checks if it needs to display every brand or only a certain oen
            if (orderingFilteringBrandVm.BrandName == "allBrands")
            {
                orderingFilteringBrandVm.Laptops = _context.Laptops.ToList();
            }
            else
            {
                orderingFilteringBrandVm.Laptops = _context.Laptops.Where(laptop => laptop.Brand.Name == orderingFilteringBrandVm.BrandName).ToList();
            }

            foreach(var laptop in orderingFilteringBrandVm.Laptops)
            {
                laptop.Brand = _context.Brands.First(brand => brand.Id == laptop.BrandId);
            }

            // looks for order type
            switch (orderingFilteringBrandVm.OrderType)
            {
                case "yearAscending":
                    orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.OrderBy(laptop => laptop.Year).ToList();
                    break;

                case "yearDescending":
                    orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.OrderByDescending(laptop => laptop.Year).ToList();
                    break;

                case "priceAscending":
                    orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.OrderBy(laptop => laptop.Price).ToList();
                    break;

                case "priceDescending":
                    orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.OrderByDescending(laptop => laptop.Price).ToList();
                    break;

                default:
                    break;
            }

            // checks if filter field is filled, if so looks for filter type
            if (orderingFilteringBrandVm.FilterInput != null)
            {
                switch (orderingFilteringBrandVm.FilterType)
                {
                    case "aboveYear":
                        orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.Where(laptop => laptop.Year > orderingFilteringBrandVm.FilterInput).ToList();
                        break;

                    case "belowYear":
                        orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.Where(laptop => laptop.Year < orderingFilteringBrandVm.FilterInput).ToList();
                        break;

                    case "abovePrice":
                        orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.Where(laptop => laptop.Price > orderingFilteringBrandVm.FilterInput).ToList();
                        break;

                    case "belowPrice":
                        orderingFilteringBrandVm.Laptops = orderingFilteringBrandVm.Laptops.Where(laptop => laptop.Price < orderingFilteringBrandVm.FilterInput).ToList();
                        break;

                    default:

                        break;
                }

            }

            //if there is no possible laptops to show
            if (orderingFilteringBrandVm.NoResultMsg == null)
            {
                orderingFilteringBrandVm.NoResultMsg = "No Laptops Match Your Search";
            }

            return View(orderingFilteringBrandVm);
        }

        public IActionResult BrandsLaptopsDisplay()
        {
            return View(new BrandLaptopsDisplayViewModel());
        }
    }
}
