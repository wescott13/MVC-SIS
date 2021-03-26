using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Models
{
    public class StudentEditVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public StudentEditVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            StateItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
            Student = new Student();
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Student == null || Student.FirstName == "" || Student.FirstName == null)
            {
                errors.Add(new ValidationResult("Please enter a First Name",
                    new[] { "Student.FirstName" }));
            }
            if (Student.LastName == "" || Student.LastName == null)
            {
                errors.Add(new ValidationResult("Please enter a Last Name",
                    new[] { "Student.LastName" }));
            }      
            if (Student.GPA < 0 || Student.GPA > 4)
            {
                errors.Add(new ValidationResult("Please enter a valid GPA between 0 and 4.0",
                    new[] { "Student.GPA" }));
            }
            if (Student.Address.Street1 == "" || Student.Address.Street1 == null)
            {
                errors.Add(new ValidationResult("Please enter a Street Address",
                    new[] { "Student.Address.Street1" }));

            }
            if (Student.Address.City == "" || Student.Address.City == null)
            {
                errors.Add(new ValidationResult("Please enter a City",
                    new[] { "Student.Address.City" }));
            }
            if (Student.Address.State.StateAbbreviation == "" || Student.Address.State.StateAbbreviation == null)
            {
                errors.Add(new ValidationResult("Please enter a State Abbreviation",
                    new[] { "Student.Address.State.StateAbbreviation" }));
            }
            if (Student.Address.PostalCode == "" || Student.Address.PostalCode == null)
            {
                errors.Add(new ValidationResult("Please enter a Postal Code",
                    new[] { "Student.Address.PostalCode" }));
            }

            return errors;
        }
    }
}