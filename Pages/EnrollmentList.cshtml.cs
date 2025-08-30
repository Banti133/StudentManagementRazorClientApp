using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp;

namespace StudentManagementRazorClientApp.Pages
{
    public class EnrollmentListModel : PageModel
    {
        private readonly EnrollmentService _service;

        public EnrollmentListModel(EnrollmentService service)
        {
            _service = service;
        }

        public Task<List<EnrollmentModel>> EnrollmentList { get; set; } = default!;

        public void OnGet()
        {
            EnrollmentList = _service.GetEnrollmentsAsync();
        }


        //public async Task OnGetAsync()
        //{
        //    EnrollmentList = await _service.GetEnrollmentsAsync();
        //}


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _service.DeleteEnrollmentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete enrollment.");
            }

            return RedirectToPage();
        }
    }
}
