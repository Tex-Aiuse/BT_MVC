using QuanLySinhVien.Models;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; } = "";

    public List<CourseClass>? CourseClasses { get; set; }
}