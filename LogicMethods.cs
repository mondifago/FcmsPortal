using FcmsPortal.Constants;
using FcmsPortal.Enums;
using FcmsPortal.Models;
using System.Text.Json;

namespace FcmsPortal;

public static class LogicMethods
{
    /// <summary>
    /// Methods involved in Initial Setup
    /// </summary>
    // Filter students based on specified education level and class level
    public static List<Student> GetStudentsByLevel(School school, EducationLevel educationLevel, ClassLevel classLevel)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        return school.Students
            .Where(student => student.Person.EducationLevel == educationLevel
                              && student.Person.ClassLevel == classLevel)
            .ToList();
    }

    //filter staff based on specified education level
    public static List<Staff> GetStaffByEducationLevel(School school, EducationLevel educationLevel)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        return school.Staff
            .Where(staff => staff.Person.EducationLevel == educationLevel).ToList();
    }

    //filter Teachers based on specified education level
    public static List<Staff> GetTeachersByEducationLevel(School school, EducationLevel educationLevel)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        return school.Staff
            .Where(staff => staff.JobRole == JobRole.Teacher &&
                            staff.Person.EducationLevel == educationLevel)
            .ToList();
    }

    //Gets a list of all distinct EducationLevels found in the school's LearningPaths
    public static List<EducationLevel> GetExistingEducationLevels(School school)
    {
        if (school?.LearningPath == null || !school.LearningPath.Any())
            return new List<EducationLevel>();

        return school.LearningPath
            .Select(lp => lp.EducationLevel)
            .Distinct()
            .OrderBy(el => el)
            .ToList();
    }

    //Gets a list of all distinct ClassLevels found in the school's LearningPaths
    public static List<ClassLevel> GetExistingClassLevels(this School school)
    {
        if (school?.LearningPath == null || !school.LearningPath.Any())
            return new List<ClassLevel>();

        return school.LearningPath
            .Select(lp => lp.ClassLevel)
            .Distinct()
            .OrderBy(cl => cl)
            .ToList();
    }

    //Get a list of all distinct ClassLevels for a specific EducationLevel from the ClassLevelMapping service 
    public static List<ClassLevel> GetAvailableClassLevels(EducationLevel educationLevel)
    {
        var classLevelMappingService = new ClassLevelMapping();
        var classLevelMappings = classLevelMappingService.GetClassLevelsByEducationLevel();

        if (educationLevel == EducationLevel.None)
            return new List<ClassLevel>();

        return classLevelMappings.TryGetValue(educationLevel, out var levels)
            ? levels
            : new List<ClassLevel>();
    }

    /// <summary>
    /// Gets a list of all distinct ClassLevels for a specific EducationLevel from the school's Existing LearningPath
    /// </summary>
    /// <param name="school">The school to analyze</param>
    /// <param name="educationLevel">The education level to filter by</param>
    /// <returns>List of distinct ClassLevels for the specified EducationLevel</returns>
    public static List<ClassLevel> GetClassLevelsByEducationLevel(this School school, EducationLevel educationLevel)
    {
        if (school?.LearningPath == null || !school.LearningPath.Any())
            return new List<ClassLevel>();

        return school.LearningPath
            .Where(lp => lp.EducationLevel == educationLevel)
            .Select(lp => lp.ClassLevel)
            .Distinct()
            .OrderBy(cl => cl)
            .ToList();
    }


    //Get all teachers from school list of staff
    public static List<Staff> GetAllTeachers(School school)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null");
        }

        if (school.Staff == null)
        {
            throw new ArgumentException("School staff list is not initialized", nameof(school));
        }
        try
        {
            return school.Staff
                .Where(staff => staff != null && staff.JobRole == JobRole.Teacher)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving teachers: {ex.Message}");
            throw;
        }
    }

    //Adding a schedule entry to learning path
    public static void AddAScheduleToLearningPath(LearningPath learningPath, ScheduleEntry scheduleEntry)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        if (scheduleEntry == null)
        {
            throw new ArgumentNullException(nameof(scheduleEntry), "Schedule entry cannot be null.");
        }

        if (scheduleEntry.ClassSession == null)
        {
            throw new ArgumentNullException(nameof(scheduleEntry.ClassSession), "Class session cannot be null.");
        }
        if (learningPath.Schedule.Any(s => s.Id == scheduleEntry.Id))
        {
            throw new ArgumentException($"A schedule with Id {scheduleEntry.Id} already exists in the learning path.");
        }
        bool hasOverlap = learningPath.Schedule.Any(existing =>
            existing.DateTime < scheduleEntry.DateTime.Add(scheduleEntry.Duration) &&
            scheduleEntry.DateTime < existing.DateTime.Add(existing.Duration));

        if (hasOverlap)
        {
            throw new InvalidOperationException("This schedule overlaps with an existing class session.");
        }
        bool sameTimePeriod = learningPath.Schedule.Any(existing =>
            existing.DateTime == scheduleEntry.DateTime &&
            existing.Duration == scheduleEntry.Duration);

        if (sameTimePeriod)
        {
            throw new InvalidOperationException("A schedule with the same time period already exists.");
        }
        learningPath.Schedule.Add(scheduleEntry);
        Console.WriteLine($"Schedule with Id {scheduleEntry.Id} has been successfully added to Learning Path Id {learningPath.Id}.");
    }

    //Adding multiple schedules to a learning path
    public static void AddMultipleSchedulesToLearningPath(LearningPath learningPath, List<ScheduleEntry> scheduleEntries)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        if (scheduleEntries == null || !scheduleEntries.Any())
        {
            throw new ArgumentNullException(nameof(scheduleEntries), "Schedule entries cannot be null or empty.");
        }
        foreach (var scheduleEntry in scheduleEntries)
        {
            if (scheduleEntry == null)
            {
                throw new ArgumentNullException(nameof(scheduleEntry), "One or more schedule entries are null.");
            }

            if (scheduleEntry.ClassSession == null)
            {
                throw new ArgumentNullException(nameof(scheduleEntry.ClassSession), "Class session cannot be null.");
            }

            if (learningPath.Schedule.Any(s => s.Id == scheduleEntry.Id))
            {
                throw new ArgumentException($"A schedule with Id {scheduleEntry.Id} already exists in the learning path.");
            }

            bool hasOverlap = learningPath.Schedule.Any(existing =>
                existing.DateTime < scheduleEntry.DateTime.Add(scheduleEntry.Duration) &&
                scheduleEntry.DateTime < existing.DateTime.Add(existing.Duration));

            if (hasOverlap)
            {
                throw new InvalidOperationException($"Schedule Id {scheduleEntry.Id} overlaps with an existing class session.");
            }

            bool sameTimePeriod = learningPath.Schedule.Any(existing =>
                existing.DateTime == scheduleEntry.DateTime &&
                existing.Duration == scheduleEntry.Duration);

            if (sameTimePeriod)
            {
                throw new InvalidOperationException($"A schedule with the same time period as Id {scheduleEntry.Id} already exists.");
            }
        }

        learningPath.Schedule.AddRange(scheduleEntries);
        Console.WriteLine($"{scheduleEntries.Count} schedules have been successfully added to Learning Path Id {learningPath.Id}.");
    }

    //Add learning path to school
    public static void AddLearningPathToSchool(School school, LearningPath learningPath)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        bool isDuplicatePath = school.LearningPath.Any(existingPath =>
            existingPath.EducationLevel == learningPath.EducationLevel &&
            existingPath.ClassLevel == learningPath.ClassLevel &&
            existingPath.Semester == learningPath.Semester);

        if (isDuplicatePath)
        {
            throw new InvalidOperationException("A learning path with the same Education Level, Class Level, and Semester already exists in the school.");
        }

        foreach (var student in learningPath.Students)
        {
            bool isStudentInAnotherPath = school.LearningPath.Any(existingPath =>
                existingPath.Students.Any(s => s.Id == student.Id));

            if (isStudentInAnotherPath)
            {
                throw new InvalidOperationException($"Student Id {student.Id} already belongs to another learning path.");
            }
        }
        school.LearningPath.Add(learningPath);

    }

    //Add student to school, which automatically adds guardian as well if the guardian is not previously added
    public static void AddStudentToSchool(School school, Student student)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");
        }

        if (school.Students.Any(s => s.Id == student.Id))
        {
            throw new ArgumentException($"Student with Id {student.Id} is already registered in the school.");
        }

        if (school.LearningPath.Any(lp => lp.Students.Any(s => s.Id == student.Id)))
        {
            throw new ArgumentException($"Student with Id {student.Id} is already enrolled in a learning path.");
        }

        if (student.Guardian != null && !school.Guardians.Any(g => g.Id == student.Guardian.Id))
        {
            school.Guardians.Add(student.Guardian);
        }
        school.Students.Add(student);
    }

    //Add student to Guardian Wards list
    public static void AddStudentToGuardianWards(Guardian guardian, Student student)
    {
        if (guardian == null)
        {
            throw new ArgumentNullException(nameof(guardian), "Guardian cannot be null.");
        }
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");
        }
        if (guardian.Wards.Any(s => s.Id == student.Id))
        {
            throw new ArgumentException($"Student with Id {student.Id} is already a ward of the guardian.");
        }
        guardian.Wards.Add(student);
    }

    //Remove student from Guardian Wards list
    public static void RemoveStudentFromGuardianWards(Guardian guardian, Student student)
    {
        if (guardian == null)
        {
            throw new ArgumentNullException(nameof(guardian), "Guardian cannot be null.");
        }
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");
        }
        if (!guardian.Wards.Any(s => s.Id == student.Id))
        {
            throw new ArgumentException($"Student with Id {student.Id} is not a ward of the guardian.");
        }
        guardian.Wards.Remove(student);
    }


    //retrieve all guardians registered to school
    public static List<Guardian> GetAllGuardians(School school)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (school.Guardians == null || !school.Guardians.Any())
        {
            Console.WriteLine("No guardians found in the school.");
            return new List<Guardian>();
        }
        return school.Guardians;
    }


    //Add Staff newly created staff to school
    public static void AddStaffToSchool(School school, Staff staff)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (staff == null)
        {
            throw new ArgumentNullException(nameof(staff), "Staff cannot be null.");
        }

        if (school.Staff.Any(s => s.Person.Email == staff.Person.Email))
        {
            throw new InvalidOperationException($"Staff with email {staff.Person.Email} is already registered in the school.");
        }
        school.Staff.Add(staff);
    }

    /// <summary>
    /// Methods involved in Scheduling
    /// </summary>
    // Method to Generate Recurring Schedule Entries
    public static List<ScheduleEntry> GenerateRecurringSchedules(ScheduleEntry baseEntry)
    {
        var schedules = new List<ScheduleEntry>();

        // Return the base entry if it's not recurring
        if (!baseEntry.IsRecurring)
        {
            schedules.Add(baseEntry);
            return schedules;
        }

        // Calculate recurring dates
        DateTime currentDate = baseEntry.DateTime;
        while (currentDate <= baseEntry.EndDate)
        {
            // Create a new schedule entry for each recurrence
            var newEntry = new ScheduleEntry
            {
                Id = baseEntry.Id, // Assign new Id if necessary
                DateTime = currentDate,
                Duration = baseEntry.Duration,
                Venue = baseEntry.Venue,
                ClassSession = baseEntry.ClassSession,
                Title = baseEntry.Title,
                Event = baseEntry.Event,
                Meeting = baseEntry.Meeting,
                Notes = baseEntry.Notes,
                IsRecurring = false, // Set to false for generated instances
            };

            schedules.Add(newEntry);

            // Update date based on recurrence pattern
            currentDate = baseEntry.RecurrencePattern switch
            {
                RecurrenceType.Daily => currentDate.AddDays(baseEntry.RecurrenceInterval),
                RecurrenceType.Weekly => currentDate.AddDays(7 * baseEntry.RecurrenceInterval),
                RecurrenceType.Monthly => currentDate.AddMonths(baseEntry.RecurrenceInterval),
                _ => currentDate
            };
        }

        return schedules;
    }

    //Method to display school calendar entries
    public static void DisplayAllCalendarEntries(School fcmSchool)
    {
        // Check if the school or its calendar is null
        if (fcmSchool == null || fcmSchool.SchoolCalendar == null || !fcmSchool.SchoolCalendar.Any())
        {
            Console.WriteLine("No calendar entries available.");
            return;
        }

        Console.WriteLine("Calendar Entries:");

        // Iterate through each calendar in the school calendar
        foreach (var calendar in fcmSchool.SchoolCalendar)
        {
            Console.WriteLine($"\nCalendar: {calendar.Name} (Id: {calendar.Id})");

            // Check if the calendar has schedule entries
            if (calendar.ScheduleEntries == null || !calendar.ScheduleEntries.Any())
            {
                Console.WriteLine("  No schedule entries in this calendar.");
                continue;
            }

            // Display each schedule entry
            foreach (var entry in calendar.ScheduleEntries)
            {
                string entryType = entry.GetScheduleType().ToString();
                DateTime endTime = entry.DateTime.Add(entry.Duration);

                Console.WriteLine($"  Id: {entry.Id}, Date: {entry.DateTime.ToShortDateString()}, " +
                                  $"Type: {entryType}, Start: {entry.DateTime.ToShortTimeString()}, " +
                                  $"End: {endTime.ToShortTimeString()}, Duration: {entry.Duration}, Recurrence: {entry.IsRecurring}, Recurring Type: {entry.RecurrencePattern}, Recurrence interval: {entry.RecurrenceInterval}");
            }
        }
    }

    //To get all schedules in a learning path
    public static List<ScheduleEntry> GetAllSchedulesInLearningPath(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        if (learningPath.Schedule == null || !learningPath.Schedule.Any())
        {
            return new List<ScheduleEntry>();
        }

        return learningPath.Schedule
        .OrderBy(s => s.DateTime)
        .ToList();
    }

    //Get all schedules of a learning path for a particular date
    public static List<ScheduleEntry> GetSchedulesByDateInLearningPath(LearningPath learningPath, DateTime date)
    {
        if (learningPath?.Schedule == null)
            return new List<ScheduleEntry>();

        return learningPath.Schedule
            .Where(s => s.DateTime.Date == date.Date)
            .OrderBy(s => s.DateTime.TimeOfDay)
            .ToList();
    }

    //Retrieves the schedule entry that contains a specific class session
    public static ScheduleEntry? GetScheduleEntryForClassSession(School school, int classSessionId)
    {
        if (school?.LearningPath == null)
            return null;

        foreach (var learningPath in school.LearningPath)
        {
            if (learningPath.Schedule == null)
                continue;

            foreach (var schedule in learningPath.Schedule)
            {
                if (schedule.ClassSession?.Id == classSessionId)
                {
                    return schedule;
                }
            }
        }

        // Ensure all code paths return a value
        return null;
    }


    //Get all class sessions in a learning path
    public static List<ClassSession> GetClassSessionsInLearningPath(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        if (learningPath.Schedule == null || !learningPath.Schedule.Any())
        {
            return new List<ClassSession>();
        }

        return learningPath.Schedule
            .Where(schedule => schedule.ClassSession != null)
            .Select(schedule => schedule.ClassSession)
            .ToList();
    }

    //Get all meetings on a particular date from any calendar
    public static List<ScheduleEntry> GetAllMeetingsByDate(Calendar calendar, DateTime date)
    {
        if (calendar == null || calendar.ScheduleEntries == null || !calendar.ScheduleEntries.Any())
        {
            Console.WriteLine("The calendar is empty or null.");
            return new List<ScheduleEntry>();
        }

        var meetings = calendar.ScheduleEntries
            .Where(entry => !string.IsNullOrEmpty(entry.Meeting) && entry.DateTime.Date == date.Date)
            .ToList();

        return meetings;
    }

    //Get all events on a particular date from any calendar
    public static List<ScheduleEntry> GetAllEventsByDate(Calendar calendar, DateTime date)
    {
        if (calendar == null || calendar.ScheduleEntries == null || !calendar.ScheduleEntries.Any())
        {
            Console.WriteLine("The calendar is empty or null.");
            return new List<ScheduleEntry>();
        }

        var events = calendar.ScheduleEntries
            .Where(entry => !string.IsNullOrEmpty(entry.Event) && entry.DateTime.Date == date.Date)
            .ToList();

        return events;
    }

    //Post meeting to all staff
    public static void PostMeetingToStaff(School school, ScheduleEntry meeting)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (meeting == null)
        {
            throw new ArgumentNullException(nameof(meeting), "Meeting cannot be null.");
        }

        if (string.IsNullOrEmpty(meeting.Meeting))
        {
            throw new ArgumentException("The provided ScheduleEntry is not a meeting.");
        }

        if (school.Staff == null || !school.Staff.Any())
        {
            Console.WriteLine("No staff available to post the meeting.");
            return;
        }

        foreach (var staff in school.Staff)
        {
            if (staff.Person.PersonalCalendar == null)
            {
                staff.Person.PersonalCalendar = new Calendar
                {
                    Id = staff.Id,
                    Name = $"{staff.Person.FirstName} {staff.Person.LastName}'s Calendar",
                    ScheduleEntries = new List<ScheduleEntry>()
                };
            }

            // Check if the meeting already exists in the staff's calendar
            if (!staff.Person.PersonalCalendar.ScheduleEntries.Any(se => se.Id == meeting.Id))
            {
                staff.Person.PersonalCalendar.ScheduleEntries.Add(meeting);
            }
        }
        Console.WriteLine($"Meeting '{meeting.Meeting}' has been posted to all staff calendars.");
    }

    //post meeting to guardians
    public static void PostMeetingToGuardian(School school, ScheduleEntry meeting)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (meeting == null)
        {
            throw new ArgumentNullException(nameof(meeting), "Meeting cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(meeting.Title) || meeting.DateTime == default || meeting.Duration == default)
        {
            throw new ArgumentException("Meeting must have a valid title, date, and duration.");
        }

        if (school.Guardians == null || school.Guardians.Count == 0)
        {
            Console.WriteLine("No guardians found in the school to post the meeting to.");
            return;
        }

        foreach (var guardian in school.Guardians)
        {
            if (guardian.Person.PersonalCalendar == null)
            {
                guardian.Person.PersonalCalendar = new Calendar
                {
                    Id = guardian.Id,
                    Name = $"{guardian.Person.LastName}'s Calendar"
                };
            }
            guardian.Person.PersonalCalendar.ScheduleEntries.Add(meeting);
        }

        Console.WriteLine($"Meeting '{meeting.Title}' has been successfully posted to all guardians.");
    }

    //Post event to all
    public static void PostEventToAll(School school, ScheduleEntry eventEntry)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }

        if (eventEntry == null)
        {
            throw new ArgumentNullException(nameof(eventEntry), "Event entry cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(eventEntry.Title) || eventEntry.DateTime == default || eventEntry.Duration == default)
        {
            throw new ArgumentException("Event must have a valid title, date, and duration.");
        }

        if (school.Students != null && school.Students.Any())
        {
            foreach (var student in school.Students)
            {
                if (student.Person.PersonalCalendar == null)
                {
                    student.Person.PersonalCalendar = new Calendar
                    {
                        Id = student.Id,
                        Name = $"{student.Person.FirstName} {student.Person.LastName}'s Calendar"
                    };
                }
                student.Person.PersonalCalendar.ScheduleEntries.Add(eventEntry);
            }
        }

        if (school.Staff != null && school.Staff.Any())
        {
            foreach (var staff in school.Staff)
            {
                if (staff.Person.PersonalCalendar == null)
                {
                    staff.Person.PersonalCalendar = new Calendar
                    {
                        Id = staff.Id,
                        Name = $"{staff.Person.LastName}'s Calendar"
                    };
                }
                staff.Person.PersonalCalendar.ScheduleEntries.Add(eventEntry);
            }
        }

        if (school.Guardians != null && school.Guardians.Any())
        {
            foreach (var guardian in school.Guardians)
            {
                if (guardian.Person.PersonalCalendar == null)
                {
                    guardian.Person.PersonalCalendar = new Calendar
                    {
                        Id = guardian.Id,
                        Name = $"{guardian.Person.LastName}'s Calendar"
                    };
                }
                guardian.Person.PersonalCalendar.ScheduleEntries.Add(eventEntry);
            }
        }

        Console.WriteLine($"Event '{eventEntry.Title}' has been successfully posted to all students, staff, and guardians.");
    }

    //display all students in a learning path along with their schedules
    public static void DisplayStudentSchedules(LearningPath learningPath)
    {
        Console.WriteLine($"Schedules for Students in Learning Path: {learningPath.Id} - {learningPath.ClassLevel} {learningPath.EducationLevel}\n");

        foreach (var student in learningPath.Students)
        {
            Console.WriteLine($"Student: {student.Person.FirstName} {student.Person.LastName}");

            if (student.Person.PersonalCalendar != null && student.Person.PersonalCalendar.ScheduleEntries.Any())
            {
                foreach (var entry in student.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($" - Schedule Entry Id: {entry.Id}, Date: {entry.DateTime}, Duration: {entry.Duration}, Topic: {entry.ClassSession?.Topic ?? "N/A"}");
                }
            }
            else
            {
                Console.WriteLine(" - No schedules available.");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Methods for Curriculum
    /// </summary>

    //semester grouping 
    private static List<SemesterCurriculum> GroupSchedulesIntoSemesters(List<ScheduleEntry> schedules)
    {
        return schedules
            .Where(se => se.ClassSession != null)
            .GroupBy(se => se.DateTime.Month <= FcmsConstants.SEMESTER_1_ENDMONTH ? Semester.First :
                           se.DateTime.Month <= FcmsConstants.SEMESTER_2_ENDMONTH ? Semester.Second : Semester.Third)
            .Select(group => new SemesterCurriculum
            {
                Semester = group.Key,
                ClassSessions = group.Select(se => se.ClassSession).ToList()
            })
            .ToList();
    }

    //check for curriculum uniqueness
    private static void ValidateCurriculumUniqueness(School school, Curriculum newCurriculum)
    {
        if (school.Curricula.Any(c =>
                c.Year == newCurriculum.Year &&
                c.EducationLevel == newCurriculum.EducationLevel &&
                c.ClassLevel == newCurriculum.ClassLevel))
        {
            throw new InvalidOperationException($"A curriculum for {newCurriculum.EducationLevel} - {newCurriculum.ClassLevel} already exists for the year {newCurriculum.Year}.");
        }
    }

    //Generate curriculum for learning path
    public static void GenerateCurriculumForLearningPath(School school, LearningPath learningPath)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (learningPath.Schedule == null || !learningPath.Schedule.Any())
            throw new InvalidOperationException("Learning path has no schedules to generate a curriculum.");

        int year = learningPath.Schedule.First().DateTime.Year;

        var semesterCurriculums = GroupSchedulesIntoSemesters(learningPath.Schedule);

        var newCurriculum = new Curriculum
        {
            Id = school.Curricula.Any() ? school.Curricula.Max(c => c.Id) + 1 : 1,
            Year = year,
            EducationLevel = learningPath.EducationLevel,
            ClassLevel = learningPath.ClassLevel,
            Semesters = semesterCurriculums
        };

        ValidateCurriculumUniqueness(school, newCurriculum);
    }

    //method to update curriculum from changes in learning path
    public static void UpdateCurriculumForLearningPath(School school, LearningPath learningPath)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (learningPath.Schedule == null || !learningPath.Schedule.Any())
            throw new InvalidOperationException("Learning path has no schedules to update the curriculum.");

        int year = learningPath.Schedule.First().DateTime.Year;

        var existingCurriculum = school.Curricula.FirstOrDefault(c =>
            c.Year == year &&
            c.EducationLevel == learningPath.EducationLevel &&
            c.ClassLevel == learningPath.ClassLevel);

        if (existingCurriculum == null)
        {
            Console.WriteLine($"No existing curriculum found for {learningPath.EducationLevel} - {learningPath.ClassLevel}, Year {year}. Generating a new curriculum...");
            GenerateCurriculumForLearningPath(school, learningPath);
            return;
        }

        var semesterCurriculums = GroupSchedulesIntoSemesters(learningPath.Schedule);

        existingCurriculum.Semesters = semesterCurriculums;
    }

    //method to update all curricula
    public static void UpdateAllCurricula(School school)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (school.LearningPath == null || !school.LearningPath.Any())
            throw new InvalidOperationException("No learning paths available in the school to update curricula.");

        foreach (var learningPath in school.LearningPath)
        {
            try
            {
                UpdateCurriculumForLearningPath(school, learningPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update curriculum for Learning Path Id: {learningPath.Id}. Error: {ex.Message}");
            }
        }
    }

    //to increment year and update all curricula
    public static void IncrementYearAndUpdateCurricula(School school)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (school.Curricula == null || !school.Curricula.Any())
            throw new InvalidOperationException("No curricula found in the school to update.");

        foreach (var curriculum in school.Curricula)
        {
            curriculum.Year++;
        }

        UpdateAllCurricula(school);
    }

    //to retrieve curriculum of any class
    public static Curriculum GetCurriculumForClass(School school, EducationLevel educationLevel, ClassLevel classLevel, int year)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (school.Curricula == null || !school.Curricula.Any())
            throw new InvalidOperationException("No curricula exist in the school.");

        var curriculum = school.Curricula
            .FirstOrDefault(c => c.EducationLevel == educationLevel &&
                                          c.ClassLevel == classLevel &&
                                          c.Year == year);

        if (curriculum == null)
            throw new InvalidOperationException($"No curriculum found for Education Level: {educationLevel}, Class Level: {classLevel}, Year: {year}.");

        return curriculum;
    }

    //Get or Create Curriculum for a Class Session
    public static Curriculum GetOrCreateCurriculumForClassSession(School school, ClassSession session)
    {
        var educationLevel = session.Teacher?.Person?.EducationLevel ?? EducationLevel.None;
        var classLevel = session.Teacher?.Person?.ClassLevel ?? ClassLevel.None;
        var academicYear = DateTime.Now.Year;

        var curriculum = school.Curricula
            .FirstOrDefault(c =>
                c.EducationLevel == educationLevel &&
                c.ClassLevel == classLevel &&
                c.Year == academicYear &&
                c.Course == session.Course);

        if (curriculum == null)
        {
            curriculum = new Curriculum
            {
                Id = school.Curricula.Any() ? school.Curricula.Max(c => c.Id) + 1 : 1,
                Year = academicYear,
                EducationLevel = educationLevel,
                ClassLevel = classLevel,
                Course = session.Course,
                Topic = session.Topic,
                Description = session.Description,
                LessonPlan = session.LessonPlan,
                Semesters = new List<SemesterCurriculum>()
            };

            school.Curricula.Add(curriculum);
        }
        else
        {
            curriculum.Topic = session.Topic;
            curriculum.Description = session.Description;
            curriculum.LessonPlan = session.LessonPlan;
        }

        return curriculum;
    }

    /// <summary>
    /// Methods for Payment
    /// </summary>

    //Assign fees for all students in a specific learning path
    public static void SetStudentFeesForLearningPath(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        if (learningPath.Students == null || !learningPath.Students.Any())
        {
            return;
        }

        foreach (var student in learningPath.Students)
        {
            if (student.Person.SchoolFees == null)
            {
                student.Person.SchoolFees = new SchoolFees();
            }
            student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;
        }
    }

    //student make payment
    public static void MakePaymentForStudent(Student student, double amount, PaymentMethod paymentMethod)
    {
        if (student?.Person?.SchoolFees == null)
        {
            throw new ArgumentException("Invalid student or school fees record.");
        }

        var payment = new Payment
        {
            Amount = amount,
            Date = DateTime.Now,
            PaymentMethod = paymentMethod
        };
        student.Person.SchoolFees.Payments.Add(payment);
    }

    //Check if student has made upto half the school fees payment
    public static bool HasMetPaymentThreshold(Student student)
    {
        if (student?.Person?.SchoolFees == null)
        {
            throw new ArgumentException("Invalid student or school fees record.");
        }

        double totalPayments = student.Person.SchoolFees.Payments.Sum(p => p.Amount);
        return totalPayments >= student.Person.SchoolFees.TotalAmount * FcmsConstants.PAYMENT_THRESHOLD_FACTOR;
    }

    public static bool IsPaymentSuccessful(Student student, LearningPath learningPath)
    {
        return learningPath.StudentsPaymentSuccessful != null &&
               learningPath.StudentsPaymentSuccessful.Any(s => s.Id == student.Id);
    }

    public static List<Student> GetUnpaidStudents(LearningPath learningPath)
    {
        if (learningPath.Students == null)
            return new List<Student>();

        return learningPath.Students
            .Where(s => !IsPaymentSuccessful(s, learningPath))
            .ToList();
    }

    //Update payment status of a student in a learning path
    public static void UpdatePaymentStatus(Student student, LearningPath learningPath)
    {
        if (student?.Person?.SchoolFees == null || learningPath == null)
            return;

        // Remove student from payment successful list if they exist there
        if (learningPath.StudentsPaymentSuccessful.Contains(student))
        {
            learningPath.StudentsPaymentSuccessful.Remove(student);
        }

        // Check if payment is now successful (at least 50% paid)
        if (IsPaymentSuccessful(student, learningPath))
        {
            learningPath.StudentsPaymentSuccessful.Add(student);
        }
    }

    //generate Student payment summery
    public static StudentPaymentReportEntry GenerateStudentPaymentReportEntry(Student student)
    {
        // Validate input
        if (student?.Person?.SchoolFees == null)
        {
            throw new ArgumentException("Invalid student or school fees record.");
        }

        // Constants for percentage calculations
        const double PERCENTAGE_MULTIPLIER = 100.0;
        const double DEFAULT_COMPLETION_RATE = 0.0;

        // Get references to related data
        var payments = student.Person.SchoolFees.Payments ?? new List<Payment>();
        var guardian = student.Guardian?.Person;
        var currentLearningPath = student.CurrentLearningPath;
        var latestPayment = payments.OrderByDescending(p => p.Date).FirstOrDefault();

        // Find primary address if available
        string studentAddress = null;
        if (student.Person.Addresses != null && student.Person.Addresses.Any())
        {
            var primaryAddress = student.Person.Addresses.FirstOrDefault(a => a.AddressType == AddressType.Home)
                               ?? student.Person.Addresses.First();
            studentAddress = $"{primaryAddress.Street}, {primaryAddress.City}, {primaryAddress.State}, {primaryAddress.Country}";
        }

        // Determine learning path name and academic details
        string learningPathName = null;
        string academicYear = null;
        string semester = null;

        if (currentLearningPath != null)
        {
            learningPathName = $"{currentLearningPath.EducationLevel} - {currentLearningPath.ClassLevel}";
            academicYear = currentLearningPath.AcademicYear;
            semester = currentLearningPath.Semester.ToString();
        }
        else if (latestPayment != null)
        {
            academicYear = latestPayment.AcademicYear;
            semester = latestPayment.Semester.ToString();
        }

        // Create payment details list
        var paymentDetails = payments.Select(p => new PaymentDetails
        {
            Date = p.Date,
            Amount = p.Amount,
            PaymentMethod = p.PaymentMethod.ToString(),
            Reference = p.Reference
        }).ToList();

        // Calculate payment completion rate
        double paymentCompletionRate = DEFAULT_COMPLETION_RATE;
        if (student.Person.SchoolFees.TotalAmount > 0)
        {
            paymentCompletionRate = (student.Person.SchoolFees.TotalPaid / student.Person.SchoolFees.TotalAmount) * PERCENTAGE_MULTIPLIER;
        }

        // Calculate timely completion rate based on business rules
        double timelyCompletionRate = DEFAULT_COMPLETION_RATE;
        // Example implementation - adjust according to your specific requirements
        if (paymentDetails.Any() && currentLearningPath != null)
        {
            // Check if payments were made before due dates
            // This is a placeholder calculation
            timelyCompletionRate = paymentCompletionRate;
        }

        // Create and return the report entry
        return new StudentPaymentReportEntry
        {
            DateAndTimeReportGenerated = DateTime.Now,
            StudentFullName = $"{student.Person.FirstName} {student.Person.MiddleName} {student.Person.LastName}",
            StudentAddress = studentAddress,
            LearningPathName = learningPathName,
            AcademicYear = academicYear,
            Semester = semester,
            TotalFees = student.Person.SchoolFees.TotalAmount,
            TotalPaid = student.Person.SchoolFees.TotalPaid,
            OutstandingBalance = student.Person.SchoolFees.Balance,
            StudentPaymentCompletionRate = paymentCompletionRate,
            StudentTimelyCompletionRate = timelyCompletionRate,
            PaymentDetails = paymentDetails
        };
    }


    //retrieve students with outstanding balance
    public static List<(Student Student, double OutstandingBalance)> GetStudentsWithOutstandingPayments(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        var result = new List<(Student Student, double OutstandingBalance)>();

        foreach (var student in learningPath.Students)
        {
            if (student.Person?.SchoolFees == null)
            {
                continue;
            }

            double outstandingBalance = student.Person.SchoolFees.Balance;
            if (outstandingBalance > 0)
            {
                result.Add((student, outstandingBalance));
            }
        }
        return result;
    }

    //Get students with access
    public static List<Student> GetStudentsWithAccess(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
        }

        return learningPath.StudentsPaymentSuccessful;
    }

    //Grant student full access to schedule entries in learning path 
    public static void GrantAccessToSchedules(Student student, LearningPath learningPath)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (learningPath == null) throw new ArgumentNullException(nameof(learningPath));

        if (HasMetPaymentThreshold(student) && !learningPath.StudentsPaymentSuccessful.Contains(student))
        {
            learningPath.StudentsPaymentSuccessful.Add(student);
        }
    }

    //Generate payment report of all students in a learning path


    //handle changes to Total amount of school fees
    public static void HandleFeeChangeForLearningPath(LearningPath learningPath, double newTotalFeeAmount)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath));
        }

        if (newTotalFeeAmount < 0)
        {
            throw new ArgumentException("Total fee amount cannot be negative.", nameof(newTotalFeeAmount));
        }

        foreach (var student in learningPath.Students)
        {
            var schoolFees = student.Person?.SchoolFees;
            if (schoolFees == null) continue;

            schoolFees.TotalAmount = newTotalFeeAmount;

            if (HasMetPaymentThreshold(student))
            {
                GrantAccessToSchedules(student, learningPath);
            }
            else
            {
                if (learningPath.StudentsPaymentSuccessful.Contains(student))
                {
                    learningPath.StudentsPaymentSuccessful.Remove(student);
                }
            }
        }
    }

    /// <summary>
    /// Methods for Enrollment
    /// </summary>

    public static LearningPath GetNextLearningPath(LearningPath currentLearningPath, School school)
    {
        if (currentLearningPath == null)
        {
            throw new ArgumentNullException(nameof(currentLearningPath), "Current learning path cannot be null.");
        }

        if (school == null || school.LearningPath == null || !school.LearningPath.Any())
        {
            throw new ArgumentException("The school must have a list of learning paths.", nameof(school));
        }

        if (currentLearningPath.Semester == Semester.Third)
        {
            Console.WriteLine("Manual promotion to the next class level is required.");
            return null;
        }

        Semester nextSemester = currentLearningPath.Semester + 1;

        var nextLearningPath = school.LearningPath.FirstOrDefault(lp =>
            lp.EducationLevel == currentLearningPath.EducationLevel &&
            lp.ClassLevel == currentLearningPath.ClassLevel &&
            lp.Semester == nextSemester);

        return nextLearningPath;
    }

    //Clear Calendar
    public static void ClearCalendar(Calendar calendar)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }

        calendar.ScheduleEntries.Clear();
    }

    //Synch student's calendar with learning path
    public static void SynchronizeSchedulesWithStudents(LearningPath learningPath)
    {
        if (learningPath == null)
        {
            throw new ArgumentNullException(nameof(learningPath), "LearningPath cannot be null.");
        }

        if (learningPath.Students == null || !learningPath.Students.Any())
        {
            return;
        }

        foreach (var student in learningPath.Students)
        {
            if (student.Person.PersonalCalendar == null)
            {
                student.Person.PersonalCalendar = new Calendar
                {
                    Id = student.Id,
                    Name = $"{student.Person.FirstName} {student.Person.LastName}'s Calendar"
                };
            }

            foreach (var entry in learningPath.Schedule)
            {
                if (!student.Person.PersonalCalendar.ScheduleEntries.Any(e => e.Id == entry.Id))
                {
                    student.Person.PersonalCalendar.ScheduleEntries.Add(new ScheduleEntry
                    {
                        Id = entry.Id,
                        DateTime = entry.DateTime,
                        Duration = entry.Duration,
                        Venue = entry.Venue,
                        ClassSession = entry.ClassSession,
                        Title = entry.Title,
                        Event = entry.Event,
                        Meeting = entry.Meeting,
                        Notes = entry.Notes,
                        IsRecurring = entry.IsRecurring,
                        RecurrencePattern = entry.RecurrencePattern,
                        DaysOfWeek = entry.DaysOfWeek,
                        DayOfMonth = entry.DayOfMonth,
                        RecurrenceInterval = entry.RecurrenceInterval,
                        EndDate = entry.EndDate
                    });
                }
            }
        }
    }

    //Transfer students to next learning path
    public static void TransferStudentsToNextLearningPath(LearningPath currentLearningPath, LearningPath nextLearningPath)
    {
        if (currentLearningPath == null)
        {
            throw new ArgumentNullException(nameof(currentLearningPath), "Current learning path cannot be null.");
        }

        if (nextLearningPath == null)
        {
            throw new ArgumentNullException(nameof(nextLearningPath), "Next learning path cannot be null.");
        }

        if (currentLearningPath.EducationLevel != nextLearningPath.EducationLevel ||
            currentLearningPath.ClassLevel != nextLearningPath.ClassLevel)
        {
            throw new InvalidOperationException("The next learning path must belong to the same education level and class level as the current learning path.");
        }

        if (nextLearningPath.Semester != currentLearningPath.Semester + 1)
        {
            throw new InvalidOperationException("The next learning path must represent the immediate next semester.");
        }

        var studentsToTransfer = currentLearningPath.Students.ToList();
        foreach (var student in studentsToTransfer)
        {
            if (!nextLearningPath.Students.Contains(student))
            {
                nextLearningPath.Students.Add(student);
            }
            currentLearningPath.Students.Remove(student);
        }
    }

    /// <summary>
    /// Methods for Calendar
    /// </summary>

    //Add schedule entry
    public static void AddScheduleEntry(Calendar calendar, ScheduleEntry entry)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        if (entry == null)
        {
            throw new ArgumentNullException(nameof(entry), "Schedule entry cannot be null.");
        }
        if (calendar.ScheduleEntries.Any(e => e.Id == entry.Id))
        {
            throw new InvalidOperationException($"A schedule entry with Id {entry.Id} already exists.");
        }

        calendar.ScheduleEntries.Add(entry);
    }

    //Remove Schedule Entry
    public static void RemoveScheduleEntry(Calendar calendar, int entryId)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        var entry = calendar.ScheduleEntries.FirstOrDefault(e => e.Id == entryId);
        if (entry == null)
        {
            throw new InvalidOperationException($"Schedule entry with Id {entryId} not found.");
        }

        calendar.ScheduleEntries.Remove(entry);
    }

    //Retrieves all schedule entries for a specific date.
    public static List<ScheduleEntry> GetEntriesByDate(Calendar calendar, DateTime date)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        return calendar.ScheduleEntries
            .Where(e => e.DateTime.Date == date.Date)
            .OrderBy(e => e.DateTime)
            .ToList();
    }

    //Get upcoming events
    public static List<ScheduleEntry> GetUpcomingEntries(Calendar calendar)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        return calendar.ScheduleEntries
            .Where(e => e.DateTime >= DateTime.Now)
            .OrderBy(e => e.DateTime)
            .ToList();
    }

    //Retrieves all past schedule entries
    public static List<ScheduleEntry> GetPastEntries(Calendar calendar)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }

        return calendar.ScheduleEntries
            .Where(e => e.DateTime < DateTime.Now)
            .OrderByDescending(e => e.DateTime)
            .ToList();
    }

    //Clears all schedule entries in the calendar
    public static void ClearAllEntries(Calendar calendar)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        calendar.ScheduleEntries.Clear();
    }

    //Retrieves all recurring schedule entries
    public static List<ScheduleEntry> GetRecurringEntries(Calendar calendar)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        return calendar.ScheduleEntries.Where(e => e.IsRecurring).ToList();
    }

    //Retrieves all schedule entries for a specific day of the week
    public static List<ScheduleEntry> GetEntriesForDayOfWeek(Calendar calendar, DayOfWeek dayOfWeek)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        return calendar.ScheduleEntries.Where(e => e.DateTime.DayOfWeek == dayOfWeek).ToList();
    }

    //Exports calendar entries to a JSON file
    public static void ExportCalendar(Calendar calendar, string filePath)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        var serializedEntries = JsonSerializer.Serialize(calendar.ScheduleEntries);
        File.WriteAllText(filePath, serializedEntries);
    }

    //Imports calendar entries from a JSON file
    public static void ImportCalendar(Calendar calendar, string filePath)
    {
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar), "Calendar cannot be null.");
        }
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file does not exist.", filePath);
        }

        var serializedEntries = File.ReadAllText(filePath);
        var importedEntries = JsonSerializer.Deserialize<List<ScheduleEntry>>(serializedEntries);

        if (importedEntries != null)
        {
            foreach (var entry in importedEntries)
            {
                if (!calendar.ScheduleEntries.Any(e => e.Id == entry.Id))
                {
                    calendar.ScheduleEntries.Add(entry);
                }
            }
        }
    }

    //generate school calendar
    public static List<ScheduleEntry> GenerateSchoolCalendar(School school)
    {
        if (school == null)
        {
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
        }
        List<ScheduleEntry> schoolCalendar = new();
        foreach (var learningPath in school.LearningPath)
        {
            schoolCalendar.AddRange(learningPath.Schedule);
        }
        schoolCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

        return schoolCalendar;
    }

    /// <summary>
    /// Methods for Attendance
    /// </summary>

    //Get expected students that ought to attend a class session, whether paid or not
    public static List<Student> GetExpectedStudentsForClassSession(School school, ClassSession classSession)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school));

        if (classSession == null)
            throw new ArgumentNullException(nameof(classSession));

        var learningPath = school.LearningPath
            .FirstOrDefault(lp => lp.Schedule.Any(s => s.ClassSession?.Id == classSession.Id));

        return learningPath?.Students ?? new List<Student>();
    }

    //take attendance for class session
    public static void TakeAttendanceForClassSession(School school, ClassSession classSession, List<Student> presentStudents, Staff teacher)
    {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");

        if (classSession == null)
            throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");

        if (teacher == null)
            throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");

        if (classSession.Teacher != teacher)
            throw new InvalidOperationException("Only the assigned teacher can take attendance for this class session.");

        List<Student> expectedStudents = GetExpectedStudentsForClassSession(school, classSession);

        if (!expectedStudents.Any())
            throw new InvalidOperationException("No students are expected for this class session.");

        if (presentStudents == null)
            throw new ArgumentNullException(nameof(presentStudents), "Present students list cannot be null.");

        foreach (var student in presentStudents)
        {
            if (!expectedStudents.Contains(student))
                throw new InvalidOperationException($"Student {student.Id} is not expected in this class session.");
        }

        List<Student> absentStudents = expectedStudents.Except(presentStudents).ToList();

        var attendanceLogEntry = new ClassAttendanceLogEntry
        {
            Id = classSession.AttendanceLog.Count + 1,
            ClassSession = classSession,
            ClassSessionId = classSession.Id,
            Teacher = teacher,
            Attendees = presentStudents,
            AbsentStudents = absentStudents,
            TimeStamp = DateTime.Now
        };
        classSession.AttendanceLog.Add(attendanceLogEntry);
    }

    //Retrieve students Absent from a class session
    public static List<Student> GetStudentsAbsentForClassSession(ClassSession classSession)
    {
        if (classSession == null)
            throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");

        var latestAttendanceLog = classSession.AttendanceLog.LastOrDefault();

        return latestAttendanceLog?.AbsentStudents ?? new List<Student>();
    }

    //Retrieve students present for a class session
    public static List<Student> GetStudentsPresentForClassSession(ClassSession classSession)
    {
        if (classSession == null)
            throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");

        var latestAttendanceLog = classSession.AttendanceLog.LastOrDefault();

        return latestAttendanceLog?.Attendees ?? new List<Student>();
    }

    //Retrieve attendance of a class session
    public static ClassAttendanceLogEntry RetrieveAttendanceForClassSession(ClassSession classSession)
    {
        if (classSession == null)
            throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");

        var latestAttendanceLog = classSession.AttendanceLog.LastOrDefault();

        if (latestAttendanceLog == null)
            throw new InvalidOperationException("No attendance records found for this class session.");

        return latestAttendanceLog;
    }

    //Retrieve all attendance recorded for a learning path
    public static List<ClassAttendanceLogEntry> GetAttendanceForLearningPath(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        List<ClassAttendanceLogEntry> attendanceLogs = new();

        foreach (var schedule in learningPath.Schedule)
        {
            if (schedule.ClassSession != null && schedule.ClassSession.AttendanceLog.Any())
            {
                attendanceLogs.AddRange(schedule.ClassSession.AttendanceLog);
            }
        }

        return attendanceLogs;
    }

    //Get attendance of all the students in a particular learning path for a select day
    public static List<ClassAttendanceLogEntry> GetAttendanceForLearningPathByDate(LearningPath learningPath, DateTime date)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        List<ClassAttendanceLogEntry> attendanceLogs = new();

        foreach (var schedule in learningPath.Schedule)
        {
            if (schedule.ClassSession != null)
            {
                var filteredLogs = schedule.ClassSession.AttendanceLog
                    .Where(log => log.TimeStamp.Date == date.Date)
                    .ToList();

                attendanceLogs.AddRange(filteredLogs);
            }
        }

        return attendanceLogs;
    }

    //Get a student's Attendance record for a semester
    public static List<ClassAttendanceLogEntry> GetAStudentSemesterAttendance(LearningPath learningPath, Student student)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        List<ClassAttendanceLogEntry> studentAttendance = new();

        foreach (var schedule in learningPath.Schedule)
        {
            if (schedule.ClassSession != null && schedule.ClassSession.AttendanceLog.Any())
            {
                var latestLog = schedule.ClassSession.AttendanceLog.LastOrDefault();

                if (latestLog != null && (latestLog.Attendees.Contains(student) || latestLog.AbsentStudents.Contains(student)))
                {
                    studentAttendance.Add(latestLog);
                }
            }
        }

        return studentAttendance;
    }

    /// <summary>
    /// Methods for Class Session Collarboration
    /// </summary>

    //Submit homework
    public static void SubmitHomework(Homework homework, Student student, string answer)
    {
        if (homework == null)
            throw new ArgumentNullException(nameof(homework), "Homework cannot be null.");

        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be null or empty.", nameof(answer));

        var submission = new HomeworkSubmission
        {
            Id = homework.Submissions.Count + 1,
            Student = student,
            Answer = answer,
            SubmissionDate = DateTime.Now,
            IsGraded = false
        };

        homework.Submissions.Add(submission);
    }

    //search and retrieve homework submitted by a particular student
    public static List<HomeworkSubmission> GetSubmissionsByStudent(Homework homework, Student student)
    {
        if (homework == null)
            throw new ArgumentNullException(nameof(homework), "Homework cannot be null.");

        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        return homework.Submissions.Where(s => s.Student.Id == student.Id).ToList();
    }

    // Add a student's graded homework to their cumulative course grade
    public static void SubmitHomeworkGradeToStudent(Student student, HomeworkSubmission submission)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (submission == null || !submission.IsGraded)
            throw new ArgumentException("Homework submission must be graded before submission to Course Grade.");

        if (submission.HomeworkGrade == null)
            throw new ArgumentException("Homework grade is missing.");

        var courseGrade = student.CourseGrades.FirstOrDefault(cg => cg.Course == submission.HomeworkGrade.Course);

        if (courseGrade == null)
            throw new InvalidOperationException($"Student has no recorded CourseGrades for {submission.HomeworkGrade.Course}.");

        courseGrade.TestGrades.Add(submission.HomeworkGrade);
    }


    //Start discussion 
    public static DiscussionThread StartDiscussion(int threadId, int firstPostId, Person author, string comment)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author), "Author cannot be null.");
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be empty.", nameof(comment));

        var now = DateTime.UtcNow;
        var firstPost = new DiscussionPost
        {
            Id = firstPostId,
            DiscussionThreadId = threadId,
            Author = author,
            Comment = comment,
            CreatedAt = now
        };

        var discussionThread = new DiscussionThread
        {
            Id = threadId,
            FirstPost = firstPost,
            Replies = new List<DiscussionPost>(),
            CreatedAt = now,
            LastUpdatedAt = now
        };

        return discussionThread;
    }

    //Add reply to discussion
    public static void AddReply(DiscussionThread thread, int replyId, Person author, string comment)
    {
        if (thread == null)
            throw new ArgumentNullException(nameof(thread), "Discussion thread cannot be null.");
        if (author == null)
            throw new ArgumentNullException(nameof(author), "Author cannot be null.");
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be empty.", nameof(comment));

        var now = DateTime.UtcNow;
        var reply = new DiscussionPost
        {
            Id = replyId,
            DiscussionThreadId = thread.Id,
            Author = author,
            Comment = comment,
            CreatedAt = now
        };

        thread.Replies.Add(reply);
        thread.UpdateLastUpdated();
    }

    /// <summary>
    /// Methods for Class Session Collarboration
    /// </summary>

    // Grade a test (Quiz, Exam, or Homework) for a student and add it to the appropriate course
    public static void AddTestGrade(Student student, string course, double score, GradeType gradeType, double weightPercentage, Staff teacher, Semester semester, string teacherRemark)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (string.IsNullOrWhiteSpace(course))
            throw new ArgumentException("Course name is required.", nameof(course));

        if (weightPercentage < 0 || weightPercentage > 100)
            throw new ArgumentException("Weight percentage must be between 0 and 100.", nameof(weightPercentage));

        var testGrade = new TestGrade
        {
            Course = course,
            Score = score,
            GradeType = gradeType,
            WeightPercentage = weightPercentage,
            Teacher = teacher,
            Semester = semester,
            Date = DateTime.Now,
            TeacherRemark = teacherRemark
        };

        var courseGrade = student.CourseGrades.FirstOrDefault(cg => cg.Course == course);

        if (courseGrade == null)
        {
            courseGrade = new CourseGrade { Course = course };
            student.CourseGrades.Add(courseGrade);
        }

        courseGrade.TestGrades.Add(testGrade);
    }


    //Grade Homework
    public static void GradeHomework(HomeworkSubmission submission, double score, double weightPercentage, Staff teacher, Semester semester, Homework homework, ClassSession classSession)
    {
        if (submission == null)
            throw new ArgumentNullException(nameof(submission), "Homework submission cannot be null.");

        if (submission.Student == null)
            throw new ArgumentNullException(nameof(submission.Student), "Student cannot be null.");

        if (string.IsNullOrWhiteSpace(classSession.Course))
            throw new ArgumentException("Course name is required.", nameof(classSession.Course));

        if (score < 0 || score > 100)
            throw new ArgumentException("Score must be between 0 and 100.", nameof(score));

        if (weightPercentage < 0 || weightPercentage > 100)
            throw new ArgumentException("Weight percentage must be between 0 and 100.", nameof(weightPercentage));

        var homeworkGrade = new TestGrade
        {
            Course = classSession.Course,
            Score = score,
            GradeType = GradeType.Homework,
            WeightPercentage = weightPercentage,
            Teacher = teacher,
            Semester = semester,
            Date = DateTime.Now
        };

        submission.HomeworkGrade = homeworkGrade;
        submission.IsGraded = true;
    }

    // Compute Total Grade for a course at the end of the semester
    public static double ComputeTotalGrade(Student student, string course)
    {
        if (student == null || student.CourseGrades == null)
            throw new ArgumentNullException(nameof(student), "Invalid student data.");

        var courseGrade = student.CourseGrades.FirstOrDefault(cg => cg.Course == course);

        if (courseGrade == null || !courseGrade.TestGrades.Any())
            return 0;

        double weightedSum = courseGrade.TestGrades.Sum(tg => tg.Score * (tg.WeightPercentage / FcmsConstants.TOTAL_SCORE));

        return Math.Round(weightedSum, FcmsConstants.GRADE_ROUNDING_DIGIT);
    }


    //Assign Grade Code
    public static string GetGradeCode(double totalGrade)
    {
        return totalGrade switch
        {
            >= FcmsConstants.A_GRADE_MIN => "A",
            >= FcmsConstants.B_GRADE_MIN => "B",
            >= FcmsConstants.C_GRADE_MIN => "C",
            >= FcmsConstants.D_GRADE_MIN => "D",
            >= FcmsConstants.E_GRADE_MIN => "E",
            _ => "F",
        };
    }

    // Compute final semester grade for each course for each student in a learning path
    public static void FinalizeSemesterGrades(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning Path cannot be null.");

        foreach (var student in learningPath.Students)
        {
            foreach (var course in CourseDefaults.GetCourseNames(learningPath.EducationLevel))
            {
                double totalGrade = ComputeTotalGrade(student, course);
                string gradeCode = GetGradeCode(totalGrade);

                var courseGrade = student.CourseGrades.FirstOrDefault(cg => cg.Course == course);

                if (courseGrade != null)
                {
                    courseGrade.TotalGrade = totalGrade;
                    courseGrade.FinalGradeCode = gradeCode;
                }
                else
                {
                    student.CourseGrades.Add(new CourseGrade
                    {
                        Course = course,
                        TotalGrade = totalGrade,
                        FinalGradeCode = gradeCode
                    });
                }
            }
        }
    }


    // Compute Semester overall grade average for a student
    public static double CalculateSemesterOverallGrade(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (student.CourseGrades == null || student.CourseGrades.Count == 0)
            throw new InvalidOperationException($"No grades found for student {student.Id}.");

        var courseGrades = student.CourseGrades
            .Select(cg => ComputeTotalGrade(student, cg.Course))
            .Where(totalGrade => totalGrade > 0)
            .ToList();

        if (courseGrades.Count == 0)
            throw new InvalidOperationException($"Student {student.Id} has no valid course grades.");

        double overallSemesterAverage = courseGrades.Sum() / courseGrades.Count;

        return overallSemesterAverage;
    }


    //Compute promotion grade for student 
    public static double CalculatePromotionGrade(Student student, List<LearningPath> learningPaths)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (learningPaths == null || learningPaths.Count == 0)
            throw new ArgumentException("Learning paths list cannot be null or empty.", nameof(learningPaths));

        var relevantLearningPaths = learningPaths
            .Where(lp => lp.Students.Contains(student))
            .OrderByDescending(lp => lp.Schedule.Min(se => se.DateTime.Year))
            .Take(3)
            .ToList();

        if (relevantLearningPaths.Count < FcmsConstants.NUMBER_OF_SEMESTERS)
            throw new InvalidOperationException($"Student {student.Id} has fewer than 3 semesters recorded for promotion.");

        var semesterGrades = relevantLearningPaths
            .Select(lp => CalculateSemesterOverallGrade(student))
            .ToList();

        return semesterGrades.Average();
    }

    //method to arrange CalculateSemesterOverallGrade() of all students in a learning path in descending order
    public static List<(Student Student, double SemesterGrade)> RankStudentsBySemesterGrade(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (learningPath.Students == null || learningPath.Students.Count == 0)
            throw new InvalidOperationException("No students found in this learning path.");

        var studentGrades = learningPath.Students
            .Select(student => (Student: student, SemesterGrade: CalculateSemesterOverallGrade(student)))
            .OrderByDescending(sg => sg.SemesterGrade)
            .ToList();

        return studentGrades;
    }

    // Retrieve all grades of all students for a particular course
    public static List<TestGrade> GetAllGradesForCourse(string courseName, List<Student> students)
    {
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

        if (students == null || students.Count == 0)
            return new List<TestGrade>();

        return students
            .Where(student => student.CourseGrades != null)
            .SelectMany(student => student.CourseGrades
                .Where(course => course.Course == courseName)
                .SelectMany(course => course.TestGrades))
            .ToList();
    }


    // Retrieve the homework grades of a student for a particular course
    public static List<TestGrade> GetHomeworkScoresForCourse(Student student, string courseName)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

        return student.CourseGrades?
            .Where(course => course.Course == courseName)
            .SelectMany(course => course.TestGrades
                .Where(grade => grade.GradeType == GradeType.Homework))
            .ToList() ?? new List<TestGrade>();
    }

    public static List<CourseGrade> GetCourseGradesByLearningPathId(School school, int learningPathId)
    {
        // Find all course grades related to the specific learning path
        return school.Students
            .SelectMany(s => s.CourseGrades)
            .Where(cg => cg.LearningPathId == learningPathId)
            .ToList();
    }
    public static LearningPathGradeReport GenerateGradeReportForLearningPath(School school, LearningPath learningPath)
    {
        var report = new LearningPathGradeReport
        {
            Id = learningPath.Id,
            LearningPath = learningPath,
            Semester = learningPath.Semester,
            RankedStudents = new List<StudentGradeSummary>()
        };

        // Get all grades for this learning path
        var grades = GetCourseGradesByLearningPathId(school, learningPath.Id);

        // Group grades by student and calculate overall grade
        var studentGrades = new Dictionary<Student, double>();

        foreach (var grade in grades)
        {
            var student = school.Students.FirstOrDefault(s => s.Id == grade.StudentId);
            if (student == null) continue;

            if (!studentGrades.ContainsKey(student))
            {
                studentGrades[student] = 0;
            }

            studentGrades[student] += grade.TotalGrade;
        }

        // Create ranked list
        foreach (var entry in studentGrades)
        {
            report.StudentSemesterGrades[entry.Key] = entry.Value;

            report.RankedStudents.Add(new StudentGradeSummary
            {
                Student = entry.Key,
                SemesterOverallGrade = entry.Value
            });
        }

        // Sort ranked students by grade (descending)
        report.RankedStudents = report.RankedStudents
            .OrderByDescending(sg => sg.SemesterOverallGrade)
            .ToList();

        return report;
    }
}
