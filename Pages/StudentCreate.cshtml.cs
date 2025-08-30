using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace StudentManagementRazorClientApp.Pages
{
    public class StudentCreateModel : PageModel
    {
        private readonly StudentService _service;

        public StudentCreateModel(StudentService service)
        {
            _service = service;
        }

        [BindProperty]
        public StudentModel NewStudent { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdStudent = await _service.AddStudentAsync(NewStudent);

            if (createdStudent == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create student.");
                return Page();
            }

            return RedirectToPage("StudentList"); // Redirect to list page after creation
        }
    }
}