﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Tamagotchi;

public partial class MainPage : ContentPage
{
	int count = 0;
    Random rnd = new Random();
    IDispatcherTimer timer;
    Stopwatch stopwatch = new Stopwatch();
	bool is_shiny = false;
	int timertext = 0;
	bool imageChangeOnce = false;

    //To do:
    //If time is above x: evolve

    public MainPage()
	{
		InitializeComponent();
		int random_number = rnd.Next(1, 101);
		if(random_number == 1)
		{
			is_shiny = true;
		}
		

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            
            TimerText.Text = timertext.ToString();

			//health_bar.Progress = health_bar.Progress - 0.1; //for testing

			if(happy_bar.Progress == 0)
			{
				health_bar.Progress = health_bar.Progress - 0.1;
			}
            if (hunger_bar.Progress == 0)
            {
                health_bar.Progress = health_bar.Progress - 0.1;
            }

            if (timertext % 5 == 0 && timertext != 0)
			{
				happy_bar.Progress = happy_bar.Progress - 0.1;
				hunger_bar.Progress = hunger_bar.Progress - 0.1;
			}

            if (health_bar.Progress <= 0.5) 
			{
				if (!imageChangeOnce)
				{
					if (is_shiny)
					{
						tamagotchi_image.Source = "shiny_chimchar_sad.png";
					}
					else
					{
						tamagotchi_image.Source = "chimchar_sad.png";
					}
					imageChangeOnce = true;
				}
               
            }

			if (health_bar.Progress == 0)
			{
				if (is_shiny)
				{
					tamagotchi_image.Source = "shiny_dead_chimchar.png";
				}
				else
				{
					tamagotchi_image.Source = "dead_chimchar.png";
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
        };
    }
	
	private void GiveFood(object sender, EventArgs e)
	{
		hunger_bar.Progress = hunger_bar.Progress + 0.05;
	}

    private void GiveLove(object sender, EventArgs e)
    {
        happy_bar.Progress = happy_bar.Progress + 0.05;
    }

    private void resetcount(object sender, EventArgs e)
	{
		is_shiny = false;
		count = 0;
        //CounterBtn.Text = "Click me";
		health_bar.Progress = 1;
		tamagotchi_image.Source = "egg.png";
		tamagotchi_image.IsEnabled = true;
		timertext = 0;
		happy_bar.Progress = 1;
		hunger_bar.Progress = 1;
		health_bar.Progress = 1;
		pet_stats.IsVisible = true;
		resetbtn.IsVisible = true;
        int random_number = rnd.Next(1, 101);
        if (random_number == 1)
        {
            is_shiny = true;
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
			if(is_shiny)
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

