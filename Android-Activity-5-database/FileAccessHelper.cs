namespace Android_Activity_5_database
{
    public class FileAccessHelper
    {
        // This method returns the local file path for the specified filename
        // The method takes a filename as input and returns the full local file path
        // It combines the app's data directory (obtained from FileSystem.AppDataDirectory) with the filename
        // This ensures that the file is stored in the app's local storage, specific to the platform (e.g., Android, iOS)
        public static string GetLocalFilePath(string filename)
        {
            // Combine the app's data directory with the filename to get the full file path
            // This will be a platform-specific file path (e.g., /data/user/0/com.example.app/files/filename on Android)
            // The file will be stored in the app's local storage directory
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
