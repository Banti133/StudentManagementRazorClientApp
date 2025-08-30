using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace StudentManagementRazorClientApp.Pages
{
    public class StudentListModel : PageModel
    {
        private readonly StudentService _service;

        public StudentListModel(StudentService service)
        {
            _service = service;
        }

        public Task<List<StudentModel>> StudentList { get; set; } = default!;

        public void OnGet()
        {
            StudentList = _service.GetStudentsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _service.DeleteStudentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete student.");
            }

            return RedirectToPage(); // Refresh the list
        }
    }
}