using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace StudentManagementRazorClientApp
{
    public class CourseService
    {
        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7051/api/Course");

                var courses = await JsonSerializer.DeserializeAsync<List<CourseModel>>(stream);
                return courses ?? new List<CourseModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching courses: {ex.Message}");
                throw;
            }
        }

        public async Task<CourseModel?> AddCourseAsync(CourseModel course)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7051/api/Course", course);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<CourseModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<CourseModel?> GetCourseByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7051/api/Course/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<CourseModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<CourseModel?> UpdateCourseAsync(int id,CourseModel course)
        {
            //try
            //{
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7051/api/Course/{id}", course);

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<CourseModel>();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception: {ex.Message}");
            //    throw;
            //}
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7051/api/Course/{id}");

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