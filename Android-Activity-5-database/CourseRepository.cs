using Android_Activity_5_database.Models;
using SQLite;

namespace Android_Activity_5_database
{
    public class CourseRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            // Check if the connection to the database is already established
            if (conn != null)
                return;

            // Create a new SQLite connection and initialize the 'Course' table
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Course>();
        }

        public CourseRepository(string dbPath)
        {
            // Constructor to initialize the database path
            _dbPath = dbPath;
        }

        public void AddNewCourse(string courseName, string professor, string credits)
        {
            // Method to add a new course to the database
            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(professor) || string.IsNullOrWhiteSpace(credits))
            {
                StatusMessage = "Please enter valid course information.";
                return;
            }

            int result = 0;
            try
            {
                Init();

                // Insert a new 'Course' record into the database
                result = conn.Insert(new Course
                {
                    CourseName = courseName,
                    Professor = professor,
                    Credits = credits
                });

                StatusMessage = $"{result} record(s) added (Course Name: {courseName}, Professor: {professor}, Credits: {credits})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {courseName}. Error: {ex.Message}";
            }
        }

        public void UpdateCourse(Course course)
        {
            // Method to update an existing course in the database
            if (course == null || course.CourseId <= 0 || string.IsNullOrWhiteSpace(course.Professor) || string.IsNullOrWhiteSpace(course.Credits))
            {
                StatusMessage = "Invalid course data for updating.";
                return;
            }

            try
            {
                Init();

                // Fetch the existing 'Course' data from the database based on CourseId
                var existingCourse = conn.Table<Course>().FirstOrDefault(s => s.CourseId == course.CourseId);

                if (existingCourse != null)
                {
                    // Update the properties of the existing course with the new values
                    existingCourse.CourseName = course.CourseName;
                    existingCourse.Professor = course.Professor;
                    existingCourse.Credits = course.Credits;

                    // Perform the database update
                    int result = conn.Update(existingCourse);

                    StatusMessage = $"{result} record(s) updated (CourseId: {course.CourseId})";
                }
                else
                {
                    StatusMessage = $"Course with CourseId {course.CourseId} not found.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update course. Error: {ex.Message}";
            }
        }

        public bool IsCourseNameExist(string courseName, int courseId)
        {
            // Method to check if a course name already exists in the database (excluding a specific courseId)
            Init();
            return conn.Table<Course>().Any(c => c.CourseName == courseName && c.CourseId != courseId);
        }

        public void DeleteCourse(Course course)
        {
            // Method to delete a course from the database
            if (course == null || course.CourseId <= 0)
            {
                StatusMessage = "Invalid student data for deletion.";
                return;
            }

            try
            {
                Init();

                // Delete the 'Course' record from the database
                int result = conn.Delete(course);
                StatusMessage = $"{result} record(s) deleted (Name: {course.CourseName}, Email: {course.Professor}, Address: {course.Credits})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete {course.CourseName}. Error: {ex.Message}";
            }
        }

        public List<Course> GetAllCourse()
        {
            // Method to retrieve all courses from the database
            try
            {
                Init();
                return conn.Table<Course>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new List<Course>();
        }
    }
}
