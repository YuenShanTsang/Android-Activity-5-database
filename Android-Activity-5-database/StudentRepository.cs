using Android_Activity_5_database.Models;
using SQLite;

namespace Android_Activity_5_database
{
    public class StudentRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            // Check if the connection to the database is already established
            if (conn != null)
                return;

            // Create a new SQLite connection and initialize the 'Student' table
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Student>();
        }

        public StudentRepository(string dbPath)
        {
            // Constructor to initialize the database path
            _dbPath = dbPath;
        }

        public void AddNewStudent(string name, string email, string address)
        {
            // Method to add a new student to the database
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address))
            {
                StatusMessage = "Please enter valid student information.";
                return;
            }

            int result = 0;
            try
            {
                Init();

                // Insert a new 'Student' record into the database
                result = conn.Insert(new Student
                {
                    Name = name,
                    Email = email,
                    Address = address
                });

                StatusMessage = $"{result} record(s) added (Name: {name}, Email: {email}, Address: {address})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }

        public void UpdateStudent(Student student)
        {
            // Method to update an existing student in the database
            if (student == null || student.StudentId <= 0 || string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Address))
            {
                StatusMessage = "Invalid student data for updating.";
                return;
            }

            try
            {
                Init();

                // Fetch the existing 'Student' data from the database based on StudentId
                var existingStudent = conn.Table<Student>().FirstOrDefault(s => s.StudentId == student.StudentId);

                if (existingStudent != null)
                {
                    // Update the properties of the existing student with the new values
                    existingStudent.Name = student.Name;
                    existingStudent.Email = student.Email;
                    existingStudent.Address = student.Address;

                    // Perform the database update
                    int result = conn.Update(existingStudent);

                    StatusMessage = $"{result} record(s) updated (StudentId: {student.StudentId})";
                }
                else
                {
                    StatusMessage = $"Student with StudentId {student.StudentId} not found.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update student. Error: {ex.Message}";
            }
        }

        public bool IsEmailExist(string email, int studentId)
        {
            // Method to check if an email already exists in the database (excluding a specific studentId)
            Init();
            return conn.Table<Student>().Any(s => s.Email == email && s.StudentId != studentId);
        }

        public void DeleteStudent(Student student)
        {
            // Method to delete a student from the database
            if (student == null || student.StudentId <= 0)
            {
                StatusMessage = "Invalid student data for deletion.";
                return;
            }

            try
            {
                Init();

                // Delete the 'Student' record from the database
                int result = conn.Delete(student);
                StatusMessage = $"{result} record(s) deleted (Name: {student.Name}, Email: {student.Email}, Address: {student.Address})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete {student.Name}. Error: {ex.Message}";
            }
        }

        public List<Student> GetAllStudent()
        {
            // Method to retrieve all students from the database
            try
            {
                Init();
                return conn.Table<Student>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new List<Student>();
        }
    }
}
