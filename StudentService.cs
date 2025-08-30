using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace StudentManagementRazorClientApp
{
    public class StudentService
    {
        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7051/api/Student");

                var students = await JsonSerializer.DeserializeAsync<List<StudentModel>>(stream);
                return students ?? new List<StudentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching students: {ex.Message}");
                throw;
            }
        }

        public async Task<StudentModel?> AddStudentAsync(StudentModel student)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7051/api/Student", student);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                //var content = await response.Content.ReadAsStringAsync();
                //if (string.IsNullOrWhiteSpace(content))
                //{
                //    Console.WriteLine("Empty response content.");
                //    return null;
                //}

                //var createdStudent = JsonSerializer.Deserialize<StudentModel>(content, new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true
                //});
                var createdStudent = await response.Content.ReadFromJsonAsync<StudentModel>();

                return createdStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7051/api/Student/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<StudentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<StudentModel?> UpdateStudentAsync(int id,StudentModel student)
        {
            //try
            //{
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7051/api/Student/{id}", student);

                if (!response.IsSuccessStatusCode)
                    return null;

                var updatedstudent = await response.Content.ReadFromJsonAsync<StudentModel>();
                return updatedstudent;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception: {ex.Message}");
            //    throw;
            //}
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7051/api/Student/{id}");

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