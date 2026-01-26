namespace FcmsPortal.Constants
{
    public static class FcmsConstants
    {
        // Academic Period
        public const int SEMESTER_1_ENDMONTH = 4;
        public const int SEMESTER_2_ENDMONTH = 8;
        public const int SEMESTER_3_ENDMONTH = 12;

        public const int SEMESTER_1_STARTMONTH = 1;
        public const int SEMESTER_2_STARTMONTH = 5;
        public const int SEMESTER_3_STARTMONTH = 9;

        public const int SEMESTER_1_STARTDAY = 10;
        public const int SEMESTER_2_STARTDAY = 10;
        public const int SEMESTER_3_STARTDAY = 1;

        public const int SEMESTER_1_ENDDAY = 30;
        public const int SEMESTER_2_ENDDAY = 15;
        public const int SEMESTER_3_ENDDAY = 15;
        public const int NUMBER_OF_SEMESTERS = 3;

        // File Size
        public const int BYTES_IN_KILOBYTE = 1024;
        public const int KILOBYTES_IN_MEGABYTE = 1024;
        public const int MAX_FILE_SIZE_MB = 5;
        public const int DAYS_IN_YEAR = 365;
        public const long MAX_FILE_SIZE = MAX_FILE_SIZE_MB * BYTES_IN_KILOBYTE * KILOBYTES_IN_MEGABYTE;
        public const double BYTES_IN_MEGABYTE = BYTES_IN_KILOBYTE * KILOBYTES_IN_MEGABYTE;
        public const int MAX_FILE_UPLOAD_COUNT = 10;

        // Grades
        public const int GRADE_ROUNDING_DIGIT = 2;
        public const double TOTAL_SCORE = 100.0;
        public const double A_GRADE_MIN = 70.0;
        public const double B_GRADE_MIN = 60.0;
        public const double C_GRADE_MIN = 50.0;
        public const double D_GRADE_MIN = 40.0;
        public const double E_GRADE_MIN = 30.0;
        public const double PASSING_GRADE = 40.0;
        public const double DEFAULT_HOMEWORK_WEIGHT = 20.0;
        public const double DEFAULT_QUIZ_WEIGHT = 20.0;
        public const double DEFAULT_EXAM_WEIGHT = 60.0;
        public const double PAYMENT_THRESHOLD_FACTOR = 0.5;
        public const int FIRST_PLACE = 1;
        public const int SECOND_PLACE = 2;
        public const int THIRD_PLACE = 3;

        // Calendar 
        public const int DAYS_IN_WEEK = 7;
        public const int MAX_WEEKS_IN_MONTH_VIEW = 6;
        public const int SCHOOL_DAY_START_HOUR = 7;
        public const int SCHOOL_DAY_END_HOUR = 19;
        public const int NOON_HOUR = 12;
        public const int MAX_CALENDAR_EVENTS_PER_SLOT = 3;

        // Schedule Defaults
        public const int DEFAULT_CLASS_DURATION_MINUTES = 60;
        public const int DEFAULT_SCHEDULE_START_HOUR = 8;
        public const int DEFAULT_RECURRENCE_END_MONTHS = 3;

        //Grade Remark Thresholds
        public const double EXCELLENT_GRADE_MIN = 90.0;
        public const double VERY_GOOD_GRADE_MIN = 80.0;
        public const double GOOD_GRADE_MIN = 70.0;
        public const double AVERAGE_GRADE_MIN = 60.0;
        public const double FAIR_GRADE_MIN = 50.0;

        // Dashboard/Pagination Defaults
        public const int DEFAULT_DASHBOARD_LIST_COUNT = 5;
        public const int MAX_QUOTES_COUNT = 10;
        public const int MAX_ANNOUNCEMENTS_COUNT = 3;

        // Metrics
        public const double PERCENTAGE_MULTIPLIER = 100.0;
        public const double DEFAULT_COMPLETION_RATE = 0.0;
    }
}
