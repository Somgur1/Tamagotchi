using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class TamaClass
    {
        public string name;
        public string image;
        public double health = 1;
        public double hunger = 1;
        public double happy = 1;
        public bool is_shiny = false;



        public TamaClass(String name)
        {
            this.name = name;
        }

        public string SetImage(String image)
        {
            this.image = image;
            return this.image;
        }



        // Property 2

        public double  subtractHunger(double by)
        {
            this.hunger = this.hunger - by;

            return this.hunger;
        }
        public double subtractHealth(double by)
        {
            this.health = this.health - by;

            return this.health;

        }
        public double subtractHappy(double by)
        {
            this.happy = this.happy - by;

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
        public string Name { get; }

        // Property 3
        public double GetHunger()
        {
            return hunger;
        }

        // Property 4
        public double GetHappy()
        {
            return happy;
        }


    }
}
