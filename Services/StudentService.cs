using StudentHub.Data;
using StudentHub.Models;

namespace StudentHub.Services;

public class StudentService
{
    private readonly StudentDbContext _db;

    public StudentService(StudentDbContext db)
    {
        _db = db;
    }

    public async Task CreateStudent(Student student)
    {
        _db.Students.Add(student);
        await _db.SaveChangesAsync();
    }

    // Filtering
    public IEnumerable<Student> GetMaleStudents()
    {
        var result = from student in _db.Students
                     where student.Gender == "Male"
                     select student;

        return result;
    }

    public IEnumerable<Student> GetFemaleStudents()
    {
        var result = from student in _db.Students
                     where student.Gender == "Female"
                     select student;

        return result;
    }

    public IEnumerable<Student> GetOthersStudents()
    {
        var result = from student in _db.Students
                     where student.Gender == "Others"
                     select student;

        return result;
    }

    // OfType
    public IEnumerable<Student> GetMaleStudentsOnly()
    {
        var result = _db.Students.OfType<Student>().Where(s => s.Gender == "Male");

        return result;
    }

    public IEnumerable<Student> GetFemaleStudentsOnly()
    {
        var result = _db.Students.OfType<Student>().Where(s => s.Gender == "Female");

        return result;
    }

    public IEnumerable<Student> GetOthersStudentsOnly()
    {
        var result = _db.Students.OfType<Student>().Where(s => s.Gender == "Others");

        return result;
    }

    // Projection
    public IEnumerable<string> GetStudentFullNames()
    {
        var result = from student in _db.Students
                     select $"{student.FirstName} {student.LastName}";

        return result;
    }

    // SelectMany
    public IEnumerable<string> GetCoursesForAllStudents()
    {
        var result = _db.Students
             .AsEnumerable()
             .SelectMany(student => student.Courses)
             .ToList();

        return result;
    }

    // Partitioning Take
    public IEnumerable<Student> GetFirstTwoStudents()
    {
        var result = _db.Students.Take(2);

        return result;
    }

    // Partitioning Skip
    public IEnumerable<Student> GetStudentsAfterSkippingFirstTwo()
    {
        var result = _db.Students.Skip(2);

        return result;
    }

    // Ordering OrderBy
    public IEnumerable<Student> GetStudentsOrderedByName()
    {
        var result = _db.Students.OrderBy(student => student.FirstName);

        return result;
    }

    // Ordering ByDescending
    public IEnumerable<Student> GetStudentsByDescendingName()
    {
        var result = _db.Students.OrderByDescending(student => student.FirstName);

        return result;
    }

    // ThenBy
    public IEnumerable<Student> GetStudentsOrderedByNameAndThenByAge()
    {
        var result = _db.Students.OrderBy(student => student.FirstName).ThenBy(student => student.Age);

        return result;
    }

    // Reverse
    public IEnumerable<Student> GetStudentsReversed()
    {
        var result = _db.Students
            .AsEnumerable()
            .Reverse();

        return result;
    }

    // Grouping
    public IEnumerable<IGrouping<int, Student>> GroupStudentsByAge()
    {
        var result = _db.Students.GroupBy(student => student.Age);

        return result;
    }

    public IEnumerable<IGrouping<string, Student>> GroupStudentsByCourses()
    {
        return _db.Students
            .GroupJoin(
                _db.Enrollments,
                student => student.Id,
                enrollment => enrollment.StudentId,
                (student, enrollments) => new { student, enrollments }
            )
            .SelectMany(
                x => x.enrollments.DefaultIfEmpty(),
                (x, enrollment) => new { Student = x.student, Enrollment = enrollment }
            )
            .GroupBy(x => x.Enrollment.CourseId.ToString(), x => x.Student);
    }

    public ILookup<Guid, Student> ToLookupByStudentId()
    {
        return _db.Students.ToLookup(student => student.Id);
    }

    //Join
    public IEnumerable<string> GetStudentCourseNames()
    {
        var result = from student in _db.Students
                     join enrollment in _db.Enrollments on student.Id equals enrollment.StudentId
                     join course in _db.Courses on enrollment.CourseId equals course.CourseId
                     select $"{student.FirstName} {student.LastName} - {course.CourseName}";
        return result;
    }

    // Conversion
    public Student[] ConvertToStudentArray()
    {
        var result = _db.Students.ToArray();

        return result;
    }

    // Element
    public Student GetFirstStudent()
    {
        var result = _db.Students.First();

        return result;
    }

    // Aggregation
    public double GetAverageScore()
    {
        var result = _db.Students.Average(student => student.Score);

        return result;
    }

    // Filtering aditional Operators
    public IEnumerable<Student> FilterStudentsByType()
    {
        return _db.Students.OfType<Student>();
    }

    public IEnumerable<string> GetDistinctCourses()
    {
        return _db.Students
            .AsEnumerable()
            .SelectMany(student => student.Courses).Distinct();
    }

    // Set Operators
    public IEnumerable<string> UnionStudentNames(IEnumerable<Student> firstList, IEnumerable<Student> secondList)
    {
        var unionList = firstList.Union(secondList);
        return unionList.Select(student => $"{student.FirstName} {student.LastName}");
    }

    public IEnumerable<string> IntersectStudentNames(IEnumerable<Student> firstList, IEnumerable<Student> secondList)
    {
        var intersectList = firstList.Intersect(secondList);
        return intersectList.Select(student => $"{student.FirstName} {student.LastName}");
    }

    public IEnumerable<string> ExceptStudentNames(IEnumerable<Student> firstList, IEnumerable<Student> secondList)
    {
        var exceptList = firstList.Except(secondList);
        return exceptList.Select(student => $"{student.FirstName} {student.LastName}");
    }

    // Quantification Operators
    public bool CheckIfAllStudentsPassed()
    {
        return _db.Students.All(student => student.Score >= 60);
    }

    public bool CheckIfAnyStudentFailed()
    {
        return _db.Students.Any(student => student.Score < 60);
    }
}