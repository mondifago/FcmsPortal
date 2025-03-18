using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal.Services
{
    public interface ISchoolDataService
    {
        School GetSchool();
        IEnumerable<Student> GetStudents();
        IEnumerable<Staff> GetStaff();
        IEnumerable<Guardian> GetGuardians();
        IEnumerable<Student> GetStudentsByClassLevel(ClassLevel classLevel);
        IEnumerable<Staff> GetTeachersByEducationLevel(EducationLevel educationLevel);
        Guardian GetGuardianById(int id);
        void UpdateGuardian(Guardian guardian);
        Task<bool> DeleteStudent(int studentId, bool isHardDelete = false);
        Task<bool> DeleteStaff(int staffId, bool isHardDelete = false);
        Task<bool> DeleteGuardian(int guardianId, bool isHardDelete = false);
    }

    public class SchoolDataService : ISchoolDataService
    {
        private readonly School _school;

        public SchoolDataService()
        {
            _school = FcmsPortal.Program.CreateTestSchool();
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

        public void UpdateGuardian(Guardian guardian)
        {
            var existingGuardian = _school.Guardians.FirstOrDefault(g => g.Id == guardian.Id);
            if (existingGuardian != null)
            {
                // Copy properties from updated guardian to existing one
                existingGuardian.Person.FirstName = guardian.Person.FirstName;
                existingGuardian.Person.MiddleName = guardian.Person.MiddleName;
                existingGuardian.Person.LastName = guardian.Person.LastName;
                existingGuardian.Occupation = guardian.Occupation;
                existingGuardian.RelationshipToStudent = guardian.RelationshipToStudent;
                // Copy other properties as needed
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

        public async Task<Staff> GetStaffById(int staffId)
        {
            return await Task.FromResult(_school.Staff.FirstOrDefault(s => s.Id == staffId));
        }

        public async Task<Staff> AddStaff(Staff staff)
        {
            if (staff.Id <= 0)
            {
                staff.Id = _school.Staff.Any() ? _school.Staff.Max(s => s.Id) + 1 : 1;
            }

            var staffList = _school.Staff.ToList();
            staffList.Add(staff);
            _school.Staff = staffList;

            return await Task.FromResult(staff);
        }

        public async Task<Staff> UpdateStaff(Staff staff)
        {
            var existingStaff = _school.Staff.FirstOrDefault(s => s.Id == staff.Id);
            if (existingStaff != null)
            {
                existingStaff.Person.FirstName = staff.Person.FirstName;
                existingStaff.Person.MiddleName = staff.Person.MiddleName;
                existingStaff.Person.LastName = staff.Person.LastName;
                existingStaff.Person.DateOfBirth = staff.Person.DateOfBirth;
                existingStaff.Person.Sex = staff.Person.Sex;
                existingStaff.Person.EducationLevel = staff.Person.EducationLevel;
                existingStaff.JobRole = staff.JobRole;
                existingStaff.DateOfEmployment = staff.DateOfEmployment;
                //existingStaff.Salary = staff.Salary;
                existingStaff.Person.IsActive = staff.Person.IsActive;
                // Update other properties as needed
            }

            return await Task.FromResult(existingStaff);
        }

        public async Task<bool> DeleteStaff(int staffId, bool isHardDelete = false)
        {
            var staff = _school.Staff.FirstOrDefault(s => s.Id == staffId);
            if (staff == null)
            {
                return await Task.FromResult(false);
            }

            if (isHardDelete)
            {
                var staffList = _school.Staff.ToList();
                staffList.Remove(staff);
                _school.Staff = staffList;
            }
            else
            {
                staff.Person.IsActive = false;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteStudent(int studentId, bool isHardDelete = false)
        {
            var student = _school.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return await Task.FromResult(false);
            }

            if (isHardDelete)
            {
                var studentList = _school.Students.ToList();
                studentList.Remove(student);
                _school.Students = studentList;
            }
            else
            {
                student.Person.IsActive = false;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteGuardian(int guardianId, bool isHardDelete = false)
        {
            var guardian = _school.Guardians.FirstOrDefault(g => g.Id == guardianId);
            if (guardian == null)
            {
                return await Task.FromResult(false);
            }

            if (isHardDelete)
            {
                var guardianList = _school.Guardians.ToList();
                guardianList.Remove(guardian);
                _school.Guardians = guardianList;
            }
            else
            {
                guardian.Person.IsActive = false;
            }

            return await Task.FromResult(true);
        }
    }
}
