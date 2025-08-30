using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class CourseListModel : PageModel
    {
        private readonly CourseService _service;

        public CourseListModel(CourseService service)
        {
            _service = service;
        }

        public Task<List<CourseModel>> CourseList { get; set; } = default!;

        public void OnGet()
        {
            CourseList =  _service.GetCoursesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _service.DeleteCourseAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete course.");
            }

            return RedirectToPage(); // Refresh the list
        }
    }
}