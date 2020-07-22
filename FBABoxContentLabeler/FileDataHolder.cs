using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBABoxContentLabeler
{
    /// <summary>
    /// This classes serves as an object to hold data from a delimited file.
    /// </summary>
  public class FileDataHolder
    {
        #region Full Properties and Fields
        private string filename;

        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }


        private List<DataFieldHolder> datafields = new List<DataFieldHolder>();

        public List<DataFieldHolder> DataFields
        {
            get { return datafields; }
            set { datafields = value; }
        }


        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor instantiates object.
        /// </summary>
        public FileDataHolder() :this("",new List<DataFieldHolder>())
        {
        }


      /// <summary>
      /// Overloaded constructor with two parameters.
      /// </summary>
      /// <param name="fname">Name of the file</param>
      /// <param name="ifields">A List of the field data for the file</param>
        public FileDataHolder(string fname, List<DataFieldHolder> ifields)
        {
            datafields = ifields;
            filename = fname;
        }
        #endregion
    }
}
