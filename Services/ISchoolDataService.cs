﻿using FcmsPortal.Constants;
using FcmsPortal.Enums;
using FcmsPortal.Models;
using Microsoft.AspNetCore.Components.Forms;
namespace FcmsPortal.Services
{
    public interface ISchoolDataService
    {
        School GetSchool();
        Staff AddStaff(Staff staff);
        Guardian AddGuardian(Guardian guardian);
        Student AddStudent(Student student);
        LearningPath AddLearningPath(LearningPath learningPath);
        IEnumerable<Student> GetStudents();
        IEnumerable<Staff> GetStaff();
        IEnumerable<Guardian> GetGuardians();
        IEnumerable<Student> GetStudentsByClassLevel(ClassLevel classLevel);
        IEnumerable<Staff> GetTeachersByEducationLevel(EducationLevel educationLevel);
        Guardian GetGuardianById(int id);
        Staff GetStaffById(int id);
        Student GetStudentById(int id);
        Guardian GetGuardianByStudentId(int studentId);
        void UpdateSchool(School updatedSchool);
        void UpdateGuardian(Guardian guardian);
        void UpdateStaff(Staff staff);
        void UpdateStudent(Student student);
        void UpdateLearningPath(LearningPath learningPath);
        bool DeleteStudent(int studentId);
        bool DeleteStaff(int staffId);
        bool DeleteGuardian(int guardianId);

        Task<int> GetNextThreadId();
        Task<int> GetNextPostId();
        Task<int> GetNextAttachmentId();
        Task AddDiscussionThread(DiscussionThread thread);
        Task UpdateDiscussionThread(DiscussionThread thread);
        Task<DiscussionThread> GetDiscussionThread(int id);
        Task<FileAttachment> UploadFileAsync(IBrowserFile file, string category);
        Task DeleteFileAsync(FileAttachment attachment);
        Task<List<FileAttachment>> GetAttachmentsAsync(string category, int referenceId);
        Task SaveAttachmentReferenceAsync(FileAttachment attachment, string category, int referenceId);
        string GetFileUrl(FileAttachment attachment);
        IEnumerable<LearningPath> GetAllLearningPaths();
        LearningPath GetLearningPathById(int id);
        bool DeleteLearningPath(int id);

    }

    public class SchoolDataService : ISchoolDataService
    {
        private readonly School _school;
        private readonly IWebHostEnvironment _environment;
        private readonly Dictionary<string, List<(int referenceId, FileAttachment attachment)>> _attachmentReferences = new();
        private int _nextAttachmentId = 1;

        public SchoolDataService(IWebHostEnvironment environment)
        {
            _school = FcmsPortal.Program.CreateSchool();
            _environment = environment;
        }

        public School GetSchool() => _school;

        public IEnumerable<Student> GetStudents() => _school.Students;

        public IEnumerable<Staff> GetStaff() => _school.Staff;

        public IEnumerable<Guardian> GetGuardians() => _school.Guardians;

        public IEnumerable<Student> GetStudentsByClassLevel(ClassLevel classLevel)
        {
            return _school.Students.Where(s => s.Person.ClassLevel == classLevel);
        }

        public IEnumerable<Staff> GetTeachersByEducationLevel(EducationLevel educationLevel)
        {
            return _school.Staff.Where(s =>
                s.JobRole == JobRole.Teacher &&
                s.Person.EducationLevel == educationLevel);
        }

        public Guardian GetGuardianById(int id)
        {
            return _school.Guardians.FirstOrDefault(g => g.Id == id);
        }

        public Staff GetStaffById(int id)
        {
            return _school.Staff.FirstOrDefault(s => s.Id == id);
        }

        public Student GetStudentById(int id)
        {
            return _school.Students.FirstOrDefault(s => s.Id == id);
        }

        public Guardian GetGuardianByStudentId(int studentId)
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

        public Task<int> GetNextThreadId()
        {
            int nextId = _school.DiscussionThreads.Any() ? _school.DiscussionThreads.Max(t => t.Id) + 1 : 1;
            return Task.FromResult(nextId);
        }

        public Task<int> GetNextPostId()
        {
            int nextId = 1;
            if (_school.DiscussionThreads.Any())
            {
                var allPosts = _school.DiscussionThreads.Select(t => t.FirstPost)
                    .Concat(_school.DiscussionThreads.SelectMany(t => t.Replies));
                nextId = allPosts.Any() ? allPosts.Max(p => p.Id) + 1 : 1;
            }
            return Task.FromResult(nextId);
        }

        /*public Task<int> GetNextAttachmentId()
        {
            int nextId = 1;
            if (_school.DiscussionThreads.Any())
            {
                var allAttachments = _school.DiscussionThreads.SelectMany(t => t.Attachments);
                nextId = allAttachments.Any() ? allAttachments.Max(a => a.Id) + 1 : 1;
            }
            return Task.FromResult(nextId);
        }*/

        public Task AddDiscussionThread(DiscussionThread thread)
        {
            var threads = _school.DiscussionThreads;
            if (!threads.Any(t => t.Id == thread.Id))
            {
                threads.Add(thread);
            }
            return Task.CompletedTask;
        }

        public Task UpdateDiscussionThread(DiscussionThread thread)
        {
            var existingThread = _school.DiscussionThreads.FirstOrDefault(t => t.Id == thread.Id);
            if (existingThread != null)
            {
                // Replace the entire thread object (simpler for in-memory)
                int index = _school.DiscussionThreads.IndexOf(existingThread);
                _school.DiscussionThreads[index] = thread;
            }
            return Task.CompletedTask;
        }

        public Task<DiscussionThread> GetDiscussionThread(int id)
        {
            var thread = _school.DiscussionThreads.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(thread);
        }

        public async Task<FileAttachment> UploadFileAsync(IBrowserFile file, string category)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be null or empty.", nameof(category));

            // Sanitize category name for folder path (remove invalid chars)
            var folderName = Path.GetInvalidFileNameChars()
                .Aggregate(category, (current, c) => current.Replace(c, '_'));

            // Create destination folder within wwwroot
            var targetFolder = Path.Combine(_environment.WebRootPath, folderName);
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            // Generate unique filename to prevent overwrites
            var extension = Path.GetExtension(file.Name);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(targetFolder, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream(FcmsConstants.MAX_FILE_SIZE).CopyToAsync(stream);
            }

            // Create public URL path
            var publicUrl = $"/{folderName}/{uniqueFileName}";

            // Create file attachment record
            var attachment = new FileAttachment
            {
                Id = await GetNextAttachmentId(),
                FileName = file.Name,
                FilePath = publicUrl,
                FileSize = file.Size,
                UploadDate = DateTime.Now
            };

            return attachment;
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

            // Remove from attachment references
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
            if (!_attachmentReferences.ContainsKey(category))
            {
                _attachmentReferences[category] = new List<(int, FileAttachment)>();
            }

            _attachmentReferences[category].Add((referenceId, attachment));
            return Task.CompletedTask;
        }

        public string GetFileUrl(FileAttachment attachment)
        {
            return attachment?.FilePath;
        }

        public Task<int> GetNextAttachmentId()
        {
            return Task.FromResult(_nextAttachmentId++);
        }

        public IEnumerable<LearningPath> GetLearningPaths() => _school.LearningPath;

        public LearningPath GetLearningPathById(int id)
        {
            return _school.LearningPath.FirstOrDefault(lp => lp.Id == id);
        }

        public IEnumerable<LearningPath> GetAllLearningPaths()
        {
            return _school.LearningPath;
        }

        public bool DeleteLearningPath(int id)
        {
            var learningPath = _school.LearningPath.FirstOrDefault(lp => lp.Id == id);
            if (learningPath == null)
            {
                return false;
            }
            var learningPaths = _school.LearningPath.ToList();
            learningPaths.Remove(learningPath);
            _school.LearningPath = learningPaths;
            return true;
        }

        public LearningPath AddLearningPath(LearningPath learningPath)
        {
            if (learningPath.Id <= 0)
            {
                learningPath.Id = _school.LearningPath.Any() ? _school.LearningPath.Max(lp => lp.Id) + 1 : 1;
            }
            var learningPaths = _school.LearningPath.ToList();
            learningPaths.Add(learningPath);
            _school.LearningPath = learningPaths;
            return learningPath;
        }

        public void UpdateLearningPath(LearningPath learningPath)
        {
            var existingLearningPath = _school.LearningPath.FirstOrDefault(lp => lp.Id == learningPath.Id);
            if (existingLearningPath != null)
            {
                existingLearningPath.EducationLevel = learningPath.EducationLevel;
                existingLearningPath.ClassLevel = learningPath.ClassLevel;
                existingLearningPath.Semester = learningPath.Semester;
                existingLearningPath.FeePerSemester = learningPath.FeePerSemester;
                existingLearningPath.AcademicYearStart = learningPath.AcademicYearStart;
                existingLearningPath.ApprovalStatus = learningPath.ApprovalStatus;
                existingLearningPath.Students = learningPath.Students;
                existingLearningPath.StudentsPaymentSuccessful = learningPath.StudentsPaymentSuccessful;
            }
        }
    }
}