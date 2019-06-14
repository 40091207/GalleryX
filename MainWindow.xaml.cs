using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalleryX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Creating the Gallery Object
        Gallery gallery = new Gallery();

        public MainWindow()
        {
            InitializeComponent();
            // Performs Database Load on Program Start
            gallery.ArtistLoad(); //Loading the Artists
            gallery.ArtworkLoad(); //Loading the Artworks
            gallery.CustomerLoad(); //Loading the Customers
            gallery.PurchasesLoad(); // Loading Purchases
        }

        #region Buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ArtistNameInput = ArtistNameInputBox.Text;
            string ArtistAddressInput = ArtistAddressInputBox.Text;
            if (ArtistNameInput == "Artist Name" || ArtistAddressInput == "Artist Address" || ArtistNameInput == "" || ArtistAddressInput == "")
            {
                MessageBox.Show("Please enter valid inputs");
            }
            else
            {
                gallery.AddArtist(ArtistNameInput, ArtistAddressInput);
                ArtistNameInputBox.Text = String.Empty;
                ArtistAddressInputBox.Text = String.Empty;
            }

        }

        private void AddArtworkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ArtistId = int.Parse(ArtIDBox.Text);
                string ArtworkDescription = ArtworkDesBox.Text;
                DateTime? Artworkdate = DatePicker.SelectedDate;
                string ArtworkDate = Artworkdate.ToString();
                string ArtworkType = ArtworkTypeBox.Text;
                string ArtworkState = ArtworkStateBox.Text;
                decimal ArtworkPrice = decimal.Parse(ArtworkPriceBox.Text);

                //Checks if input is within bounds and produces an error if not
                if (ArtistId < 101 & ArtistId > 0)
                {
                    if (ArtworkDescription != "Artwork Description" & ArtworkDescription != "")
                    {
                        if (ArtworkType.ToLower() != "painting" || ArtworkType.ToLower() == "sculpture")
                        {
                            if (ArtworkState.ToLower() != "gallery" || ArtworkState.ToLower() != "sold" || ArtworkState.ToLower() != "returned")
                            {
                                if (ArtworkPrice > 0 & ArtworkPrice < 99999999999999)
                                {
                                    gallery.AddArtwork(ArtistId, ArtworkDescription, ArtworkDate, ArtworkPrice, ArtworkType, ArtworkState);
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Valid Inputs");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please Enter Valid Inputs");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Enter Valid Inputs");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
            }

            catch
            {
            }
            finally
            {
                MessageBox.Show("Please Enter valid inputs");
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Calls the save methods
            gallery.ArtistSave();
            gallery.ArtworkSave();
            gallery.CustomerSave();
            gallery.PurchasesSave();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            //Calls the load methods
            gallery.ArtistLoad();
            gallery.ArtworkLoad();
            gallery.CustomerLoad();
            gallery.PurchasesLoad();
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            //displays all artists to the listbox
            DisplayListBox.ItemsSource = gallery.GalleryArtists;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //displays all artwork to the listbox
            DisplayListBox.ItemsSource = gallery.GetAllArtwork();
        }

        private void ArtistArtWorkBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string inArtistName = ArtistArworkName.Text;
            //checks if input has not been left blank and produces an error if it has
            if (inArtistName != "")
            {   //retrieves all artwork by the artist and displays in Listbox
                DisplayListBox.ItemsSource = gallery.GetArtworkByArtistName(inArtistName);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
            
        }

        private void AddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {

            string customerName = CustomerName.Text;

            //checks if input field has either been left blank or unchanged and produces an error if it has
            if (customerName == "Customer Name" || customerName == "")
            {
                MessageBox.Show("Please enter valid inputs");
            }
            else
            {
                //Calls the function to add a customer
                gallery.AddCustomer(customerName);
            }


        }

        private void AddPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int CustomerID = int.Parse(CustomerIDBox.Text);
                int StockItemID = int.Parse(StockItemIDBox.Text);
                DateTime? Purchasedate = PurchaseDatePicker.SelectedDate;
                string ArtworkDate = Purchasedate.ToString();
                //checks that the inputs are within bounds, displays an error if they are not
                if (CustomerID < 101 & CustomerID > 0)
                {
                    if (StockItemID < 101 & StockItemID > 0)
                    {
                        gallery.AddPurchase(CustomerID, StockItemID, ArtworkDate);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }

            }
            catch
            {
            }
            finally
            {
                MessageBox.Show("Please enter valid inputs");
            }
        }

        private void DisplayCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            //displays all customers of the gallery
            DisplayListBox.ItemsSource = gallery.GalleryCustomers;
        }

        private void DisplayPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            //displays all purchases made by customers (Currently not operating as should)
            DisplayListBox.ItemsSource = gallery.GetAllPurchasesByGallery();
        }

        private void GetArtworksOlderThanTwoWeeksBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBox.ItemsSource = gallery.GetAllArtworkOlderThanTwoWeeks();
        }

        private void EditArtistBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            int ArtistID = int.Parse(AtistIDBox.Text);
            string ArtistName = EditArtistNameBox.Text;
            string ArtistAddress = EditArtistAddressBox.Text;
            //checks that the inputs are within bounds and displays an error if they are not
            if (ArtistID < 101 & ArtistID > 0)
            {
                if(ArtistName != "" & ArtistName != "Artist Name")
                {
                    if (ArtistAddress != "" & ArtistAddress != "Artist Address")
                    {
                        //Calls the function to edit the artist using inputted data
                        gallery.EditArtist(ArtistID, ArtistName, ArtistAddress);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
            
            }
            catch
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }

        }

        private void SearchPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            //checks that the inputs are within bounds
            string customerName = CustomerPurchasesBox.Text;
            if (customerName != "")
            {
                DisplayListBox.ItemsSource = gallery.GetPurchasesByCustomer(customerName);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
        }

        private void DisplayArtworksinGalleryBtn_Click(object sender, RoutedEventArgs e)
        {
            //displays all artwork currently on show in the gallery
            DisplayListBox.ItemsSource = gallery.GetAllArtworkIntheGallery();
        }

        private void ReturnArtworkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            int ArtworkID = int.Parse(ReturnArtworkIdBox.Text);
                //checks that the inputs are within bounds and displays an error if they are not
                if (ArtworkID < 99999 & ArtworkID > 0)
                {
                    //displays the artwork by an artist
                    gallery.ReturnArtwork(ArtworkID);
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
                
            }
            catch
            {
            }
            finally
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
            

            
        }

        private void ChangeArtworkPriceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newArtworkPrice = int.Parse(EditPriceBox.Text);
                int ArtworkID = int.Parse(EditPriceArtworkIDBox.Text);
                //checks that the inputs are within bounds and displays an error if they are not
                if (ArtworkID < 9999999 & ArtworkID > 0)
                {
                    if (newArtworkPrice < 99999999 & newArtworkPrice > 0)
                    {
                        //edits the artworkpiece using the data inputted
                        gallery.EditArtworkPrice(ArtworkID, newArtworkPrice);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
                
            }
            catch
            {

            }
            finally
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
            
        }

        private void SellArtworkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ArtworkID = int.Parse(SellArtworkIDBox.Text);
                int CustomerID = int.Parse(SellArtworkCustomerID.Text);
                //checks that the inputs are within bounds and displays an error if they are not
                if (ArtworkID < 9999999 & ArtworkID > 0)
                {
                    if (ArtworkID < 101 & ArtworkID > 0)
                    {
                        //calls the function to mark an artwork as sold and to add the purchase to the customer
                        gallery.SellArtwork(ArtworkID, CustomerID);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
                
            }
            catch
            {

            }
            finally
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }
            
        }
        #endregion

        private void EditCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string customerName = CustomerName.Text;
                int customerID = int.Parse(EditCustomerIDBox.Text);
                //checks that the inputs are within bounds and displays an error if they are not
                if (customerID < 1000 & customerID > 0)
                {
                    if (customerName != "" & customerName != "Artist Name")
                    {
                        //calls the funstion to edit the customer
                        gallery.EditCustomer(customerID, customerName);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Inputs");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Inputs");
                }
 
            }
            catch
            {
            }
            finally
            {
                MessageBox.Show("Please Enter Valid Inputs");
            }

        }

    }

    class Gallery
    {
        public int newArtistID = 1;
        public int newArtworkID = 1;
        public int newCustomerID = 1;

        public List<Artist> GalleryArtists = new List<Artist>(); //Creates the Artist list
        public List<Customer> GalleryCustomers = new List<Customer>(); //Creates the Customer list

        #region Lists
        ///<summary>
        ///Retrieves all Artwork in the database
        ///</summary>
        public List<Artwork> GetAllArtwork()
        {
            List<Artwork> result = new List<Artwork>();

            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    result.Add(a);
                }
            }

            return result;
        }

        ///<summary>
        ///Retrieves all Artwork that is by an Artist from the ArtistName inputted
        ///</summary>
        public List<Artwork> GetArtworkByArtistName(string name)
        {
            foreach (Artist artist in GalleryArtists)
            {
                if (artist.ArtistName.ToUpper() == name.ToUpper())
                    return artist.ArtistArtwork;
            }
            return null;
        }

        ///<summary>
        ///Retrieves all artworks that were first displayed over two weeks ago
        ///</summary>
        public List<Artwork> GetAllArtworkOlderThanTwoWeeks()
        {
            List<Artwork> result = new List<Artwork>();

            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    string Date = a.artworkDisplayDate;
                    DateTime ArtworkDate = DateTime.Parse(Date);

                    if (ArtworkDate.Date < DateTime.Today.AddDays(-14))
                    {
                        result.Add(a);
                    }

                }
            }

            return result;
        }

        ///<summary>
        ///Retrieves all purchase information made by customers
        ///</summary>
        //////<remarks>
        ///incomplete, should display the artwork info not the purchase details
        ///</remarks>
        public List<Purchases> GetAllPurchasesByGallery()
        {
            List<Purchases> purchases = new List<Purchases>();

            foreach (Customer customer in GalleryCustomers)
            {
                foreach (Purchases a in customer.CustomerPurchases)
                {
                    purchases.Add(a);
                }
            }

            return purchases;
        }

        ///<summary>
        ///Retrieves all purchases made by a customer by the name inputted
        ///</summary>
        public List<Purchases> GetPurchasesByCustomer(string name)
        {
            List<Purchases> purchases = new List<Purchases>();

            foreach (Customer customer in GalleryCustomers)
            {
                if (customer.CustomerName.ToUpper() == name.ToUpper())
                {
                    foreach (Purchases purchase in customer.CustomerPurchases)
                    {

                        return customer.CustomerPurchases;
                    }

                }

            }

            return null;
        }

        ///<summary>
        ///Retrieves all artworks currently still on display in the gallery
        ///</summary>
        public List<Artwork> GetAllArtworkIntheGallery()
        {
            List<Artwork> result = new List<Artwork>();

            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    if (a.artworkState.ToUpper() == "GALLERY")
                    {
                        result.Add(a);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Functions
        ///<summary>
        ///Creates an Artist using the details inputted
        ///</summary>
        public void AddArtist(string artistName, string artistAddress)
        {
            Artist a = new Artist(artistName, artistAddress, newArtistID);
            GalleryArtists.Add(a);
            newArtistID = newArtistID + 1;
        }

        ///<summary>
        ///Creates an Artwork using the information inputted by the user and adds to the artist
        ///</summary>
        public void AddArtwork(int ArtistID, string inDes, string inDate, decimal inPrice, string inType, string inState)
        {
            foreach (Artist a in GalleryArtists)
            {
                if (a.ArtistID == ArtistID)
                {
                    a.AddArtwork(ArtistID, inDes, inDate, inPrice, inType, inState, newArtworkID);
                    newArtworkID = newArtworkID + 1;
                }
            }
        }

        ///<summary>
        ///creates a customer using the details inputted
        ///</summary>
        public void AddCustomer(string customerName)
        {
            Customer a = new Customer(customerName, newCustomerID);
            GalleryCustomers.Add(a);
            newCustomerID = newCustomerID + 1;
        }

        ///<summary>
        ///creates a record of a purchase made using the details inputed
        ///</summary>
        public void AddPurchase(int inCustomerID, int StockID, string inDate)
        {
            foreach (Customer a in GalleryCustomers)
            {
                if (a.CustomerID == inCustomerID)
                {
                    a.AddPurchase(inCustomerID, StockID, inDate);

                }
            }
        }

        ///<summary>
        ///edits an artists details using the information inputted
        ///</summary>
        public void EditArtist(int artistID, string artistName, string artistAddress)
        {
            artistID = artistID - 1;
            GalleryArtists[artistID].ArtistName = artistName;
            GalleryArtists[artistID].ArtistAddress = artistAddress;

        }

        ///<summary>
        ///edits a customers details using the information inputted
        ///</summary>
        public void EditCustomer(int customerID, string customerName)
        {
            customerID = customerID - 1;
            GalleryCustomers[customerID].CustomerName = customerName;
        }

        ///<summary>
        ///retrieves the number of artworks by an artist, used when saving to allow the load method to know how many times to loop
        ///</summary>
        public int GetNumberOfArtistsArt(int ArtistID)
        {
            int numberofArtworks = 0;

            foreach (Artist a in GalleryArtists)
            {
                if (a.ArtistID == ArtistID)
                {
                    numberofArtworks = a.GetNumberOfArtistsArt(ArtistID);
                }

            }

            return numberofArtworks;
        }

        ///<summary>
        ///retrieves the total number of artworks by every artists, used when saving to allow the load method to know how many times to loop
        ///</summary>
        public int TotalNumberofArtworks()
        {
            int NumberofArtworks = 0;

            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    NumberofArtworks++;
                }
            }
            return NumberofArtworks;
        }

        ///<summary>
        ///retrieves the total number of purchases by all customers, used when saving to allow the load method to know how many times to loop
        ///</summary>
        public int TotalNumberofPurchases()
        {
            int NumberofArtworks = 0;

            foreach (Customer customer in GalleryCustomers)
            {
                foreach (Purchases a in customer.CustomerPurchases)
                {
                    NumberofArtworks++;
                }
            }
            return NumberofArtworks;
        }

        ///<summary>
        ///changes an artwork state from its current state to returned to artist
        ///</summary>
        public void ReturnArtwork(int ArtworkID)
        {
            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    if (a.artworkID == ArtworkID)
                    {
                        a.artworkState = "Returned";
                    }
                }
            }
        }

        ///<summary>
        ///changes the price of an artwork to an inputted value
        ///</summary>
        public void EditArtworkPrice(int ArtworkID, int newArtworkPrice)
        {
            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    if (a.artworkID == ArtworkID)
                    {
                        a.artworkPrice = newArtworkPrice;
                    }
                }
            }
        }

        ///<summary>
        ///changes the artwork from its current state to sold
        ///</summary>
        public void SellArtwork(int ArtworkID, int CustomerID)
        {
            foreach (Artist artist in GalleryArtists)
            {
                foreach (Artwork a in artist.ArtistArtwork)
                {
                    if (a.artworkID == ArtworkID)
                    {
                        a.artworkState = "Sold";
                    }
                }
            }

            DateTime CurrentDate = DateTime.Now;
            string currentdate = CurrentDate.ToString();

            AddPurchase(CustomerID, ArtworkID, currentdate);

        }
        #endregion

        #region Save
        ///<summary>
        ///Creates the file to save artists to and creates a streamwriter
        ///</summary>
        public void ArtistSave()
        {

            System.IO.TextWriter textOut = null;

            try
            {
                string filename = "GalleryX_Artists.txt";
                textOut = new System.IO.StreamWriter(filename);
                ArtistSave(textOut);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }

        }

        ///<summary>
        ///saves the details of each artist in the gallery
        ///</summary>
        public void ArtistSave(System.IO.TextWriter textOut)
        {
            textOut.WriteLine("Gallery_X");
            textOut.WriteLine(GalleryArtists.Count);

            foreach (Artist a in GalleryArtists)
            {
                textOut.WriteLine(a.ArtistName);
                textOut.WriteLine(a.ArtistAddress);
                textOut.WriteLine(a.ArtistID);
            }

        }

        ///<summary>
        ///Creates the file to save artwork to and creates a streamwriter
        ///</summary>
        public void ArtworkSave()
        {

            System.IO.TextWriter textOut = null;

            try
            {
                string filename = "GalleryX_Artwork.txt";
                textOut = new System.IO.StreamWriter(filename);
                ArtworkSave(textOut);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }

        }

        ///<summary>
        ///saves the details for each piece of artwork in the gallery
        ///</summary>
        public void ArtworkSave(System.IO.TextWriter textOut)
        {
            textOut.WriteLine("Gallery_X");
            int NumberofArtworks = TotalNumberofArtworks();
            textOut.WriteLine(NumberofArtworks);

            foreach (Artist a in GalleryArtists)
            {
                a.Save(textOut);
            }

        }

        ///<summary>
        ///creates the file to save the customer details to
        ///</summary>
        public void CustomerSave()
        {

            System.IO.TextWriter textOut = null;

            try
            {
                string filename = "GalleryX_Customers.txt";
                textOut = new System.IO.StreamWriter(filename);
                CustomerSave(textOut);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }

        }

        ///<summary>
        ///writes the details of each customer at the gallery to the file
        ///</summary>
        public void CustomerSave(System.IO.TextWriter textOut)
        {
            textOut.WriteLine("Gallery_X");
            textOut.WriteLine(GalleryCustomers.Count);

            foreach (Customer a in GalleryCustomers)
            {
                textOut.WriteLine(a.CustomerName);
                textOut.WriteLine(a.CustomerID);
            }

        }

        ///<summary>
        ///creates the file to save the details of all purchase made
        ///</summary>
        public void PurchasesSave()
        {

            System.IO.TextWriter textOut = null;

            try
            {
                string filename = "GalleryX_Purchases.txt";
                textOut = new System.IO.StreamWriter(filename);
                PurchasesSave(textOut);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }

        }

        ///<summary>
        ///writes the details of each purchase made by customers to the file
        ///</summary>
        public void PurchasesSave(System.IO.TextWriter textOut)
        {
            textOut.WriteLine("Gallery_X");
            int NumberofPurchases = TotalNumberofPurchases();
            textOut.WriteLine(NumberofPurchases);

            foreach (Customer a in GalleryCustomers)
            {
                a.Save(textOut);
            }

        }
        #endregion

        #region Load
        ///<summary>
        ///Attempts to read the file containing the artists information and retrives the text
        ///</summary>
        public void ArtistLoad()
        {
            System.IO.TextReader textIn = null;
            try
            {
                string filename = "GalleryX_Artists.txt"; ;
                textIn = new System.IO.StreamReader(filename);
                ArtistLoad(textIn);
            }
            catch (Exception)
            {
                //throw e;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }
        }
        ///<summary>
        ///reads the number of artists and loops for each artist reading them in and then adding it to the list of artists
        ///</summary>
        public void ArtistLoad(System.IO.TextReader textIn)
        {
            textIn.ReadLine();
            int NumberofArtists = int.Parse(textIn.ReadLine());

            for (int i = 0; i < NumberofArtists; i++)
            {
                string artistName = textIn.ReadLine();
                string artistAddress = textIn.ReadLine();
                int ArtistID = int.Parse(textIn.ReadLine());

                AddArtist(artistName, artistAddress);
            }
        }

        ///<summary>
        ///Attempts to read the file containing the artworks information and retrives the text
        ///</summary>
        public void ArtworkLoad()
        {
            System.IO.TextReader textIn = null;
            try
            {
                string filename = "GalleryX_Artwork.txt"; ;
                textIn = new System.IO.StreamReader(filename);
                ArtworkLoad(textIn);
            }
            catch (Exception)
            {
                //throw e;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }
        }

        ///<summary>
        ///reads the number of artworks and loops for each artwork reading them in and then adding it to the list of artists
        ///</summary>
        public void ArtworkLoad(System.IO.TextReader textIn)
        {

            textIn.ReadLine();
            int NumberofArtworks = int.Parse(textIn.ReadLine());
            for (int i = 0; i < NumberofArtworks; i++)
            {
                int ArtistID = int.Parse(textIn.ReadLine());
                string artworkDes = textIn.ReadLine();
                string artworkDate = textIn.ReadLine();
                string artworkType = textIn.ReadLine();
                string artworkState = textIn.ReadLine();
                decimal artworkPrice = decimal.Parse(textIn.ReadLine());
                textIn.ReadLine();

                AddArtwork(ArtistID, artworkDes, artworkDate, artworkPrice, artworkType, artworkState);
            }
        }

        ///<summary>
        ///Attempts to read the file containing the customer information and retrives the text
        ///</summary>
        public void CustomerLoad()
        {
            System.IO.TextReader textIn = null;
            try
            {
                string filename = "GalleryX_Customers.txt"; ;
                textIn = new System.IO.StreamReader(filename);
                CustomerLoad(textIn);
            }
            catch (Exception)
            {
                //throw e;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }
        }

        ///<summary>
        ///reads the number of Customers and loops for each artist reading them in and then adding it to the list of artists
        ///</summary>
        public void CustomerLoad(System.IO.TextReader textIn)
        {
            textIn.ReadLine();
            int NumberofCustomers = int.Parse(textIn.ReadLine());

            for (int i = 0; i < NumberofCustomers; i++)
            {
                string customerName = textIn.ReadLine();
                int customerID = int.Parse(textIn.ReadLine());

                AddCustomer(customerName);
            }
        }

        ///<summary>
        ///Attempts to read the file containing each purchase information and retrives the text
        ///</summary>
        public void PurchasesLoad()
        {
            System.IO.TextReader textIn = null;
            try
            {
                string filename = "GalleryX_Purchases.txt"; ;
                textIn = new System.IO.StreamReader(filename);
                PurchasesLoad(textIn);
            }
            catch (Exception)
            {
                //throw e;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }
        }

        ///<summary>
        ///reads the number of artists and loops for each artist reading them in and then adding it to the list of artists
        ///</summary>
        public void PurchasesLoad(System.IO.TextReader textIn)
        {
            textIn.ReadLine();
            int NumberofPurchases = int.Parse(textIn.ReadLine());
            for (int i = 0; i < NumberofPurchases; i++)
            {
                int customerID = int.Parse(textIn.ReadLine());
                string purchaseDate = textIn.ReadLine();
                int purchaseID = int.Parse(textIn.ReadLine());

                AddPurchase(customerID, purchaseID, purchaseDate);
            }
        }

        #endregion

    }

    /// <summary>
    /// Artist Class containing each artist
    /// </summary>
    public class Artist
    {
        public string ArtistName;
        public string ArtistAddress;
        public int ArtistID = 1;

        public List<Artwork> ArtistArtwork = new List<Artwork>();

        //Artist attributes
        public Artist(string inName, string inAddress, int inArtistID)
        {
            ArtistName = inName;
            ArtistAddress = inAddress;
            ArtistID = inArtistID;
            ArtistArtwork = new List<Artwork>();
        }

        //Adding an artwork to the artist
        public Artwork AddArtwork(int inArtistID, string inDes, string inDate, decimal inPrice, string inType, string inState, int ArtworkID)
        {
            Artwork result = new Artwork(inArtistID, inDes, inDate, inPrice, inType, inState, ArtworkID);
            ArtistArtwork.Add(result);
            return result;
        }

        //retrieving the number of artworks by the artist used in gallery.GetNumberOfArtistsArt()
        public int GetNumberOfArtistsArt(int ArtistID)
        {
            return ArtistArtwork.Count;
        }

        public void Save(System.IO.TextWriter textOut)
        {
            foreach (Artwork a in ArtistArtwork)
            {
                a.Save(textOut);
            }
        }

        //ToString used to return the attributes of an artist as a single string
        public override string ToString()
        {
            return "Name : " + ArtistName + " Address : " + ArtistAddress + " ID : " + ArtistID;
        }

    }
    /// <summary>
    /// Artist Class containing each artwork for the artist
    /// </summary>
    public class Artwork
    {
        public int ArtistID;
        public string artworkDes;
        public string artworkDisplayDate;
        public decimal artworkPrice;
        public string artworkType;
        public string artworkState;
        public int artworkID;

        // Adding an artwork using the information inputted
        public Artwork(int inArtistID, string inDes, string inDate, decimal inPrice, string inType, string inState, int inArtworkID)
        {
            ArtistID = inArtistID;
            artworkDes = inDes;
            artworkDisplayDate = inDate;
            artworkPrice = inPrice;
            artworkType = inType;
            artworkState = inState;
            artworkID = inArtworkID;
        }

        //Save method for the artwork
        public bool Save(System.IO.TextWriter textOut)
        {
            try
            {
                textOut.WriteLine(ArtistID);
                textOut.WriteLine(artworkDes);
                textOut.WriteLine(artworkDisplayDate);
                textOut.WriteLine(artworkType);
                textOut.WriteLine(artworkState);
                textOut.WriteLine(artworkPrice);
                textOut.WriteLine();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //ToString used to return the attributes of an artwork as a single string
        public override string ToString()
        {
            string toString = "Description : " + artworkDes + "  Display Date : " + artworkDisplayDate + " Type : " + artworkType + " State : " + artworkState + " Price : " + artworkPrice + " Arwork ID : " + artworkID;

            return toString;
        }
    }
    /// <summary>
    /// Customer Class containing each customer
    /// </summary>
    public class Customer
    {
        public string CustomerName;
        public int CustomerID = 1;

        public List<Purchases> CustomerPurchases = new List<Purchases>();

        // Customer attributes
        public Customer(string inName, int inCustomerID)
        {
            CustomerName = inName;
            CustomerID = inCustomerID;
            CustomerPurchases = new List<Purchases>();
        }
        //adds a purchase to the purchases list
        public Purchases AddPurchase(int inCustomerID, int inStockID, string date)
        {
            Purchases result = new Purchases(inCustomerID, date, inStockID);
            CustomerPurchases.Add(result);
            return result;
        }

        public void Save(System.IO.TextWriter textOut)
        {
            foreach (Purchases a in CustomerPurchases)
            {
                a.Save(textOut);
            }
        }

        //returns a string version of the purchase attributes
        public override string ToString()
        {
            return "Customer Name: " + CustomerName + " Customer ID: " + CustomerID;
        }
    }
    /// <summary>
    /// Customer Class containing each purchase by a customer
    /// </summary>
    public class Purchases
    {
        public int CustomerID;
        public int PurchasesID;
        public int StockPurchasedID = 1;
        public string PurchaseDate;



        public Purchases(int inCustomerID, string inPurchaseDate, int inStockPurchaseID)
        {
            CustomerID = inCustomerID;
            PurchaseDate = inPurchaseDate;
            StockPurchasedID = inStockPurchaseID;
        }

        public bool Save(System.IO.TextWriter textOut)
        {
            try
            {
                textOut.WriteLine(CustomerID);
                textOut.WriteLine(PurchaseDate);
                textOut.WriteLine(StockPurchasedID);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public override string ToString()
        {
            return "Customer ID : " + CustomerID + " Stock ID Purchased : " + StockPurchasedID + " Date : " + PurchaseDate;
        }

    }


}
