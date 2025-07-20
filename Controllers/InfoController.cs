using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudApp.Data;
using CrudApp.Models;
using System.Globalization;
using X.PagedList;
using X.PagedList.Extensions;



namespace CrudApp.Controllers
{
    public class InfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Get countries
        private List<SelectListItem> GetAllCountries()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var countryList = new HashSet<string>();

            foreach (var culture in cultures)
            {
                try
                {
                    var region = new RegionInfo(culture.LCID);
                    countryList.Add(region.EnglishName);
                }
                catch
                {

                }
            }

            return countryList
                .OrderBy(c => c)
                .Select(c => new SelectListItem { Value = c, Text = c })
                .ToList();
        }

        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var infos = from i in _context.Infos
                        select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                infos = infos.Where(i =>
                    i.Name.Contains(searchString) ||
                    i.Email.Contains(searchString) ||
                    i.Phone.Contains(searchString) ||
                    i.Country.Contains(searchString)
                );
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 7;
            int pageNumber = page ?? 1;

            var list = await infos.OrderBy(i => i.Id).ToListAsync();
            var pagedList = list.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }


        // GET: Info/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var info = await _context.Infos.FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
                return NotFound();

            return View(info);
        }

        // GET: Info/Create
        public IActionResult Create()
        {
            ViewBag.Countries = GetAllCountries(); // إضافة الدول للـ ViewBag
            return View();
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,BirthDate,Country,IsSubscribed")] Info info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Countries = GetAllCountries(); // لو حصل خطأ، نرجع الدول تاني
            return View(info);
        }

        // GET: Info/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var info = await _context.Infos.FindAsync(id);
            if (info == null)
                return NotFound();

            ViewBag.Countries = GetAllCountries(); // الدول في صفحة التعديل
            return View(info);
        }

        // POST: Info/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,BirthDate,Country,IsSubscribed")] Info info)
        {
            if (id != info.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Countries = GetAllCountries(); // إعادة الدول في حالة فشل التعديل
            return View(info);
        }

        // GET: Info/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var info = await _context.Infos.FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
                return NotFound();

            return View(info);
        }

        // POST: Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var info = await _context.Infos.FindAsync(id);
            if (info != null)
                _context.Infos.Remove(info);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
            return _context.Infos.Any(e => e.Id == id);
        }
    }
}
