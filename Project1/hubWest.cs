using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace back
{
    
    class hubWest
    {
        public int timeLeft;
        public List<package> packagesFromTake;
        public List<package> packagesFromWait;
        public List<package> packagesToTake;
        public List<package> packagesToWait;
        public List<truck> trucks;
        truck truck;
        public Thread truckFrom;
        public Thread truckTo;
         Form1 Form1 = new Form1();
        hub hub;

        public hubWest()
        {
            

            timeLeft = 0;
           
            packagesFromWait = new List<package>(); 
            packagesFromTake = new List<package>();
            packagesToTake = new List<package>();
            packagesToWait = new List<package>();
            Form1 = new Form1();
            trucks = new List<truck>();
            trucks = createTrucks();
        }
    
        public List<package> addFromWait(package packages1)
        {
            packagesFromWait.Add(packages1);

            return packagesFromWait;
        }
        public List<package> addFromTake(package packages1)
        {
            packagesFromTake.Add(packages1);

            return packagesFromTake;
        }

    
        public List<package> addtoTake(package packages1)
        {
            packagesToTake.Add(packages1);

            return packagesToTake;
        }
        public List<package> addtoWait(package packages1)
        {
            packagesToWait.Add(packages1);

            return packagesToWait;
        }

        public List<truck> createTrucks()
        {
            for (int i = 0; i < 2; i++)
            {
                truck = new truck();

                trucks.Add(truck);



            }
            return trucks;
        }

    }
}

