using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class EnrollmentEditModel : PageModel
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EnrollmentEditModel(EnrollmentService enrollmentService, StudentService studentService,CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]
        public EnrollmentModel EditEnrollment { get; set; } = default!;

        public SelectList StudentOptions { get; set; } = default!;
        public SelectList CourseOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            EditEnrollment = enrollment;

            await PopulateDropdownsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            var updated = await _enrollmentService.UpdateEnrollmentAsync(EditEnrollment);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to update enrollment.");
                await PopulateDropdownsAsync();
                return Page();
            }

            return RedirectToPage("/EnrollmentList");
        }

        //private async Task PopulateDropdownsAsync()
        //{
        //    var students = await _studentService.GetStudentsAsync();
        //    var courses = await _courseService.GetCoursesAsync();

        //    StudentOptions = new SelectList(students, "StudentId", "Name");
        //    CourseOptions = new SelectList(courses, "CourseId", "CourseName");
        //}
        private async Task PopulateDropdownsAsync()
        {
            var students = await _studentService.GetStudentsAsync() ?? new List<StudentModel>();
            var courses = await _courseService.GetCoursesAsync() ?? new List<CourseModel>();

            StudentOptions = new SelectList(students, "StudentId", "Name");
            CourseOptions = new SelectList(courses, "CourseId", "Title");
        }
    }
}






//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using StudentManagementRazorClientApp;

//namespace StudentManagementRazorClientApp.Pages
//{
//    public class EnrollmentEditModel : PageModel
//    {
//        private readonly EnrollmentService _service;

//        public EnrollmentEditModel(EnrollmentService service)
//        {
//            _service = service;
//        }

//        [BindProperty]
//        public EnrollmentModel EditEnrollment { get; set; } = default!;

//        public async Task<IActionResult> OnGetAsync(int id)
//        {
//            var enrollment = await _service.GetEnrollmentByIdAsync(id);
//            if (enrollment == null)
//            {
//                return NotFound();
//            }

//            EditEnrollment = enrollment;
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//                return Page();

//            var updated = await _service.UpdateEnrollmentAsync(EditEnrollment);
//            if (updated == null)
//            {
//                ModelState.AddModelError(string.Empty, "Failed to update enrollment.");
//                return Page();
//            }

//            return RedirectToPage("/EnrollmentList");
//        }
//    }
//}
