using FcmsPortal.Enums;

namespace FcmsPortal;

public class SemesterCurriculum
{
    public Semester Semester { get; set; }
    public List<ClassSession> ClassSessions { get; set; } = new();
}