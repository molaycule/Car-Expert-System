using System.Windows;

namespace Car_Expert_System
{
	/// <summary>
	/// Interaction logic for MenuWindow.xaml
	/// </summary>
	public partial class MenuWindow : Window
	{
		public enum CarState
		{
			CarRefuseToStart,
			CarBreakdown,
			UnusualCarSound,
			SmokeFromCar,
			EngineOverheating
		}

		public CarState State;

		public static MenuWindow Instance;

		public MenuWindow()
		{
			InitializeComponent();

			Instance = this; // singleton
		}

		private void proceedButton_Click(object sender, RoutedEventArgs e)
		{
			if (radioButton1.IsChecked.Value)
			{
				State = CarState.CarRefuseToStart;
			}
			else if (radioButton2.IsChecked.Value)
			{
				State = CarState.CarBreakdown;
			}
			else if (radioButton3.IsChecked.Value)
			{
				State = CarState.UnusualCarSound;
			}
			else if (radioButton4.IsChecked.Value)
			{
				State = CarState.SmokeFromCar;
			}
			else if (radioButton5.IsChecked.Value)
			{
				State = CarState.EngineOverheating;
			}
			else
			{
				// TODO: speech synthesizer for not selecting an option
				return;
			}

			proceedToDiagnosingWindow();
		}

		private void proceedToDiagnosingWindow()
		{
			DiagnosingWindow diagnosingWindow = new DiagnosingWindow();
			diagnosingWindow.Show(); // show diagnosing window
			this.Close(); // close this window
		}
	}
}
