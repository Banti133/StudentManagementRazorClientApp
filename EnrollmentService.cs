using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace StudentManagementRazorClientApp
{
    public class EnrollmentService
    {
        public async Task<List<EnrollmentModel>> GetEnrollmentsAsync()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7051/api/Enrollment");

                var enrollments = await JsonSerializer.DeserializeAsync<List<EnrollmentModel>>(stream);
                return enrollments ?? new List<EnrollmentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching enrollments: {ex.Message}");
                throw;
            }
        }

        public async Task<EnrollmentModel?> AddEnrollmentAsync(EnrollmentModel enrollment)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7051/api/Enrollment", enrollment);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<EnrollmentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<EnrollmentModel?> GetEnrollmentByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7051/api/Enrollment/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<EnrollmentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<EnrollmentModel?> UpdateEnrollmentAsync(EnrollmentModel enrollment)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7051/api/Enrollment/{enrollment.EnrollemntId}", enrollment);

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<EnrollmentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7051/api/Enrollment/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}