using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int INCREMENT = 2, FORMWIDTH = 954, FORMSTARTHEIGHT = 380, FORMEXPANDHEIGHT = 620;
        private void Form1_Load(object sender, EventArgs e)
        {
            MembershipDetailsGroupBox.Visible = false;
            PricingGroupBox.Visible = false;
            SummaryGroupBox.Visible = false;
            SearchGroupBox.Visible = false;
            ButtonPanel.Visible = false;
            PasswordPanel.Visible = true;
            this.Size = new Size(FORMWIDTH, FORMSTARTHEIGHT);
        }

        //Global Variable Declaration
        int attempts,initial = 3;

        //Action performed when Submit Button is clicked
        private void PasswordSubmitButton_Click(object sender, EventArgs e)
        {
            string password = "ILuvVisualC#";
            do
            {
                if (PasswordTextBox.Text != password & attempts < 2)
                {
                    initial--;
                    MessageBox.Show("Invalid Password\nNumber of attempts left: " + initial, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PasswordTextBox.Clear();
                }

                //Application closes if password is incorrectly entered for more than 3 times
                else if (PasswordTextBox.Text != password & attempts <= 2)
                {
                    MessageBox.Show("Attempts Exceeded","Oops!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    break;
                }
                else if (PasswordTextBox.Text == password & attempts < 3)
                {
                    PasswordPanel.Visible = false;
                    PricingGroupBox.Visible = true;
                    ButtonPanel.Visible = true;
                    break;
                }
                attempts++;
            } while (attempts >= 3);
        }

        //Action performed when Display Button is clicked
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            try
            {
                int term = int.Parse(TermTextBox.Text);
                if(term > 0)
                {

                    //Function call to calculate the Price Next Term based on the term entered
                    CalculatePrice(term);
                    if (term > 60)
                    {
                        SalesPromptLabel.Visible = false;
                        PriceNextTermLabel.Text = "";
                    }
                    else
                    {
                        decimal nexttermpricey = NextTermPrice(nextterm);
                        nexttermpricey = Math.Round(nexttermpricey, 2);
                        PriceNextTermLabel.Text = nexttermpricey.ToString();
                        SalesPromptLabel.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Enter valid term","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TermTextBox.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Enter valid term","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TermTextBox.Focus();
            }
        }

        //Global Variable Declaration
        int nextterm;
        const decimal baseprice = 59m;
        const decimal discount1 = 0m, discount2 = 0.10m, discount3 = 0.20m, discount4 = 0.25m, discount5 = 0.3333m, discount6 = 0.40m, discount7 = 0.6666m;
        decimal monthprice, fulltermprice;

        //User defined function to calculate the Price Per Month and Price Full Term based on the term entered
        private void CalculatePrice(int term)
        {
            if(term <= 2)
            {
                monthprice = baseprice - (baseprice * discount1);
                fulltermprice = term * monthprice;
                nextterm = 1;
            }
            else if(term <= 6)
            {
                monthprice = baseprice - (baseprice * discount2);
                fulltermprice = term * monthprice;
                nextterm = 2;
            }
            else if(term <= 12)
            {
                monthprice = baseprice - (baseprice * discount3);
                fulltermprice = term * monthprice;
                nextterm = 3;
            }
            else if(term <= 18)
            {
                monthprice = baseprice - (baseprice * discount4);
                fulltermprice = term * monthprice;
                nextterm = 4;
            }
            else if(term <= 24)
            {
                monthprice = baseprice - (baseprice * discount5);
                fulltermprice = term * monthprice;
                nextterm = 5;
            }
            else if(term <=60)
            {
                monthprice = baseprice - (baseprice * discount6);
                fulltermprice = term * monthprice;
                nextterm = 6;
            }
            else
            {
                monthprice = baseprice - (baseprice * discount7);
                fulltermprice = term * monthprice;
                nextterm = 7;
            }
            monthprice = Math.Round(monthprice, 2);
            fulltermprice = Math.Round(fulltermprice, 2);
            PricePerMonthLabel.Text = monthprice.ToString();
            PriceFullTermLabel.Text = fulltermprice.ToString();
        }

        //User defined function value returning function to calculate Next Term Price
        private decimal NextTermPrice(int nextterm)
        {
            decimal currentvalue = decimal.Parse(PriceFullTermLabel.Text);
            decimal nexttermvalue;
            decimal differencevalue;
            switch (nextterm)
            {
                case (1):
                    nexttermvalue = (3 * baseprice) - (3 * baseprice * discount2);
                    break;
                case (2):
                    nexttermvalue = (7 * baseprice) - (7 * baseprice * discount3);
                    break;
                case (3):
                    nexttermvalue = (13 * baseprice) - (13 * baseprice * discount4);
                    break;
                case (4):
                    nexttermvalue = (19 * baseprice) - (19 * baseprice * discount5);
                    break;
                case (5):
                    nexttermvalue = (25 * baseprice) - (25 * baseprice * discount6);
                    break;
                default:
                    nexttermvalue = (61 * baseprice) - (61 * baseprice * discount7);
                    break;
            }
            differencevalue = nexttermvalue - currentvalue;
            differencevalue = System.Math.Abs(differencevalue);
            differencevalue = Math.Round(differencevalue, 2);
            SalesPromptLabel.Text = "Save more than " + differencevalue +" by choosing next term";
            return nexttermvalue;
        }

        //Action performed when Proceed Button is clicked
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            try
            {
                int confirmedterm = int.Parse(ClientConfirmedTextBox.Text);
                if (confirmedterm > 0)
                {
                    CalculatePrice(confirmedterm);
                    MembershipDetailsGroupBox.Visible = true;
                    DisplayButton.Enabled = false;
                    TermTextBox.Enabled = false;
                    SalesPromptLabel.Visible = false;
                    PricePerMonthLabel.Text = "";
                    PriceFullTermLabel.Text = "";
                    PriceNextTermLabel.Text = "";
                    DateTime currenttime = new DateTime();
                    currenttime = DateTime.Now;
                    JoinDayLabel.Text = currenttime.ToShortDateString();
                    UniqueMembershipID();
                }
                else
                {
                    MessageBox.Show("Enter valid term","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClientConfirmedTextBox.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Enter valid term","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientConfirmedTextBox.Focus();
            }
        }

        //User defined function to genearate unique 6 digit Membership ID
        private void UniqueMembershipID()
        {
            Random rand = new Random();
            int value = rand.Next(0, 999999);
            try
            {
                StreamReader file = File.OpenText("ClientInformation.txt");
                int Totallines = CountTotalLines();
                Totallines=+Totallines;
                for (int i = 1; i <= Totallines; i++)
                {
                    if (file.ReadLine() != value.ToString("D6"))
                    {
                        MembershipIDLabel.Text = value.ToString("D6");
                    }
                    else
                    {
                        UniqueMembershipID();
                    }
                }
                file.Close();
            }
            catch
            {
                MessageBox.Show("File Does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Keypress to restrict the user from entering numbers in Name Text Box
        private void FullNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) & !char.IsLetter(e.KeyChar) & !char.IsPunctuation(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Digits are not allowed for name","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Keypress to restrict the user from entering letters and any other special characters in Telephone Text Box
        private void TelephoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) & !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Alphabets and special cases are not allowed for telephone number","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Action performed when Confirm Button is clicked
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (FullNameTextBox.Text != "")
            {
               int TelephoneNumberLength = TelephoneTextBox.Text.Length;
                if (TelephoneNumberLength == 10)
                {
                    if (EmailTextBox.Text.Contains("@") & (EmailTextBox.Text.EndsWith(".com")| EmailTextBox.Text.EndsWith(".ie")) & EmailTextBox.Text != null)
                    {
                        string monthcoststr = monthprice.ToString();
                        string fulltermcoststr = fulltermprice.ToString();

                        //Displaying a message with user inputted values to get final confirmation
                        StringBuilder MessageText = new StringBuilder();
                        MessageText.AppendLine(string.Format("Check the details of the booking below"));
                        MessageText.AppendLine(string.Format("\nName of Client:\t\t {0}", FullNameTextBox.Text));
                        MessageText.AppendLine(string.Format("Date of Joining:\t\t {0}", JoinDayLabel.Text));
                        MessageText.AppendLine(string.Format("Telephone Number:\t {0}", TelephoneTextBox.Text));
                        MessageText.AppendLine(string.Format("Email Address:\t\t {0}", EmailTextBox.Text));
                        MessageText.AppendLine(string.Format("Membership ID of Client:\t {0}", MembershipIDLabel.Text));
                        MessageText.AppendLine(string.Format("Total Term Cost:\t\t {0}", "€"+fulltermcoststr));
                        MessageText.AppendLine(string.Format("Cost Per Month:\t\t {0}", "€"+monthcoststr));
                        MessageText.AppendLine(string.Format("Number of months:\t {0}", ClientConfirmedTextBox.Text));
                        if (MessageBox.Show(MessageText.ToString(), "Are you sure you want to proceed?",MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            MessageBox.Show("Success !!\nWelcome to Halo Fitness Family!", "Booking Successful");

                            //Appending the details to the file -- Client Information
                            try
                            {
                                StreamWriter OutputFile = File.AppendText("ClientInformation.txt");
                                OutputFile.WriteLine(MembershipIDLabel.Text);
                                OutputFile.WriteLine(FullNameTextBox.Text);
                                OutputFile.WriteLine(TelephoneTextBox.Text);
                                OutputFile.WriteLine(EmailTextBox.Text);
                                OutputFile.WriteLine(JoinDayLabel.Text);
                                OutputFile.WriteLine(ClientConfirmedTextBox.Text);
                                OutputFile.WriteLine(fulltermcoststr);
                                OutputFile.Close();
                                MessageBox.Show("Details have been saved","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            ClearButton_Click(sender, e);
                        }
                        else
                        { 
                            ClearButton.Focus(); 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter valid email address", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        EmailTextBox.Clear();
                        EmailTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter 10 digit phone number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TelephoneTextBox.Clear();
                    TelephoneTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Enter name of customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FullNameTextBox.Focus();
            }
        }

        //Action performed when Clear Button is clicked
        private void ClearButton_Click(object sender, EventArgs e)
        {
            if ((SummaryGroupBox.Visible) || (SearchGroupBox.Visible))
            {
                for (int i = 620; i > 380; i -= INCREMENT)
                {
                    this.Size = new Size(FORMWIDTH, i);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
            }
            SummaryGroupBox.Visible = false;
            SearchGroupBox.Visible = false;
            MembershipDetailsGroupBox.Visible = false;
            FullNameTextBox.Clear();
            TelephoneTextBox.Clear();
            EmailTextBox.Clear();
            ClientConfirmedTextBox.Clear();
            TermTextBox.Clear();
            DisplayButton.Enabled = true;
            PricePerMonthLabel.Text = "";
            PriceFullTermLabel.Text = "";
            PriceNextTermLabel.Text = "";
            SalesPromptLabel.Visible = false;
            TermTextBox.Enabled = true;
            MembershipListBox.Items.Clear();
            SearchedItemsListBox.Items.Clear();
            SearchByIDTextBox.Clear();
            SearchByDateTextBox.Clear();
        }

        //User defined value returning function to count the total number of lines in the file
        private int CountTotalLines()
        {
            int countlines;
            int count = 1;
            try
            {
                StreamReader file = File.OpenText("ClientInformation.txt");
                while (file.ReadLine() != null)
                {
                    count++;
                }
                countlines = count;
                file.Close();
            }
            catch
            {
                countlines = 0;
                this.Close();
            }
            return countlines;

        }

        //Action performed when Summary Button is clicked
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            SearchGroupBox.Visible = false;
            SearchByIDTextBox.Clear();
            SearchByDateTextBox.Clear();
            SearchedItemsListBox.Items.Clear();
            SummaryGroupBox.Visible = true;
            for (int i = FORMSTARTHEIGHT; i < FORMEXPANDHEIGHT; i += INCREMENT)
            {
                this.Size = new Size(FORMWIDTH, i);
                this.Update();
                System.Threading.Thread.Sleep(1);
            }
            int Totallines = CountTotalLines();

            //Reads the file from the 1st line and adds every membership ID to the List Box
            try
            {
                StreamReader file = File.OpenText("ClientInformation.txt");
                MembershipListBox.Items.Clear();
                for (int ID = 1; ID <= Totallines-1; ID += 7)
                {
                    MembershipListBox.Items.Add(SpecificLine(ID));
                }
                file.Close();
            }
            catch
            {
                MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            decimal totalmembershipfees = TotalValue(7);
            totalmembershipfees = Math.Round(totalmembershipfees, 2);
            TotalMembershipFeesLabel.Text = "€" + totalmembershipfees.ToString();
            AverageMembershipFeeLabel.Text = "€" + (totalmembershipfees / (Totallines / 7)).ToString();
            AverageMembershipTermLabel.Text = (TotalValue(6) / (Totallines / 7)).ToString("N2");
        }

        //User defined value returning function to calculate the total of the every nth line passed to the function
        private decimal TotalValue(int number)
        {
            decimal Total = 0;
            try
            {
                StreamReader file = File.OpenText("ClientInformation.txt");
                int Totallines = CountTotalLines();
                for (int i = number; i <= Totallines-1; i += 7)
                {
                    decimal Tot = Convert.ToDecimal(SpecificLine(i));
                    Total += Tot;
                }
                file.Close();
            }
            catch
            {
                MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Total;
        }

        //User defined function to return the data of the specific line
        static string SpecificLine(int lineNumber)
        {
            string content = null;
            try
            {
                using (StreamReader file = File.OpenText("ClientInformation.txt"))
                {
                    for (int i = 1; i < lineNumber; i++)
                    {
                        file.ReadLine();
                        if (file.EndOfStream)
                        {
                            MessageBox.Show("End of file reached");
                            break;  
                        }
                    }
                    content = file.ReadLine();
                    file.Close();
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            return content;
        }

        //Action performed when Search Button is clicked
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SummaryGroupBox.Visible = false;
            SearchGroupBox.Visible = true;
            for (int i = FORMSTARTHEIGHT; i < FORMEXPANDHEIGHT; i += INCREMENT)
            {
                this.Size = new Size(FORMWIDTH, i);
                this.Update();
                System.Threading.Thread.Sleep(1);
            }
        }

        //Action performed when Search by ID Button is clicked
        private void SearchByIDButton_Click(object sender, EventArgs e)
        {
            SearchedItemsListBox.Items.Clear();
            SearchByDateTextBox.Clear();
            try
            {
                int tempID = int.Parse(SearchByIDTextBox.Text);
                try
                {
                    StreamReader file = File.OpenText("ClientInformation.txt");
                    int Totallines = CountTotalLines();
                    for (int i = 1; i <= Totallines; i++)
                    {
                        if (tempID.ToString("D6") == file.ReadLine())
                        {
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            SearchedItemsListBox.Items.Add(file.ReadLine());
                            break;
                        }
                        else if(i == Totallines-1)
                        {
                            MessageBox.Show("Entered ID does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchByIDButton.Focus();
                            SearchByIDTextBox.Clear();
                        }
                    }
                    file.Close();
                }
                catch
                {
                    MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SearchByIDButton.Focus();
                    SearchByIDTextBox.Clear();
                }   
            }
            catch
            {
                MessageBox.Show("Enter 6 digit numeric value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }    
        }

        //Action performed when Search by Date Button is clicked
        private void SearchByDateButton_Click(object sender, EventArgs e)
        {
            SearchedItemsListBox.Items.Clear();
            SearchByIDTextBox.Clear();
            try
            {
                DateTime getdate = DateTime.ParseExact(SearchByDateTextBox.Text, "MM/dd/yyyy", null);
                try
                {
                    string tempdate = SearchByDateTextBox.Text;
                    StreamReader file = File.OpenText("ClientInformation.txt");
                    int Totallines = CountTotalLines();
                    for (int i = 5; i <= Totallines; i += 7)
                    {
                        if (tempdate == SpecificLine(i))
                        {
                            string ID = SpecificLine(i - 4);
                            string Name = SpecificLine(i - 3);
                            string phonenumber = SpecificLine(i - 2);
                            string mailID = SpecificLine(i - 1);
                            string term = SpecificLine(i + 1);
                            string price = SpecificLine(i + 2);
                            SearchedItemsListBox.Items.Add(ID);
                            SearchedItemsListBox.Items.Add(Name);
                            SearchedItemsListBox.Items.Add(phonenumber);
                            SearchedItemsListBox.Items.Add(mailID);
                            SearchedItemsListBox.Items.Add(term);
                            SearchedItemsListBox.Items.Add("€" + price);
                            SearchedItemsListBox.Items.Add("");
                        }
                        else if (i == Totallines - 3)
                        {
                            MessageBox.Show("Entered Date does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchByDateButton.Focus();
                            SearchByDateTextBox.Clear();
                        }
                    }
                    file.Close();
                }
                catch
                {
                    MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SearchByDateButton.Focus();
                    SearchByDateTextBox.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Enter valid Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SearchByDateButton.Focus();
                SearchByDateTextBox.Clear();
            }
        }

        //Action performed when Exit Button is clicked
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
