using FcmsPortal.Enums;
using FcmsPortal.Models;
using Microsoft.AspNetCore.Components.Forms;
namespace FcmsPortal.Services
{
    public interface ISchoolDataService
    {
        #region School
        School GetSchool();
        void UpdateSchool(School updatedSchool);
        #endregion

        #region Addresses
        Address AddAddress(Address address);
        int GetNextAddressId();
        #endregion

        #region Staff
        Staff AddStaff(Staff staff);
        IEnumerable<Staff> GetStaff();
        Staff? GetStaffById(int id);
        void UpdateStaff(Staff staff);
        bool DeleteStaff(int staffId);
        #endregion

        #region Guardians
        Guardian AddGuardian(Guardian guardian);
        IEnumerable<Guardian> GetGuardians();
        Guardian? GetGuardianById(int id);
        Guardian? GetGuardianByStudentId(int studentId);
        void UpdateGuardian(Guardian guardian);
        bool DeleteGuardian(int guardianId);
        #endregion

        #region Students
        Student AddStudent(Student student);
        IEnumerable<Student> GetStudents();
        Student? GetStudentById(int id);
        void UpdateStudent(Student student);
        bool DeleteStudent(int studentId);
        #endregion

        #region Learning Paths
        LearningPath AddLearningPath(LearningPath learningPath);
        IEnumerable<LearningPath> GetAllLearningPaths();
        LearningPath? GetLearningPathById(int id);
        LearningPath? GetLearningPathByScheduleEntry(int scheduleEntryId);
        LearningPath? GetLearningPathByClassSessionId(int classSessionId);
        void UpdateLearningPath(LearningPath learningPath);
        bool DeleteLearningPath(int id);
        void AddMultipleStudentsToLearningPath(LearningPath learningPath, List<Student> studentsToAdd);
        void AddStudentToLearningPath(LearningPath learningPath, Student student);
        #endregion

        #region Learning Path Templates
        void CreateTemplateFromLearningPath(LearningPath learningPath);
        string GenerateTemplateKey(EducationLevel educationLevel, ClassLevel classLevel, Semester semester);
        LearningPath? GetTemplate(EducationLevel educationLevel, ClassLevel classLevel, Semester semester);
        bool HasTemplate(EducationLevel educationLevel, ClassLevel classLevel, Semester semester);
        LearningPath? ApplyTemplateToNewLearningPath(LearningPath template, DateTime newAcademicYearStart);
        #endregion

        #region Calendar & Scheduling
        ScheduleEntry? AddScheduleEntry(int learningPathId, ScheduleEntry scheduleEntry);
        IEnumerable<ScheduleEntry> GetAllSchoolCalendarSchedules();
        bool UpdateScheduleEntry(int learningPathId, ScheduleEntry scheduleEntry);
        bool DeleteScheduleEntry(int learningPathId, int scheduleEntryId);
        ScheduleEntry? AddGeneralScheduleEntry(ScheduleEntry scheduleEntry);
        void UpdateScheduleInSchoolCalendar(ScheduleEntry scheduleEntry);
        void RemoveScheduleFromSchoolCalendar(ScheduleEntry scheduleEntry);
        bool UpdateGeneralCalendarScheduleEntry(ScheduleEntry scheduleEntry);
        bool DeleteGeneralCalendarScheduleEntry(int scheduleEntryId);
        int GetNextScheduleId();
        #endregion

        #region Class Sessions
        bool UpdateClassSession(ClassSession classSession);
        ClassSession? GetClassSessionById(int classSessionId);
        int GetNextClassSessionId();
        #endregion

        #region Homework
        Homework? GetHomeworkById(int id);
        HomeworkSubmission? SubmitHomework(int homeworkId, Student student, string answer);
        void UpdateHomework(Homework homework);
        bool DeleteHomework(int id);
        HomeworkSubmission? GetHomeworkSubmissionById(int id);
        HomeworkSubmission? AddHomeworkSubmission(HomeworkSubmission submission);
        void UpdateHomeworkSubmission(HomeworkSubmission submission);
        int GetNextHomeworkId();
        #endregion

        #region Discussions
        int GetNextThreadId(int classSessionId);
        int GetNextPostId();
        Task AddDiscussionThread(DiscussionThread thread, int classSessionId);
        Task UpdateDiscussionThread(DiscussionThread thread, int classSessionId);
        #endregion

        #region File Attachments
        Task<FileAttachment> UploadFileAsync(IBrowserFile file, string category);
        Task DeleteFileAsync(FileAttachment attachment);
        Task<List<FileAttachment>> GetAttachmentsAsync(string category, int referenceId);
        Task SaveAttachmentReferenceAsync(FileAttachment attachment, string category, int referenceId);
        #endregion

        #region Payments
        Payment AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int id);
        Payment PrepareNewPayment(Student student);
        SchoolFees? GetSchoolFees(int id);
        int GetNextSchoolFeesId();
        Student? GetStudentBySchoolFeesId(int schoolFeesId);
        #endregion

        #region Grading
        void SaveCourseGradingConfiguration(CourseGradingConfiguration configuration);
        CourseGradingConfiguration? GetCourseGradingConfiguration(int learningPathId, string courseName);
        List<CourseGradingConfiguration> GetAllCourseGradingConfigurations(int learningPathId);
        List<string> GetCoursesWithoutGradingConfiguration(int learningPathId);
        List<GradesReport> GetGradesReports(string academicYear, string semester);
        int GetNextTestGradeId();
        int GetNextCourseGradeId();
        int GetNextCourseGradingConfigurationId();
        #endregion

        #region Curriculum
        List<Curriculum> GetFullCurriculum();
        List<Curriculum> FilterCurriculum(List<Curriculum> curriculum, EducationLevel educationLevel, ClassLevel classLevel, Semester? semester = null);
        #endregion

        #region Attendance
        DailyAttendanceLogEntry SaveAttendance(int learningPathId, List<int> presentStudentIds, int teacherId, DateTime? attendanceDate = null);
        bool HasAttendanceBeenTaken(int learningPathId, DateTime date);
        #endregion

        #region Archives
        void ArchiveStudent(Student student);
        List<Student> GetArchivedStudents();
        #endregion
    }


}