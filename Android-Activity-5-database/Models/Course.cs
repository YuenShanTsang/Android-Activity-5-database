using SQLite;

namespace Android_Activity_5_database.Models
{
    // Define the table name for this class in the database
    [Table("course")]
    public class Course
    {
        // Primary key and AutoIncrement attribute to specify CourseId as the primary key with auto-incrementing values
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }

        // Unique attribute to ensure that CourseName is unique in the database
        // MaxLength attribute to set the maximum length of the CourseName to 100 characters
        public string CourseName { get; set; }

        // MaxLength attribute to set the maximum length of the Professor's name to 100 characters
        public string Professor { get; set; }

        // MaxLength attribute to set the maximum length of the Credits to 10 characters
        public string Credits { get; set; }
    }
}
