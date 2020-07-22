using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
    public class ZPLLabel
    {
        #region Full properties and Fields
       
        private AMZWarehouse amzwhse = new AMZWarehouse();
        private List<Box> boxes = new List<Box>();

        private string command;

        public string ZPLCommand
        {
            get { return command; }
            set { command = value; }
        }





        #endregion
       
        #region Constructors
        /// <summary>
        /// Default constructor for creating a ZPLLabel object.
        /// </summary>
        public ZPLLabel():this(null, null)
        {

        }

        /// <summary>
        /// Overloaded constructor for creating a ZPLLabel Object for FBA Shipments.
        /// </summary>
        /// <param name="iwhse">The AMZWarehouse object that the shipment will be going to.</param>
        /// <param name="iboxes">A list of Box objects that are being shipped to Amazon.</param>
        public ZPLLabel( AMZWarehouse iwhse, List<Box> iboxes)
        {
            command = "";
            amzwhse = iwhse;
            boxes = iboxes;
            

        }

        #endregion


        #region Methods
        /// <summary>
        /// Method that formats text in ZPL 2 to print a label for FBA Shipment.
        /// </summary>
        public void CreateLabels()
        {
            CreateLabels(amzwhse, boxes);
        }

        /// <summary>
        /// Method formats text in ZPL2 to print a label for FBA shipment.
        /// </summary>
        /// <param name="iwhse">The Amazon warehouse it is going to.</param>
        /// <param name="iboxes">List of Boxes object if which is being shipped to Amazon.</param>
        public void CreateLabels(AMZWarehouse iwhse, List<Box> iboxes)
        {
            command = "";
            int boxcount = iboxes.Count;
            for(int i = 0; i<boxcount;++i)
            {
                //Below is the ZPL command being created for each label. 
                command += "^XA" + // ^XA starts the ZPL Command for the printer to interpret
                //start of the Amazon Warehouse Label
                     "^FO50,50^ADN,40,20^FDFBA^FS" +
                     "^FO450,50^ADN,40,20^FDBox " + iboxes[i].BoxNumber + " of " + boxcount + "^FS" +
                     "^FO30,90^GB700,0,8^FS" +
                     "^FO50,105^ADN,25,10^FDSHIP FROM:^FS" +
                     "^FO50,125^ADN,25,10^FDSoleConnect^FS" +
                     "^FO50,145^ADN,25,10^FD4580 Lincoln Rd. NE^FS" +
                     "^FO50,165^ADN,25,10^FDSuite C^FS"+
                     "^FO50,185^ADN,25,10^FDAlbuquerque, NM 87109^FS"+
                     "^FO50,205^ADN,25,10^FDUnited States^FS" +
                     "^FO450,105^ADN,25,10^FDSHIP TO:^FS"+
                     "^FO450,125^ADN,25,10^FDFBA:Shoe Fitters, Inc.^FS"+
                     "^FO450,145^ADN,25,10^FD"+iwhse.Name+"^FS"+
                     "^FO450,165^ADN,25,10^FD"+iwhse.Address+"^FS"+
                     "^FO450,185^ADN,25,10^FD"+iwhse.CityStateZip+"^FS"+
                     "^FO450,205^ADN,25,10^FDUnited States^FS"+
                     "^FO30,235^GB700,0,8^FS"+
                     //code 128 barcode for Amazon label of boxID
                     "^FO100,255^BY3"+
                     "^BCN,150,Y,N,N"+
                     "^FD"+iboxes[i].BoxID+"^FS"+
                     "^FO130,450,^BY3"+
                     //pdf417 barcode for Amazon Label of BOXID
                     "^B7N,8,6,,,N^FD"+iboxes[i].BoxID+"^FS"+ 
                     "^FO5, 700^ADN,30,15^FDPlease leave this label uncovered^FS"+
                     "^FO30,755^GB700,0,8^FS"+
                     "^FO50,780^ADN,40,20^FDBox Content^FS"+
                     //PDF417 barcode of all the items within a box.
                     "^FO30,840,^BY"+
                     "^B7N,8,6,10,75,N^FD"+ iboxes[i].FBALabel()+"^FS"+
                     "^FO5, 1550^ADN,30,15^FDPlease leave this label uncovered^FS"+
                     "^XZ";
            }
            

        }
        
        #endregion
    }
}
