using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HocVienController : Controller
    {
        private readonly MyDbcontext _context;

        public HocVienController(MyDbcontext context)
        {
            _context = context;
        }

        // GET: HocVien
        public async Task<IActionResult> Index()
        {
              return _context.HocViens != null ? 
                          View(await _context.HocViens.ToListAsync()) :
                          Problem("Entity set 'MyDbcontext.HocViens'  is null.");
        }

        // GET: HocVien/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.HocViens == null)
            {
                return NotFound();
            }

            var hocVien = await _context.HocViens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hocVien == null)
            {
                return NotFound();
            }

            return View(hocVien);
        }

        // GET: HocVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HocVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoTen,Tuoi,ChuyenNganh")] HocVien hocVien)
        {
            if (ModelState.IsValid)
            {
                hocVien.ID = Guid.NewGuid();
                _context.Add(hocVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hocVien);
        }

        // GET: HocVien/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.HocViens == null)
            {
                return NotFound();
            }

            var hocVien = await _context.HocViens.FindAsync(id);
            if (hocVien == null)
            {
                return NotFound();
            }
            var jsonData = JsonConvert.SerializeObject(hocVien);// ép kiểu dữ liệu sang kiểu json
            HttpContext.Session.SetString("edited", jsonData);
            return View(hocVien);
        }

        // POST: HocVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,HoTen,Tuoi,ChuyenNganh")] HocVien hocVien)
        {
            if (id != hocVien.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocVienExists(hocVien.ID))
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
            return View(hocVien);
        }
        public IActionResult RetriewEditData()
        {
            //lấy dữ liệu đã xóa đc lưu vào sessiomn

            var jsonData = HttpContext.Session.GetString("edited");
            if (jsonData != null)
            {
                var editHocvien = JsonConvert.DeserializeObject<HocVien>(jsonData);
                return View("RetriewEditData", editHocvien);
            }
            else
            {
                //nếu k tìm thấy dữ liệu lưu trong session
                return RedirectToAction("Index");
            }
        }
        public IActionResult RollBack()
        {
            if (HttpContext.Session.Keys.Contains("edited"))
            {
                var jsonData = HttpContext.Session.GetString("edited");
                //tạo đối jg có dữ liệu y hệt như dữ liệu cũ
                var editHocvien = JsonConvert.DeserializeObject<HocVien>(jsonData);
                _context.HocViens.Update(editHocvien);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Error");
            }


        }
        // GET: HocVien/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.HocViens == null)
            {
                return NotFound();
            }

            var hocVien = await _context.HocViens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hocVien == null)
            {
                return NotFound();
            }

            return View(hocVien);
        }

        // POST: HocVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.HocViens == null)
            {
                return Problem("Entity set 'MyDbcontext.HocViens'  is null.");
            }
            var hocVien = await _context.HocViens.FindAsync(id);
            if (hocVien != null)
            {
                _context.HocViens.Remove(hocVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocVienExists(Guid id)
        {
          return (_context.HocViens?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
