using Microsoft.Maui.Layouts;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using static Tamagotchi.TamaClass;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Web;

namespace Tamagotchi;


public partial class MainPage : ContentPage
{
	int count = 0;
    Random rnd = new Random();
    IDispatcherTimer timer;
    Stopwatch stopwatch = new Stopwatch();
	int timertext;
	uint easingtime = 500;
	bool isSadImage = false;
	bool isHappyImage = true;

	bool testJSON = false;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        string name = HttpUtility.UrlDecode(query["name"].ToString());
        string location = HttpUtility.UrlDecode(query["location"].ToString());   
    }



    TamaClass Tamagotchi = new TamaClass("Chimchar", 1, "egg.png", 1, 1, 1, false);



    public class Person
    {
		public string name { get; set; }
        public double health { get; set; }
        public double hunger { get; set; }
        public double happy { get; set; }

		public int time_alive { get; set; }
		public string image { get; set; }
    }

    string path = FileSystem.AppDataDirectory;
    public ObservableCollection<TamaClass> TamaClasses { get; set; } = new ObservableCollection<TamaClass>();



    public MainPage()
	{
		InitializeComponent();
        
        string file = string.Concat(path, "TamaJson.json");
        var RawData = File.ReadAllText(file);
        TamaClasses = JsonSerializer.Deserialize<ObservableCollection<TamaClass>>(RawData);
		BindingContext = this;
        if (testJSON)
        {
            string text = File.ReadAllText("C:/Users/vince/source/repos/Tamagotchi/json/tamagotchi.json");
            var tam = System.Text.Json.JsonSerializer.Deserialize<Person>(text);
            Tamagotchi.SetName(tam.name);
			Tamagotchi.SetHappy(tam.happy);
			Tamagotchi.SetHunger(tam.hunger);
			Tamagotchi.SetHealth(tam.health);
			timertext = Tamagotchi.SetTime(tam.time_alive);
			tamagotchi_image.Source = Tamagotchi.SetImage(tam.image);
            tamagotchi_image.IsEnabled = false;
            tamagotchi_image.Opacity = 1;
            give_buttons.IsVisible = true;
            pet_stats.IsVisible = true;
            
        }
        else
        {
			
            Tamagotchi.SetName("Cool Name");
			timertext = 0;
        }
        health_label.Text = Tamagotchi.health.ToString();
        tam_name.Text = Tamagotchi.health.ToString();


        int random_number = rnd.Next(1, 101);
		if (random_number == 1)
		{
			Tamagotchi.is_shiny = true;
		}
		timer = Dispatcher.CreateTimer();
		timer.Interval = TimeSpan.FromSeconds(1);
		if (testJSON)
		{
            stopwatch.Start();
            timer.Start();
        }
		timer.Tick += async (s, e) =>
		{
			//TimerText.Text = timertext.ToString();
			TimerText.Text = Tamagotchi.time_alive.ToString();
			//health_bar.Progress = Tamagotchi.subtractHealth(0.1); //for testing
			if (timertext % 2 == 0 && timertext != 0)
			{
				if (Tamagotchi.hunger >= 0)
				{
					//hunger_bar.Progress = Tamagotchi.subtractHunger(0.1);
                     await hunger_bar.ProgressTo(Tamagotchi.subtractHunger(0.1), easingtime, Easing.Linear);
                }
				if (Tamagotchi.happy > 0)
				{
					//happy_bar.Progress = Tamagotchi.subtractHappy(0.1);
					 await happy_bar.ProgressTo(Tamagotchi.subtractHappy(0.1), easingtime, Easing.Linear);
				}
			}
			
			if (Tamagotchi.happy == 0)
			{
				health_bar.Progress = Tamagotchi.subtractHealth(0.1);
			}
			if (Tamagotchi.hunger == 0)
			{
				health_bar.Progress = Tamagotchi.subtractHealth(0.1);
			}
			if (Tamagotchi.hunger > 0.5 && Tamagotchi.happy > 0.5 && Tamagotchi.health < 1)
			{
				health_bar.Progress = Tamagotchi.addHealth(0.1);
			}

			if (Tamagotchi.health <= 0.5 && Tamagotchi.health != 0)
			{
				if (!isSadImage)
				{
					if (Tamagotchi.is_shiny)
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("shiny_chimchar_sad.png");
					}
					else
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("chimchar_sad.png");
					}
					isSadImage = true;
					isHappyImage = false;
				}

			}
			else
			{
				if (!isHappyImage)
				{
					if (Tamagotchi.is_shiny)
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("shiny_chimchar.png");
					}
					else
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("chimchar.png");
					}
					isSadImage = false;
					isHappyImage = true;
				}
				if (Tamagotchi.health == 0)
				{
					if (Tamagotchi.is_shiny)
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("shiny_dead_chimchar.png");
					}
					else
					{
						tamagotchi_image.Source = Tamagotchi.SetImage("dead_chimchar.png");
					}
					resetbtn.Opacity = 1;
					pet_stats.IsVisible = false;
					give_buttons.IsVisible = false;
					resetbtn.IsVisible = true;
					TimerText.Text = "";
					stopwatch.Stop();
					timer.Stop();
				}
				timertext++;
                Tamagotchi.SetTime(timertext);
                health_label.Text = Tamagotchi.health.ToString();
				hunger_label.Text = Tamagotchi.hunger.ToString();
				happy_label.Text = Tamagotchi.happy.ToString();
                //string output = JsonConvert.SerializeObject(Tamagotchi);
                //File.WriteAllText("C:/Users/vince/source/repos/Tamagotchi/json/tamagotchi.json", output.ToString());
				//File.OpenWrite
				//File.
            };
		};
	}
	
	private void GiveFood(object sender, EventArgs e)
	{
		//Tamagotchi.hunger = Tamagotchi.hunger + 0.05;
        hunger_bar.Progress = Tamagotchi.addHunger(0.1);
		hunger_label.Text = Tamagotchi.hunger.ToString();
    }

    private void GiveLove(object sender, EventArgs e)
    {
		//Tamagotchi.happy = Tamagotchi.happy + 0.05;
        happy_bar.Progress = Tamagotchi.addHappy(0.1);
		happy_label.Text = Tamagotchi.happy.ToString();
    }

    private void resetcount(object sender, EventArgs e)
	{
		Tamagotchi.is_shiny = false;
		count = 0;
		//CounterBtn.Text = "Click me";
		isHappyImage = true;
		isSadImage = false;
		Tamagotchi.health = 1;
		health_bar.Progress = Tamagotchi.health;
		tamagotchi_image.Source = Tamagotchi.SetImage("egg.png");
		tamagotchi_image.IsEnabled = true;
		timertext = 0;
		Tamagotchi.happy = 1;
		happy_bar.Progress = Tamagotchi.happy;
		Tamagotchi.hunger = 1;
		hunger_bar.Progress = Tamagotchi.hunger;
		Tamagotchi.health = 1;
		health_bar.Progress = Tamagotchi.health;
		pet_stats.IsVisible = true;
		resetbtn.IsVisible = true;
        int random_number = rnd.Next(1, 101);
        if (random_number == 1)
        {
            Tamagotchi.is_shiny = true;
        }
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
		if(count == 1) {
			tamagotchi_image.Source = Tamagotchi.SetImage("egg_cracked.png");
		}
		if(count == 10)
		{
			if(Tamagotchi.is_shiny)
			{
				tamagotchi_image.Source = Tamagotchi.SetImage("shiny_chimchar.png");
            }
			else {
				tamagotchi_image.Source = Tamagotchi.SetImage("chimchar.png");
            }
			
			tamagotchi_image.IsEnabled = true;
			tamagotchi_image.Opacity = 1;
			give_buttons.IsVisible = true;
			pet_stats.IsVisible = true;
            stopwatch.Start();
            timer.Start();
        }

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainMenu());
    }
}

