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
    }

    public class SchoolDataService : ISchoolDataService
    {
        private readonly School _school;

        public SchoolDataService(School school)
        {
            _school = school;
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
    }
}
