using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Windows.Documents;

namespace FBABoxContentLabeler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shipment shipment = new Shipment();
        ZPLLabel labeler = new ZPLLabel();
        List<AMZWarehouse> amzwhses = new List<AMZWarehouse>();



        public MainWindow()
        {
            InitializeComponent();
            DataBind();
            cmbx_AmazonWhses.ItemsSource = amzwhses;
            
        }


        #region Methods
        /// <summary>
        /// Method queries database for all Amazon warehouses and fills the AMZWarehouseList object.
        /// </summary>
        private void DataBind()
        {
            amzwhses.Clear();
            string connString = ConfigurationManager.ConnectionStrings["Database1"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM AMZWHSES", conn);
                SqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    AMZWarehouse whse = new AMZWarehouse();
                    whse.WhseCode = datareader["WHSEID"].ToString();
                    whse.Name = datareader["WHSENAME"].ToString();
                    whse.Address = datareader["ADDRESSLINE"].ToString();
                    whse.CityStateZip = datareader["CITYSTATEZIP"].ToString();
                    amzwhses.Add(whse);

                }
            }
        }

        /// <summary>
        /// Method adds an Amazon Warehouse to the DB and list of warehouses.
        /// </summary>
        private void AddWhse(AMZWarehouse iwhse)
        {
            string connString = ConfigurationManager.ConnectionStrings["Database1"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlCommand comm = new SqlCommand(@"INSERT INTO AMZWHSES VALUES(@WHSEID, @WHSENAME, @ADDRESSLINE, @CITYSTATEZIP);", conn);
                comm.Parameters.AddWithValue("WHSEID", iwhse.WhseCode);
                comm.Parameters.AddWithValue("WHSENAME", iwhse.Name);
                comm.Parameters.AddWithValue("ADDRESSLINE", iwhse.Address);
                comm.Parameters.AddWithValue("CITYSTATEZIP", iwhse.CityStateZip);
                comm.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Method writes the ZPL command to ZPLLabel.txt file.
        /// </summary>
        /// <param name="text">ZPL Command to write into text.</param>
        private void WriteToText(string text)
        {

            string file = "ZPLLabel.txt";
            string dest = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            try
            {
                File.WriteAllText(dest, text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to write to text file. Error :" + ex.Message);
            }


        }

        /// <summary>
        /// Creates a FlowDocument object to be printed.
        /// </summary>
        /// <param name="icommand">ZPL command that will be written to the document.</param>
        /// <returns></returns>
        private FlowDocument CreateFlow(string icommand)
        {
            FlowDocument doc = new FlowDocument();
            Section sec = new Section();

            sec.Blocks.Add(new Paragraph(new Run(icommand)));

            doc.Blocks.Add(sec);
            return doc;

        }


        #endregion



        #region Click Events
        //opens "OpenFileDialog" to add box files.
        private void btn_AddBoxes_Click(object sender, RoutedEventArgs e)
        {
            shipment.Boxes.Clear();
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;


            List<string> vreads = new List<string>();
            List<string> filenames = new List<string>();
            string[] fnames;



            if (openFileDialog1.ShowDialog() == true)
            {
                //check if any files are selected
                if (openFileDialog1.FileName != null || openFileDialog1.FileNames != null)
                {
                    //if files are select then...
                    try
                    {
                        foreach (string file in openFileDialog1.FileNames)
                        {

                            using (StreamReader sr = new StreamReader(file))
                            {
                                // reads the file and saves in a list of strings
                                vreads.Add(sr.ReadToEnd());


                            }
                        }
                        fnames = openFileDialog1.SafeFileNames;
                        foreach (string f in fnames)
                        {
                            filenames.Add(f);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                        openFileDialog1.Reset();
                    }


                    DataSorter ds = new DataSorter(filenames, vreads);

                    

                    foreach (FileDataHolder fs in ds.DataHolders)
                    {
                        shipment.Boxes.Add(new Box(fs));
                    }

                    shipment.SortBoxes();

                    lsbx_AddedBoxes.ItemsSource = shipment.Boxes;
                

                    
                }

                openFileDialog1.Reset();
                lsbx_AddedBoxes.Items.Refresh();
                lbl_BoxCount.Content = shipment.Boxes.Count;
                
              


            }


        }

        //Adds a warehouse to the DB.
        private void btn_AddWhse_Click(object sender, RoutedEventArgs e)
        {
            AMZWarehouse newwhse = new AMZWarehouse();
            WarehouseWindow amzWin = new WarehouseWindow(newwhse);
            amzWin.ShowDialog();

            if (amzWin.DialogResult == true)
            {
                AddWhse(newwhse);
                DataBind();
                cmbx_AmazonWhses.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Unable to add new Amazon warehouse.");

            }
        }

        //Clears all values.
        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            shipment.Boxes.Clear();
            lsbx_AddedBoxes.Items.Refresh();
            labeler.ZPLCommand = "";
            DataBind();
            cmbx_AmazonWhses.Items.Refresh();
            lbl_BoxCount.Content = "0";
           

        }


        /*Button checks that there are boxes and an Amazon warehouse is selected from the combobox. 
        If all good, writes to text file and sends to printer.*/
        private void btn_PrintBoxes_Click(object sender, RoutedEventArgs e)
        {
            if (shipment.Boxes.Count > 0 & cmbx_AmazonWhses.SelectedIndex > -1)
            {
                AMZWarehouse shipto;
                if (cmbx_AmazonWhses.SelectedItem is AMZWarehouse)
                {
                    shipto = (AMZWarehouse)cmbx_AmazonWhses.SelectedItem;
                    labeler.CreateLabels(shipto, shipment.Boxes);
                    string com = labeler.ZPLCommand;
                    RawPrinterHelper.SendStringToPrinter("Zebra  ZP 450-200 dpi", com);

                    string file = shipment.Boxes[0].PO+".txt";
                    string dest = System.IO.Path.Combine("C:\\Users\\Back-warehouse\\Dropbox\\FBA Shipment Contents", file);            

                        try
                        {
                            File.WriteAllText(dest, shipment.ToString());
                            MessageBox.Show("File successfully saved!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to write to text file. Error :" + ex.Message);
                        }                                  
                }

            }
            else if (shipment.Boxes.Count == 0)
            {
                MessageBox.Show("No boxes added to print. Add boxes to print.");
            }
            else if (cmbx_AmazonWhses.SelectedIndex == -1)
            {
                MessageBox.Show("Failed to select a warehouse. Please select a warehouse.");
            }
        }//end btn_PrintBoxes_Click
        #endregion
    }

}
