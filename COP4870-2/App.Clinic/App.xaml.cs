namespace App.Clinic;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		//AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

		MainPage = new AppShell();
	}

	// void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
    // {
    //     Exception ex = (Exception)e.ExceptionObject;
    //     Console.WriteLine("Unhandled Exception: " + ex.Message);
    //     // Optionally display an alert
    //     MainThread.BeginInvokeOnMainThread(() =>
    //     {
	// 		if (Application.Current?.MainPage != null)
	// 		{
	// 			Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
	// 		}
	// 		else
	// 		{
	// 			// Handle the case where MainPage is null, possibly by logging or alternative UI
	// 			Console.WriteLine("MainPage is null. Cannot display alert.");
	// 		}
    //     });
    //}
}
