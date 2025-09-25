using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMarksMVC.Data;
using StudentMarksMVC.Models;
using StudentMarksMVC.ViewModels;
using System.Diagnostics;


namespace StudentMarksMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // 1. Lấy dữ liệu từ DB (gồm cả Scores + Subjects)
            var students = await _context.Students
                .Include(s => s.Scores)
                    .ThenInclude(sc => sc.Subject)
                .OrderBy(s => s.StudentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 2. Map sang ViewModel (LINQ trong memory)
            var data = students.Select(s => new StudentWithScoresViewModel
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Grade = s.Grade,
                Class = s.Class,
                Math = s.Scores.FirstOrDefault(x => x.Subject.SubjectName == "Math")?.ScoreValue ?? 0,
                Science = s.Scores.FirstOrDefault(x => x.Subject.SubjectName == "Science")?.ScoreValue ?? 0,
                English = s.Scores.FirstOrDefault(x => x.Subject.SubjectName == "English")?.ScoreValue ?? 0,
                History = s.Scores.FirstOrDefault(x => x.Subject.SubjectName == "History")?.ScoreValue ?? 0,
                Art = s.Scores.FirstOrDefault(x => x.Subject.SubjectName == "Art")?.ScoreValue ?? 0
            }).ToList();

            // 3. Tính toán thống kê (chỉ trên data đã load)
            int totalRecords = await _context.Students.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            ViewBag.TotalStudents = totalRecords;
            ViewBag.AverageScore = data.Any() ? data.Average(s => s.Average) : 0;
            ViewBag.TopStudent = data.Any() ? data.Max(s => s.Average) : 0;
            ViewBag.LowScores = data.Count(s => s.Average < 80);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(data);
          
        }

       
        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật học sinh thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Có lỗi khi cập nhật học sinh!";
                }
            }
            return View(student);
        }



        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
