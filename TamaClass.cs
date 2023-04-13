using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class TamaClass
    {
        //public string name = "Tamagotchi";
        //public string image;
        //public double health = 1;
        //public double hunger = 1;
        //public double happy = 1;
        //public bool is_shiny = false;
        //public int time_alive = 0;

        public string name { get; set; }
        public int time_alive { get; set; }
        public string image { get; set; }
        public double happy { get; set; }
        public double hunger { get; set; }
        public double health { get; set; }
        public bool is_shiny { get; set; }
        public TamaClass(string name, int time_alive, string image, double happy, double hunger, double health, bool is_shiny)
        {
            this.name = name;
            this.time_alive = 0;
            this.image = image;
            this.happy = 1;
            this.hunger = 1;
            this.health = 1;
            this.is_shiny = false;
        }



        public int SetTime(int time)
        {
            this.time_alive = time;

            return this.time_alive;
        }


        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetHunger(double hunger)
        {
            this.hunger = hunger;
        }

        public void SetHappy(double happy)
        {
            this.happy = happy;
        }

        public void SetHealth(double health)
        {
            this.health = health;
        }

        public string SetImage(String image)
        {
            this.image = image;
            return this.image;
        }


        public double  subtractHunger(double by)
        {
            this.hunger = this.hunger - by;
            this.hunger = Math.Round(this.hunger, 2);

            return this.hunger;
        }
        public double subtractHealth(double by)
        {
            this.health = this.health - by;
            this.health = Math.Round(this.health, 2);

            if(this.health < 0)
            {
                this.health = 0;
            }

            return this.health;

        }

        public double addHealth(double by)
        {
            this.health = this.health + by;
            this.health = Math.Round(this.health, 2);

            if (this.health > 1)
            {
                this.health = 1;
            }

            return this.health;
        }
        public double addHunger(double by) {
            this.hunger += by;

            this.hunger = Math.Round(this.hunger, 2);

            if (this.hunger > 1)
            {
                this.hunger = 1;
            }

            return this.hunger;
        }
        public double addHappy(double by)
        {
            this.happy += by;
            this.happy = Math.Round(this.happy, 2);
            if (this.happy > 1)
            {
                this.happy = 1;
            }

            return this.happy;
        }
        public double subtractHappy(double by)
        {
            this.happy = this.happy - by;
            this.happy = Math.Round(this.happy, 2);
            if(this.happy < 0) { 
            this.happy = 0;
            }

            return this.happy;
        }
        public double GetHealth()
        {
            return health;
        }
        public String GetName()
        {
            return name;
        }
        

       


    }
}
