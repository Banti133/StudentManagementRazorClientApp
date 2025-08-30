using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp
{
    public record class EnrollmentModel(
        [property: JsonPropertyName("enrollemntId")] int EnrollemntId,
        [property: JsonPropertyName("studentId")] int StudentId,
        [property: JsonPropertyName("courseId")] int CourseId,
        [property: JsonPropertyName("joiningdate")] DateTime Joiningdate,
        [property: JsonPropertyName("student")] StudentModel? Student,
        [property: JsonPropertyName("course")] CourseModel? Course
    );
}