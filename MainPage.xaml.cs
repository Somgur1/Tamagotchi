using Microsoft.Maui.Layouts;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using static Tamagotchi.TamaClass;

namespace Tamagotchi;

public partial class MainPage : ContentPage
{
	int count = 0;
    Random rnd = new Random();
    IDispatcherTimer timer;
    Stopwatch stopwatch = new Stopwatch();
	int timertext = 0;
	bool imageChangeOnce = false;
	TamaClass Chimchar = new TamaClass("Chimchar");

	

	


    //To do:
    //If time is above x: evolve

    public MainPage()
	{
		InitializeComponent();
		int random_number = rnd.Next(1, 101);
		if(random_number == 1)
		{
            Chimchar.is_shiny = true;
		}

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            
            TimerText.Text = timertext.ToString();

			//health_bar.Progress = Chimchar.subtractHealth(0.1); //for testing

			if(happy_bar.Progress == 0)
			{
				health_bar.Progress = Chimchar.subtractHealth(0.1);
			}
            if (hunger_bar.Progress == 0)
            {
                health_bar.Progress = Chimchar.subtractHealth(0.1);
            }
            if (timertext % 5 == 0 && timertext != 0)
			{
				hunger_bar.Progress = Chimchar.subtractHunger(0.1);
				happy_bar.Progress = Chimchar.subtractHappy(0.1);
			}

            if (health_bar.Progress <= 0.5) 
			{
				if (!imageChangeOnce)
				{
					if (Chimchar.is_shiny)
					{
                        tamagotchi_image.Source = Chimchar.SetImage("shiny_chimchar_sad.png"); 
					}
					else
					{
                        tamagotchi_image.Source = Chimchar.SetImage("chimchar_sad.png");
					}
                    imageChangeOnce = true;
				}
               
            }

			if (health_bar.Progress == 0)
			{
				if (Chimchar.is_shiny)
				{
					tamagotchi_image.Source = Chimchar.SetImage("shiny_dead_chimchar.png");
				}
				else
				{
                    tamagotchi_image.Source = Chimchar.SetImage("dead_chimchar.png");
				}
                //tamagotchi_image.Source = Chimchar.image;
                resetbtn.Opacity = 1;
				pet_stats.IsVisible = false;
				give_buttons.IsVisible = false;
				resetbtn.IsVisible = true;
				TimerText.Text = "";
                stopwatch.Stop();
                timer.Stop();
            }
            timertext++;
        };
    }
	
	private void GiveFood(object sender, EventArgs e)
	{
		Chimchar.hunger = Chimchar.hunger + 0.05;
        hunger_bar.Progress = Chimchar.hunger;
    }

    private void GiveLove(object sender, EventArgs e)
    {
		Chimchar.happy = Chimchar.happy + 0.05;
        happy_bar.Progress = Chimchar.happy;
    }

    private void resetcount(object sender, EventArgs e)
	{
		Chimchar.is_shiny = false;
		count = 0;
		//CounterBtn.Text = "Click me";
		Chimchar.health = 1;
		health_bar.Progress = Chimchar.health;
		tamagotchi_image.Source = Chimchar.SetImage("egg.png");
		tamagotchi_image.IsEnabled = true;
		timertext = 0;
		Chimchar.happy = 1;
		happy_bar.Progress = Chimchar.happy;
		Chimchar.hunger = 1;
		hunger_bar.Progress = Chimchar.hunger;
		Chimchar.health = 1;
		health_bar.Progress = Chimchar.health;
		pet_stats.IsVisible = true;
		resetbtn.IsVisible = true;
        int random_number = rnd.Next(1, 101);
        if (random_number == 1)
        {
            Chimchar.is_shiny = true;
        }
    }



private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if(count == 1) {
			tamagotchi_image.Source = "egg_cracked.png";
		}

		if(count == 10)
		{
			if(Chimchar.is_shiny)
			{
				tamagotchi_image.Source = "shiny_chimchar.png";
            }
			else {
                tamagotchi_image.Source = "chimchar.png";
            }
			tamagotchi_image.IsEnabled = false;
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
}

