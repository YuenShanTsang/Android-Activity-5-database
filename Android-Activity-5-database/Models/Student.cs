using SQLite;

namespace Android_Activity_5_database.Models
{
    // Define the table name for this class in the database
    [Table("student")]
    
    public class Student
    {
        // Primary key and AutoIncrement attribute to specify StudentId as the primary key with auto-incrementing values
        [PrimaryKey, AutoIncrement]
        public int StudentId { get; set; }

        // MaxLength attribute to set the maximum length of the Name to 100 characters
        public string Name { get; set; }

        // Unique attribute to ensure that Email is unique in the database
        // MaxLength attribute to set the maximum length of the Email to 100 characters
        public string Email { get; set; }

        // MaxLength attribute to set the maximum length of the Address to 250 characters
        public string Address { get; set; }
    }
}
