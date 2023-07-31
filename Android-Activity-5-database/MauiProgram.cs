using Microsoft.Extensions.Logging;

namespace Android_Activity_5_database;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        string dbPath = FileAccessHelper.GetLocalFilePath("students.db3");
        builder.Services.AddSingleton<StudentRepository>(s => ActivatorUtilities.CreateInstance<StudentRepository>(s, dbPath));
        builder.Services.AddSingleton<CourseRepository>(s => ActivatorUtilities.CreateInstance<CourseRepository>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

