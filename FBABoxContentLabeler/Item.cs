using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
  public  class Item
    {
        #region Full Properties and Fields
        /// <summary>
        /// Get/Set 
        /// </summary>
        private string itemcode;

        public string ItemCode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }

        private string quantity;

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        #endregion
         

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Item() :this ("", "")
        {
          
        }
        /// <summary>
        /// Overloaded constructor to create Item object.
        /// </summary>
        /// <param name="iItem">Item Code string (FNSKU)</param>
        /// <param name="iqty">Quantity of items</param>
        public Item(string iItem, string iqty)
        {
            itemcode = iItem;
            quantity = iqty;
        }
        #endregion



        #region Methods
        /// <summary>
        /// Overridden ToString method. Formats the object as "FNSKU:(value),QTY:(Value),".
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string format = "FNSKU:" + itemcode + ",QTY:" + quantity + ",";

            return format;
        }


       /// <summary>
       /// Checks and validates data passed in from read in values that are passed in.
       /// </summary>
        public void Validate()
        {

        }
        #endregion

    }
}
