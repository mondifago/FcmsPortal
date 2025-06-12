using FcmsPortal.Constants;
using FcmsPortal.Enums;
using FcmsPortal.Models;

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

    /// <summary>
    /// Methods involved in Scheduling
    /// </summary>
    // Method to Generate Recurring Schedule Entries
    public static List<ScheduleEntry> GenerateRecurringSchedules(ScheduleEntry baseEntry)
    {
        var schedules = new List<ScheduleEntry>();

        if (!baseEntry.IsRecurring)
        {
            schedules.Add(baseEntry);
            return schedules;
        }

        DateTime currentDate = baseEntry.DateTime;
        int idCounter = 0;

        while (currentDate.Date <= baseEntry.EndDate?.Date)
        {
            var newEntry = new ScheduleEntry
            {
                Id = baseEntry.Id + idCounter++,
                DateTime = currentDate,
                Duration = baseEntry.Duration,
                Venue = baseEntry.Venue,
                ClassSession = baseEntry.ClassSession,
                Title = baseEntry.Title,
                Event = baseEntry.Event,
                Meeting = baseEntry.Meeting,
                IsRecurring = false,
            };

            schedules.Add(newEntry);

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

    /// <summary>
    /// Methods for Curriculum
    /// </summary>

    //Generate Curriculum from Learning Paths 
    public static List<Curriculum> GenerateCurriculumFromLearningPaths(List<LearningPath> learningPaths)
    {
        var curriculumByClass = new Dictionary<(EducationLevel, ClassLevel), Curriculum>();

        foreach (var lp in learningPaths)
        {
            var key = (lp.EducationLevel, lp.ClassLevel);
            if (!curriculumByClass.ContainsKey(key))
            {
                curriculumByClass[key] = new Curriculum
                {
                    AcademicYear = lp.AcademicYear,
                    EducationLevel = lp.EducationLevel,
                    ClassLevel = lp.ClassLevel,
                    Semesters = new List<SemesterCurriculum>()
                };
            }

            var curriculum = curriculumByClass[key];
            var semesterCurriculum = curriculum.Semesters.FirstOrDefault(s => s.Semester == lp.Semester);
            if (semesterCurriculum == null)
            {
                semesterCurriculum = new SemesterCurriculum
                {
                    Semester = lp.Semester,
                    ClassSessions = new List<ClassSession>()
                };
                curriculum.Semesters.Add(semesterCurriculum);
            }

            var classSessions = lp.Schedule
                .Where(s => s.ClassSession != null)
                .Select(s => s.ClassSession)
                .ToList();

            semesterCurriculum.ClassSessions.AddRange(classSessions);
        }

        return curriculumByClass.Values.ToList();
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

    //Update payment status of a student in a learning path
    public static void UpdatePaymentStatus(Student student, LearningPath learningPath)
    {
        if (student?.Person?.SchoolFees == null || learningPath?.StudentsWithAccess == null)
            return;

        bool hasAccess = HasMetPaymentThreshold(student);

        // Remove if already in list
        learningPath.StudentsWithAccess.RemoveAll(s => s.Id == student.Id);

        // Add only if payment threshold met
        if (hasAccess)
        {
            learningPath.StudentsWithAccess.Add(student);
        }
    }

    //generate Student payment summery
    public static StudentPaymentReportEntry GenerateStudentPaymentReportEntry(Student student)
    {
        if (student?.Person?.SchoolFees == null)
        {
            throw new ArgumentException("Invalid student or school fees record.");
        }

        var payments = student.Person.SchoolFees.Payments ?? new List<Payment>();
        var currentLearningPath = student.CurrentLearningPath;
        var latestPayment = payments.OrderByDescending(p => p.Date).FirstOrDefault();

        string studentAddress = null;
        if (student.Person.Addresses != null && student.Person.Addresses.Any())
        {
            var primaryAddress = student.Person.Addresses.FirstOrDefault(a => a.AddressType == AddressType.Home)
                               ?? student.Person.Addresses.First();
            studentAddress = $"{primaryAddress.Street}, {primaryAddress.City}, {primaryAddress.State}, {primaryAddress.Country}";
        }

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

        var paymentDetails = payments.Select(p => new PaymentDetails
        {
            Date = p.Date,
            Amount = p.Amount,
            PaymentMethod = p.PaymentMethod.ToString(),
            Reference = p.Reference
        }).ToList();

        double paymentCompletionRate = CalculatePaymentCompletionRate(
        student.Person.SchoolFees.TotalPaid,
        student.Person.SchoolFees.TotalAmount
        );

        double timelyCompletionRate = FcmsConstants.DEFAULT_COMPLETION_RATE;
        if (currentLearningPath != null && latestPayment != null)
        {
            timelyCompletionRate = CalculateTimelyCompletionRate(
                currentLearningPath.SemesterStartDate,
                currentLearningPath.SemesterEndDate,
                latestPayment.Date
            );
        }

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

    public static double CalculatePaymentCompletionRate(double totalPaid, double totalFees)
    {
        if (totalFees <= FcmsConstants.DEFAULT_COMPLETION_RATE)
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return (totalPaid / totalFees) * FcmsConstants.PERCENTAGE_MULTIPLIER;
    }

    public static double CalculateTimelyCompletionRate(DateTime semesterStart, DateTime semesterEnd, DateTime lastPaymentDate)
    {
        double semesterDurationDays = (semesterEnd - semesterStart).TotalDays;
        double paymentDurationDays = (lastPaymentDate - semesterStart).TotalDays;

        if (semesterDurationDays <= 0 || paymentDurationDays < 0)
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        double rate = (1 - (paymentDurationDays / semesterDurationDays)) * FcmsConstants.PERCENTAGE_MULTIPLIER;
        return Math.Clamp(rate, FcmsConstants.DEFAULT_COMPLETION_RATE, FcmsConstants.PERCENTAGE_MULTIPLIER);
    }

    public static double CalculateAveragePaymentCompletionRate(List<Student> students)
    {
        if (students == null || !students.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var completionRates = students
            .Where(s => s?.Person?.SchoolFees != null && s.Person.SchoolFees.TotalAmount > 0)
            .Select(s => CalculatePaymentCompletionRate(
                s.Person.SchoolFees.TotalPaid,
                s.Person.SchoolFees.TotalAmount))
            .ToList();

        if (!completionRates.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return completionRates.Average();
    }

    public static double CalculateAverageTimelyCompletionRate(List<Student> students)
    {
        if (students == null || !students.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var timelyRates = students
            .Where(s => s?.CurrentLearningPath != null &&
                        s?.Person?.SchoolFees?.Payments != null &&
                        s.Person.SchoolFees.Payments.Any())
            .Select(s =>
            {
                var latestPayment = s.Person.SchoolFees.Payments.OrderByDescending(p => p.Date).FirstOrDefault();
                if (latestPayment == null) return FcmsConstants.DEFAULT_COMPLETION_RATE;

                return CalculateTimelyCompletionRate(
                    s.CurrentLearningPath.SemesterStartDate,
                    s.CurrentLearningPath.SemesterEndDate,
                    latestPayment.Date);
            })
            .ToList();

        if (!timelyRates.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return timelyRates.Average();
    }

    public static double CalculateOverallPaymentCompletionRate(List<Student> students)
    {
        if (students == null || students.Count == 0)
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        double totalPaid = 0;
        double totalFees = 0;

        foreach (var student in students)
        {
            var fees = student.Person?.SchoolFees;
            if (fees == null) continue;

            totalPaid += fees.TotalPaid;
            totalFees += fees.TotalAmount;
        }

        return CalculatePaymentCompletionRate(totalPaid, totalFees);
    }

    public static double CalculateOverallTimelyCompletionRate(List<LearningPath> learningPaths, List<Student> students)
    {
        if (students == null || learningPaths == null || students.Count == 0 || learningPaths.Count == 0)
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var timelyRates = new List<double>();

        foreach (var student in students)
        {
            var currentPath = student.CurrentLearningPath;
            if (currentPath == null)
                continue;

            var path = learningPaths.FirstOrDefault(lp =>
                lp.Id == currentPath.Id &&
                lp.AcademicYear == currentPath.AcademicYear &&
                lp.Semester == currentPath.Semester);

            if (path == null)
                continue;

            var payments = student.Person?.SchoolFees?.Payments;
            if (payments == null || payments.Count == 0)
                continue;

            var latestPaymentDate = payments
                .Where(p => p.Date >= path.SemesterStartDate && p.Date <= path.SemesterEndDate)
                .OrderByDescending(p => p.Date)
                .Select(p => p.Date)
                .FirstOrDefault();

            if (latestPaymentDate == default)
                continue;

            double rate = CalculateTimelyCompletionRate(
                path.SemesterStartDate,
                path.SemesterEndDate,
                latestPaymentDate
            );

            timelyRates.Add(rate);
        }

        if (timelyRates.Count == 0)
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return timelyRates.Average();
    }

    public static SchoolPaymentReportEntry GenerateSchoolPaymentReport(List<LearningPath> allLearningPaths, List<Student> allStudents)
    {
        var semesterStart = allLearningPaths.Min(lp => lp.SemesterStartDate);
        var semesterEnd = allLearningPaths.Max(lp => lp.SemesterEndDate);

        double totalFees = 0;
        double totalPaid = 0;
        double totalOutstanding = 0;

        foreach (var student in allStudents)
        {
            if (student.Person?.SchoolFees != null)
            {
                totalFees += student.Person.SchoolFees.TotalAmount;
                totalPaid += student.Person.SchoolFees.TotalPaid;
                totalOutstanding += student.Person.SchoolFees.Balance;
            }
        }

        double schoolCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees);
        double avgStudentCompletionRate = CalculateAveragePaymentCompletionRate(allStudents);
        double avgTimelyRate = CalculateAverageTimelyCompletionRate(allStudents);

        return new SchoolPaymentReportEntry
        {
            AcademicYear = allLearningPaths.FirstOrDefault()?.AcademicYear ?? "",
            Semester = allLearningPaths.FirstOrDefault()?.Semester.ToString() ?? "",
            SemesterStartDate = semesterStart,
            SemesterEndDate = semesterEnd,
            DateAndTimeReportGenerated = DateTime.Now,
            TotalStudents = allStudents.Count,
            TotalSchoolFeesAmount = totalFees,
            TotalAmountPaid = totalPaid,
            TotalOutstanding = totalOutstanding,
            SchoolPaymentCompletionRate = schoolCompletionRate,
            AverageStudentPaymentCompletionRateInSchool = avgStudentCompletionRate,
            AverageStudentTimelyCompletionRate = avgTimelyRate
        };
    }

    //Generate payment report of all students in a learning path
    public static LearningPathPaymentReportEntry GenerateLearningPathPaymentReport(LearningPath learningPath, List<Student> studentsInPath)
    {
        var semesterStart = learningPath.SemesterStartDate;
        var semesterEnd = learningPath.SemesterEndDate;

        double totalFees = 0;
        double totalPaid = 0;
        double totalOutstanding = 0;

        foreach (var student in studentsInPath)
        {
            if (student.Person?.SchoolFees != null)
            {
                totalFees += student.Person.SchoolFees.TotalAmount;
                totalPaid += student.Person.SchoolFees.TotalPaid;
                totalOutstanding += student.Person.SchoolFees.Balance;
            }
        }

        double pathCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees);
        double avgStudentCompletionRate = CalculateAveragePaymentCompletionRate(studentsInPath);
        double avgTimelyRate = CalculateAverageTimelyCompletionRate(studentsInPath);

        var allPayments = studentsInPath.SelectMany(s => s.Person?.SchoolFees?.Payments ?? new List<Payment>()).ToList();
        double pathTimelyRate = CalculateTimelyCompletionRate(
            semesterStart,
            semesterEnd,
            allPayments.OrderByDescending(p => p.Date).FirstOrDefault()?.Date ?? semesterEnd);

        return new LearningPathPaymentReportEntry
        {
            AcademicYear = learningPath.AcademicYear,
            Semester = learningPath.Semester.ToString(),
            LearningPathName = $"{learningPath.EducationLevel} - {learningPath.ClassLevel}",
            SemesterStartDate = semesterStart,
            SemesterEndDate = semesterEnd,
            ReportGeneratedDateAndTime = DateTime.Now,
            TotalStudentsInPath = studentsInPath.Count,
            TotalFeesForPath = totalFees,
            TotalPaidForPath = totalPaid,
            OutstandingForPath = totalOutstanding,
            LearningPathPaymentCompletionRate = pathCompletionRate,
            AverageStudentPaymentCompletionRateInPath = avgStudentCompletionRate,
            LearningPathTimelyCompletionRateInPath = pathTimelyRate,
            AverageStudentTimelyCompletionRate = avgTimelyRate
        };
    }

    public static LearningPathPaymentSummary CalculateLearningPathPaymentSummary(LearningPath learningPath)
    {
        if (learningPath == null)
            return new LearningPathPaymentSummary();

        var expectedRevenue = learningPath.FeePerSemester * learningPath.Students.Count;
        var totalPaid = GetTotalPaidForLearningPath(learningPath);
        var outstanding = expectedRevenue - totalPaid;
        var paymentRate = CalculatePaymentCompletionRate(totalPaid, expectedRevenue);

        var lastPaymentDate = learningPath.Students
            .SelectMany(s => s.Person?.SchoolFees?.Payments ?? new List<Payment>())
            .Where(p => p.Date >= learningPath.SemesterStartDate && p.Date <= learningPath.SemesterEndDate)
            .OrderByDescending(p => p.Date)
            .Select(p => p.Date)
            .FirstOrDefault();

        var timelyRate = lastPaymentDate == default
            ? FcmsConstants.DEFAULT_COMPLETION_RATE
            : CalculateTimelyCompletionRate(learningPath.SemesterStartDate, learningPath.SemesterEndDate, lastPaymentDate);

        return new LearningPathPaymentSummary
        {
            ExpectedRevenue = expectedRevenue,
            TotalPaid = totalPaid,
            Outstanding = outstanding,
            PaymentCompletionRate = paymentRate,
            TimelyCompletionRate = timelyRate,
            LastPaymentDate = lastPaymentDate == default ? null : lastPaymentDate,
            StudentCount = learningPath.Students.Count,
            FeePerSemester = learningPath.FeePerSemester
        };
    }

    private static double GetTotalPaidForLearningPath(LearningPath learningPath)
    {
        double totalPaid = 0;

        foreach (var student in learningPath.Students)
        {
            var studentFees = student.Person.SchoolFees;
            if (studentFees != null)
            {
                var studentPayments = studentFees.Payments
                    .Where(p => p.LearningPathId == learningPath.Id)
                    .Sum(p => p.Amount);

                totalPaid += studentPayments;
            }
        }

        return totalPaid;
    }

    //Grant student full access to schedule entries in learning path 
    public static void GrantAccessToSchedules(Student student, LearningPath learningPath)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (learningPath == null) throw new ArgumentNullException(nameof(learningPath));

        if (HasMetPaymentThreshold(student) && !learningPath.StudentsWithAccess.Contains(student))
        {
            learningPath.StudentsWithAccess.Add(student);
        }
    }

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
                if (learningPath.StudentsWithAccess.Contains(student))
                {
                    learningPath.StudentsWithAccess.Remove(student);
                }
            }
        }
    }

    /// <summary>
    /// Methods for Class Session Collarboration
    /// </summary>

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
        {
            courseGrade = new CourseGrade { Course = submission.HomeworkGrade.Course };
            student.CourseGrades.Add(courseGrade);
        }

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

    /// <summary>
    /// Methods for Attendance
    /// </summary>

    // Take attendance for a learning path on a specific date
    public static DailyAttendanceLogEntry TakeAttendanceForLearningPath(LearningPath learningPath, List<Student> presentStudents, Staff teacher, DateTime? attendanceDate = null)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (teacher == null)
            throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");

        if (presentStudents == null)
            throw new ArgumentNullException(nameof(presentStudents), "Present students list cannot be null.");

        var targetDate = attendanceDate ?? DateTime.Now;

        var allStudents = learningPath.Students ?? new List<Student>();

        var absentStudents = allStudents.Where(s => !presentStudents.Any(p => p.Id == s.Id)).ToList();

        var attendanceLogEntry = new DailyAttendanceLogEntry
        {
            Id = (learningPath.AttendanceLog?.Count ?? 0) + 1,
            LearningPathId = learningPath.Id,
            LearningPath = learningPath,
            TeacherId = teacher.Id,
            Teacher = teacher,
            PresentStudents = presentStudents,
            AbsentStudents = absentStudents,
            TimeStamp = targetDate
        };

        if (learningPath.AttendanceLog == null)
            learningPath.AttendanceLog = new List<DailyAttendanceLogEntry>();

        learningPath.AttendanceLog.Add(attendanceLogEntry);

        return attendanceLogEntry;
    }

    // Calculate attendance rate as a percentage
    public static double CalculateAttendanceRate(int presentCount, int totalCount)
    {
        if (totalCount == 0) return 0;
        return Math.Round((double)presentCount / totalCount * 100, 1);
    }

    // Get attendance data for a specific date across multiple learning paths
    public static DailyAttendanceLogEntry? GetDailyAttendanceEntry(LearningPath learningPath, DateTime date)
    {
        if (learningPath?.AttendanceLog == null) return null;

        return learningPath.AttendanceLog
            .FirstOrDefault(log => log.TimeStamp.Date == date.Date);
    }


    // Generate semester attendance report for a learning path
    public static SemesterAttendanceReport GenerateSemesterAttendanceReport(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        var report = new SemesterAttendanceReport
        {
            LearningPathId = learningPath.Id,
            LearningPathName = GetLearningPathDisplayName(learningPath),
            StartDate = learningPath.SemesterStartDate,
            EndDate = learningPath.SemesterEndDate
        };

        if (learningPath.AttendanceLog == null || !learningPath.AttendanceLog.Any())
        {
            return report;
        }

        var attendanceDates = learningPath.AttendanceLog
            .Select(log => log.TimeStamp.Date)
            .Distinct()
            .OrderBy(date => date)
            .ToList();

        report.AttendanceDates = attendanceDates;

        var allStudents = learningPath.Students ?? new List<Student>();

        foreach (var student in allStudents)
        {
            var studentAttendance = new StudentSemesterAttendance
            {
                StudentId = student.Id,
                StudentName = $"{student.Person.FirstName} {student.Person.LastName}",
            };

            foreach (var date in attendanceDates)
            {
                var dayLog = learningPath.AttendanceLog.FirstOrDefault(log => log.TimeStamp.Date == date);
                if (dayLog != null)
                {
                    var wasPresent = dayLog.PresentStudents?.Any(s => s.Id == student.Id) == true;
                    studentAttendance.AttendanceByDate[date] = wasPresent;
                }
            }

            report.Students.Add(studentAttendance);
        }

        return report;
    }

    private static string GetLearningPathDisplayName(LearningPath learningPath)
    {
        return $"{learningPath.EducationLevel} - {learningPath.ClassLevel} ({learningPath.AcademicYear} {learningPath.Semester})";
    }

    public static ClassSessionReport CreateClassSessionReport(ScheduleEntry scheduleEntry, LearningPath learningPath)
    {
        if (scheduleEntry?.ClassSession == null || learningPath == null)
            return null;

        return new ClassSessionReport
        {
            ClassSessionId = scheduleEntry.ClassSession.Id,
            LearningPathName = GetLearningPathDisplayName(learningPath),
            Course = scheduleEntry.ClassSession.Course,
            Topic = scheduleEntry.ClassSession.Topic,
            SubmittedBy = scheduleEntry.ClassSession.Teacher?.Person?.LastName ?? "Unknown",
            TimeSubmitted = scheduleEntry.DateTime
        };
    }
}
