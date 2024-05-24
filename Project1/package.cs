using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back
{
      class package
    {
       
        public int distanceTo;
        public int distanceFrom;
        public int id=0;
        public string regionTo;
        public string regionFrom;
        public string adressTo; 
        public string adressFrom;

        public  package(string adressFrom, string adressTo , int distance1To, int distance1From,
            string region1To, string region1From,int id1 )
        {

            this.id = id1;
            this.distanceTo = distance1To;
            this.distanceFrom = distance1From;

            this.regionTo = region1To;
            this.regionFrom = region1From;

            this.adressTo = adressTo;
            this.adressFrom = adressFrom;
        }
        public int getId()
        {
            return id;  
        }



    }
}
