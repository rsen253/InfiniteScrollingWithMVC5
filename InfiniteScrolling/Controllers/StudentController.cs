using InfiniteScrolling.DataAccessLayer;
using InfiniteScrolling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfiniteScrolling.Controllers
{
    public class StudentController : Controller
    {
        public const int rowsPerPage = 15;
        private DataBaseContext db = new DataBaseContext();
        // GET: Student
        public StudentController()
        {
            ViewBag.RecordsPerPage = rowsPerPage;
        }
        public ActionResult Index()
        {
            //return View(db.Student.OrderBy(s => s.Name).ToList());
            return RedirectToAction("GetStudents");
        }

        public ActionResult GetStudents(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;
            if (Request.IsAjaxRequest())
            {
                var students = GetRowsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = (students.Any()) && ((pageNum.Value * rowsPerPage) >= students.Last().Key);
                System.Threading.Thread.Sleep(2000);
                return PartialView("_StudentRow", students);
            }
            else
            {
                LoadAllStudentsToSession();
                ViewBag.Students = GetRowsForPage(pageNum.Value);
                return View("Index");
            }
        }

        public void LoadAllStudentsToSession()
        {
            //var studentRepo = new StudentRepo();
            var students = db.Student.ToList();
            int custIndex = 1;
            Session["Students"] = students.ToDictionary(x => custIndex++, x => x);
            ViewBag.TotalNumberStudents = students.Count();
        }

        public Dictionary<int, Student> GetRowsForPage(int pageNum)
        {
            Dictionary<int, Student> students = (Session["Students"] as Dictionary<int, Student>);

            int from = (pageNum * rowsPerPage);
            int to = from + rowsPerPage;

            return students
                .Where(x => x.Key > from && x.Key <= to)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}