using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentManagementRazorClientApp.Pages
{
    public class CourseEditModel : PageModel
    {
        private readonly CourseService _service;

        public CourseEditModel(CourseService service)
        {
            _service = service;
        }

        [BindProperty]
        public CourseModel EditCourse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _service.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            EditCourse = course;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updated = await _service.UpdateCourseAsync(EditCourse.CourseId, EditCourse);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to update course.");
                return Page();
            }

            return RedirectToPage("/CourseList");
        }
    }
}
