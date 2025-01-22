namespace FcmsPortal;

public class SemesterCurriculum
{
    public int Semester { get; set; } 
    public List<ClassSession> ClassSessions { get; set; } = new();
}