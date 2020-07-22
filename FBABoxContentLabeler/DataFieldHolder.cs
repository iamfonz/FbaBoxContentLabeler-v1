using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
    /// <summary>
    /// This class is used to hold read in values from a text file. 
    /// </summary>
   public class DataFieldHolder
    {
        #region Full Properties and Fields
        /// <summary>
        /// The first line(header) read from a text file is set the attribute name.
        /// </summary>
        private string attributename;

        public string AttributeName
        {
            get { return attributename; }
            set { attributename = value; }
        }

        /// <summary>
        /// The actual values for each line of data after the header are stored inside a list of strings.
        /// </summary>
        private List<string> attributevalues = new List<string>();

        public List<string> AttributeValues
        {
            get { return attributevalues; }
            set { attributevalues = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataFieldHolder() :this("tbd", new List<string>())
        {

        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="iname">Name of the value</param>
        /// <param name="ivalues"></param>
         public DataFieldHolder(string iname, List<string> ivalues)
        {
            attributename = iname;
            attributevalues = ivalues;
        }

        #endregion


    }
}
