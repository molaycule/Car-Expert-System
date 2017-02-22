using System.Windows;

namespace Car_Expert_System
{
	/// <summary>
	/// Interaction logic for ResultWindow.xaml
	/// </summary>
	public partial class ResultWindow : Window
	{
		public ResultWindow()
		{
			InitializeComponent();

			resultTextBlock.Text = "cause(s):\n";

			foreach (var cause in DiagnosingWindow.Instance.possibleCauses)
			{
				resultTextBlock.Text += cause + "\n";
			}

			resultTextBlock.Text += "\nsolution(s):\n";

			foreach (var solution in DiagnosingWindow.Instance.possibleSolutions)
			{
				resultTextBlock.Text += solution + "\n";
			}
		}

		private void backToMenuButton_Click(object sender, RoutedEventArgs e)
		{
			MenuWindow menuWindow = new MenuWindow();
			menuWindow.Show(); // show menu window
			this.Close(); // close this window
		}
	}
}
