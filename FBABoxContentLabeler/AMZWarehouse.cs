using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
   public class AMZWarehouse
    {
        #region Full Properties and Fields
        private string whsecode;

        public string WhseCode
        {
            get { return whsecode; }
            set { whsecode = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string citystatezip;

        public string CityStateZip
        {
            get { return citystatezip; }
            set { citystatezip = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for AMZWarehouse object.
        /// </summary>
        public AMZWarehouse():this("","","","")
        {                            
        }

      /// <summary>
      /// Overloaded constructor for AMZWarehouse object that takes in parameters.
      /// </summary>
      /// <param name="iwhse">String of the warehouse code.</param>
      /// <param name="iname">String of the name/company for the warehouse.</param>
      /// <param name="iaddress">The address line for the AMZ warehouse.</param>
      /// <param name="icitystatezip">String of City,State Zip for AMZ warehouse.</param>
        public AMZWarehouse(string iwhse, string iname, string iaddress, string icitystatezip)
        {
            whsecode = iwhse;
            name = iname;
            address = iaddress;
            citystatezip = icitystatezip;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Overridden ToString()
        /// </summary>
        /// <returns>Resturns WHSECode and Addressline.</returns>
        public override string ToString()
        {
            string format = whsecode + ":" + "\t"+ address;
            return format;
        }
        #endregion

    }
}
