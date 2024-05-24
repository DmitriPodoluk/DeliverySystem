using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace back
{
      class truck
    {
         public string status = "";
   
        public string region;
        public package packageName;
        public List<package> packagesTake=new List<package>();
        public List<package> packagesWait = new List<package>();
        public int timeLeft = 0;
        public int distance = 0;
        public static int id ;
        public string adress;


        public truck()
        {

            id++;
            packagesTake = new List<package>();
            packagesWait = new List<package>();
            this.status = "waiting for order";
            this.timeLeft = 0;
            this.distance = 0;
            this.packageName = new package("","",0,0,"","",0);
            this.adress = "";
        }
       public void addPackageTake(package package)
        {
            packagesTake.Add(package);

        }
        public void addPackageWait(package package)
        {
            packagesWait.Add(package);

        }




    }
}
