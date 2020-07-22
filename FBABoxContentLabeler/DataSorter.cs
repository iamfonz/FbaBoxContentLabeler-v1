using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FBABoxContentLabeler
{
    public class DataSorter
    {
        #region Full Properties and Fields
        private List<FileDataHolder> dataholders = new List<FileDataHolder>();

        public List<FileDataHolder> DataHolders
        {
            get { return dataholders; }
            set { dataholders = value; }
        }


        private List<string> fnames = new List<string>();
        private List<string> fcontent = new List<string>();


        #endregion


        #region Constructors
        public DataSorter() : this(new List<string>(), new List<string>())
        {

        }


        public DataSorter(List<string> ifnames, List<string> ifcontent)
        {
            fnames = ifnames;
            fcontent = ifcontent;

            ParseData();
        }

        #endregion


        #region Methods
        /// <summary>
        /// Method takes the names of the read files as well as their content and parses them organizing
        /// in a structred object.
        /// </summary>
        private void ParseData()
        {
            ParseData(fnames, fcontent);
        }

        /// <summary>
        /// Method takes the names of the read files as well as their content and parses them organizing
        /// in a structred object.
        /// </summary>
        /// <param name="ifnames">List of strings of the read in file names.</param>
        /// <param name="ifcontent">List of strings of the content within the read files.</param>
        public void ParseData(List<string> ifnames, List<string> ifcontent)
        {

            //Add a new FileDataHolder object to the dataholders by adding one filename at a time
            for (int i = 0; i < ifnames.Count; ++i)
            {
                dataholders.Add(new FileDataHolder(fnames[i], new List<DataFieldHolder>()));
            }



            string endline = "\n";
            string newendline = "!";

            //loop through every file content
            for (int i = 0; i < ifcontent.Count; ++i)
            {
                /*replace the end of each line with a new character and store in temp variable of the new content.
                original content remains the same */
                string newcontent = ifcontent[i].Replace(endline, newendline);

                //split the new content into rows for each line of data
                string[] rows = newcontent.Split('!');


                //extract data one line/row at a time.
                for (int j = 0; j < rows.Length - 1; ++j)
                {
                    string[] splitrow = rows[j].Split(';');



                    for (int h = 0; h < splitrow.Length - 1; ++h)
                    {
                        //first row is header. want to save that data inside the DataHolder
                        if (j == 0)
                        {
                            dataholders[i].DataFields.Add(new DataFieldHolder(splitrow[h], new List<string>()));
                        }
                        else
                        {
                            dataholders[i].DataFields[h].AttributeValues.Add(splitrow[h]);
                        }
                    }

                }

            }

        }
        #endregion
    }
}
