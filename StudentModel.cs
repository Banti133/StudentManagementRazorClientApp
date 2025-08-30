using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp
{
    public record class StudentModel
    (
        [property: JsonPropertyName("studentId")] int StudentId,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("age")] int Age
        );
}
