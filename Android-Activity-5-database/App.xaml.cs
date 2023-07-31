namespace Android_Activity_5_database
{
    public partial class App : Application
    {
        // Static properties to hold instances of StudentRepository and CourseRepository
        // These properties will be accessible throughout the application
        public static StudentRepository StudentRepo { get; private set; }
        public static CourseRepository CourseRepo { get; private set; }

        // Constructor for the App class
        // It takes instances of StudentRepository and CourseRepository as parameters
        // These instances are passed from the platform-specific project (e.g., Android or iOS)
        public App(StudentRepository repo, CourseRepository courseRepo)
        {
            // Initialize the application
            InitializeComponent();

            // Set the MainPage of the application to a new instance of AppShell
            // AppShell is typically the starting point of the application's navigation
            MainPage = new AppShell();

            // Assign the passed StudentRepository and CourseRepository instances to the static properties
            // This ensures that the same instances are used throughout the application's lifecycle
            StudentRepo = repo;
            CourseRepo = courseRepo;
        }
    }
}
