using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MyShopDbContext _context;

        public ProductsController(MyShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var myShopDbContext = _context.Products.Include(p => p.Category).Include(p => p.Color).Include(p => p.Gender);
            return View(await myShopDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
           
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,ImageUri,ColorId,CategoryId,GenderId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", product.ColorId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", product.GenderId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            product.AvalableSizes = _context.ProductSizes.Where(x => x.ProductId == product.Id).ToList();
            if (product == null)
            {
                return NotFound();
            }
            //product.AvalableSizes = _context.Sizes.Where(ps => ps.ProductId == product.Id).ToList();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", product.ColorId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", product.GenderId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name");
            // return View(new ProductSize { Product = product, ProductSizes = _context.Sizes.ToList(), SizeOfProduct = _context.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id).Select(ps => ps.Size).ToList() });
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Description,Price,DiscountPrice,ImageUri,ColorId,CategoryId,GenderId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", product.ColorId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", product.GenderId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpPost]
        public void CreateSizes(int ProductId, string SizeId)
        {
            int sz = 0;
            Int32.TryParse(SizeId, out sz);

            var product = _context.Products.Include(p => p.AvalableSizes).FirstOrDefault(p => p.Id == ProductId);
            //product.AvalableSizes = _context.ProductSizes.Where(pr => pr.ProductId == ProductId).ToList();
            var size = _context.Sizes.FirstOrDefault(s => s.Id == sz);

            //if (_context.ProductSizes.Any(pr => pr.ProductId == ProductId & pr.SizeId == sz))
            //{
            //    // do nothing
            //    NotFound();
            //    return;
            //}

            var ps = new ProductSizes
            {
                ProductId = product.Id,
                SizeId = size.Id
                
            };

            product.AvalableSizes.Add(ps);
                _context.SaveChanges();

            //if (product.AvalableSizes == null)
            //    product.AvalableSizes = new List<Size>();
            //product.AvalableSizes.Add(ps);
            //    //_context.ProductSizes.Add(ps);
           
        }
    }
}
