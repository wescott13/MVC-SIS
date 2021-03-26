using MVC_SIS_Data;
using MVC_SIS_Models;
using MVC_SIS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentAddVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
                
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentAddVM viewModel)
        {
            viewModel.Student.Courses = new List<Course>();

            foreach (var id in viewModel.SelectedCourseIds)
                viewModel.Student.Courses.Add(CourseRepository.Get(id));

            viewModel.Student.Major = MajorRepository.Get(viewModel.Student.Major.MajorId);

            if (!ModelState.IsValid)
            {
                viewModel.SetMajorItems(MajorRepository.GetAll());
                viewModel.SetCourseItems(CourseRepository.GetAll());
                return View(viewModel);
            }

            StudentRepository.Add(viewModel.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            StudentEditVM viewmodel = new StudentEditVM();

            viewmodel.SetMajorItems(MajorRepository.GetAll());
            viewmodel.SetCourseItems(CourseRepository.GetAll());

            Student student = StudentRepository.Get(id);
            if (student.Courses != null)
            {
                foreach (Course course in student.Courses)
                {
                    viewmodel.SelectedCourseIds.Add(course.CourseId);
                }
            }

            viewmodel.Student = StudentRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentEditVM viewModel)
        {
            viewModel.Student.Major = MajorRepository.Get(viewModel.Student.Major.MajorId);

            viewModel.Student.Courses = new List<Course>();
            foreach (int id in viewModel.SelectedCourseIds)
            {
                viewModel.Student.Courses.Add(CourseRepository.Get(id));
            }

            if (!ModelState.IsValid)
            {
                viewModel.SetMajorItems(MajorRepository.GetAll());
                viewModel.SetCourseItems(CourseRepository.GetAll());
                return View(viewModel);
            }

            StudentRepository.Edit(viewModel.Student);
            StudentRepository.SaveAddress(viewModel.Student.StudentId, viewModel.Student.Address);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            StudentDeleteVM viewModel = new StudentDeleteVM();
            viewModel.currentStudent = StudentRepository.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteStudent(StudentDeleteVM viewModel)
        {
            StudentRepository.Delete(viewModel.currentStudent.StudentId);
            return RedirectToAction("List");
        }

    }
}