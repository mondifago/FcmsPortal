namespace FcmsPortal.Constants
{
    public static class FcmsConstants
    {
        public const int SEMESTER_1_ENDMONTH = 4;
        public const int SEMESTER_2_ENDMONTH = 8;
        public const int SEMESTER_3_ENDMONTH = 12;
        public const int GRADE_ROUNDING_DIGIT = 2;
        public const int NUMBER_OF_SEMESTERS = 3;
        public const int BYTES_IN_KILOBYTE = 1024;
        public const int KILOBYTES_IN_MEGABYTE = 1024;
        public const int MAX_FILE_SIZE_MB = 3;
        public const long MAX_FILE_SIZE = MAX_FILE_SIZE_MB * BYTES_IN_KILOBYTE * KILOBYTES_IN_MEGABYTE;
        public const double BYTES_IN_MEGABYTE = BYTES_IN_KILOBYTE * KILOBYTES_IN_MEGABYTE;
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
        public const double PERCENTAGE_MULTIPLIER = 100.0;
        public const double DEFAULT_COMPLETION_RATE = 0.0;
    }
}
