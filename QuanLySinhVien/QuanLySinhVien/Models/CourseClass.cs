using QuanLySinhVien.Models;

public class CourseClass
{
    public int Id { get; set; }
    public string ClassName { get; set; } = "";

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public List<Enrollment>? Enrollments { get; set; }
}