using FcmsPortal.Constants;
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

    public class SchoolDataService : ISchoolDataService
    {
        private readonly School _school;
        private readonly IWebHostEnvironment _environment;
        private readonly Dictionary<string, List<(int referenceId, FileAttachment attachment)>> _attachmentReferences = new();
        private int _nextAttachmentId = 1;
        private List<Payment> _payments = new List<Payment>();
        private List<SchoolFees> _schoolFees = new List<SchoolFees>();
        private List<Student> _archivedStudents = new List<Student>();
        private List<Address> _addresses = new List<Address>();
        private static readonly object _idLock = new object();
        private static readonly Dictionary<string, int> _entityCounters = new Dictionary<string, int>();
        public SchoolDataService(IWebHostEnvironment environment)
        {
            _school = Program.CreateSchool();
            _environment = environment;
            InitializeIdCounters();
        }

        private int GetNextId(string entityType, Func<int> getCurrentMaxId)
        {
            lock (_idLock)
            {
                if (!_entityCounters.ContainsKey(entityType))
                {
                    _entityCounters[entityType] = getCurrentMaxId();
                }

                _entityCounters[entityType]++;
                return _entityCounters[entityType];
            }
        }

        private void InitializeIdCounters()
        {
            // Pre-populate the ID counters to avoid the initial max ID calculation
            _entityCounters["Address"] = _addresses.Any() ? _addresses.Max(a => a.Id) : 0;
            GetNextId("Homework", () => GetMaxHomeworkId());
            GetNextId("HomeworkSubmission", () => GetMaxHomeworkSubmissionId());
            GetNextId("TestGrade", () => GetMaxTestGradeId());
            GetNextId("CourseGrade", () => GetMaxCourseGradeId());
            // Add other entity types as needed
        }

        #region School
        public School GetSchool() => _school;

        public void UpdateSchool(School updatedSchool)
        {
            // Update the properties of the existing school
            _school.Name = updatedSchool.Name;
            _school.LogoUrl = updatedSchool.LogoUrl;
            _school.Email = updatedSchool.Email;
            _school.PhoneNumber = updatedSchool.PhoneNumber;
            _school.WebsiteUrl = updatedSchool.WebsiteUrl;

            // Update address
            if (_school.Address != null && updatedSchool.Address != null)
            {
                _school.Address.Street = updatedSchool.Address.Street;
                _school.Address.City = updatedSchool.Address.City;
                _school.Address.State = updatedSchool.Address.State;
                _school.Address.PostalCode = updatedSchool.Address.PostalCode;
                _school.Address.Country = updatedSchool.Address.Country;
            }
        }
        #endregion

        #region Addresses
        public Address AddAddress(Address address)
        {
            if (address.Id <= 0)
            {
                address.Id = GetNextAddressId();
            }
            _addresses.Add(address);
            return address;
        }

        public int GetNextAddressId()
        {
            return GetNextId("Address", () => _addresses.Any() ? _addresses.Max(a => a.Id) : 0);
        }
        #endregion

        #region Staff
        public IEnumerable<Staff> GetStaff() => _school.Staff;

        public Staff? GetStaffById(int id)
        {
            return _school.Staff.FirstOrDefault(s => s.Id == id);
        }

        public Staff AddStaff(Staff staff)
        {
            if (staff.Id <= 0)
            {
                staff.Id = _school.Staff.Any() ? _school.Staff.Max(s => s.Id) + 1 : 1;
            }

            var staffList = _school.Staff.ToList();
            staffList.Add(staff);
            _school.Staff = staffList;

            return staff;
        }

        public void UpdateStaff(Staff staff)
        {
            var existingStaff = _school.Staff.FirstOrDefault(s => s.Id == staff.Id);
            if (existingStaff != null)
            {
                existingStaff.JobRole = staff.JobRole;
                existingStaff.DateOfEmployment = staff.DateOfEmployment;
                existingStaff.JobDescription = staff.JobDescription;
                existingStaff.WorkExperience = staff.WorkExperience;
                existingStaff.AreaOfSpecialization = staff.AreaOfSpecialization;
                existingStaff.Qualifications = staff.Qualifications;
                existingStaff.Person.ProfilePictureUrl = staff.Person.ProfilePictureUrl;
                existingStaff.Person.FirstName = staff.Person.FirstName;
                existingStaff.Person.MiddleName = staff.Person.MiddleName;
                existingStaff.Person.LastName = staff.Person.LastName;
                existingStaff.Person.Sex = staff.Person.Sex;
                existingStaff.Person.StateOfOrigin = staff.Person.StateOfOrigin;
                existingStaff.Person.LgaOfOrigin = staff.Person.LgaOfOrigin;
                existingStaff.Person.Email = staff.Person.Email;
                existingStaff.Person.PhoneNumber = staff.Person.PhoneNumber;
                existingStaff.Person.DateOfBirth = staff.Person.DateOfBirth;
                existingStaff.Person.EmergencyContact = staff.Person.EmergencyContact;
                existingStaff.Person.EducationLevel = staff.Person.EducationLevel;
                existingStaff.Person.IsActive = staff.Person.IsActive;
            }
        }

        public bool DeleteStaff(int staffId)
        {
            var staff = _school.Staff.FirstOrDefault(s => s.Id == staffId);
            if (staff == null)
            {
                return false;
            }

            var staffList = _school.Staff.ToList();
            staffList.Remove(staff);
            _school.Staff = staffList;

            return true;
        }
        #endregion

        #region Guardians
        public IEnumerable<Guardian> GetGuardians() => _school.Guardians;

        public Guardian? GetGuardianById(int id)
        {
            return _school.Guardians.FirstOrDefault(g => g.Id == id);
        }

        public Guardian? GetGuardianByStudentId(int studentId)
        {
            return _school.Guardians.FirstOrDefault(g => g.Wards.Any(w => w.Id == studentId));
        }

        public void UpdateGuardian(Guardian guardian)
        {
            var existingGuardian = _school.Guardians.FirstOrDefault(g => g.Id == guardian.Id);
            if (existingGuardian != null)
            {
                existingGuardian.Person.FirstName = guardian.Person.FirstName;
                existingGuardian.Person.MiddleName = guardian.Person.MiddleName;
                existingGuardian.Person.LastName = guardian.Person.LastName;
                existingGuardian.Person.Sex = guardian.Person.Sex;
                existingGuardian.Person.StateOfOrigin = guardian.Person.StateOfOrigin;
                existingGuardian.Person.LgaOfOrigin = guardian.Person.LgaOfOrigin;
                existingGuardian.Person.Email = guardian.Person.Email;
                existingGuardian.Person.PhoneNumber = guardian.Person.PhoneNumber;
                existingGuardian.Person.DateOfEnrollment = guardian.Person.DateOfEnrollment;
                existingGuardian.Occupation = guardian.Occupation;
                existingGuardian.RelationshipToStudent = guardian.RelationshipToStudent;
                existingGuardian.Person.ProfilePictureUrl = guardian.Person.ProfilePictureUrl;
                existingGuardian.Person.IsActive = guardian.Person.IsActive;
            }
        }

        public Guardian AddGuardian(Guardian guardian)
        {
            if (guardian.Id <= 0)
            {
                guardian.Id = _school.Guardians.Any() ? _school.Guardians.Max(g => g.Id) + 1 : 1;
            }

            var guardians = _school.Guardians.ToList();
            guardians.Add(guardian);
            _school.Guardians = guardians;

            return guardian;
        }

        public bool DeleteGuardian(int guardianId)
        {
            var guardian = _school.Guardians.FirstOrDefault(g => g.Id == guardianId);
            if (guardian == null)
            {
                return false;
            }

            var guardianList = _school.Guardians.ToList();
            guardianList.Remove(guardian);
            _school.Guardians = guardianList;

            return true;
        }
        #endregion

        #region Students
        public IEnumerable<Student> GetStudents() => _school.Students;

        public Student? GetStudentById(int id)
        {
            return _school.Students.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = _school.Students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Person.FirstName = student.Person.FirstName;
                existingStudent.Person.MiddleName = student.Person.MiddleName;
                existingStudent.Person.LastName = student.Person.LastName;
                existingStudent.Person.ProfilePictureUrl = student.Person.ProfilePictureUrl;
                existingStudent.Person.DateOfBirth = student.Person.DateOfBirth;
                existingStudent.Person.EducationLevel = student.Person.EducationLevel;
                existingStudent.Person.ClassLevel = student.Person.ClassLevel;
                existingStudent.Person.Sex = student.Person.Sex;
                existingStudent.Person.StateOfOrigin = student.Person.StateOfOrigin;
                existingStudent.Person.LgaOfOrigin = student.Person.LgaOfOrigin;
                existingStudent.Person.DateOfEnrollment = student.Person.DateOfEnrollment;
                existingStudent.Person.EmergencyContact = student.Person.EmergencyContact;
                existingStudent.Person.Email = student.Person.Email;
                existingStudent.Person.PhoneNumber = student.Person.PhoneNumber;
                existingStudent.Person.IsActive = student.Person.IsActive;
                existingStudent.PositionAmongSiblings = student.PositionAmongSiblings;
                existingStudent.LastSchoolAttended = student.LastSchoolAttended;
                existingStudent.GuardianId = student.GuardianId;
            }
        }

        public Student AddStudent(Student student)
        {
            if (student.Id <= 0)
            {
                student.Id = _school.Students.Any() ? _school.Students.Max(s => s.Id) + 1 : 1;
            }
            var students = _school.Students.ToList();
            students.Add(student);
            _school.Students = students;
            return student;
        }

        public bool DeleteStudent(int studentId)
        {
            var student = _school.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return false;
            }

            var studentList = _school.Students.ToList();
            studentList.Remove(student);
            _school.Students = studentList;

            return true;
        }
        #endregion

        #region Learning Paths
        public LearningPath AddLearningPath(LearningPath learningPath)
        {
            if (learningPath.Id <= 0)
            {
                learningPath.Id = _school.LearningPaths.Any() ? _school.LearningPaths.Max(lp => lp.Id) + 1 : 1;
            }
            var learningPaths = _school.LearningPaths.ToList();
            learningPaths.Add(learningPath);
            _school.LearningPaths = learningPaths;
            return learningPath;
        }

        public LearningPath? GetLearningPathById(int id)
        {
            return _school.LearningPaths.FirstOrDefault(lp => lp.Id == id);
        }

        public IEnumerable<LearningPath> GetAllLearningPaths()
        {
            return _school.LearningPaths.Where(lp => lp != null && !lp.IsTemplate);
        }

        public LearningPath? GetLearningPathByScheduleEntry(int scheduleEntryId)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                if (learningPath.Schedule != null && learningPath.Schedule.Any(s => s.Id == scheduleEntryId))
                {
                    return learningPath;
                }
            }
            return null;
        }

        public LearningPath? GetLearningPathByClassSessionId(int classSessionId)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.Id == classSessionId)
                    {
                        return learningPath;
                    }
                }
            }
            return null;
        }

        public bool DeleteLearningPath(int id)
        {
            var learningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == id);
            if (learningPath == null)
            {
                return false;
            }
            var learningPaths = _school.LearningPaths.ToList();
            learningPaths.Remove(learningPath);
            _school.LearningPaths = learningPaths;
            return true;
        }

        public void UpdateLearningPath(LearningPath learningPath)
        {
            var existingLearningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == learningPath.Id);
            if (existingLearningPath != null)
            {
                existingLearningPath.SemesterStartDate = learningPath.SemesterStartDate;
                existingLearningPath.SemesterEndDate = learningPath.SemesterEndDate;
                existingLearningPath.ExamsStartDate = learningPath.ExamsStartDate;
                existingLearningPath.EducationLevel = learningPath.EducationLevel;
                existingLearningPath.ClassLevel = learningPath.ClassLevel;
                existingLearningPath.Semester = learningPath.Semester;
                existingLearningPath.FeePerSemester = learningPath.FeePerSemester;
                existingLearningPath.AcademicYearStart = learningPath.AcademicYearStart;
                existingLearningPath.ApprovalStatus = learningPath.ApprovalStatus;
                existingLearningPath.Students = learningPath.Students;
                existingLearningPath.StudentsWithAccess = learningPath.StudentsWithAccess;
            }
        }

        public void AddStudentToLearningPath(LearningPath learningPath, Student student)
        {
            if (learningPath == null)
                throw new ArgumentNullException(nameof(learningPath));
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (learningPath.Students == null)
                learningPath.Students = new List<Student>();

            if (!learningPath.Students.Contains(student))
            {
                learningPath.Students.Add(student);
                student.Person.SchoolFees = new SchoolFees();
                student.Person.SchoolFees.Id = GetNextSchoolFeesId();
                student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;

                if (student.CurrentLearningPathId == 0)
                {
                    student.CurrentLearningPathId = learningPath.Id;
                    student.CurrentLearningPath = learningPath;
                }

                if (student.Person.SchoolFees.TotalPaid >= learningPath.FeePerSemester * FcmsConstants.PAYMENT_THRESHOLD_FACTOR &&
                    !learningPath.StudentsWithAccess.Contains(student))
                {
                    learningPath.StudentsWithAccess.Add(student);
                }
            }
        }

        public void AddMultipleStudentsToLearningPath(LearningPath learningPath, List<Student> studentsToAdd)
        {
            if (learningPath == null)
                throw new ArgumentNullException(nameof(learningPath));
            if (studentsToAdd == null || !studentsToAdd.Any())
                throw new ArgumentException("Students list cannot be null or empty.", nameof(studentsToAdd));

            foreach (var student in studentsToAdd)
            {
                if (student == null)
                    continue;

                AddStudentToLearningPath(learningPath, student);
            }
        }
        #endregion

        #region Learning Path Templates
        public void CreateTemplateFromLearningPath(LearningPath learningPath)
        {
            if (learningPath == null) return;

            string templateKey = GenerateTemplateKey(learningPath.EducationLevel, learningPath.ClassLevel, learningPath.Semester);

            _school.LearningPaths.RemoveAll(lp => lp.IsTemplate && lp.TemplateKey == templateKey);

            var template = new LearningPath
            {
                Id = GetNextId("LearningPath", () => _school.LearningPaths.Count > 0 ? _school.LearningPaths.Max(lp => lp.Id) : 0),
                EducationLevel = learningPath.EducationLevel,
                ClassLevel = learningPath.ClassLevel,
                Semester = learningPath.Semester,
                AcademicYearStart = learningPath.AcademicYearStart,
                SemesterStartDate = learningPath.SemesterStartDate,
                SemesterEndDate = learningPath.SemesterEndDate,
                ExamsStartDate = learningPath.ExamsStartDate,
                FeePerSemester = learningPath.FeePerSemester,
                IsTemplate = true,
                TemplateKey = templateKey,
                ApprovalStatus = PrincipalApprovalStatus.Approved,

                Schedule = learningPath.Schedule.Select(s => new ScheduleEntry
                {
                    Id = GetNextScheduleId(),
                    Title = s.Title,
                    DateTime = s.DateTime,
                    Duration = s.Duration,
                    Venue = s.Venue,
                    ClassSession = s.ClassSession != null ? new ClassSession
                    {
                        Id = GetNextClassSessionId(),
                        Course = s.ClassSession.Course,
                        Topic = s.ClassSession.Topic,
                        Description = s.ClassSession.Description,
                        LessonPlan = s.ClassSession.LessonPlan,
                        Teacher = s.ClassSession.Teacher,
                        StudyMaterials = new List<FileAttachment>(),
                        DiscussionThreads = new List<DiscussionThread>()
                    } : null
                }).ToList(),

                // Empty collections for template
                Students = new List<Student>(),
                StudentsWithAccess = new List<Student>()
            };

            _school.LearningPaths.Add(template);
        }

        public string GenerateTemplateKey(EducationLevel educationLevel, ClassLevel classLevel, Semester semester)
        {
            return $"{educationLevel}_{classLevel}_{semester}";
        }

        public LearningPath? GetTemplate(EducationLevel educationLevel, ClassLevel classLevel, Semester semester)
        {
            string templateKey = GenerateTemplateKey(educationLevel, classLevel, semester);
            return _school.LearningPaths.FirstOrDefault(lp => lp.IsTemplate && lp.TemplateKey == templateKey);
        }

        public bool HasTemplate(EducationLevel educationLevel, ClassLevel classLevel, Semester semester)
        {
            return GetTemplate(educationLevel, classLevel, semester) != null;
        }

        public LearningPath? ApplyTemplateToNewLearningPath(LearningPath template, DateTime newAcademicYearStart)
        {
            if (template == null || !template.IsTemplate) return null;

            var templateYearStart = template.AcademicYearStart;
            var dateOffset = newAcademicYearStart - templateYearStart;

            var newLearningPath = new LearningPath
            {
                EducationLevel = template.EducationLevel,
                ClassLevel = template.ClassLevel,
                Semester = template.Semester,
                AcademicYearStart = newAcademicYearStart,
                SemesterStartDate = template.SemesterStartDate.Add(dateOffset),
                SemesterEndDate = template.SemesterEndDate.Add(dateOffset),
                ExamsStartDate = template.ExamsStartDate.Add(dateOffset),
                FeePerSemester = template.FeePerSemester,
                IsTemplate = false,
                TemplateKey = null,
                ApprovalStatus = PrincipalApprovalStatus.Pending,

                Schedule = template.Schedule.Select(s => new ScheduleEntry
                {
                    Title = s.Title,
                    DateTime = s.DateTime.Add(dateOffset),
                    Duration = s.Duration,
                    Venue = s.Venue,
                    ClassSession = s.ClassSession != null ? new ClassSession
                    {
                        Course = s.ClassSession.Course,
                        Topic = s.ClassSession.Topic,
                        Description = s.ClassSession.Description,
                        LessonPlan = s.ClassSession.LessonPlan,
                        Teacher = s.ClassSession.Teacher,
                        StudyMaterials = new List<FileAttachment>(),
                        DiscussionThreads = new List<DiscussionThread>()
                    } : null
                }).ToList(),

                Students = new List<Student>(),
                StudentsWithAccess = new List<Student>()
            };

            return newLearningPath;
        }
        #endregion

        #region Calendar & Scheduling
        public ScheduleEntry? AddScheduleEntry(int learningPathId, ScheduleEntry scheduleEntry)
        {
            var learningPath = GetLearningPathById(learningPathId);
            if (learningPath == null)
                return null;

            scheduleEntry.Id = GetNextScheduleId();
            learningPath.Schedule.Add(scheduleEntry);
            UpdateLearningPath(learningPath);
            AddGeneralScheduleEntry(scheduleEntry);

            return scheduleEntry;
        }

        public IEnumerable<ScheduleEntry> GetAllSchoolCalendarSchedules()
        {
            var allSchedules = new List<ScheduleEntry>();

            if (_school.SchoolCalendar != null)
            {
                foreach (var calendar in _school.SchoolCalendar)
                {
                    if (calendar.ScheduleEntries != null)
                    {
                        allSchedules.AddRange(calendar.ScheduleEntries);
                    }
                }
            }

            return allSchedules;
        }

        public bool UpdateScheduleEntry(int learningPathId, ScheduleEntry scheduleEntry)
        {
            var learningPath = GetLearningPathById(learningPathId);
            if (learningPath == null)
                return false;

            var existingEntry = learningPath.Schedule.FirstOrDefault(s => s.Id == scheduleEntry.Id);
            if (existingEntry == null)
                return false;

            learningPath.Schedule.Remove(existingEntry);
            learningPath.Schedule.Add(scheduleEntry);
            UpdateLearningPath(learningPath);
            UpdateScheduleInSchoolCalendar(scheduleEntry);

            return true;
        }

        public bool DeleteScheduleEntry(int learningPathId, int scheduleEntryId)
        {
            var learningPath = GetLearningPathById(learningPathId);
            if (learningPath == null)
                return false;

            var entry = learningPath.Schedule.FirstOrDefault(s => s.Id == scheduleEntryId);
            if (entry == null)
                return false;

            learningPath.Schedule.Remove(entry);
            UpdateLearningPath(learningPath);
            RemoveScheduleFromSchoolCalendar(entry);

            return true;
        }

        public int GetNextScheduleId()
        {
            int maxId = 0;

            if (_school.SchoolCalendar != null)
            {
                foreach (var calendar in _school.SchoolCalendar)
                {
                    if (calendar.ScheduleEntries != null && calendar.ScheduleEntries.Any())
                    {
                        var calendarMaxId = calendar.ScheduleEntries.Max(s => s.Id);
                        if (calendarMaxId > maxId)
                            maxId = calendarMaxId;
                    }
                }
            }

            return maxId + 1;
        }

        public ScheduleEntry? AddGeneralScheduleEntry(ScheduleEntry scheduleEntry)
        {
            var mainCalendar = _school.SchoolCalendar.FirstOrDefault();
            if (mainCalendar == null)
            {
                mainCalendar = new CalendarModel
                {
                    Id = 1,
                    Name = "Main School Calendar",
                    ScheduleEntries = new List<ScheduleEntry>()
                };
                _school.SchoolCalendar.Add(mainCalendar);
            }

            if (mainCalendar.ScheduleEntries == null)
            {
                mainCalendar.ScheduleEntries = new List<ScheduleEntry>();
            }

            if (scheduleEntry.Id > 0)
            {
                var existingSchedule = mainCalendar.ScheduleEntries.FirstOrDefault(s => s.Id == scheduleEntry.Id);
                if (existingSchedule == null)
                {
                    mainCalendar.ScheduleEntries.Add(scheduleEntry);
                }
                return scheduleEntry;
            }

            if (scheduleEntry.IsRecurring)
            {
                var recurringEntries = LogicMethods.GenerateRecurringSchedules(scheduleEntry);
                foreach (var entry in recurringEntries)
                {
                    entry.Id = GetNextScheduleId();
                    mainCalendar.ScheduleEntries.Add(entry);
                }
                return recurringEntries.FirstOrDefault();
            }
            else
            {
                scheduleEntry.Id = GetNextScheduleId();
                mainCalendar.ScheduleEntries.Add(scheduleEntry);
                return scheduleEntry;
            }
        }

        public void UpdateScheduleInSchoolCalendar(ScheduleEntry scheduleEntry)
        {
            if (_school.SchoolCalendar == null)
                return;

            foreach (var calendar in _school.SchoolCalendar)
            {
                if (calendar.ScheduleEntries != null)
                {
                    var existingIndex = calendar.ScheduleEntries.FindIndex(s => s.Id == scheduleEntry.Id);
                    if (existingIndex >= 0)
                    {
                        calendar.ScheduleEntries[existingIndex] = scheduleEntry;
                        break;
                    }
                }
            }
        }

        public void RemoveScheduleFromSchoolCalendar(ScheduleEntry scheduleEntry)
        {
            if (_school.SchoolCalendar == null)
                return;

            foreach (var calendar in _school.SchoolCalendar)
            {
                if (calendar.ScheduleEntries != null)
                {
                    var scheduleToRemove = calendar.ScheduleEntries.FirstOrDefault(s => s.Id == scheduleEntry.Id);
                    if (scheduleToRemove != null)
                    {
                        calendar.ScheduleEntries.Remove(scheduleToRemove);
                        break;
                    }
                }
            }
        }

        public bool UpdateGeneralCalendarScheduleEntry(ScheduleEntry scheduleEntry)
        {
            if (_school.SchoolCalendar == null)
                return false;

            foreach (var calendar in _school.SchoolCalendar)
            {
                if (calendar.ScheduleEntries != null)
                {
                    var existingIndex = calendar.ScheduleEntries.FindIndex(s => s.Id == scheduleEntry.Id);
                    if (existingIndex >= 0)
                    {
                        calendar.ScheduleEntries[existingIndex] = scheduleEntry;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteGeneralCalendarScheduleEntry(int scheduleEntryId)
        {
            if (_school.SchoolCalendar == null)
                return false;

            foreach (var calendar in _school.SchoolCalendar)
            {
                if (calendar.ScheduleEntries != null)
                {
                    var scheduleToRemove = calendar.ScheduleEntries.FirstOrDefault(s => s.Id == scheduleEntryId);
                    if (scheduleToRemove != null)
                    {
                        calendar.ScheduleEntries.Remove(scheduleToRemove);
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Class Sessions
        public ClassSession? GetClassSessionById(int classSessionId)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession != null && schedule.ClassSession.Id == classSessionId)
                    {
                        return schedule.ClassSession;
                    }
                }
            }
            return null;
        }

        public int GetNextClassSessionId()
        {
            int nextId = 1;
            List<ClassSession> allSessions = new();

            foreach (var lp in _school.LearningPaths)
            {
                foreach (var entry in lp.Schedule)
                {
                    if (entry.ClassSession != null)
                    {
                        allSessions.Add(entry.ClassSession);
                    }
                }
            }

            if (allSessions.Count > 0)
            {
                nextId = allSessions.Max(s => s.Id) + 1;
            }

            return nextId;
        }


        public bool UpdateClassSession(ClassSession classSession)
        {
            try
            {
                var found = false;

                foreach (var learningPath in _school.LearningPaths)
                {
                    foreach (var schedule in learningPath.Schedule)
                    {
                        if (schedule.ClassSession?.Id == classSession.Id)
                        {
                            schedule.ClassSession = classSession;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                return found;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating class session: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Homework
        public Homework? GetHomeworkById(int id)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails != null)
                    {
                        if (schedule.ClassSession.HomeworkDetails.Id == id)
                            return schedule.ClassSession.HomeworkDetails;
                    }
                }
            }
            return null;
        }

        public HomeworkSubmission? SubmitHomework(int homeworkId, Student student, string answer)
        {
            var homework = GetHomeworkById(homeworkId);
            if (homework == null)
                throw new ArgumentException($"Homework with ID {homeworkId} not found.");

            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");

            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException("Answer cannot be null or empty.", nameof(answer));

            var submission = new HomeworkSubmission
            {
                Student = student,
                Answer = answer,
                IsGraded = false,
                Homework = homework
            };

            return AddHomeworkSubmission(submission);
        }

        public void UpdateHomework(Homework homework)
        {
            if (homework == null)
                return;

            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails != null &&
                        schedule.ClassSession.HomeworkDetails.Id == homework.Id)
                    {
                        schedule.ClassSession.HomeworkDetails.Title = homework.Title;
                        schedule.ClassSession.HomeworkDetails.AssignedDate = homework.AssignedDate;
                        schedule.ClassSession.HomeworkDetails.DueDate = homework.DueDate;
                        schedule.ClassSession.HomeworkDetails.Question = homework.Question;
                        schedule.ClassSession.HomeworkDetails.Submissions = homework.Submissions;
                        return;
                    }
                }
            }
        }

        public bool DeleteHomework(int id)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails != null &&
                        schedule.ClassSession.HomeworkDetails.Id == id)
                    {
                        schedule.ClassSession.HomeworkDetails = null;
                        return true;
                    }
                }
            }
            return false;
        }

        public HomeworkSubmission? GetHomeworkSubmissionById(int id)
        {
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails != null)
                    {
                        var submission = schedule.ClassSession.HomeworkDetails.Submissions?.FirstOrDefault(s => s.Id == id);
                        if (submission != null)
                            return submission;
                    }
                }
            }
            return null;
        }

        public HomeworkSubmission? AddHomeworkSubmission(HomeworkSubmission submission)
        {
            if (submission == null)
                return null;

            var homework = GetHomeworkById(submission.Homework?.Id ?? 0);
            if (homework == null)
                return null;

            if (homework.Submissions == null)
                homework.Submissions = new List<HomeworkSubmission>();

            if (submission.Id <= 0)
            {
                submission.Id = GetNextId("HomeworkSubmission", () => GetMaxHomeworkSubmissionId());
            }

            submission.Homework = homework;
            submission.SubmissionDate = DateTime.Now;
            homework.Submissions.Add(submission);

            return submission;
        }

        private int GetMaxHomeworkSubmissionId()
        {
            int maxId = 0;
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails?.Submissions != null)
                    {
                        var localMaxId = schedule.ClassSession.HomeworkDetails.Submissions
                            .Select(s => s.Id)
                            .DefaultIfEmpty(0)
                            .Max();

                        if (localMaxId > maxId)
                            maxId = localMaxId;
                    }
                }
            }
            return maxId;
        }

        public int GetNextHomeworkId()
        {
            return GetNextId("Homework", () =>
            {
                int maxId = 0;
                foreach (var learningPath in _school.LearningPaths)
                {
                    foreach (var schedule in learningPath.Schedule)
                    {
                        if (schedule.ClassSession?.HomeworkDetails != null)
                        {
                            if (schedule.ClassSession.HomeworkDetails.Id > maxId)
                                maxId = schedule.ClassSession.HomeworkDetails.Id;
                        }
                    }
                }
                return maxId;
            });
        }

        private int GetMaxHomeworkId()
        {
            int maxId = 0;
            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.HomeworkDetails != null)
                    {
                        if (schedule.ClassSession.HomeworkDetails.Id > maxId)
                            maxId = schedule.ClassSession.HomeworkDetails.Id;
                    }
                }
            }
            return maxId;
        }

        public void UpdateHomeworkSubmission(HomeworkSubmission submission)
        {
            if (submission == null)
                return;
            var existingSubmission = GetHomeworkSubmissionById(submission.Id);
            if (existingSubmission != null)
            {
                existingSubmission.Answer = submission.Answer;
                existingSubmission.IsGraded = submission.IsGraded;
                existingSubmission.FeedbackComment = submission.FeedbackComment;
                existingSubmission.HomeworkGrade = submission.HomeworkGrade;
            }
        }
        #endregion

        #region Discussions
        public int GetNextThreadId(int classSessionId)
        {
            var classSession = GetClassSessionById(classSessionId);
            if (classSession == null || classSession.DiscussionThreads == null || !classSession.DiscussionThreads.Any())
                return 1;

            return classSession.DiscussionThreads.Max(t => t.Id) + 1;
        }

        public int GetNextPostId()
        {
            int maxId = 0;

            foreach (var learningPath in _school.LearningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession?.DiscussionThreads != null)
                    {
                        foreach (var thread in schedule.ClassSession.DiscussionThreads)
                        {
                            maxId = Math.Max(maxId, thread.FirstPost.Id);

                            if (thread.Replies != null)
                            {
                                foreach (var reply in thread.Replies)
                                {
                                    maxId = Math.Max(maxId, reply.Id);
                                }
                            }
                        }
                    }
                }
            }

            return maxId + 1;
        }

        public async Task AddDiscussionThread(DiscussionThread thread, int classSessionId)
        {
            var classSession = GetClassSessionById(classSessionId);
            if (classSession == null)
                throw new ArgumentException("Class session not found.");

            if (classSession.DiscussionThreads == null)
                classSession.DiscussionThreads = new List<DiscussionThread>();

            classSession.DiscussionThreads.Add(thread);
            await Task.CompletedTask;
        }

        public async Task UpdateDiscussionThread(DiscussionThread thread, int classSessionId)
        {
            var classSession = GetClassSessionById(classSessionId);
            if (classSession == null || classSession.DiscussionThreads == null)
                throw new ArgumentException("Class session or discussion threads not found.");

            var existingIndex = classSession.DiscussionThreads.FindIndex(t => t.Id == thread.Id);
            if (existingIndex >= 0)
            {
                classSession.DiscussionThreads[existingIndex] = thread;
            }
            else
            {
                throw new ArgumentException("Discussion thread not found.");
            }

            await Task.CompletedTask;
        }
        #endregion

        #region File Attachments
        public async Task<FileAttachment> UploadFileAsync(IBrowserFile file, string category)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be null or empty.", nameof(category));
            if (file.Size > FcmsConstants.MAX_FILE_SIZE)
                throw new InvalidOperationException($"File size exceeds the {FcmsConstants.MAX_FILE_SIZE_MB}MB limit. File size: {file.Size / FcmsConstants.BYTES_IN_MEGABYTE:F2}MB");

            var folderName = Path.GetInvalidFileNameChars()
                .Aggregate(category, (current, c) => current.Replace(c, '_'));

            var targetFolder = Path.Combine(_environment.WebRootPath, folderName);
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            var extension = Path.GetExtension(file.Name);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(targetFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream(FcmsConstants.MAX_FILE_SIZE).CopyToAsync(stream);
            }

            var publicUrl = $"/{folderName}/{uniqueFileName}";

            var attachment = new FileAttachment
            {
                Id = GetNextAttachmentId(),
                FileName = file.Name,
                FilePath = publicUrl,
                FileSize = file.Size,
                UploadDate = DateTime.Now
            };

            return attachment;
        }

        private int GetNextAttachmentId()
        {
            return _nextAttachmentId++;
        }

        public Task DeleteFileAsync(FileAttachment attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            var filePath = Path.Combine(_environment.WebRootPath, attachment.FilePath.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            foreach (var categoryDict in _attachmentReferences)
            {
                categoryDict.Value.RemoveAll(x => x.attachment.Id == attachment.Id);
            }

            return Task.CompletedTask;
        }

        public Task<List<FileAttachment>> GetAttachmentsAsync(string category, int referenceId)
        {
            if (_attachmentReferences.TryGetValue(category, out var references))
            {
                var attachments = references
                    .Where(x => x.referenceId == referenceId)
                    .Select(x => x.attachment)
                    .ToList();
                return Task.FromResult(attachments);
            }

            return Task.FromResult(new List<FileAttachment>());
        }

        public Task SaveAttachmentReferenceAsync(FileAttachment attachment, string category, int referenceId)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            if (!_attachmentReferences.ContainsKey(category))
            {
                _attachmentReferences[category] = new List<(int, FileAttachment)>();
            }

            _attachmentReferences[category].Add((referenceId, attachment));
            return Task.CompletedTask;
        }
        #endregion

        #region Payments
        public Payment AddPayment(Payment payment)
        {
            var schoolFees = GetSchoolFees(payment.SchoolFeesId);
            if (schoolFees != null)
            {
                payment.Id = GetNextPaymentId();
                schoolFees.Payments.Add(payment);

                var student = GetStudentBySchoolFeesId(payment.SchoolFeesId);
                if (student != null && student.CurrentLearningPath != null)
                {
                    LogicMethods.UpdatePaymentStatus(student, student.CurrentLearningPath);
                }
            }
            return payment;
        }

        public void UpdatePayment(Payment payment)
        {
            var schoolFees = GetSchoolFees(payment.SchoolFeesId);
            if (schoolFees != null)
            {
                var existingPayment = schoolFees.Payments.FirstOrDefault(p => p.Id == payment.Id);
                if (existingPayment != null)
                {
                    existingPayment.Amount = payment.Amount;
                    existingPayment.Date = payment.Date;
                    existingPayment.PaymentMethod = payment.PaymentMethod;
                    existingPayment.Reference = payment.Reference;
                    existingPayment.Semester = payment.Semester;
                    existingPayment.AcademicYearStart = payment.AcademicYearStart;
                    existingPayment.LearningPathId = payment.LearningPathId;

                    var student = GetStudentBySchoolFeesId(payment.SchoolFeesId);
                    if (student != null && student.CurrentLearningPath != null)
                    {
                        LogicMethods.UpdatePaymentStatus(student, student.CurrentLearningPath);
                    }
                }
            }
        }

        public void DeletePayment(int paymentId)
        {
            var school = GetSchool();
            foreach (var student in school.Students)
            {
                if (student.Person.SchoolFees?.Payments != null)
                {
                    var payment = student.Person.SchoolFees.Payments.FirstOrDefault(p => p.Id == paymentId);
                    if (payment != null)
                    {
                        student.Person.SchoolFees.Payments.Remove(payment);

                        if (student.CurrentLearningPath != null)
                        {
                            LogicMethods.UpdatePaymentStatus(student, student.CurrentLearningPath);
                        }
                        break;
                    }
                }
            }
        }

        private int GetNextPaymentId()
        {
            var school = GetSchool();
            int maxId = 0;
            foreach (var student in school.Students)
            {
                if (student.Person.SchoolFees?.Payments != null)
                {
                    foreach (var payment in student.Person.SchoolFees.Payments)
                    {
                        if (payment.Id > maxId)
                            maxId = payment.Id;
                    }
                }
            }
            return maxId + 1;
        }

        public Payment PrepareNewPayment(Student student)
        {
            var payment = new Payment { Date = DateTime.Today };

            if (student.Person.SchoolFees != null)
            {
                payment.SchoolFeesId = student.Person.SchoolFees.Id;
            }

            if (student.CurrentLearningPath != null)
            {
                payment.AcademicYearStart = student.CurrentLearningPath.AcademicYearStart;
                payment.Semester = student.CurrentLearningPath.Semester;
                payment.LearningPathId = student.CurrentLearningPath.Id;
            }

            return payment;
        }

        public SchoolFees? GetSchoolFees(int id)
        {
            var schoolFees = _schoolFees.FirstOrDefault(sf => sf.Id == id);
            if (schoolFees == null)
            {
                var student = GetStudentBySchoolFeesId(id);
                if (student != null)
                {
                    schoolFees = student.Person.SchoolFees;
                }
            }
            return schoolFees;
        }

        public int GetNextSchoolFeesId()
        {
            var existingIds = new List<int>();

            foreach (var student in GetStudents())
            {
                if (student.Person != null &&
                    student.Person.SchoolFees != null &&
                    student.Person.SchoolFees.Id > 0)
                {
                    existingIds.Add(student.Person.SchoolFees.Id);
                }
            }

            if (_schoolFees != null)
            {
                foreach (var sf in _schoolFees)
                {
                    existingIds.Add(sf.Id);
                }
            }

            return existingIds.Count > 0 ? existingIds.Max() + 1 : 1;
        }

        public Student? GetStudentBySchoolFeesId(int schoolFeesId)
        {
            var school = GetSchool();
            return school.Students.FirstOrDefault(s => s.Person.SchoolFees?.Id == schoolFeesId);
        }
        #endregion

        #region Grading
        public void SaveCourseGradingConfiguration(CourseGradingConfiguration configuration)
        {
            var learningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == configuration.LearningPathId);
            if (learningPath == null) return;

            var existingConfig = learningPath.CourseGradingConfigurations
                .FirstOrDefault(c => c.Course == configuration.Course);

            if (existingConfig != null)
            {
                existingConfig.HomeworkWeightPercentage = configuration.HomeworkWeightPercentage;
                existingConfig.QuizWeightPercentage = configuration.QuizWeightPercentage;
                existingConfig.FinalExamWeightPercentage = configuration.FinalExamWeightPercentage;
            }
            else
            {
                configuration.Id = GetNextCourseGradingConfigurationId();
                learningPath.CourseGradingConfigurations.Add(configuration);
            }

            UpdateCourseGradeConfigurations(learningPath.Id, configuration.Course, configuration);
        }

        private void UpdateCourseGradeConfigurations(int learningPathId, string course, CourseGradingConfiguration config)
        {
            var affectedCourseGrades = _school.Students
                .SelectMany(s => s.CourseGrades)
                .Where(cg => cg.LearningPathId == learningPathId && cg.Course == course)
                .ToList();

            foreach (var courseGrade in affectedCourseGrades)
            {
                courseGrade.GradingConfiguration = config;

                if (!courseGrade.IsFinalized)
                {
                    LogicMethods.RecalculateCourseGrade(courseGrade);
                }
            }
        }

        public CourseGradingConfiguration? GetCourseGradingConfiguration(int learningPathId, string courseName)
        {
            var learningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == learningPathId);
            if (learningPath == null) return null;

            return learningPath.CourseGradingConfigurations
                .FirstOrDefault(c => c.Course == courseName);
        }

        public List<CourseGradingConfiguration> GetAllCourseGradingConfigurations(int learningPathId)
        {
            var learningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == learningPathId);
            if (learningPath == null) return new List<CourseGradingConfiguration>();

            return learningPath.CourseGradingConfigurations.ToList();
        }

        public int GetNextCourseGradingConfigurationId()
        {
            return GetNextId("CourseGradingConfiguration", () =>
            {
                var allConfigurations = _school.LearningPaths
                    .SelectMany(lp => lp.CourseGradingConfigurations);
                return allConfigurations.Any() ? allConfigurations.Max(c => c.Id) : 0;
            });
        }

        public List<string> GetCoursesWithoutGradingConfiguration(int learningPathId)
        {
            var learningPath = _school.LearningPaths.FirstOrDefault(lp => lp.Id == learningPathId);
            if (learningPath == null) return new List<string>();

            var allCourses = CourseDefaults.GetCourseNames(learningPath.EducationLevel);
            var configuredCourses = learningPath.CourseGradingConfigurations.Select(c => c.Course).ToList();

            return allCourses.Except(configuredCourses).ToList();
        }

        private int GetMaxTestGradeId()
        {
            int maxId = 0;
            foreach (var student in _school.Students)
            {
                foreach (var courseGrade in student.CourseGrades)
                {
                    foreach (var testGrade in courseGrade.TestGrades)
                    {
                        if (testGrade.Id > maxId)
                            maxId = testGrade.Id;
                    }
                }
            }
            return maxId;
        }

        private int GetMaxCourseGradeId()
        {
            int maxId = 0;
            foreach (var student in _school.Students)
            {
                foreach (var courseGrade in student.CourseGrades)
                {
                    if (courseGrade.Id > maxId)
                        maxId = courseGrade.Id;
                }
            }
            return maxId;
        }

        public List<GradesReport> GetGradesReports(string academicYear, string semester)
        {
            return LogicMethods.GetGradesReports(_school, academicYear, semester);
        }

        public int GetNextTestGradeId()
        {
            return GetNextId("TestGrade", () => GetMaxTestGradeId());
        }

        public int GetNextCourseGradeId()
        {
            return GetNextId("CourseGrade", () => GetMaxCourseGradeId());
        }
        #endregion

        #region Curriculum
        public List<Curriculum> GetFullCurriculum()
        {
            var learningPaths = GetAllLearningPaths().ToList();
            return LogicMethods.GenerateCurriculumFromLearningPaths(learningPaths);
        }

        public List<Curriculum> FilterCurriculum(
                                 List<Curriculum> curriculum,
                                 EducationLevel educationLevel,
                                 ClassLevel classLevel,
                                 Semester? semester = null
                             )
        {

            var filteredCurricula = curriculum
                .Where(c => c.EducationLevel == educationLevel && c.ClassLevel == classLevel)
                .Select(c => new Curriculum
                {
                    AcademicYear = c.AcademicYear,
                    EducationLevel = c.EducationLevel,
                    ClassLevel = c.ClassLevel,
                    Semesters = semester == null
                        ? c.Semesters
                        : c.Semesters
                            .Where(s => s.Semester == semester)
                            .Select(s => new SemesterCurriculum
                            {
                                Semester = s.Semester,
                                ClassSessions = s.ClassSessions
                            }).ToList()
                })
                .ToList();

            return filteredCurricula;
        }
        #endregion

        #region Attendance
        // Save attendance for a learning path
        public DailyAttendanceLogEntry SaveAttendance(int learningPathId, List<int> presentStudentIds, int teacherId, DateTime? attendanceDate = null)
        {
            var learningPath = GetLearningPathById(learningPathId);
            if (learningPath == null)
                throw new ArgumentException($"Learning path with ID {learningPathId} not found.");

            var teacher = GetStaffById(teacherId);
            if (teacher == null)
                throw new ArgumentException($"Teacher with ID {teacherId} not found.");

            var presentStudents = learningPath.Students
                .Where(s => presentStudentIds.Contains(s.Id))
                .ToList();

            return LogicMethods.TakeAttendanceForLearningPath(learningPath, presentStudents, teacher, attendanceDate);
        }

        // Check if attendance has been taken for a specific date
        public bool HasAttendanceBeenTaken(int learningPathId, DateTime date)
        {
            var learningPath = GetLearningPathById(learningPathId);
            if (learningPath?.AttendanceLog == null)
                return false;

            return learningPath.AttendanceLog
                .Any(log => log.TimeStamp.Date == date.Date);
        }
        #endregion

        #region Archives
        public void ArchiveStudent(Student student)
        {
            if (student == null) return;

            foreach (var learningPath in _school.LearningPaths)
            {
                learningPath.Students.RemoveAll(s => s.Id == student.Id);
                learningPath.StudentsWithAccess.RemoveAll(s => s.Id == student.Id);
            }

            _school.Students.RemoveAll(s => s.Id == student.Id);

            student.Person.IsArchived = true;
            student.ArchivedDate = DateTime.Now;
            _archivedStudents.Add(student);

            // TODO: 
            // 1. Create an Archive database table
            // 2. Move student data to archive table
            // 4. Send graduation notification
            // 5. Update school statistics
        }

        public List<Student> GetArchivedStudents()
        {
            return _archivedStudents.ToList();
        }
        #endregion  
    }
}