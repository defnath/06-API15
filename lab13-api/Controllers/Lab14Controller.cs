using lab13_api.Models;
using lab13_api.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab13_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Lab14Controller : ControllerBase
    {
        private readonly SchoolContext _context;

        public Lab14Controller(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "InsertCourse")]

        public void PostCourseExample1(CourseRequestExample1 request)
        {
            _context.Courses.Add(new Course
            {
                Name = request.Name,
                Credit = request.Credit
            });
            _context.SaveChanges();
        }

        [HttpPut(Name = "Delete Course")]
        public void DeleteCourseExample2(CourseRequestExample2 request)
        {
            var res = _context.Courses.Find(request.CourseID);

            res.IsActive = false;

            _context.Entry(res).State = EntityState.Modified;
            _context.SaveChanges();
        }



        [HttpPost(Name = "InsertGrade")]
        public void PostGradeExample3(GradeRequestExample3 request)
        {
            _context.Grades.Add(new Grade
            {
                Name = request.Name,
                Description = request.Description
            });
            _context.SaveChanges();

        }

        [HttpPut(Name = "Delete Grade")]
        public void DeleteGradeExample4(GradeRequestExample4 request)
        {
            var res = _context.Grades.Find(request.GradeID);

            res.IsActive = false;
            _context.Entry(res).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpPost(Name = "InsertStudent")]
        public void PostStudentExample5(StudentRequestInsertExample5 request)
        {
            var student = new Student
            {
                GradeID = request.GradeID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        [HttpPut(Name = "Update Contact")]

        public void UpdateStudentExample6(StudentRequestInsertExample6 request)
        {
            var student = _context.Students.Find(request.StudentID);

            student.Phone = request.Phone;
            student.Email = request.Email;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpPut(Name = "Update Student")]
        public void UpdateStudentExample7(StudentRequestUpdateExample7 request)
        {
            var student = _context.Students.Find(request.StudentID);

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpPost(Name = "InsertStudentByGrade")]
        public void PostStudentExample8(StudentRequestUpdateExample8 request)
        {
            var student = request.Students.Select(x => new Student
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                Email = x.Email,
                GradeID = request.GradeID
            }).ToList();
            _context.Students.AddRange(student);
            _context.SaveChanges();
        }

        [HttpPut(Name = "DeleteCoursesList")]

        public void DeleteCourseExample9(StudentRequestUpdateExample9 request)
        {

            var course = request.Courses.Select(x => x.CourseID).ToList();

            var coursesToUpdate = _context.Courses
            .Where(c => course.Contains(c.CourseID))
            .ToList();

            coursesToUpdate.ForEach(c => c.IsActive = false);

            _context.Courses.UpdateRange(coursesToUpdate);
            _context.SaveChanges();
        }

        [HttpPut(Name = "InsertEnrollmentList")]

        public void InsertEnrollmentExample10(EnrollementRequestInsertExample10 request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentID == request.StudentID);
            // var student = _context.Students.Find(request.StudentID);

            var courseIDs = request.Courses.Select(c => c.CourseID).ToList();

            var existingCourses = _context.Courses.Where(c => courseIDs.Contains(c.CourseID)).ToList();

            var enrollments = existingCourses.Select(course => new Enrollment
            {
                Student = student,
                Course = course,
                date = DateTime.Now

            }).ToList();

            _context.Enrollments.AddRange(enrollments);
            _context.SaveChanges();
        }
    }
}
