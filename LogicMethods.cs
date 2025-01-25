using FcmsPortal.Enums;
using FcmsPortal.ViewModel;

namespace FcmsPortal
{
    internal static class LogicMethods
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
                throw new ArgumentException($"A schedule with ID {scheduleEntry.Id} already exists in the learning path.");
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
            Console.WriteLine($"Schedule with ID {scheduleEntry.Id} has been successfully added to Learning Path ID {learningPath.Id}.");
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
                    throw new ArgumentException($"A schedule with ID {scheduleEntry.Id} already exists in the learning path.");
                }
                
                bool hasOverlap = learningPath.Schedule.Any(existing =>
                    existing.DateTime < scheduleEntry.DateTime.Add(scheduleEntry.Duration) &&
                    scheduleEntry.DateTime < existing.DateTime.Add(existing.Duration));

                if (hasOverlap)
                {
                    throw new InvalidOperationException($"Schedule ID {scheduleEntry.Id} overlaps with an existing class session.");
                }
                
                bool sameTimePeriod = learningPath.Schedule.Any(existing =>
                    existing.DateTime == scheduleEntry.DateTime &&
                    existing.Duration == scheduleEntry.Duration);

                if (sameTimePeriod)
                {
                    throw new InvalidOperationException($"A schedule with the same time period as ID {scheduleEntry.Id} already exists.");
                }
            }
            
            learningPath.Schedule.AddRange(scheduleEntries);
            Console.WriteLine($"{scheduleEntries.Count} schedules have been successfully added to Learning Path ID {learningPath.Id}.");
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
                    existingPath.Students.Any(s => s.ID == student.ID));

                if (isStudentInAnotherPath)
                {
                    throw new InvalidOperationException($"Student ID {student.ID} already belongs to another learning path.");
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
            
            if (school.Students.Any(s => s.ID == student.ID))
            {
                throw new ArgumentException($"Student with ID {student.ID} is already registered in the school.");
            }
            
            if (school.LearningPath.Any(lp => lp.Students.Any(s => s.ID == student.ID)))
            {
                throw new ArgumentException($"Student with ID {student.ID} is already enrolled in a learning path.");
            }
            
            if (student.Guardian != null && !school.Guardians.Any(g => g.Id == student.Guardian.Id))
            {
                school.Guardians.Add(student.Guardian);
            }
            school.Students.Add(student);
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
                    Id = baseEntry.Id, // Assign new ID if necessary
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
                Console.WriteLine($"\nCalendar: {calendar.Name} (ID: {calendar.Id})");

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

                    Console.WriteLine($"  ID: {entry.Id}, Date: {entry.DateTime.ToShortDateString()}, " +
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
            
            return learningPath.Schedule;
        }
        
        //Get all schedules of a learning path for a particular date
        public static List<ScheduleEntry> GetSchedulesByDateInLearningPath(LearningPath learningPath, DateTime date)
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
                .Where(schedule => schedule.DateTime.Date == date.Date) 
                .ToList();
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
                        Id = student.ID,
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
                        Console.WriteLine($" - Schedule Entry ID: {entry.Id}, Date: {entry.DateTime}, Duration: {entry.Duration}, Topic: {entry.ClassSession?.Topic ?? "N/A"}");
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
        public static void GenerateCurriculumForLearningPath(School school, LearningPath learningPath)
        {
        if (school == null)
            throw new ArgumentNullException(nameof(school), "School cannot be null.");
    
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
    
        if (learningPath.Schedule == null || !learningPath.Schedule.Any())
            throw new InvalidOperationException("Learning path has no schedules to generate a curriculum.");
    
        
        int year = learningPath.Schedule.First().DateTime.Year;
            
            var semesterCurriculums = learningPath.Schedule
                .Where(se => se.ClassSession != null) 
                .GroupBy(se => se.DateTime.Month <= 4 ? 1 : se.DateTime.Month <= 8 ? 2 : 3)
                .Select(group => new SemesterCurriculum
                {
                    Semester = group.Key,
                    ClassSessions = group.Select(se => se.ClassSession).ToList()
                })
                .ToList();
            
            var newCurriculum = new Curriculum
                {
                    Id = school.Curricula.Any() ? school.Curricula.Max(c => c.Id) + 1 : 1, 
                    Year = year,
                    EducationLevel = learningPath.EducationLevel,
                    ClassLevel = learningPath.ClassLevel,
                    Semesters = semesterCurriculums
                };
            
            if (school.Curricula.Any(c =>
                    c.Year == newCurriculum.Year &&
                    c.EducationLevel == newCurriculum.EducationLevel &&
                    c.ClassLevel == newCurriculum.ClassLevel))
            {
                throw new InvalidOperationException($"A curriculum for {newCurriculum.EducationLevel} - {newCurriculum.ClassLevel} already exists for the year {newCurriculum.Year}.");
            }
            school.Curricula.Add(newCurriculum);
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
            
            var semesterCurriculums = learningPath.Schedule
                .Where(se => se.ClassSession != null) 
                .GroupBy(se => se.DateTime.Month <= 4 ? 1 : se.DateTime.Month <= 8 ? 2 : 3)
                .Select(group => new SemesterCurriculum
                {
                    Semester = group.Key,
                    ClassSessions = group.Select(se => se.ClassSession).ToList()
                })
                .ToList();
            
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
                    Console.WriteLine($"Failed to update curriculum for Learning Path ID: {learningPath.Id}. Error: {ex.Message}");
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

            Console.WriteLine("Incrementing year and updating curricula...");
            
            foreach (var curriculum in school.Curricula)
            {
                curriculum.Year++;
            }
            
            foreach (var learningPath in school.LearningPath)
            {
                try
                {
                    UpdateCurriculumForLearningPath(school, learningPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to update curriculum for Learning Path ID: {learningPath.Id}. Error: {ex.Message}");
                }
            }
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
                Console.WriteLine("No students are enrolled in this learning path.");
                return;
            }
            
            foreach (var student in learningPath.Students)
            {
                if (student.Person.SchoolFees == null)
                {
                    student.Person.SchoolFees = new Schoolfees();
                }
                student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;
            }
        }
        
        //Assign fees to all students in all learning paths of a school assuming all students in the school has uniform school fees
        public static void SetFeesForAllLearningPaths(School school)
        {
            if (school == null) throw new ArgumentNullException(nameof(school));

            foreach (var learningPath in school.LearningPath)
            {
                SetStudentFeesForLearningPath(learningPath);
            }
        }
        
        //Assign fees to list of selected learning paths 
        public static void AssignFeesForSelectedLearningPaths(List<LearningPath> selectedLearningPaths)
        {
            if (selectedLearningPaths == null || selectedLearningPaths.Count == 0)
            {
                throw new ArgumentException("The list of selected learning paths cannot be null or empty.");
            }

            foreach (var learningPath in selectedLearningPaths)
            {
                if (learningPath == null)
                {
                    throw new ArgumentException("One of the learning paths in the list is null.");
                }

                foreach (var student in learningPath.Students)
                {
                    if (student.Person.SchoolFees == null)
                    {
                        student.Person.SchoolFees = new Schoolfees();
                    }

                    // Assign the semester fee
                    student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;
                }
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
            return totalPayments >= (student.Person.SchoolFees.TotalAmount / 2);
        }
        
        //get student's outstanding balance
        public static double GetStudentOutstandingBalance(Student student)
        {
            if (student?.Person?.SchoolFees == null)
            {
                throw new ArgumentException("Invalid student or school fees record.");
            }
            return student.Person.SchoolFees.Balance;
        }
        
        //generate payment summery
        public static List<string> GeneratePaymentSummaryForStudent(Student student)
        {
            if (student?.Person?.SchoolFees == null)
            {
                throw new ArgumentException("Invalid student or school fees record.");
            }
            var paymentSummary = new List<string>();
            double totalPaid = 0;
            
            foreach (var payment in student.Person.SchoolFees.Payments)
            {
                totalPaid += payment.Amount;
                paymentSummary.Add($"Date: {payment.Date:yyyy-MM-dd}\tAmount paid: {payment.Amount:C}\t\tPayment Method: {payment.PaymentMethod}");
            }
            paymentSummary.Add($"Total Paid: {totalPaid:C}");
            paymentSummary.Add($"Outstanding Balance: {student.Person.SchoolFees.Balance:C}");

            return paymentSummary;
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
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            if (student.Person?.SchoolFees == null)
            {
                throw new ArgumentException("Student does not have a valid school fees record.");
            }
            
            double totalPaid = student.Person.SchoolFees.Payments.Sum(payment => payment.Amount);
            
            if (totalPaid >= student.Person.SchoolFees.TotalAmount / 2)
            {
                if (!learningPath.StudentsPaymentSuccessful.Contains(student))
                {
                    learningPath.StudentsPaymentSuccessful.Add(student);
                }
            }
        }
        
        //Generate payment report of all students in a learning path
        public static List<PaymentReportEntry> GetPaymentReportForLearningPath(LearningPath learningPath)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning Path cannot be null.");
            }

            var paymentReport = learningPath.Students.Select(student => new PaymentReportEntry
            {
                StudentName = $"{student.Person.FirstName} {student.Person.LastName}",
                TotalFees = student.Person.SchoolFees.TotalAmount,
                TotalPaid = student.Person.SchoolFees.Payments.Sum(payment => payment.Amount),
                OutstandingBalance = student.Person.SchoolFees.Balance,
                PaymentDetails = student.Person.SchoolFees.Payments.Select(payment => new PaymentDetails
                {
                    Date = payment.Date,
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod.ToString()
                }).ToList()
            }).ToList();

            return paymentReport;
        }
        
        //send notification of outstanding 
        public static void NotifyStudentsOfPaymentStatus(LearningPath learningPath)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath));
            }
            
            foreach (var student in learningPath.Students)
            {
                var schoolFees = student.Person?.SchoolFees;
                if (schoolFees == null) continue;
                var outstandingBalance = schoolFees.Balance;
                
                if (outstandingBalance > 0)
                {
                    string studentMessage = $"Dear {student.Person.FirstName}, " +
                        $"you have an outstanding balance of {outstandingBalance:C}. " +
                        "Please ensure payment is made to retain access to your classes.";

                    string guardianMessage = student.GuardianId.HasValue
                        ? $"Dear Guardian of {student.Person.FirstName}, " +
                          $"your ward has an outstanding balance of {outstandingBalance:C}. " +
                          "Please ensure payment is made promptly."
                        : null;
                    
                    SendNotification(student.Person.Email, studentMessage, "Outstanding Payment Reminder");

                    if (!string.IsNullOrEmpty(student.Guardian?.Person.Email))
                    {
                        SendNotification(student.Guardian.Person.Email, guardianMessage, "Outstanding Payment Reminder");
                    }
                }
            }
        }

        private static void SendNotification(string email, string message, string subject)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("No email address provided. Notification skipped.");
                return;
            }
            Console.WriteLine($"Notification sent to {email}:\nSubject: {subject}\nMessage: {message}\n");
        }




        
            
          



        
        
        
        
        
        
        public static void SynchronizeSchedulesWithStudents(LearningPath learningPath)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "LearningPath cannot be null.");
            }

            if (learningPath.Students == null || !learningPath.Students.Any())
            {
                throw new InvalidOperationException("No students are enrolled in the learning path.");
            }

            foreach (var student in learningPath.Students)
            {
                // Ensure the student's calendar is initialized
                if (student.Person.PersonalCalendar == null)
                {
                    student.Person.PersonalCalendar = new Calendar
                    {
                        Id = student.ID,
                        Name = $"{student.Person.FirstName} {student.Person.LastName}'s Calendar"
                    };
                }
                // Synchronize the schedule entries from the learning path to the student's calendar
                foreach (var entry in learningPath.Schedule)
                {
                    // Avoid duplicate entries by checking if the entry already exists
                    if (!student.Person.PersonalCalendar.ScheduleEntries.Contains(entry))
                    {
                        student.Person.PersonalCalendar.ScheduleEntries.Add(entry);
                    }
                }
            }
        }

        
        
        
        
        
        
        
        
        
        
        





        

        
       

        //generate a student's calendar
        public static List<ScheduleEntry> GenerateStudentCalendar(School school, Student student)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            List<ScheduleEntry> studentCalendar = new();
            foreach (var learningPath in school.LearningPath)
            {
                if (learningPath.Students.Contains(student))
                {
                    studentCalendar.AddRange(learningPath.Schedule);
                }
            }
            studentCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return studentCalendar;
            
        }

        //generate teacher's calendar
        public static List<ScheduleEntry> GenerateTeacherCalendar(School school, Staff teacher)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }

            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");
            }
            List<ScheduleEntry> teacherCalendar = new();

            foreach (var learningPath in school.LearningPath)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession.Teacher != null && schedule.ClassSession.Teacher.Id == teacher.Id)
                    {
                        teacherCalendar.Add(schedule);
                    }
                }
            }
            teacherCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return teacherCalendar;
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

        //take attendance for class session
        public static void TakeAttendanceForClassSession(ClassSession classSession, List<Student> presentStudents, Staff teacher)
        {
            if (classSession == null)
            {
                throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");
            }
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");
            }
            if (presentStudents == null || !presentStudents.Any())
            {
                throw new ArgumentException("The list of present students cannot be null or empty.", nameof(presentStudents));
            }
            if (classSession.Teacher != teacher)
            {
                throw new InvalidOperationException("Only the assigned teacher can take attendance for this class session.");
            }

            var attendanceLogEntry = new ClassAttendanceLogEntry
            {
                Id = classSession.AttendanceLog.Count + 1,
                Teacher = teacher,
                Attendees = presentStudents,
                TimeStamp = DateTime.Now
            };

            classSession.AttendanceLog.Add(attendanceLogEntry);
        }

        //Retrieve a student's attendance for a particular course in a semester
        public static List<ClassAttendanceLogEntry> GetStudentAttendanceForCourse(
        Student student,
        string course,
        LearningPath learningPath)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(course))
            {
                throw new ArgumentException("Course cannot be null or empty.", nameof(course));
            }
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            var validCourses = CourseDefaults.GetCourseNames(learningPath.EducationLevel);
            if (!validCourses.Contains(course))
            {
                throw new ArgumentException($"The course '{course}' is not valid for the education level {learningPath.EducationLevel}.", nameof(course));
            }

            // Retrieve all schedule entries in the learning path
            var scheduleEntries = learningPath.Schedule;

            // Filter schedule entries for the specified course
            var courseSessions = scheduleEntries
                .Where(se => se.ClassSession.Course.Equals(course, StringComparison.OrdinalIgnoreCase))
                .Select(se => se.ClassSession)
                .ToList();

            // Get attendance logs where the student is marked as present
            var attendanceLogs = courseSessions
                .SelectMany(cs => cs.AttendanceLog)
                .Where(log => log.Attendees.Contains(student))
                .ToList();

            return attendanceLogs;
        }

        //Get attendance of all the students in a particular learning path for a day
        public static List<ClassAttendanceLogEntry> GetAttendanceForLearningPathOnDay(
        LearningPath learningPath,
        DateTime date)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            var relevantScheduleEntries = learningPath.Schedule
                .Where(se => se.DateTime.Date == date.Date)
                .ToList();

            if (!relevantScheduleEntries.Any())
            {
                Console.WriteLine($"No class sessions were scheduled for the date {date.ToShortDateString()}.");
                return new List<ClassAttendanceLogEntry>();
            }

            var attendanceLogs = relevantScheduleEntries
                .SelectMany(se => se.ClassSession.AttendanceLog)
                .ToList();

            return attendanceLogs;
        }

        //Get attendance of a teacher for a specified time period
        public static List<ClassAttendanceLogEntry> GetTeacherAttendanceForPeriod(
           Staff teacher,
           List<LearningPath> learningPaths,
           DateTime startDate,
           DateTime endDate)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");

            if (learningPaths == null || !learningPaths.Any())
                throw new ArgumentException("Learning paths list cannot be null or empty.", nameof(learningPaths));

            if (endDate < startDate)
                throw new ArgumentException("End date must be greater than or equal to start date.");

            var teacherAttendance = new List<ClassAttendanceLogEntry>();

            foreach (var learningPath in learningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    var classSession = schedule.ClassSession;
                    var attendanceLogs = classSession.AttendanceLog
                        .Where(log => log.Teacher.Id == teacher.Id &&
                                      log.TimeStamp >= startDate &&
                                      log.TimeStamp <= endDate)
                        .ToList();

                    teacherAttendance.AddRange(attendanceLogs);
                }
            }
            return teacherAttendance;
        }

        //To retrieve all Grades of all students for a particular course
        public static List<TestGrade> GetAllGradesForCourse(string courseName, List<Student> students)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

            if (students == null || !students.Any())
                return new List<TestGrade>();

            var allGrades = new List<TestGrade>();

            foreach (var student in students)
            {
                var courseGrades = student.CourseGrade?.TestGrades
                    .Where(grade => grade.Course == courseName)
                    .ToList();

                if (courseGrades != null)
                    allGrades.AddRange(courseGrades);
            }

            return allGrades;
        }

        //To retrieve the homework Grades of a students for a particular course
        public static List<TestGrade> GetHomeworkScoresForCourse(Student student, string courseName)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");

            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

            // Filter for homework grades for the specified course
            return student.CourseGrade?.TestGrades
                .Where(grade => grade.Course == courseName && grade.GradeType == GradeType.Homework)
                .ToList() ?? new List<TestGrade>();
        }
    }
}
