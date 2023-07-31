using System.Diagnostics;
using Android_Activity_5_database.Models;

namespace Android_Activity_5_database.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Method to handle the click event of the "Add Student" button
        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            // Clear the status message text
            statusMessage.Text = "";

            try
            {
                // Call the AddNewStudent method from the StudentRepository to add a new student
                App.StudentRepo.AddNewStudent(newStudent.Text, newEmail.Text, newAddress.Text);
                statusMessage.Text = App.StudentRepo.StatusMessage;
            }
            catch (Exception ex)
            {
                // Log or display the error message in case of failure
                Debug.WriteLine($"Error adding new student: {ex.Message}");
                statusMessage.Text = "Failed to add a new student.";
            }
        }

        // Method to handle the click event of the "Edit" button in the CollectionView
        public async void OnUpdateButtonClicked(object sender, EventArgs args)
        {
            // Check if the sender is a Button and if the CommandParameter is a Student object
            if (sender is Button button && button.CommandParameter is Student student)
            {
                // Store the original values of the student before updating
                string originalName = student.Name;
                string originalEmail = student.Email;
                string originalAddress = student.Address;

                // Show input dialogs to get the new values for student name, email, and address
                string newName = await DisplayPromptAsync("Update Name", "Enter the new name:", "OK", "Cancel", placeholder: student.Name);
                string newEmail = await DisplayPromptAsync("Update Email", "Enter the new email:", "OK", "Cancel", placeholder: student.Email);
                string newAddress = await DisplayPromptAsync("Update Address", "Enter the new address:", "OK", "Cancel", placeholder: student.Address);

                // If the user cancels the update or enters empty values, do not proceed with the update
                if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newEmail) || string.IsNullOrWhiteSpace(newAddress))
                    return;

                // Update the student object with the new values
                student.Name = newName;
                student.Email = newEmail;
                student.Address = newAddress;

                try
                {
                    // Check if the new email already exists for another student in the database
                    if (!App.StudentRepo.IsEmailExist(student.Email, student.StudentId))
                    {
                        // Update the student in the database using the StudentRepository
                        App.StudentRepo.UpdateStudent(student);

                        // Restore the original values since the update was successful
                        student.Name = originalName;
                        student.Email = originalEmail;
                        student.Address = originalAddress;

                        // Show a success alert indicating the update was successful
                        await DisplayAlert("Success", "Student information updated successfully.", "OK");

                        // Refresh the student list by triggering the "Get All Students" button click event
                        OnGetButtonClicked(sender, args);
                    }
                    else
                    {
                        // Show an error alert indicating that the new email already exists for another student in the database
                        await DisplayAlert("Error", "Email already exists for another student.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Show an error alert if the update fails
                    await DisplayAlert("Error", $"Failed to update student: {ex.Message}", "OK");
                }
            }
        }

        // Method to handle the click event of the "Get All Students" button
        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            // Clear the status message text
            statusMessage.Text = "";

            try
            {
                // Call the GetAllStudent method from the StudentRepository to retrieve all students from the database
                List<Student> students = App.StudentRepo.GetAllStudent();
                studentList.ItemsSource = students; // Set the collection view's item source to the retrieved students
            }
            catch (Exception ex)
            {
                // Log or display the error message in case of failure
                Debug.WriteLine($"Error retrieving students: {ex.Message}");
                statusMessage.Text = "Failed to retrieve students.";
            }
        }

        // Method to handle the click event of the "Delete" button in the CollectionView
        public void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            // Check if the sender is a Button and if the CommandParameter is a Student object
            if (sender is Button button && button.CommandParameter is Student student)
            {
                try
                {
                    // Call the DeleteStudent method from the StudentRepository to delete the selected student
                    App.StudentRepo.DeleteStudent(student);
                    statusMessage.Text = App.StudentRepo.StatusMessage;
                }
                catch (Exception ex)
                {
                    // Log or display the error message in case of failure
                    Debug.WriteLine($"Error deleting student: {ex.Message}");
                    statusMessage.Text = "Failed to delete the student.";
                }
            }
        }
    }
}
