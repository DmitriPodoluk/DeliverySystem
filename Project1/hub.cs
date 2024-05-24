using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace back
{
     class hub
    {
        public hubSouth south;
        public hubNorth north;
        public hubWest west;
        public hubEast east;
        public  List <package> packagesFromTake;
        public List<package> packagesFromWait;
        public List<package> packagesToTake;
        public List<package> packagesToWait;
        public List<truck> trucks;
        truck truck;
        Form1 form;
        public hub(hubSouth s, hubNorth n, hubWest w, hubEast e,Form1 form)
        {
            this.form = form;
            south=s;
            north= n;
            west = w;  
            east=e;
            
            packagesFromWait = new List<package>(); 
            packagesFromTake =  new List<package>();
            packagesToTake = new List<package>();
            packagesToWait = new List<package>();

            trucks = new List<truck>();
           
        }
        public void sort(package p)
        {
           
                if (p.regionTo.ToString().Equals("south"))
                {

                    south.addtoWait(p);

                }
                if (p.regionTo.ToString().Equals("north"))
                {
                    north.addtoWait(p);
                }
                if (p.regionTo.ToString().Equals("west"))
                {
                    west.addtoWait(p);
                }
                if (p.regionTo.ToString().Equals("east"))
                {
                    east.addtoWait(p);

                
            }
                

            }



        

    }
}
