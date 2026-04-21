using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;

namespace QuanLySinhVien.Controllers
{
    public class CourseClassesController : Controller
    {
        private readonly AppDbContext _context;

        public CourseClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CourseClasses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CourseClasses.Include(c => c.Course).Include(c => c.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CourseClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // GET: CourseClasses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }

        // POST: CourseClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,CourseId,TeacherId")] CourseClass courseClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseClass.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", courseClass.TeacherId);
            return View(courseClass);
        }

        // GET: CourseClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses.FindAsync(id);
            if (courseClass == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseClass.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", courseClass.TeacherId);
            return View(courseClass);
        }

        // POST: CourseClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,CourseId,TeacherId")] CourseClass courseClass)
        {
            if (id != courseClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseClassExists(courseClass.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseClass.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", courseClass.TeacherId);
            return View(courseClass);
        }

        // GET: CourseClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // POST: CourseClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseClass = await _context.CourseClasses.FindAsync(id);
            if (courseClass != null)
            {
                _context.CourseClasses.Remove(courseClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseClassExists(int id)
        {
            return _context.CourseClasses.Any(e => e.Id == id);
        }
    }
}
