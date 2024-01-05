using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentHub.Data;
using StudentHub.Models;
using StudentHub.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentDbContext>(options =>
{
    options.UseSqlite("Data Source=students_db.db");
});
builder.Services.AddTransient<StudentService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentHub", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentHub API V1");
    c.RoutePrefix = string.Empty;
});

// Map API endpoints
app.MapPost("/api/students", async (Student student, StudentService studentService) =>
{
    await studentService.CreateStudent(student);
    return Results.Ok();
});

app.MapGet("/api/students/male", (StudentService studentService) => studentService.GetMaleStudents());

app.MapGet("/api/students/female", (StudentService studentService) => studentService.GetFemaleStudents());

app.MapGet("/api/students/others", (StudentService studentService) => studentService.GetOthersStudents());

app.MapGet("/api/students/male-only", (StudentService studentService) => studentService.GetMaleStudentsOnly());

app.MapGet("/api/students/female-only", (StudentService studentService) => studentService.GetFemaleStudentsOnly());

app.MapGet("/api/students/others-only", (StudentService studentService) => studentService.GetOthersStudentsOnly());

app.MapGet("/api/students/full-names", (StudentService studentService) => studentService.GetStudentFullNames());

app.MapGet("/api/students/courses", (StudentService studentService) => studentService.GetCoursesForAllStudents());

app.MapGet("/api/students/first-two", (StudentService studentService) => studentService.GetFirstTwoStudents());

app.MapGet("/api/students/ordered-by-name", (StudentService studentService) => studentService.GetStudentsOrderedByName());

app.MapGet("/api/students/ordered-by-name-age", (StudentService studentService) => studentService.GetStudentsOrderedByNameAndThenByAge());

app.MapGet("/api/students/reversed", (StudentService studentService) => studentService.GetStudentsReversed());

app.MapGet("/api/students/grouped-by-age", (StudentService studentService) => studentService.GroupStudentsByAge());

app.MapGet("/api/students/to-array", (StudentService studentService) => studentService.ConvertToStudentArray());

app.MapGet("/api/students/first", (StudentService studentService) => studentService.GetFirstStudent());

app.MapGet("/api/students/average-score", (StudentService studentService) => studentService.GetAverageScore());

app.MapGet("/api/students/filterByType", (StudentService studentService) => studentService.FilterStudentsByType());

app.MapGet("/api/students/distinctCourses", (StudentService studentService) => studentService.GetDistinctCourses());

app.MapGet("/api/students/unionNames", (StudentService studentService, IEnumerable<Student> firstList, IEnumerable<Student> secondList) => studentService.UnionStudentNames(firstList, secondList));

app.MapGet("/api/students/intersectNames", (StudentService studentService, IEnumerable<Student> firstList, IEnumerable<Student> secondList) => studentService.IntersectStudentNames(firstList, secondList));

app.MapGet("/api/students/exceptNames", (StudentService studentService, IEnumerable<Student> firstList, IEnumerable<Student> secondList) => studentService.ExceptStudentNames(firstList, secondList));

app.MapGet("/api/students/allPassed", (StudentService studentService) => studentService.CheckIfAllStudentsPassed());

app.MapGet("/api/students/anyFailed", (StudentService studentService) => studentService.CheckIfAnyStudentFailed());

app.MapGet("/api/students/groupByCourses", (StudentService studentService) => studentService.GroupStudentsByCourses());

app.MapGet("/api/students/toLookupByStudentId", (StudentService studentService) => studentService.ToLookupByStudentId());

app.MapGet("/api/students/studentCourseNames", (StudentService studentService) => studentService.GetStudentCourseNames());

app.MapGet("/api/students/descendingNames", (StudentService studentService) => studentService.GetStudentsByDescendingName());

app.MapGet("/api/students/skipFirstTwo", (StudentService studentService) => studentService.GetStudentsAfterSkippingFirstTwo());

app.Run();
