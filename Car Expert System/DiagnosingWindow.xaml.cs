using System.Collections.Generic;
using System.Windows;

namespace Car_Expert_System
{
	/// <summary>
	/// Interaction logic for DiagnosingWindow.xaml
	/// </summary>
	public partial class DiagnosingWindow : Window
	{
		int iterator = 0;

		char previousChoice;

		string[] CarRefuseToStartqueries = new string[]
		{
					"Does the engine crank?",
					"Has the starter been acting up? (unusual noises, slow cranking etc",
					"Have the starter cables been replaced recently?",
					"Have the battery or battery cables been replaced recently?",
					"Have there been any other electrical problems?",
					"Was it running fine before it quit suddenly?",
					"Does the headlights go out when you start the car?",
					"Engine cranks but car did not start?",
					"Engine cranks, has spark but car did not start?",
					"Engine has fuel and spark but car did not start?"
		};

		string[] CarBreakdown_EngineOverheating_queries = new string[]
		{
					"Does the engine overheat?",
					"Does the temperature guage read high?",
					"Is there water leakage?",
					"Did you notice a break in fan blade or fan belt?",
					"Did the engine known down completely?",
					"Is the engine producing combustion?",
					"Is the engine still working but car not moving?"
		};

		string[] UnusualCarSoundqueries = new string[]
		{
					"Is it a backfiring sound?",
					"Is it a whining or chattering sound from the engine?",
					"Is it a knocking sound from engine which increases with engine speed?",
					"Does it screech under acceleration?",
					"Does it screech when steering?",
					"Moan from engine when steering?",
					"Roar or rasp sound under acceleration?",
					"Squeal or groan when braking?",
					"Is it a hissing sound from engine?",
					"Is it a clicking sound from wheels?"
		};

		string[] CarSmokequeries = new string[]
		{
					"Is your car emiting blue smoke?",
					"Is your car emiting white smoke?",
					"Is your car emiting black smoke?"
		};

		public List<string> possibleCauses = new List<string>();

		public List<string> possibleSolutions = new List<string>();

		public static DiagnosingWindow Instance;

		public DiagnosingWindow()
		{
			InitializeComponent();

			Instance = this;

			CarDiagnosingTest(iterator);
		}

		private void proceedButton_Click(object sender, RoutedEventArgs e)
		{
			if (yesRadioButton.IsChecked.Value)
			{
				CarDiagnosingTest(++iterator, "y");
			}
			else if (noRadioButton.IsChecked.Value)
			{
				CarDiagnosingTest(++iterator, "n");
			}
			else
			{
				// TODO: speech synthesizer for not selecting an option
				return;
			}

			yesRadioButton.IsChecked = false;
			noRadioButton.IsChecked = false;
		}

		public void TestCompleted()
		{
			if (possibleCauses.Count == 0)
			{
				possibleSolutions.Add("refer to a mechanic");
			}
			else
			{
				foreach (var cause in possibleCauses)
				{
					switch (cause)
					{
						case "starter problem":
							possibleSolutions.Add("check or replace starter");
							break;
						case "battery problem":
							possibleSolutions.Add("recharge or replace battery");
							break;
						case "cable problem":
							possibleSolutions.Add("check for correct cable connection or change cable");
							break;
						case "lack ignition - ignition module":
							possibleSolutions.Add("check ignition");
							break;
						case "fuel pump failure":
							possibleSolutions.Add("check or replace fuel pump");
							break;
						case "broken timing belt":
							possibleSolutions.Add("replace broken timing belt");
							break;
						case "poor battery connection":
							possibleSolutions.Add("check battery connection");
							break;
						case "radiator problem":
							possibleSolutions.Add("check or replace radiator");
							break;
						case "fan belt or fan blade problem":
							possibleSolutions.Add("replace fan belt or fan blade");
							break;
						case "gear problem":
							possibleSolutions.Add("replace car gears");
							break;
						case "oil pump failure or oil pump leakage":
							possibleSolutions.Add("change oil pump");
							break;
						case "radiator or hose problem":
							possibleSolutions.Add("change radiator or hose");
							break;
						case "water pump problem":
							possibleSolutions.Add("change water pump");
							break;
						case "incorrect ignition timing, faulty ignition or leaking valves":
							possibleSolutions.Add("check ignition and/or valves");
							break;
						case "incorrectly tensioned camshaft drive belt":
							possibleSolutions.Add("refer to a mechanic as soon as possible");
							break;
						case "worn camshaft":
							possibleSolutions.Add("refer to a mechanic as soon as possible");
							break;
						case "slipping auxiliary drive belt/fan belt":
							possibleSolutions.Add("check/adjust belt or have replacement fitted");
							break;
						case "power steering belt slipping":
							possibleSolutions.Add("check/adjust belt");
							break;
						case "power steering fluid level too low":
							possibleSolutions.Add("top up power steering fluid level");
							break;
						case "blown exhaust":
							possibleSolutions.Add("check exhaust");
							break;
						case "worn or defective brake components":
							possibleSolutions.Add("refer to a mechanic as soon as possible");
							break;
						case "leak from coolant or air/vacuum hoses":
							possibleSolutions.Add("refer to a mechanic");
							break;
						case "loose hubcap; stone in hubcap, or stone lodged in tyre thread":
							possibleSolutions.Add("tighten hubcap; remove stone");
							break;
						case "valve guide seals or piston rings are worn out":
							possibleSolutions.Add("change valve guide seals or piston rings");
							break;
						case "blown head gasket, a damaged cylinder head or a cracked engine block":
							possibleSolutions.Add("refer to a mechanic");
							break;
						case "fuel injectors and fuel-pressure regulator problem":
							possibleSolutions.Add("check air filter, fuel injectors and fuel-pressure regulator");
							break;
						default:
							possibleSolutions.Add("refer to a mechanic");
							break;
					}
				}
			}

			ResultWindow resultWindow = new ResultWindow();
			resultWindow.Show(); // show result window
			this.Close(); // close result window
		}

		public void CarDiagnosingTest(int i, string userChoice = null)
		{
			if (MenuWindow.Instance.State == MenuWindow.CarState.CarRefuseToStart)
			{
				if (i == 0)
				{
					questionTextBlock.Text = CarRefuseToStartqueries[0];
				}
				else if (i == 1)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("lack ignition - ignition module");
						possibleCauses.Add("fuel pump failure");
						possibleCauses.Add("broken timing belt");

						previousChoice = 'y';

						questionTextBlock.Text = CarRefuseToStartqueries[5];
					}
					else if (userChoice == "n")
					{
						possibleCauses.Add("starter problem");
						possibleCauses.Add("battery problem");

						previousChoice = 'n';

						questionTextBlock.Text = CarRefuseToStartqueries[1];
					}
				}
				else if (i == 2)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							previousChoice = 'y';

							questionTextBlock.Text = CarRefuseToStartqueries[7];
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							//questionTextBlock.Text = CarRefuseToStartqueries[6];

							TestCompleted();
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Remove("battery problem");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							previousChoice = 'n';

							questionTextBlock.Text = CarRefuseToStartqueries[2];
						}
					}
				}
				else if (i == 3)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							previousChoice = 'y';

							questionTextBlock.Text = CarRefuseToStartqueries[8];
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							possibleCauses.Add("poor battery connection");

							TestCompleted();
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Remove("battery problem");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Remove("starter problem");

							previousChoice = 'n';

							questionTextBlock.Text = CarRefuseToStartqueries[3];
						}
					}
				}
				else if (i == 4)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							possibleCauses.Remove("lack ignition - ignition module");
							possibleCauses.Remove("broken timing belt");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							questionTextBlock.Text = CarRefuseToStartqueries[9];
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Remove("battery problem");

							previousChoice = 'n';

							possibleCauses.Add("cable problem");

							TestCompleted();
						}
					}
				}
				else if (i == 5)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							possibleCauses.Clear();

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							TestCompleted();
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Add("broken timing belt");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							TestCompleted();
						}
					}
				}
			}
			else if ((MenuWindow.Instance.State == MenuWindow.CarState.CarBreakdown) || (MenuWindow.Instance.State == MenuWindow.CarState.EngineOverheating))
			{
				if (i == 0)
				{
					questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[0];
				}
				else if (i == 1)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("radiator problem");
						possibleCauses.Add("fan belt or fan blade problem");

						previousChoice = 'y';

						questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[1];
					}
					else if (userChoice == "n")
					{
						previousChoice = 'n';

						questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[6];
					}
				}
				else if (i == 2)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							previousChoice = 'y';

							questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[3];
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[4];
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Add("gear problem");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							previousChoice = 'n';

							TestCompleted();
						}
					}
				}
				else if (i == 3)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							possibleCauses.Remove("radiator problem");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Remove("fan belt or fan blade problem");

							previousChoice = 'n';

							questionTextBlock.Text = CarBreakdown_EngineOverheating_queries[2];
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Clear();

							possibleCauses.Add("oil pump failure or oil pump leakage");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();

							previousChoice = 'n';

							TestCompleted();
						}
					}
				}
				else if (i == 4)
				{
					if (previousChoice == 'y')
					{
						if (userChoice == "y")
						{
							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							previousChoice = 'y';

							TestCompleted();
						}
					}
					else if (previousChoice == 'n')
					{
						if (userChoice == "y")
						{
							possibleCauses.Clear();
							possibleCauses.Add("radiator or hose problem");

							previousChoice = 'y';

							TestCompleted();
						}
						else if (userChoice == "n")
						{
							possibleCauses.Clear();
							possibleCauses.Add("water pump problem");

							previousChoice = 'n';

							TestCompleted();
						}
					}
				}
			}
			else if (MenuWindow.Instance.State == MenuWindow.CarState.UnusualCarSound)
			{
				if (i == 0)
				{
					questionTextBlock.Text = UnusualCarSoundqueries[0];
				}
				else if (i == 1)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("incorrect ignition timing, faulty ignition or leaking valves");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[1];
					}
				}
				else if (i == 2)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("incorrectly tensioned camshaft drive belt");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[2];
					}
				}
				else if (i == 3)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("worn camshaft");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[3];
					}
				}
				else if (i == 4)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("slipping auxiliary drive belt/fan belt");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[4];
					}
				}
				else if (i == 5)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("power steering belt slipping");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[5];
					}
				}
				else if (i == 6)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("power steering fluid level too low");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[6];
					}
				}
				else if (i == 7)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("blown exhaust");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[7];
					}
				}
				else if (i == 8)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("worn or defective brake components");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[8];
					}
				}
				else if (i == 9)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("leak from coolant or air/vacuum hoses");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = UnusualCarSoundqueries[9];
					}
				}
				else if (i == 10)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("loose hubcap; stone in hubcap, or stone lodged in tyre thread");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						TestCompleted();
					}
				}
			}
			else if (MenuWindow.Instance.State == MenuWindow.CarState.SmokeFromCar)
			{
				if (i == 0)
				{
					questionTextBlock.Text = CarSmokequeries[0];
				}
				else if (i == 1)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("valve guide seals or piston rings are worn out");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = CarSmokequeries[1];
					}
				}
				else if (i == 2)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("blown head gasket, a damaged cylinder head or a cracked engine block");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						questionTextBlock.Text = CarSmokequeries[2];
					}
				}
				else if (i == 3)
				{
					if (userChoice == "y")
					{
						possibleCauses.Add("fuel injectors and fuel-pressure regulator problem");
						TestCompleted();
					}
					else if (userChoice == "n")
					{
						TestCompleted();
					}
				}
			}
		}
	}
}
