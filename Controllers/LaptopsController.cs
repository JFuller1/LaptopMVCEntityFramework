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
    public class LaptopsController : Controller
    {
        private readonly LaptopDBContext _context;

        public LaptopsController(LaptopDBContext context)
        {
            _context = context;
        }

        // GET: Laptops
        public async Task<IActionResult> Index()
        {
            var laptopDBContext = _context.Laptops.Include(l => l.Brand);
            return View(await laptopDBContext.ToListAsync());
        }

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .Include(l => l.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,BrandId,Price,Year")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", laptop.BrandId);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", laptop.BrandId);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,BrandId,Price,Year")] Laptop laptop)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", laptop.BrandId);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .Include(l => l.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Laptops == null)
            {
                return Problem("Entity set 'LaptopDBContext.Laptops'  is null.");
            }
            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop != null)
            {
                _context.Laptops.Remove(laptop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
          return _context.Laptops.Any(e => e.Id == id);
        }

        public IActionResult CheapestLaptops()
        {
            List<Laptop> CheapestLaptops = _context.Laptops.OrderBy(laptop => laptop.Price).Take(3).ToList();

            foreach (var laptop in CheapestLaptops)
            {
                laptop.Brand = _context.Brands.First(brand => brand.Id == laptop.BrandId);
            }

            return View(CheapestLaptops);
        }

        public IActionResult CompareLaptops()
        {
            return View(new CompareLaptopsViewModel());
        }

        [HttpPost]
        public IActionResult CompareLaptops(CompareLaptopsViewModel compareLaptopsVm)
        {
            compareLaptopsVm.LaptopOne = _context.Laptops.First(laptop => laptop.Model == compareLaptopsVm.LaptopOneString);
            compareLaptopsVm.LaptopTwo = _context.Laptops.First(laptop => laptop.Model == compareLaptopsVm.LaptopTwoString);

            compareLaptopsVm.LaptopOne.Brand = _context.Brands.First(brand => brand.Id == compareLaptopsVm.LaptopOne.BrandId);
            compareLaptopsVm.LaptopTwo.Brand = _context.Brands.First(brand => brand.Id == compareLaptopsVm.LaptopTwo.BrandId);
            return View(compareLaptopsVm);
        }

        public IActionResult ExpensiveLaptops()
        {
            List<Laptop> ExpensiveLaptops = _context.Laptops.OrderByDescending(laptop => laptop.Price).Take(2).ToList();

            foreach (var laptop in ExpensiveLaptops)
            {
                laptop.Brand = _context.Brands.First(brand => brand.Id == laptop.BrandId);
            }

            return View(ExpensiveLaptops);
        }

        public IActionResult LaptopsInBudget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LaptopsInBudget(LaptopsInBudgetViewModel laptopsInBudgetVm)
        {
            //checks if the price is lower than or equal to the budget
            laptopsInBudgetVm.ValidLaptops = _context.Laptops.OrderByDescending(laptop => laptop.Price).Where(laptop => laptop.Price <= laptopsInBudgetVm.Budget).ToList();

            foreach(var laptop in laptopsInBudgetVm.ValidLaptops)
            {
                laptop.Brand = _context.Brands.First(brand => brand.Id == laptop.BrandId);
            }

            return View(laptopsInBudgetVm);
        }
    }
}
