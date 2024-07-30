using System;
using System.Windows;
using Microsoft.Data.SqlClient;
using Dapper;

namespace _24._05
{
    public partial class MainWindow : Window
    {
        string connectionString = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    OutputTextBox.Text = "Connection successful!\n";
                    LoadAllData(connection);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Connection error: {ex.Message}";
            }
        }

        private void LoadAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    LoadAllData(connection);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Error loading data: {ex.Message}";
            }
        }

        private void LoadAllData(SqlConnection connection)
        {
            OutputTextBox.Text = "";

            DisplayAllCustomers(connection);
            DisplayAllEmails(connection);
            DisplayAllCities(connection);
            DisplayAllCountries(connection);
        }

        private void DisplayAllCustomers(SqlConnection connection)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers");
            OutputTextBox.Text += "All customers:\n";
            foreach (var customer in customers)
            {
                OutputTextBox.Text += $"{customer.CustomerID}: {customer.FullName}, {customer.Email}\n";
            }
        }

        private void DisplayAllEmails(SqlConnection connection)
        {
            var emails = connection.Query<string>("SELECT Email FROM Customers");
            OutputTextBox.Text += "Emails of all customers:\n";
            foreach (var email in emails)
            {
                OutputTextBox.Text += email + "\n";
            }
        }

        private void DisplayAllCities(SqlConnection connection)
        {
            var cities = connection.Query<string>("SELECT DISTINCT City FROM Customers");
            OutputTextBox.Text += "All cities:\n";
            foreach (var city in cities)
            {
                OutputTextBox.Text += city + "\n";
            }
        }

        private void DisplayAllCountries(SqlConnection connection)
        {
            var countries = connection.Query<string>("SELECT DISTINCT Country FROM Customers");
            OutputTextBox.Text += "All countries:\n";
            foreach (var country in countries)
            {
                OutputTextBox.Text += country + "\n";
            }
        }

        private void CityButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    DisplayCustomersByCity(connection, city);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Connection error: {ex.Message}";
            }
        }

        private void DisplayCustomersByCity(SqlConnection connection, string city)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers WHERE City = @City", new { City = city });
            OutputTextBox.Text += $"Customers from {city}:\n";
            foreach (var customer in customers)
            {
                OutputTextBox.Text += $"{customer.CustomerID}: {customer.FullName}, {customer.Email}\n";
            }
        }

        private void CountryButton_Click(object sender, RoutedEventArgs e)
        {
            string country = CountryTextBox.Text;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    DisplayCustomersByCountry(connection, country);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Connection error: {ex.Message}";
            }
        }

        private void DisplayCustomersByCountry(SqlConnection connection, string country)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers WHERE Country = @Country", new { Country = country });
            OutputTextBox.Text += $"Customers from {country}:\n";
            foreach (var customer in customers)
            {
                OutputTextBox.Text += $"{customer.CustomerID}: {customer.FullName}, {customer.Email}\n";
            }
        }
    }
}