using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;

namespace Car_Expert_System
{
	/// <summary>
	/// Interaction logic for SignUpWindow.xaml
	/// </summary>
	public partial class SignUpWindow : Window
	{
		SpeechSynthesizer speechSynthesizer;
		public SignUpWindow()
		{
			InitializeComponent();

			speechSynthesizer = new SpeechSynthesizer();
		}

		private async void signUpButton_Click(object sender, RoutedEventArgs e)
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
			string email = emailTextBox.Text;
			string insertCommandText = $@"INSERT INTO car_es_table (UserName, Password, Email) VALUES ('{userName}', '{password}', '{email}')"; // sql command

			try
			{
				MailAddress mailAddress = new MailAddress(email); // email format verification

				string selectCommandText = "SELECT * FROM car_es_table"; // sql command

				using (SqlConnection sqlConn = new SqlConnection("Data Source=car-es-server.database.windows.net;Initial Catalog=car-es-db;User ID=car-es-admin;Password=Raystar15"))
				{
					sqlConn.Open();

					using (SqlCommand sqlComm = new SqlCommand(selectCommandText, sqlConn))
					{
						SqlDataReader sqlData = sqlComm.ExecuteReader();

						while (sqlData.Read())
						{
							if (userName.Equals(sqlData["UserName"]))
							{
								sqlData.Close();
								notificationTextBlock.Text = "User name already exist, use a different user name";
								return;
							}
						}

						sqlData.Close();
					}

					using (SqlCommand sqlComm = new SqlCommand(insertCommandText, sqlConn))
					{
						sqlComm.ExecuteNonQuery();
					}

					notificationTextBlock.Text = "Account has been Created";
				}

				await Task.Delay(1000); // same as System.Threading.Thread.Sleep(1000);

				MenuWindow menuWindow = new MenuWindow();
				menuWindow.Show(); // show menu window
				this.Close(); // close this window
			}
			catch (FormatException)
			{
				notificationTextBlock.Text = "Email is not valid";
			}
			catch (Exception)
			{
				notificationTextBlock.Text = "An error occured while creating your account";
			}
		}
	}
}
