using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace StudentManagementRazorClientApp.Pages
{
    public class StudentDetailsModel : PageModel
    {
        private readonly StudentService _service;

        public StudentDetailsModel(StudentService service)
        {
            _service = service;
        }

        public StudentModel StudentDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var student = await _service.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            StudentDetails = student;
            return Page();
        }
    }
}