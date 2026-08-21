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
                RecurrenceType.Weekly => currentDate.AddDays(FcmsConstants.DAYS_IN_WEEK * baseEntry.RecurrenceInterval),
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

    public static ClassSessionReport? CreateClassSessionReport(ScheduleEntry? scheduleEntry)
    {
        if (scheduleEntry?.ClassSession == null)
            return null;

        var classSession = scheduleEntry.ClassSession;

        return new ClassSessionReport
        {
            ClassSessionId = classSession.Id,
            LearningPathName = "",
            Course = classSession.Course,
            Topic = classSession.Topic,
            SubmittedBy = !string.IsNullOrEmpty(classSession.RemarksSubmittedByName)
                ? classSession.RemarksSubmittedByName
                : classSession.Teacher?.Person?.LastName ?? "Unknown",
            TimeSubmitted = classSession.RemarksSubmittedAt ?? scheduleEntry.DateTime
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

    public static SchoolFees? GetFeesForLearningPath(Student student, int learningPathId)
    {
        return student?.SchoolFees?.FirstOrDefault(fees => fees.LearningPathId == learningPathId);
    }

    public static double GetOutstandingBroughtForward(List<SchoolFees> studentFees, LearningPath currentLearningPath)
    {
        if (studentFees == null || currentLearningPath == null)
            return 0;

        return studentFees
            .Where(fees => fees.LearningPathId != currentLearningPath.Id &&
                           fees.LearningPath != null &&
                           fees.LearningPath.SemesterStartDate < currentLearningPath.SemesterStartDate &&
                           fees.Balance > 0)
            .Sum(fees => fees.Balance);
    }

    public static bool IsPaymentWithinBalance(SchoolFees schoolFees, double paymentAmount, int excludePaymentId = 0)
    {
        if (schoolFees == null)
            return false;

        double alreadyPaid = schoolFees.Payments
            .Where(payment => payment.Id != excludePaymentId)
            .Sum(payment => payment.Amount);

        return paymentAmount <= schoolFees.TotalAmount - alreadyPaid;
    }

    private static List<PaymentDetails> GetPaymentDetails(List<Payment> payments)
    {
        return payments.Select(p => new PaymentDetails
        {
            Date = p.Date,
            Amount = p.Amount,
            PaymentMethod = p.PaymentMethod.ToString(),
            Reference = p.Reference,
        }).ToList();
    }

    //generate Student payment summery
    public static StudentPaymentReportEntry GenerateStudentPaymentReportEntry(Student student, int learningPathId)
    {
        var schoolFees = GetFeesForLearningPath(student, learningPathId);

        if (schoolFees == null)
            throw new ArgumentException("No school fees record found for this student in this learning path.");

        var learningPath = schoolFees.LearningPath;
        var payments = schoolFees.Payments.OrderBy(payment => payment.Date).ToList();
        var latestPayment = payments.LastOrDefault();

        double broughtForward = GetOutstandingBroughtForward(student.SchoolFees, learningPath);

        double timelyCompletionRate = FcmsConstants.DEFAULT_COMPLETION_RATE;
        if (learningPath != null && latestPayment != null)
        {
            timelyCompletionRate = CalculateTimelyCompletionRate(
                learningPath.SemesterStartDate, learningPath.SemesterEndDate, latestPayment.Date);
        }

        return new StudentPaymentReportEntry
        {
            DateAndTimeReportGenerated = DateTime.Now,
            StudentFullName = student.Person != null
                ? $"{student.Person.FirstName} {student.Person.LastName}"
                : string.Empty,
            StudentAddress = student.Person?.Address != null
                ? $"{student.Person.Address.Street}, {student.Person.Address.City}, " +
                  $"{student.Person.Address.State}, {student.Person.Address.Country}"
                : string.Empty,
            LearningPathName = learningPath != null
                ? $"{learningPath.EducationLevel} - {learningPath.ClassLevel}"
                : string.Empty,
            AcademicYear = learningPath?.AcademicYear ?? string.Empty,
            Semester = learningPath?.Semester.ToString() ?? string.Empty,

            TotalFees = schoolFees.TotalAmount,
            TotalPaid = schoolFees.TotalPaid,
            OutstandingBalance = schoolFees.Balance,
            BroughtForwardOutstanding = broughtForward,
            TotalOutstanding = schoolFees.Balance + broughtForward,

            StudentPaymentCompletionRate =
                CalculatePaymentCompletionRate(schoolFees.TotalPaid, schoolFees.TotalAmount),
            StudentTimelyCompletionRate = timelyCompletionRate,
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

    public static double CalculateAveragePaymentCompletionRate(List<SchoolFees> fees)
    {
        if (fees == null || !fees.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var completionRates = fees
            .Where(schoolFees => schoolFees.TotalAmount > 0)
            .Select(schoolFees => CalculatePaymentCompletionRate(schoolFees.TotalPaid, schoolFees.TotalAmount))
            .ToList();

        if (!completionRates.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return completionRates.Average();
    }

    public static double CalculateAverageTimelyCompletionRate(List<SchoolFees> fees)
    {
        if (fees == null || !fees.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        var timelyRates = new List<double>();

        foreach (var schoolFees in fees)
        {
            if (schoolFees.LearningPath == null || !schoolFees.Payments.Any())
                continue;

            var latestPaymentDate = schoolFees.Payments.Max(payment => payment.Date);

            timelyRates.Add(CalculateTimelyCompletionRate(
                schoolFees.LearningPath.SemesterStartDate,
                schoolFees.LearningPath.SemesterEndDate,
                latestPaymentDate));
        }

        if (!timelyRates.Any())
            return FcmsConstants.DEFAULT_COMPLETION_RATE;

        return timelyRates.Average();
    }


    public static SchoolPaymentReportEntry GenerateSchoolPaymentReport(
        List<LearningPath> currentLearningPaths, List<SchoolFees> allStudentFees)
    {
        if (currentLearningPaths == null || !currentLearningPaths.Any() || allStudentFees == null)
            return new SchoolPaymentReportEntry();

        var currentLearningPathIds = currentLearningPaths.Select(learningPath => learningPath.Id).ToHashSet();

        var currentFees = allStudentFees
            .Where(fees => currentLearningPathIds.Contains(fees.LearningPathId))
            .ToList();

        var broughtForwardFees = allStudentFees
            .Where(fees => !currentLearningPathIds.Contains(fees.LearningPathId) && fees.Balance > 0)
            .ToList();

        double totalFees = currentFees.Sum(fees => fees.TotalAmount);
        double totalPaid = currentFees.Sum(fees => fees.TotalPaid);
        double broughtForward = broughtForwardFees.Sum(fees => fees.Balance);

        var firstLearningPath = currentLearningPaths.First();

        return new SchoolPaymentReportEntry
        {
            AcademicYear = firstLearningPath.AcademicYear,
            Semester = firstLearningPath.Semester.ToString(),
            SemesterStartDate = currentLearningPaths.Min(learningPath => learningPath.SemesterStartDate),
            SemesterEndDate = currentLearningPaths.Max(learningPath => learningPath.SemesterEndDate),
            DateAndTimeReportGenerated = DateTime.Now,

            TotalStudents = currentFees.Select(fees => fees.StudentId).Distinct().Count(),
            TotalSchoolFeesAmount = totalFees,
            TotalAmountPaid = totalPaid,
            TotalOutstanding = totalFees - totalPaid,
            TotalBroughtForwardOutstanding = broughtForward,
            TotalOutstandingIncludingBroughtForward = (totalFees - totalPaid) + broughtForward,

            SchoolPaymentCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees),
            AverageStudentPaymentCompletionRateInSchool = CalculateAveragePaymentCompletionRate(currentFees),
            AverageStudentTimelyCompletionRate = CalculateAverageTimelyCompletionRate(currentFees)
        };
    }

    //Generate payment report of all students in a learning path
    public static LearningPathPaymentReportEntry GenerateLearningPathPaymentReport(
     LearningPath learningPath, List<SchoolFees> feesInPath)
    {
        if (learningPath == null || feesInPath == null)
            return new LearningPathPaymentReportEntry();

        double totalFees = feesInPath.Sum(fees => fees.TotalAmount);
        double totalPaid = feesInPath.Sum(fees => fees.TotalPaid);

        var latestPaymentDate = feesInPath
            .SelectMany(fees => fees.Payments)
            .OrderByDescending(payment => payment.Date)
            .Select(payment => payment.Date)
            .FirstOrDefault();

        if (latestPaymentDate == default)
            latestPaymentDate = learningPath.SemesterEndDate;

        return new LearningPathPaymentReportEntry
        {
            AcademicYear = learningPath.AcademicYear,
            Semester = learningPath.Semester.ToString(),
            LearningPathName = $"{learningPath.EducationLevel} - {learningPath.ClassLevel}",
            SemesterStartDate = learningPath.SemesterStartDate,
            SemesterEndDate = learningPath.SemesterEndDate,
            ReportGeneratedDateAndTime = DateTime.Now,
            TotalStudentsInPath = feesInPath.Count,
            TotalFeesForPath = totalFees,
            TotalPaidForPath = totalPaid,
            OutstandingForPath = totalFees - totalPaid,
            LearningPathPaymentCompletionRate = CalculatePaymentCompletionRate(totalPaid, totalFees),
            AverageStudentPaymentCompletionRateInPath = CalculateAveragePaymentCompletionRate(feesInPath),
            LearningPathTimelyCompletionRateInPath = CalculateTimelyCompletionRate(
                learningPath.SemesterStartDate, learningPath.SemesterEndDate, latestPaymentDate),
            AverageStudentTimelyCompletionRate = CalculateAverageTimelyCompletionRate(feesInPath)
        };
    }

    public static LearningPathPaymentSummary CalculateLearningPathPaymentSummary(
    LearningPath learningPath, List<SchoolFees> feesInPath)
    {
        if (learningPath == null || feesInPath == null)
            return new LearningPathPaymentSummary();

        double expectedRevenue = feesInPath.Sum(fees => fees.TotalAmount);
        double totalPaid = feesInPath.Sum(fees => fees.TotalPaid);

        var lastPaymentDate = feesInPath
            .SelectMany(fees => fees.Payments)
            .Where(payment => payment.Date >= learningPath.SemesterStartDate &&
                              payment.Date <= learningPath.SemesterEndDate)
            .OrderByDescending(payment => payment.Date)
            .Select(payment => payment.Date)
            .FirstOrDefault();

        double timelyRate = lastPaymentDate == default
            ? FcmsConstants.DEFAULT_COMPLETION_RATE
            : CalculateTimelyCompletionRate(
                learningPath.SemesterStartDate, learningPath.SemesterEndDate, lastPaymentDate);

        return new LearningPathPaymentSummary
        {
            ExpectedRevenue = expectedRevenue,
            TotalPaid = totalPaid,
            Outstanding = expectedRevenue - totalPaid,
            PaymentCompletionRate = CalculatePaymentCompletionRate(totalPaid, expectedRevenue),
            TimelyCompletionRate = timelyRate,
            LastPaymentDate = lastPaymentDate == default ? null : lastPaymentDate,
            StudentCount = feesInPath.Count,
            FeePerSemester = learningPath.FeePerSemester
        };
    }

    public static SchoolPaymentSummary CalculateSchoolPaymentSummary(
    List<LearningPath> currentLearningPaths, List<SchoolFees> allStudentFees)
    {
        var summary = new SchoolPaymentSummary();

        if (currentLearningPaths == null || allStudentFees == null)
            return summary;

        var currentLearningPathIds = currentLearningPaths.Select(learningPath => learningPath.Id).ToHashSet();

        var currentFees = allStudentFees
            .Where(fees => currentLearningPathIds.Contains(fees.LearningPathId))
            .ToList();

        var broughtForwardFees = allStudentFees
            .Where(fees => !currentLearningPathIds.Contains(fees.LearningPathId) && fees.Balance > 0)
            .ToList();

        summary.TotalLearningPaths = currentLearningPaths.Count;
        summary.TotalStudents = currentFees.Select(fees => fees.StudentId).Distinct().Count();

        summary.TotalExpectedRevenue = currentFees.Sum(fees => fees.TotalAmount);
        summary.TotalAmountReceived = currentFees.Sum(fees => fees.TotalPaid);
        summary.TotalOutstanding = summary.TotalExpectedRevenue - summary.TotalAmountReceived;

        summary.TotalBroughtForwardOutstanding = broughtForwardFees.Sum(fees => fees.Balance);
        summary.TotalOutstandingIncludingBroughtForward =
            summary.TotalOutstanding + summary.TotalBroughtForwardOutstanding;

        summary.FullyPaidStudents = currentFees.Count(fees => fees.TotalAmount > 0 && fees.Balance <= 0);
        summary.StudentsWithBalance = currentFees.Count(fees => fees.TotalAmount > 0 && fees.Balance > 0);

        summary.PaymentCompletionRate =
            CalculatePaymentCompletionRate(summary.TotalAmountReceived, summary.TotalExpectedRevenue);
        summary.TimelyCompletionRate = CalculateAverageTimelyCompletionRate(currentFees);

        return summary;
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
        var examGrades = courseGrade.TestGrades.Where(tg => tg.GradeType == GradeType.Exam);

        double homeworkAvg = homeworkGrades.Any() ? homeworkGrades.Average(g => g.Score) : 0;
        double quizAvg = quizGrades.Any() ? quizGrades.Average(g => g.Score) : 0;
        double examAvg = examGrades.Any() ? examGrades.Average(g => g.Score) : 0;

        double weightedSum = (homeworkAvg * config.HomeworkWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER) +
                             (quizAvg * config.QuizWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER) +
                             (examAvg * config.FinalExamWeightPercentage / FcmsConstants.PERCENTAGE_MULTIPLIER);

        courseGrade.TotalGrade = Math.Round(weightedSum, FcmsConstants.GRADE_ROUNDING_DIGIT);
        courseGrade.FinalGradeCode = GetGradeCode(courseGrade.TotalGrade);
    }

    // Map a total grade to its letter grade code
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

    // Promotion grade averages only the semesters a student actually has,
    // so a mid-year transfer is not averaged against terms never sat.
    public static double CalculatePromotionGrade(Dictionary<Semester, double> semesterGrades)
    {
        return semesterGrades.Any()
            ? Math.Round(semesterGrades.Values.Average(), FcmsConstants.GRADE_ROUNDING_DIGIT)
            : 0;
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
            LearningPathName = "",
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

    // Every report card must carry both teacher and principal remarks before finalization
    public static List<Student> GetStudentsWithIncompleteReportCardRemarks(LearningPath learningPath,List<StudentReportCard> reportCards)
    {
        return learningPath.Students
            .Where(student =>
            {
                var card = reportCards.FirstOrDefault(rc => rc.StudentId == student.Id);
                return card == null ||
                       string.IsNullOrWhiteSpace(card.TeacherRemarks) ||
                       string.IsNullOrWhiteSpace(card.PrincipalRemarks);
            })
            .ToList();
    }

    public static bool IsLearningPathReadOnly(PrincipalApprovalStatus approvalStatus)
    {
        return approvalStatus == PrincipalApprovalStatus.Approved;
    }
    #endregion

    #region QUOTES
    public static Quote? GetRandomQuote(List<Quote> quotes)
    {
        if (quotes == null || !quotes.Any())
            return null;

        var random = new Random();
        var index = random.Next(quotes.Count);

        return quotes[index];
    }
    #endregion

    #region ACADEMIC PERIOD
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
    #endregion
}
