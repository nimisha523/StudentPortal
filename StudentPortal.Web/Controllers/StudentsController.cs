using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models.Entities;
using StudentPortal.Web.Models.ViewModels;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Create(AddStudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                Phone = addStudentViewModel.Phone,
                Subscribed = addStudentViewModel.Subscribed,
                Dob = addStudentViewModel.Dob,
            };
            await applicationDbContext.Students.AddAsync(student);
            await applicationDbContext.SaveChangesAsync();

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await applicationDbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await applicationDbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student studentlist)
        {
           var student = await applicationDbContext.Students.FindAsync(studentlist.Id);
            if(student != null)
            {
                student.Name = studentlist.Name;
                student.Email = studentlist.Email;
                student.Phone = studentlist.Phone;
                student.Subscribed = studentlist.Subscribed;
                student.Dob = studentlist.Dob;

                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Student delstudent)
        {
            var student = await applicationDbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == delstudent.Id);

            if (student != null)
            {
                applicationDbContext.Students.Remove(delstudent);
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }


    }
}
