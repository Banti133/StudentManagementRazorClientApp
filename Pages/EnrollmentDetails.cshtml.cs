using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class EnrollmentDetailsModel : PageModel
    {
        private readonly EnrollmentService _service;

        public EnrollmentDetailsModel(EnrollmentService service)
        {
            _service = service;
        }

        public EnrollmentModel EnrollmentDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var enrollment = await _service.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            EnrollmentDetails = enrollment;
            return Page();
        }
    }
}
