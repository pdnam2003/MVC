using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;
using CodeFirst.data;

namespace CodeFirst.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                          .Include(s => s.Courses)
                          .ToListAsync();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            // ⚠️ ValueField must match the Course model's Id property
            ViewBag.CourseList = new MultiSelectList(_context.Courses, "Id", "Title");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age")] Student student, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                if (selectedCourses != null && selectedCourses.Length > 0)
                {
                    student.Courses = new List<Course>();
                    foreach (var courseId in selectedCourses)
                    {
                        var course = await _context.Courses.FindAsync(courseId);
                        if (course != null)
                        {
                            student.Courses.Add(course);
                        }
                    }
                }

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseList = new MultiSelectList(_context.Courses, "Id", "Title");
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                            .Include(s => s.Courses)
                            .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            // Đưa danh sách khóa học để chọn lại
            ViewBag.CourseList = new MultiSelectList(_context.Courses, "Id", "Title",
                student.Courses.Select(c => c.Id));

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] Student student, int[] selectedCourses)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await _context.Students
                        .Include(s => s.Courses)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (existingStudent == null) return NotFound();

                    existingStudent.Name = student.Name;
                    existingStudent.Age = student.Age;

                    // Update Courses
                    existingStudent.Courses.Clear();
                    if (selectedCourses != null)
                    {
                        foreach (var courseId in selectedCourses)
                        {
                            var course = await _context.Courses.FindAsync(courseId);
                            if (course != null)
                            {
                                existingStudent.Courses.Add(course);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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

            ViewBag.CourseList = new MultiSelectList(_context.Courses, "Id", "Title");
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
