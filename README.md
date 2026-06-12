# FcmsPortal — Domain Library

FcmsPortal is the core domain library for the Future Champions Model School Portal system. It is a class library project that defines all domain models, business logic, constants, enumerations, and utility helpers used across the application. The UI project — [FcmsPortalUI](https://github.com/mondifago/FcmsPortal) — references this library and depends on it for all domain-level concerns.

---

## Table of Contents

- [Purpose](#purpose)
- [Project Structure](#project-structure)
- [Domain Models](#domain-models)
- [Enumerations](#enumerations)
- [Business Logic](#business-logic)
- [Constants](#constants)
- [Utilities](#utilities)
- [Relationship to FcmsPortalUI](#relationship-to-fcmsportalui)

---

## Purpose

FcmsPortal serves as the **domain layer** of the school management system. It has no dependency on ASP.NET, Blazor, Entity Framework, or any infrastructure concern. Its sole responsibility is to define and enforce the academic domain — the entities, rules, calculations, and mappings that reflect how the school operates.

This separation ensures that business logic remains independently testable and free from UI or persistence concerns.

---

## Project Structure

```
FcmsPortal/
├── Models/               # Domain entity classes
├── Enums/                # Application-wide enumerations
├── Constants/            # Named constants (FcmsConstants.cs)
├── LogicMethods.cs       # Pure domain logic and calculations
├── Util.cs               # Shared helper and formatting utilities
└── Models/
    ├── ClassLevelMapping.cs
    └── CourseDefaults.cs
```

---

## Domain Models

The library defines all core entities of the school management domain:

| Model | Description |
|---|---|
| `School` | Root aggregate. Holds all school-level data and relationships. |
| `AcademicPeriod` | Defines the active academic year, term, and semester date boundaries. |
| `Person` | Base identity entity. Extends ASP.NET Identity's `IdentityUser<int>`. |
| `Student` | A person enrolled in the school, linked to a `Guardian` and optionally a `LearningPath`. |
| `Staff` | A school employee, assigned a `UserRole` and linked to class sessions. |
| `Guardian` | A person responsible for one or more students (wards). |
| `LearningPath` | Represents a unique class group for a specific education level, class level, term, and academic year. |
| `ClassSession` | The smallest academic unit — a scheduled delivery of a course topic. |
| `ScheduleEntry` | A calendar item that can represent a class session, an event, or a meeting. |
| `CalendarModel` | A named container of schedule entries. |
| `Homework` | An assignment attached to a class session, with student submissions. |
| `HomeworkSubmission` | A student's response to a homework assignment. |
| `CourseGrade` | Aggregates all test grades for a student in a specific course within a learning path. |
| `TestGrade` | A single scored assessment entry (homework, quiz, or exam). |
| `CourseGradingConfiguration` | Defines the percentage weight for each assessment type per course per learning path. |
| `StudentReportCard` | End-of-term academic summary for a student, including grade, attendance, rank, and promotion status. |
| `DailyAttendanceLogEntry` | Records present and absent students for a learning path on a given day. |
| `SchoolFees` | Tracks a student's total fee liability and all payment records. |
| `Payment` | An individual fee payment transaction. |
| `DiscussionThread` | A thread attached to a class session for student–teacher interaction. |
| `FirstPost` / `Reply` | Posts within a discussion thread. |
| `FileAttachment` | Metadata for an uploaded file resource. |
| `AccountInvitation` | A token-based record used to invite a person to create their system account. |
| `Announcement` | A time-bound message displayed to portal users. |
| `Quote` | An inspirational quote displayed on the portal dashboard. |
| `Address` | An owned value object embedded within `Person` and `School`. |

### Archive Models

Separate denormalized models are used to preserve historical data independently of mutable live entities:

- `ArchivedStudentPayment` / `ArchivedPaymentDetail`
- `ArchivedLearningPathPayment`
- `ArchivedSchoolPaymentSummary`
- `AttendanceArchive`
- `ArchivedLearningPathGrade`
- `ArchivedStudentGrade`
- `ArchivedCourseGrade`
- `ArchivedTestGrade`

---

## Enumerations

| Enum | Values |
|---|---|
| `UserRole` | `Developer`, `Principal`, `Admin`, `Teacher`, `Staff`, `Student`, `Guardian` |
| `EducationLevel` | `Kindergarten`, `Primary`, `JuniorCollege`, `SeniorCollege` |
| `ClassLevel` | `KG_Daycare` through `SC_3` |
| `Semester` | `First`, `Second`, `Third` |
| `GradeType` | `Homework`, `Quiz`, `Exam` |
| `PaymentMethod` | `Cash`, `BankTransfer`, `Card`, `Online` |
| `PrincipalApprovalStatus` | `Pending`, `Review`, `Approved` |
| `Gender` | `Male`, `Female` |
| `Relationship` | `Father`, `Mother`, `FosterParent`, `Sister`, `Brother`, `Uncle`, `Aunt`, `Sponsor` |
| `RecurrenceType` | `Daily`, `Weekly`, `Monthly` |
| `ScheduleType` | `ClassSession`, `Event`, `Meeting` |
| `GuardianSelectionMode` | `Existing`, `New` |

---

## Business Logic

All pure domain calculations and operations live in `LogicMethods.cs`. This class has no external dependencies and is fully static.

### Scheduling
- `GenerateRecurringSchedules` — Expands a recurring schedule entry into individual occurrences.
- `GetAllSchedulesInLearningPath` — Returns ordered schedule entries for a learning path.
- `GetSchedulesByDateInLearningPath` — Filters schedules for a specific date.
- `CreateClassSessionReport` — Builds a report entry from a schedule entry's class session.

### Curriculum
- `GenerateCurriculumFromLearningPaths` — Dynamically compiles a curriculum from the class sessions within learning paths. No static curriculum is stored.

### Payment Calculations
- `HasMetPaymentThreshold` — Checks whether a student has paid at least 50% of fees for a learning path.
- `UpdatePaymentStatus` — Grants or revokes course access based on payment threshold.
- `CalculatePaymentCompletionRate` — Total paid as a percentage of total fees.
- `CalculateTimelyCompletionRate` — How early in the semester full payment was completed.
- `CalculateLearningPathPaymentSummary` — Aggregated payment summary for a learning path.
- `CalculateSchoolPaymentSummary` — School-wide payment aggregation.
- `GenerateStudentPaymentReportEntry` — Builds a student-level payment report.
- `GenerateLearningPathPaymentReport` — Builds a learning-path-level payment report.
- `GenerateSchoolPaymentReport` — Builds a school-wide payment report.
- `GenerateArchivedLearningPathPaymentReport` / `GenerateArchivedSchoolPaymentReport` — Report generation from archived payment records.

### Grading
- `RecalculateCourseGrade` — Applies configured weights to produce a weighted course total.
- `FinalizeSemesterGrades` — Locks and calculates all course grades for all students in a learning path.
- `CalculateSemesterOverallGrade` — Averages finalized course grades for a student.
- `RankStudentsBySemesterGrade` — Returns students ordered by semester grade descending.
- `GenerateLearningPathGradeReport` — Produces a ranked grade report for a learning path.
- `GetHighestCourseGrade` / `GetLowestCourseGrade` — Identifies performance extremes per student.

### Attendance
- `CalculateAttendanceRate` — Present days as a percentage of total days.
- `CalculateStudentAttendance` — Derives present days, total days, and rate from attendance logs.
- `GetDailyAttendanceEntry` — Retrieves the attendance entry for a specific date in a learning path.
- `GenerateSemesterAttendanceReport` — Builds a full attendance matrix for all students across all recorded dates.

### Semester Transition
- `IsLastClassInEducationLevel` — Checks whether a class level is the terminal class for its education level.
- `GetNextClassLevel` — Returns the next class level within the same education level.
- `GetNextEducationLevelAndClass` — Resolves the next education level and entry class upon promotion.
- `ShouldArchiveStudent` — Determines whether a student has completed the final level (`SC_3`) and should be archived.

### Academic Period Defaults
- `GetDefaultSemesterDates` — Returns inferred semester dates based on the current calendar month.
- `GetAcademicPeriodFormDefaults` — Populates form defaults from an existing academic period or system defaults.

---

## Constants

All numeric and configuration literals are extracted to `FcmsConstants.cs` to eliminate magic numbers throughout the codebase.

Key constant groups:

- **Academic Period** — Semester start/end months and days, number of semesters.
- **File Handling** — Max file size, bytes per kilobyte/megabyte, max upload count.
- **Grades** — Grade thresholds (A through F), passing grade, rounding precision, default assessment weights.
- **Calendar** — Days in a week, school day hours, max events per slot, max weeks in month view.
- **Schedule Defaults** — Default class duration, default recurrence end offset.
- **Dashboard** — Default list item count, max quotes, max announcements.
- **Metrics** — Percentage multiplier (100.0), default completion rate (0.0).
- **Payment** — Payment threshold factor (0.5).

---

## Utilities

`Util.cs` provides shared formatting and helper methods used by both business logic and UI components:

- `GetFullName(Person)` — Formats a person's display name.
- `GetLearningPathName(LearningPath)` — Formats a learning path's display name.
- `GetGradeCode(double)` — Converts a numeric grade to a letter code (A–F).
- `GetPromotionStatusForArchive(LearningPath, bool)` — Returns a contextual promotion status string.
- `ToDisplayName()` — Switch-expression extension method on `EducationLevel`, `ClassLevel`, and other enums for UI-safe display strings.
- `ToTermDisplay()` — Extension method on `Semester` for term label formatting.

`ClassLevelMapping.cs` provides the authoritative map of which class levels belong to each education level, used for filtering, navigation, and validation.

`CourseDefaults.cs` provides the standard course list for each education level, which drives learning path course configuration and grading setup.

---

## Relationship to FcmsPortalUI

FcmsPortal is referenced by `FcmsPortalUI` as a project dependency:

```xml
<ProjectReference Include="..\NewFcms\FcmsPortal.csproj" />
```

FcmsPortalUI depends on this library for:
- All domain entity types passed to/from `ISchoolDataService`
- All enumerations used in UI components and filters
- All business logic in `LogicMethods` called from service and component layers
- All constants referenced throughout the UI and service implementations
- All utility/display formatting helpers used in Blazor components

FcmsPortal itself has no knowledge of FcmsPortalUI, Entity Framework, or Blazor. The dependency flows in one direction only.
