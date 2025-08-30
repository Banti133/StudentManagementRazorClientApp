using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace StudentManagementRazorClientApp.Pages
{
    public class CourseCreateModel : PageModel
    {
        private readonly CourseService _service;

        public CourseCreateModel(CourseService service)
        {
            _service = service;
        }

        [BindProperty]
        public CourseModel NewCourse { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdCourse = await _service.AddCourseAsync(NewCourse);

            if (createdCourse == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create course.");
                return Page();
            }

            return RedirectToPage("CourseList"); // Redirect to list page after creation
        }
    }
}