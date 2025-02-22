using FcmsPortal.Enums;

namespace FcmsPortal.Models;

public class SemesterCurriculum
{
    public int Id { get; set; }
    public Semester Semester { get; set; }
    public List<ClassSession> ClassSessions { get; set; } = new();
}