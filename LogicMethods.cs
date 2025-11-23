using FcmsPortal.Constants;
using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal;

public static class LogicMethods
{
    #region INITIAL SETUP METHODS
    /// <summary>
    /// Methods involved in Initial Setup and Data Filtering
    /// </summary> 

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
    #endregion

    #region SCHEDULING METHODS
    /// <summary>
    /// Methods for Schedule Management and Calendar Operations
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

        while (currentDate.Date <= baseEntry.EndDate?.Date)
        {
            var newEntry = new ScheduleEntry
            {
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

    public static ClassSessionReport? CreateClassSessionReport(ScheduleEntry? scheduleEntry, LearningPath? learningPath)
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

    #endregion

    #region CURRICULUM METHODS
    /// <summary>
    /// Methods for Curriculum Generation and Management
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
                .Select(s => s.ClassSession)
                .OfType<ClassSession>()
                .ToList();

            semesterCurriculum.ClassSessions.AddRange(classSessions);
        }

        return curriculumByClass.Values.ToList();
    }
    #endregion

    #region PAYMENT METHODS
    /// <summary>
    /// Methods for Payment Processing, Fee Management and Financial Reporting
    /// </summary>

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

        learningPath.StudentsWithAccess.RemoveAll(s => s.Id == student.Id);

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
        var currentLearningPath = student.LearningPath;
        var latestPayment = payments.OrderByDescending(p => p.Date).FirstOrDefault();

        string academicYear = string.Empty;
        string semester = string.Empty;

        if (currentLearningPath is not null)
        {
            academicYear = currentLearningPath.AcademicYear;
            semester = currentLearningPath.Semester.ToString();
        }

        if (string.IsNullOrEmpty(academicYear) && latestPayment is not null)
        {
            academicYear = latestPayment.AcademicYear;
            semester = latestPayment.Semester.ToString();
        }

        double paymentCompletionRate = CalculatePaymentCompletionRate(
            student.Person.SchoolFees.TotalPaid,
            student.Person.SchoolFees.TotalAmount
        );

        double timelyCompletionRate = FcmsConstants.DEFAULT_COMPLETION_RATE;
        if (currentLearningPath is not null && latestPayment is not null)
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
            AcademicYear = academicYear,
            Semester = semester,
            TotalFees = student.Person.SchoolFees.TotalAmount,
            TotalPaid = student.Person.SchoolFees.TotalPaid,
            OutstandingBalance = student.Person.SchoolFees.Balance,
            StudentPaymentCompletionRate = paymentCompletionRate,
            StudentTimelyCompletionRate = timelyCompletionRate,
            StudentAddress = student.Person?.Address != null
            ? $"{student.Person.Address.Street}, {student.Person.Address.City}, {student.Person.Address.State}, {student.Person.Address.Country}"
            : string.Empty,
            PaymentDetails = GetPaymentDetails(payments)
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

        var completionRates = new List<double>();

        foreach (var student in students)
        {
            if (student?.Person?.SchoolFees is { TotalAmount: > 0 } fees)
            {
                completionRates.Add(CalculatePaymentCompletionRate(fees.TotalPaid, fees.TotalAmount));
            }
        }

        if (!completionRates.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return completionRates.Average();
    }

    public static double CalculateAverageTimelyCompletionRate(List<Student> students)
    {
        if (students == null || !students.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var timelyRates = new List<double>();

        foreach (var student in students)
        {
            if (student?.LearningPath != null &&
                student?.Person?.SchoolFees?.Payments != null &&
                student.Person.SchoolFees.Payments.Any())
            {
                var latestPayment = student.Person.SchoolFees.Payments
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();

                if (latestPayment != null)
                {
                    var rate = CalculateTimelyCompletionRate(
                        student.LearningPath.SemesterStartDate,
                        student.LearningPath.SemesterEndDate,
                        latestPayment.Date);

                    timelyRates.Add(rate);
                }
            }
        }

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
            var currentPath = student.LearningPath;
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
        var firstLearningPath = allLearningPaths.FirstOrDefault();

        var (totalFees, totalPaid, totalOutstanding) = GetAggregatedFeeData(allStudents);

        return new SchoolPaymentReportEntry
        {
            AcademicYear = firstLearningPath?.AcademicYear ?? "",
            Semester = firstLearningPath?.Semester.ToString() ?? "",
            SemesterStartDate = semesterStart,
            SemesterEndDate = semesterEnd,
            DateAndTimeReportGenerated = DateTime.Now,
            TotalStudents = allStudents.Count,
            TotalSchoolFeesAmount = totalFees,
            TotalAmountPaid = totalPaid,
            TotalOutstanding = totalOutstanding,
            SchoolPaymentCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees),
            AverageStudentPaymentCompletionRateInSchool = CalculateAveragePaymentCompletionRate(allStudents),
            AverageStudentTimelyCompletionRate = CalculateAverageTimelyCompletionRate(allStudents)
        };
    }


    //Generate payment report of all students in a learning path
    public static LearningPathPaymentReportEntry GenerateLearningPathPaymentReport(LearningPath learningPath, List<Student> studentsInPath)
    {
        var semesterStart = learningPath.SemesterStartDate;
        var semesterEnd = learningPath.SemesterEndDate;

        var (totalFees, totalPaid, totalOutstanding) = GetAggregatedFeeData(studentsInPath);

        var allPayments = studentsInPath
            .SelectMany(s => s.Person?.SchoolFees?.Payments ?? new List<Payment>())
            .ToList();

        var latestPaymentDate = allPayments.OrderByDescending(p => p.Date).FirstOrDefault()?.Date ?? semesterEnd;

        return new LearningPathPaymentReportEntry
        {
            AcademicYear = learningPath.AcademicYear,
            Semester = learningPath.Semester.ToString(),
            SemesterStartDate = semesterStart,
            SemesterEndDate = semesterEnd,
            ReportGeneratedDateAndTime = DateTime.Now,
            TotalStudentsInPath = studentsInPath.Count,
            TotalFeesForPath = totalFees,
            TotalPaidForPath = totalPaid,
            OutstandingForPath = totalOutstanding,
            LearningPathPaymentCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees),
            AverageStudentPaymentCompletionRateInPath = CalculateAveragePaymentCompletionRate(studentsInPath),
            LearningPathTimelyCompletionRateInPath = CalculateTimelyCompletionRate(semesterStart, semesterEnd, latestPaymentDate),
            AverageStudentTimelyCompletionRate = CalculateAverageTimelyCompletionRate(studentsInPath)
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

    public static LearningPathPaymentReportEntry GenerateArchivedLearningPathPaymentReport(ArchivedLearningPathPayment archive)
    {
        return new LearningPathPaymentReportEntry
        {
            LearningPathName = $"{archive.EducationLevel} - {archive.ClassLevel}",
            AcademicYear = archive.AcademicYear,
            Semester = archive.Semester.ToString(),
            SemesterStartDate = archive.SemesterStartDate,
            SemesterEndDate = archive.SemesterEndDate,
            ReportGeneratedDateAndTime = archive.ArchivedDate,

            TotalStudentsInPath = archive.TotalStudentsInPath,
            TotalFeesForPath = archive.LearningPathExpectedRevenue,
            TotalPaidForPath = archive.TotalPaid,
            OutstandingForPath = archive.Outstanding,

            LearningPathPaymentCompletionRate = archive.LearningPathPaymentCompletionRate,
            AverageStudentPaymentCompletionRateInPath = archive.AverageStudentPaymentCompletionRateInPath,

            LearningPathTimelyCompletionRateInPath = archive.LearningPathTimelyCompletionRate,
            AverageStudentTimelyCompletionRate = archive.AverageStudentTimelyCompletionRateInPath
        };
    }

    public static SchoolPaymentReportEntry GenerateArchivedSchoolPaymentReport(ArchivedSchoolPaymentSummary archive)
    {
        return new SchoolPaymentReportEntry
        {
            AcademicYear = archive.AcademicYear,
            Semester = archive.Semester.ToString(),
            SemesterStartDate = archive.SemesterStartDate,
            SemesterEndDate = archive.SemesterEndDate,
            DateAndTimeReportGenerated = archive.ArchivedDate,

            TotalStudents = archive.TotalStudents,
            TotalSchoolFeesAmount = archive.TotalExpectedRevenue,
            TotalAmountPaid = archive.TotalAmountReceived,
            TotalOutstanding = archive.TotalOutstandingBalance,

            SchoolPaymentCompletionRate = archive.SchoolWidePaymentCompletionRate,
            AverageStudentPaymentCompletionRateInSchool = archive.AverageStudentPaymentCompletionRateInSchool,
            AverageStudentTimelyCompletionRate = archive.AverageStudentTimelyCompletionRateInSchool
        };
    }

    #endregion

    #region GRADING METHODS
    /// <summary>
    /// Methods for Grading, Grade Calculations and Academic Performance
    /// </summary>

    // Recalculate the total grade for a course based on its test grades and configuration
    public static void RecalculateCourseGrade(CourseGrade courseGrade)
    {
        if (courseGrade?.GradingConfiguration == null || courseGrade.TestGrades == null || !courseGrade.TestGrades.Any())
        {
            if (courseGrade != null)
            {
                courseGrade.TotalGrade = 0;
                courseGrade.FinalGradeCode = "F";
            }
            return;
        }

        var config = courseGrade.GradingConfiguration;
        var homeworkGrades = courseGrade.TestGrades.Where(tg => tg.GradeType == GradeType.Homework);
        var quizGrades = courseGrade.TestGrades.Where(tg => tg.GradeType == GradeType.Quiz);
        var examGrades = courseGrade.TestGrades.Where(tg => tg.GradeType == GradeType.FinalExam);

        double homeworkAvg = homeworkGrades.Any() ? homeworkGrades.Average(g => g.Score) : 0;
        double quizAvg = quizGrades.Any() ? quizGrades.Average(g => g.Score) : 0;
        double examAvg = examGrades.Any() ? examGrades.Average(g => g.Score) : 0;

        double weightedSum = (homeworkAvg * config.HomeworkWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER) +
                             (quizAvg * config.QuizWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER) +
                             (examAvg * config.FinalExamWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER);

        courseGrade.TotalGrade = Math.Round(weightedSum, FcmsConstants.GRADE_ROUNDING_DIGIT);
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
                var courseGrade = student.CourseGrades.FirstOrDefault(cg =>
                    cg.Course == course && cg.LearningPathId == learningPath.Id);

                if (courseGrade != null)
                {
                    RecalculateCourseGrade(courseGrade);
                    courseGrade.IsFinalized = true;
                }
                else
                {
                    var gradingConfig = learningPath.CourseGradingConfigurations
                        .FirstOrDefault(c => c.Course == course);

                    student.CourseGrades.Add(new CourseGrade
                    {
                        Course = course,
                        TotalGrade = 0,
                        FinalGradeCode = "F",
                        LearningPathId = learningPath.Id,
                        StudentId = student.Id,
                        GradingConfiguration = gradingConfig,
                        IsFinalized = true
                    });
                }
            }
        }
    }

    // Compute Semester overall grade average for a student
    public static double CalculateSemesterOverallGrade(Student student, LearningPath learningPath)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");

        var courseGrades = student.CourseGrades
            .Where(cg => cg.LearningPathId == learningPath.Id && cg.TotalGrade > 0)
            .ToList();

        if (!courseGrades.Any())
            return 0;

        return Math.Round(courseGrades.Average(cg => cg.TotalGrade), FcmsConstants.GRADE_ROUNDING_DIGIT);
    }


    //method to arrange CalculateSemesterOverallGrade() of all students in a learning path in descending order
    public static List<(Student Student, double SemesterGrade)> RankStudentsBySemesterGrade(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        if (learningPath.Students == null || learningPath.Students.Count == 0)
            return new List<(Student, double)>();

        var studentGrades = learningPath.Students
            .Select(student => (Student: student, SemesterGrade: CalculateSemesterOverallGrade(student, learningPath)))
            .OrderByDescending(sg => sg.SemesterGrade)
            .ToList();

        return studentGrades;
    }

    public static (string CourseName, double Grade) GetHighestCourseGrade(Student student, int learningPathId)
    {
        var courseGrades = student.CourseGrades
            .Where(cg => cg.LearningPathId == learningPathId && cg.TotalGrade > 0)
            .OrderByDescending(cg => cg.TotalGrade)
            .FirstOrDefault();

        return courseGrades != null
            ? (courseGrades.Course, courseGrades.TotalGrade)
            : ("N/A", 0);
    }

    public static (string CourseName, double Grade) GetLowestCourseGrade(Student student, int learningPathId)
    {
        var courseGrades = student.CourseGrades
            .Where(cg => cg.LearningPathId == learningPathId && cg.TotalGrade > 0)
            .OrderBy(cg => cg.TotalGrade)
            .FirstOrDefault();

        return courseGrades != null
            ? (courseGrades.Course, courseGrades.TotalGrade)
            : ("N/A", 0);
    }

    public static double CalculateWeightedContribution(CourseGrade courseGrade, GradeType gradeType, double weightPercentage)
    {
        if (courseGrade == null) return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var grades = courseGrade.TestGrades.Where(g => g.GradeType == gradeType).ToList();
        if (!grades.Any()) return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var average = grades.Average(g => g.Score);
        var contribution = (average / FcmsConstants.PERCENTAGE_MULTIPLIER) * weightPercentage;

        return Math.Round(contribution, FcmsConstants.GRADE_ROUNDING_DIGIT);
    }

    public static List<GradesReport> GetGradesReports(School school, string academicYear, string semester)
    {
        var reports = new List<GradesReport>();

        if (string.IsNullOrEmpty(academicYear) || string.IsNullOrEmpty(semester))
            return reports;

        var submittedLearningPaths = school.LearningPaths
            .Where(lp => lp.AcademicYear == academicYear &&
                         lp.Semester.ToString() == semester && !lp.IsTemplate &&
                         (lp.ApprovalStatus == PrincipalApprovalStatus.Review ||
                          lp.ApprovalStatus == PrincipalApprovalStatus.Approved))
            .ToList();

        foreach (var learningPath in submittedLearningPaths)
        {
            var teacher = GetPrimaryTeacherForLearningPath(school, learningPath);

            reports.Add(new GradesReport
            {
                LearningPathId = learningPath.Id,
                LearningPathName = GetLearningPathDisplayName(learningPath),
                DateSubmitted = DateTime.Now,
                SubmittedBy = $"{teacher.Person.FirstName} {teacher.Person.LastName}",
                NumberOfStudents = learningPath.Students?.Count ?? 0,
                Status = learningPath.ApprovalStatus
            });
        }

        return reports.OrderByDescending(r => r.DateSubmitted).ToList();
    }

    private static Staff GetPrimaryTeacherForLearningPath(School school, LearningPath learningPath)
    {
        var recentTeacher = learningPath.Schedule?
            .Where(s => s.ClassSession?.Teacher != null)
            .OrderByDescending(s => s.DateTime)
            .FirstOrDefault()?.ClassSession?.Teacher;

        if (recentTeacher != null)
            return recentTeacher;

        return school.Staff?.FirstOrDefault() ?? new Staff
        {
            Person = new Person { FirstName = "Unknown", LastName = "Teacher" }
        };
    }

    public static LearningPathGradeReport GenerateLearningPathGradeReport(LearningPath learningPath)
    {
        if (learningPath == null)
            throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");

        var report = new LearningPathGradeReport
        {
            LearningPath = learningPath,
            Semester = learningPath.Semester,
            IsFinalized = false
        };

        foreach (var student in learningPath.Students)
        {
            var semesterGrade = CalculateSemesterOverallGrade(student, learningPath);
            report.StudentSemesterGrades[student] = semesterGrade;
        }

        report.RankedStudents = report.StudentSemesterGrades
            .Select(kvp => new StudentGradeSummary
            {
                Student = kvp.Key,
                SemesterOverallGrade = kvp.Value
            })
            .OrderByDescending(sg => sg.SemesterOverallGrade)
            .ToList();

        return report;
    }
    #endregion

    #region ATTENDANCE METHODS
    /// <summary>
    /// Methods for Attendance Management and Reporting
    /// </summary>

    // Calculate attendance rate as a percentage
    public static double CalculateAttendanceRate(int presentCount, int totalCount)
    {
        if (totalCount == 0) return 0;
        return Math.Round((double)presentCount / totalCount * FcmsConstants.PERCENTAGE_MULTIPLIER, 1);
    }

    public static (int presentDays, int totalDays, double attendanceRate) CalculateStudentAttendance(
    List<DailyAttendanceLogEntry> attendanceLog, int studentId)
    {
        if (attendanceLog == null || !attendanceLog.Any())
            return (0, 0, 0);

        var totalDays = attendanceLog.Count;
        var presentDays = attendanceLog.Count(log =>
            log.PresentStudents?.Any(s => s.Id == studentId) == true);

        var rate = CalculateAttendanceRate(presentDays, totalDays);

        return (presentDays, totalDays, rate);
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
    #endregion

    #region SEMESTER TRANSITION METHODS
    /// <summary>
    /// Methods for Student Progression, Class Level Management and Archiving
    /// </summary>

    public static bool IsLastClassInEducationLevel(EducationLevel educationLevel, ClassLevel classLevel)
    {
        var classLevelMapping = new ClassLevelMapping();
        var mappings = classLevelMapping.GetClassLevelsByEducationLevel();
        if (mappings.TryGetValue(educationLevel, out var levels))
        {
            return levels.LastOrDefault() == classLevel;
        }
        return false;
    }

    public static ClassLevel? GetNextClassLevel(EducationLevel educationLevel, ClassLevel currentClassLevel)
    {
        var classLevelMapping = new ClassLevelMapping();
        var mappings = classLevelMapping.GetClassLevelsByEducationLevel();
        if (mappings.TryGetValue(educationLevel, out var levels))
        {
            var currentIndex = levels.IndexOf(currentClassLevel);
            if (currentIndex >= 0 && currentIndex < levels.Count - 1)
            {
                return levels[currentIndex + 1];
            }
        }
        return null;
    }

    public static (EducationLevel?, ClassLevel?) GetNextEducationLevelAndClass(EducationLevel currentEducationLevel, ClassLevel currentClassLevel)
    {
        var classLevelMapping = new ClassLevelMapping();
        var mappings = classLevelMapping.GetClassLevelsByEducationLevel();
        if (mappings.TryGetValue(currentEducationLevel, out var currentLevels))
        {
            if (currentLevels.LastOrDefault() == currentClassLevel)
            {
                if (currentEducationLevel == EducationLevel.Kindergarten)
                {
                    return (EducationLevel.Primary, ClassLevel.PRI_1);
                }
                if (currentEducationLevel == EducationLevel.Primary)
                {
                    return (EducationLevel.JuniorCollege, ClassLevel.JC_1);
                }
                if (currentEducationLevel == EducationLevel.JuniorCollege)
                {
                    return (EducationLevel.SeniorCollege, ClassLevel.SC_1);
                }
                if (currentEducationLevel == EducationLevel.SeniorCollege)
                {
                    return (null, null);
                }
            }
        }
        return (null, null);
    }

    public static bool ShouldArchiveStudent(EducationLevel educationLevel, ClassLevel classLevel)
    {
        return educationLevel == EducationLevel.SeniorCollege && classLevel == ClassLevel.SC_3;
    }
    #endregion

    #region UTILITY METHODS
    private static List<PaymentDetails> GetPaymentDetails(List<Payment> payments)
    {
        return payments.Select(p => new PaymentDetails
        {
            Date = p.Date,
            Amount = p.Amount,
            PaymentMethod = p.PaymentMethod.ToString(),
            Reference = p.Reference
        }).ToList();
    }

    private static (double totalFees, double totalPaid, double totalOutstanding) GetAggregatedFeeData(List<Student> students)
    {
        double totalFees = 0;
        double totalPaid = 0;
        double totalOutstanding = 0;

        foreach (var student in students)
        {
            var schoolFees = student.Person?.SchoolFees;
            if (schoolFees != null)
            {
                totalFees += schoolFees.TotalAmount;
                totalPaid += schoolFees.TotalPaid;
                totalOutstanding += schoolFees.Balance;
            }
        }

        return (totalFees, totalPaid, totalOutstanding);
    }

    #endregion

    public static Quote? GetRandomQuote(List<Quote> quotes)
    {
        if (quotes == null || !quotes.Any())
            return null;

        var random = new Random();
        var index = random.Next(quotes.Count);

        return quotes[index];
    }

    public static (Semester semester, DateTime startDate, DateTime endDate) GetDefaultSemesterDates()
    {
        int currentMonth = DateTime.Now.Month;

        // First Semester: January - April
        if (currentMonth >= FcmsConstants.SEMESTER_1_STARTMONTH && currentMonth <= FcmsConstants.SEMESTER_1_ENDMONTH)
        {
            return (
                Semester.First,
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_1_STARTMONTH,
                    FcmsConstants.SEMESTER_1_STARTDAY),
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_1_ENDMONTH,
                    FcmsConstants.SEMESTER_1_ENDDAY)
            );
        }
        // Second Semester: May - August
        else if (currentMonth > FcmsConstants.SEMESTER_1_ENDMONTH &&
                 currentMonth <= FcmsConstants.SEMESTER_2_ENDMONTH)
        {
            return (
                Semester.Second,
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_2_STARTMONTH,
                    FcmsConstants.SEMESTER_2_STARTDAY),
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_2_ENDMONTH,
                    FcmsConstants.SEMESTER_2_ENDDAY)
            );
        }
        // Third Semester: September - December
        else
        {
            return (
                Semester.Third,
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_3_STARTMONTH,
                    FcmsConstants.SEMESTER_3_STARTDAY),
                new DateTime(DateTime.Now.Year,
                    FcmsConstants.SEMESTER_3_ENDMONTH,
                    FcmsConstants.SEMESTER_3_ENDDAY)
            );
        }
    }

    public static (int academicYearStartYear, Semester semester, DateTime semesterStartDate, DateTime semesterEndDate, DateTime? examsStartDate)
    GetAcademicPeriodFormDefaults(AcademicPeriod? currentPeriod)
    {
        if (currentPeriod != null)
        {
            return (
                currentPeriod.AcademicYearStart.Year,
                currentPeriod.Semester,
                currentPeriod.SemesterStartDate,
                currentPeriod.SemesterEndDate,
                currentPeriod.ExamsStartDate
            );
        }
        else
        {
            var (semester, startDate, endDate) = GetDefaultSemesterDates();
            return (
                DateTime.Now.Year,
                semester,
                startDate,
                endDate,
                null
            );
        }
    }
}
