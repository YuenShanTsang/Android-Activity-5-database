using System.Diagnostics;
using Android_Activity_5_database.Models;

namespace Android_Activity_5_database.Views
{
    public partial class CoursePage : ContentPage
    {
        public CoursePage()
        {
            InitializeComponent();
        }

        // Method to handle the click event of the "Add Course" button
        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            // Clear the status message text
            statusMessage.Text = "";

            try
            {
                // Call the AddNewCourse method from the CourseRepository to add a new course
                App.CourseRepo.AddNewCourse(newCourse.Text, newProfessor.Text, newCredits.Text);
                statusMessage.Text = App.CourseRepo.StatusMessage;
            }
            catch (Exception ex)
            {
                // Log or display the error message in case of failure
                Debug.WriteLine($"Error adding new course: {ex.Message}");
                statusMessage.Text = "Failed to add a new course.";
            }
        }

        // Method to handle the click event of the "Edit" button in the CollectionView
        public async void OnUpdateButtonClicked(object sender, EventArgs args)
        {
            // Check if the sender is a Button and if the CommandParameter is a Course object
            if (sender is Button button && button.CommandParameter is Course course)
            {
                // Store the original values of the course before updating
                string originalCourseName = course.CourseName;
                string originalProfessor = course.Professor;
                string originalCredits = course.Credits;

                // Show input dialogs to get the new values for course name, professor, and credits
                string newCourseName = await DisplayPromptAsync("Update Course Name", "Enter the new course name:", "OK", "Cancel", placeholder: course.CourseName);
                string newProfessor = await DisplayPromptAsync("Update Professor", "Enter the new professor name:", "OK", "Cancel", placeholder: course.Professor);
                string newCredits = await DisplayPromptAsync("Update Credits", "Enter the new credits:", "OK", "Cancel", placeholder: course.Credits);

                // If the user cancels the update or enters empty values, do not proceed with the update
                if (string.IsNullOrWhiteSpace(newCourseName) || string.IsNullOrWhiteSpace(newProfessor) || string.IsNullOrWhiteSpace(newCredits))
                    return;

                // Update the course object with the new values
                course.CourseName = newCourseName;
                course.Professor = newProfessor;
                course.Credits = newCredits;

                try
                {
                    // Check if the new course name already exists in the database
                    if (!App.CourseRepo.IsCourseNameExist(course.CourseName, course.CourseId))
                    {
                        // Update the course in the database using the CourseRepository
                        App.CourseRepo.UpdateCourse(course);

                        // Restore the original values since the update was successful
                        course.CourseName = originalCourseName;
                        course.Professor = originalProfessor;
                        course.Credits = originalCredits;

                        // Show a success alert indicating the update was successful
                        await DisplayAlert("Success", "Course information updated successfully.", "OK");

                        // Refresh the course list by triggering the "Get All Courses" button click event
                        OnGetButtonClicked(sender, args);
                    }
                    else
                    {
                        // Show an error alert indicating that the new course name already exists in the database
                        await DisplayAlert("Error", "Course already exists.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Show an error alert if the update fails
                    await DisplayAlert("Error", $"Failed to update course: {ex.Message}", "OK");
                }
            }
        }

        // Method to handle the click event of the "Get All Courses" button
        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            // Clear the status message text
            statusMessage.Text = "";

            try
            {
                // Call the GetAllCourse method from the CourseRepository to retrieve all courses from the database
                List<Course> courses = App.CourseRepo.GetAllCourse();
                courseList.ItemsSource = courses; // Set the collection view's item source to the retrieved courses
            }
            catch (Exception ex)
            {
                // Log or display the error message in case of failure
                Debug.WriteLine($"Error retrieving courses: {ex.Message}");
                statusMessage.Text = "Failed to retrieve courses.";
            }
        }

        // Method to handle the click event of the "Delete" button in the CollectionView
        public void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            // Check if the sender is a Button and if the CommandParameter is a Course object
            if (sender is Button button && button.CommandParameter is Course course)
            {
                try
                {
                    // Call the DeleteCourse method from the CourseRepository to delete the selected course
                    App.CourseRepo.DeleteCourse(course);
                    statusMessage.Text = App.CourseRepo.StatusMessage;
                }
                catch (Exception ex)
                {
                    // Log or display the error message in case of failure
                    Debug.WriteLine($"Error deleting course: {ex.Message}");
                    statusMessage.Text = "Failed to delete the course.";
                }
            }
        }
    }
}
