using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp
{
    public record class CourseModel
    (
        [property: JsonPropertyName("courseId")] int CourseId,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("credit")] int  Credit    
        );
}
