using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Car_Expert_System
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		PromptStyle promptStyle;

		PromptBuilder promptBuilder;

		SpeechSynthesizer speechSynthesizer;

		public MainWindow()
		{
			InitializeComponent();

			// style of pronounciation
			promptStyle = new PromptStyle();

			// prompt builder for speech synthesizer
			promptBuilder = new PromptBuilder();

			// the speeech synthesizer
			speechSynthesizer = new SpeechSynthesizer();
		}

		private async void titleTextBlock_Loaded(object sender, RoutedEventArgs e)
		{
			await Task.Delay(100);

			promptBuilder.AppendText($"Welcome to {titleTextBlock.Text}");

			// speak the content of the prompt builder
			speechSynthesizer.Speak(promptBuilder);
		}

		private async void loginButton_Click(object sender, RoutedEventArgs e)
		{
			if (new Ping().Send("www.google.com").Status != IPStatus.Success)
			{
				speechSynthesizer.Speak("Check your internet connection");
				notificationTextBlock.Text = "No Connection";
				return;
			}
			else
			{
				notificationTextBlock.Text = String.Empty;
			}

			string userName = usernameTextBox.Text;
			string password = passwordTextBox.Password;
			string commandText = "SELECT * FROM car_es_table"; // sql command

			using (SqlConnection sqlConn = new SqlConnection("Data Source=car-es-server.database.windows.net;Initial Catalog=car-es-db;User ID=car-es-admin;Password=*********"))
			{
				sqlConn.Open();

				using (SqlCommand sqlComm = new SqlCommand(commandText, sqlConn))
				{
					SqlDataReader sqlData = sqlComm.ExecuteReader();

					while (sqlData.Read())
					{
						if (userName.Equals(sqlData["UserName"]) && password.Equals(sqlData["Password"]))
						{
							sqlData.Close();
							notificationTextBlock.Text = "Login Successful";

							await Task.Delay(1000); // same as System.Threading.Thread.Sleep(1000);

							MenuWindow menuWindow = new MenuWindow();
							menuWindow.Show(); // show menu window
							this.Close(); // close this window

							return;
						}
					}

					sqlData.Close();
					notificationTextBlock.Text = "Incorrect User name or Password";
				}
			}
		}

		private async void loadingProgressBar_Loaded(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < 100; i++)
			{
				await Task.Delay(10); // same as System.Threading.Thread.Sleep(10);

				loadingProgressBar.Value++;

				if (loadingProgressBar.Value == 100)
				{
					backgroundImage.Visibility = Visibility.Hidden;
					loadingProgressBar.Visibility = Visibility.Hidden;
					loadingTextBlock.Visibility = Visibility.Hidden;

					promptStyle.Emphasis = PromptEmphasis.Strong;
					promptStyle.Rate = PromptRate.ExtraSlow;
					promptStyle.Volume = PromptVolume.ExtraLoud;

					await Task.Delay(100); // same as System.Threading.Thread.Sleep(100);

					promptBuilder.ClearContent();
					promptBuilder.AppendText("Login ");
					promptBuilder.StartStyle(promptStyle);
					promptBuilder.AppendText("or ");
					promptBuilder.EndStyle();
					promptBuilder.AppendText("SignUp if you have not yet register");

					speechSynthesizer.Speak(promptBuilder);
				}
			}
		}

		private void signupButton_Click(object sender, RoutedEventArgs e)
		{
			SignUpWindow signUpWindow = new SignUpWindow();
			signUpWindow.Show(); // show sign up window
			this.Close(); // close this window
		}

		private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
	}

	public static class CustomCommands
	{
		public static readonly RoutedUICommand Exit = new RoutedUICommand
			(
				"Exit",
				"Exit",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.F4, ModifierKeys.Alt)
				}
			);
	}
}
