using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
   public class FulfillmentCenters
    {
        #region Full properties and Fields
        private List<AMZWarehouse> whse = new List<AMZWarehouse>();

        public List<AMZWarehouse> FBAWarehouses
        {
            get { return whse; }
            set { whse = value; }
        }
        #endregion

        #region Constructor
        public FulfillmentCenters(): this(null)
        {  

        }

        public FulfillmentCenters(List<AMZWarehouse> iwhses)
        {
            whse = iwhses;
        }
        #endregion


    }
}
