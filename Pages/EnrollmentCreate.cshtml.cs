using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class EnrollmentCreateModel : PageModel
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EnrollmentCreateModel( EnrollmentService enrollmentService, StudentService studentService, CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]
        public EnrollmentModel NewEnrollment { get; set; } = default!;

        public SelectList StudentOptions { get; set; } = default!;
        public SelectList CourseOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
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

            var created = await _enrollmentService.AddEnrollmentAsync(NewEnrollment);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create enrollment.");
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