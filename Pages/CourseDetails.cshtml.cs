using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class CourseDetailsModel : PageModel
    {
        private readonly CourseService _service;

        public CourseDetailsModel(CourseService service)
        {
            _service = service;
        }

        public CourseModel CourseDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _service.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            CourseDetails = course;
            return Page();
        }
    }
}