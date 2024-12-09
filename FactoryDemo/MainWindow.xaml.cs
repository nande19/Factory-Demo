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

namespace FactoryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private InsuranceFactory _insuranceFactory;
        private Policy _policy; // Declare policy object at the class level
        private string clame1State;

        public MainWindow()
        {
            InitializeComponent();
            _insuranceFactory = new VehicleInsuranceFactory();
            _policy = new Policy();

            // Create claims
            Claim claim1 = new Claim { State = "pending", Policy = _policy };

            // Attach event handler to the policy's ClaimCompleted event
            _policy.ClaimCompleted += Policy_ClaimCompleted;

            // Simulate completing a claim
            claim1.State = "complete";

            string clame1State = claim1.State;

            // Console.WriteLine(clame1State + "\n" + claime2State);
            MessageBox.Show(clame1State + "\n");

        }

        // Event handler for the policy's ClaimCompleted event
        private void Policy_ClaimCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("All claims against the policy have been completed.");
        }
    
    //--------------------------------------------------------------------------------------------------------//

    /// <summary>
    /// types of policies dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PolicyType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)PolicyType.SelectedItem;
            if (selectedItem.Content.ToString() == "Vehicle Insurance")
            {
                _insuranceFactory = new VehicleInsuranceFactory();
                VehicleDetailsPanel.Visibility = Visibility.Visible;
                ContentsValuePanel.Visibility = Visibility.Collapsed;
            }
            else if (selectedItem.Content.ToString() == "Household Contents Insurance")
            {
                _insuranceFactory = new HouseholdContentsFactory();
                VehicleDetailsPanel.Visibility = Visibility.Collapsed;
                ContentsValuePanel.Visibility = Visibility.Visible;
            }
        }
        //--------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// actions that take place after clicking button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePolicyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_insuranceFactory != null)
            {
                InsurancePolicy policy = _insuranceFactory.CreatePolicy();
                if (policy != null)
                {
                    string policyDetails = "";

                    if (policy is VehicleInsurance)
                    {
                        ((VehicleInsurance)policy).Model = ModelTextBox.Text;
                        ((VehicleInsurance)policy).Color = ColorTextBox.Text;

                        int year;
                        if (int.TryParse(YearTextBox.Text, out year))
                        {
                            ((VehicleInsurance)policy).YearOfRegistration = year;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid year of registration.");
                            return;
                        }

                        policyDetails += "Policy Type: Vehicle Insurance\n";
                        policyDetails += $"Model: {((VehicleInsurance)policy).Model}\n";
                        policyDetails += $"Color: {((VehicleInsurance)policy).Color}\n";
                        policyDetails += $"Year of Registration: {((VehicleInsurance)policy).YearOfRegistration}\n";
                    }
                    else if (policy is HouseholdContentsInsurance)
                    {
                        int value;
                        if (int.TryParse(ContentsValueTextBox.Text, out value))
                        {
                            ((HouseholdContentsInsurance)policy).EstimatedValue = value;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid estimated value.");
                            return;
                        }

                        policyDetails += "Policy Type: Household Contents Insurance\n";
                        policyDetails += $"Estimated Contents Value: {((HouseholdContentsInsurance)policy).EstimatedValue}\n";
                    }

                    // Display policy details immediately
                    MessageBox.Show($"Policy Created Successfully!\n\nPolicy Details:\n{policyDetails}");

                    // Notify the related policy when all claims are completed
                    _policy.ClaimCompleted += (s, args) =>
                    {
                        MessageBox.Show("All claims against the policy have been completed.");

                        // Use Dispatcher to delay showing the complete message box
                        Dispatcher.BeginInvoke((Action)(() =>
                        {
                            MessageBox.Show(clame1State);
                        }));
                    };
                }
            }
            else
            {
                MessageBox.Show("Please select a policy type first.");
            }
        }

    }
}
        //---------------------------------------- END OF FILE -------------------------------------------------------//

