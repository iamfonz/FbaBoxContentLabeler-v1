using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
    public class Shipment
    {
        #region Full Properties and Fields
        /// <summary>
        /// Box objects to represent boxes in an FBA Shipment
        /// </summary>
        /// 


        private List<Box> boxes = new List<Box>();

        public List<Box> Boxes
        {
            get { return boxes; }
            set { boxes = value; }
        }




        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor of FBA Shipment object.
        /// </summary>
        public Shipment() :this(new List<Box>())
        {

        }

        /// <summary>
        /// Overloaded construcor of FBA Shipment object. Passes in a list of Box objects
        /// </summary>
        /// <param name="iboxes">List of Box objects to be passed in.</param>
        public Shipment( List<Box> iboxes)
        {
            boxes = iboxes;
        }
        #endregion


        #region Methods

        public void SortBoxes()
        {
            int size = boxes.Count;
            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < (size - i); j++)
                {
                    if (boxes[j].BoxNumber > boxes[j + 1].BoxNumber)
                    {
                        Box temp = boxes[j];
                        boxes[j] = boxes[j + 1];
                        boxes[j + 1] = temp;
                    }
                }
            }

        }



        /// <summary>
        /// ToString() Override Method.
        /// </summary>
        /// <returns>Returns string formatted with Box.FileContents for each Box in shipment object.</returns>
        public override string ToString()
        {
            string format = "";
            foreach(Box b in boxes)
            {
                format += b.ContentsToFile();
            }
            return format;
        }

        #endregion

    }
}
