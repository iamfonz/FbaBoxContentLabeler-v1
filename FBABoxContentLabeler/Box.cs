using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
    public class Box
    {
        #region Full Properties and Fields
        /// <summary>
        /// Full properites and fields to represent contents of a FBA shipment box.
        /// </summary>
        /// 
        private string boxid;

        public string BoxID
        {
            get { return boxid; }
            set { boxid = value; }
        }

        private int boxnumber;

        public int BoxNumber
        {
            get { return boxnumber; }
            set { boxnumber = value; }
        }


        private List<Item> contents = new List<Item>();

        public List<Item> Contents
        {
            get { return contents; }
            set { contents = value; }
        }

        private string po;
        public string PO
        {
        get { return po; }
            
        }


        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor of Box object.
        /// </summary>
        public Box() : this(new List<Item>(), "TBD")
        {


        }

        /// <summary>
        /// Overloaded constructor of Box object where a list of items is passed in.
        /// </summary>
        /// <param name="iItems"> List of Items class object to instantiate Box object</param>
        public Box(List<Item> iItems, string iName)
        {
            contents = iItems;
            boxid = iName;
        }

        /// <summary>
        /// Overloaded constructor for Box object. 
        /// </summary>
        /// <param name="fdh">Pass in a FileDataHolder object and all properties are set.</param>
        public Box(FileDataHolder ifdh)
        {
            SortData(ifdh);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Overidden ToString
        /// </summary>
        /// <returns>Returns box ID</returns>
        public override string ToString()
        {
            return boxid;
        }

        public string ContentsToFile()
        {
            string format = "BoxID:" + boxid + Environment.NewLine;
            foreach(Item i in contents)
            {
                format += i.ToString() + Environment.NewLine;
            }
            format += Environment.NewLine;

            return format;

        }

        /// <summary>
        /// Formats the proper string for Amazon's Fulfillment 2D barcode requires
        /// </summary>
        /// <returns>Returns formatted string for Amazon's 2d Barcode requirements</returns>
        public string FBALabel()
        {
            string format = "AMZN,PO:" + po + ",";
            foreach (Item i in contents)
            {
                format += i.ToString();
            }

            return format;
        }

        /// <summary>
        /// Takes in a FileDataHolder object and goes through it looking for a 'productID' and 'quantity
        /// field. Then sets them into this object's 'itemcode' and 'quantity' property.
        /// </summary>
        /// <param name="fdh">A filled in FileDataHolder object is to be passed in.</param>
        public void SortData(FileDataHolder ifdh)
        {
            string fixname = ifdh.FileName.Remove(0, 5);
            string[] rmvext = fixname.Split('.');
            boxid = rmvext[0];


            string[] usplit = boxid.Split('u');
            po = usplit[0];
            boxnumber = Int32.Parse(usplit[1]);
            boxid = boxid.ToUpper();
            po = po.ToUpper();

            //loop through all the datafields and gather the wanted information
            for (int i = 0; i < ifdh.DataFields.Count; ++i)
            {
                //add FNSKU first
                if (ifdh.DataFields[i].AttributeName.Contains("productId"))
                {
                    for (int j = 0; j < ifdh.DataFields[i].AttributeValues.Count; ++j)
                    {
                        contents.Add(new Item(ifdh.DataFields[i].AttributeValues[j], ""));
                    }
                }

                //add quantity next
                else if (ifdh.DataFields[i].AttributeName.Contains("quantity"))
                {
                    for (int j = 0; j < ifdh.DataFields[i].AttributeValues.Count; ++j)
                    {
                        contents[j].Quantity = ifdh.DataFields[i].AttributeValues[j];
                    }
                }

            }
        }

        #endregion

    }
}
