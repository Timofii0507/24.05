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
                    OutputTextBox.Text = "Підключення успішне!\n";

                    DisplayAllCustomers(connection);
                    DisplayAllEmails(connection);
                    DisplayAllSections(connection);
                    DisplayAllPromotions(connection);
                    DisplayAllCities(connection);
                    DisplayAllCountries(connection);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Помилка підключення: {ex.Message}";
            }
        }

        private void DisplayAllCustomers(SqlConnection connection)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers");
            OutputTextBox.Text += "Усі покупці:\n";
            foreach (var customer in customers)
            {
                OutputTextBox.Text += $"{customer.CustomerID}: {customer.FullName}, {customer.Email}\n";
            }
        }

        private void DisplayAllEmails(SqlConnection connection)
        {
            var emails = connection.Query<string>("SELECT Email FROM Customers");
            OutputTextBox.Text += "Email усіх покупців:\n";
            foreach (var email in emails)
            {
                OutputTextBox.Text += email + "\n";
            }
        }

        private void DisplayAllSections(SqlConnection connection)
        {
            var sections = connection.Query<Section>("SELECT * FROM Sections");
            OutputTextBox.Text += "Усі розділи:\n";
            foreach (var section in sections)
            {
                OutputTextBox.Text += $"{section.SectionID}: {section.SectionName}\n";
            }
        }

        private void DisplayAllPromotions(SqlConnection connection)
        {
            var promotions = connection.Query<Promotion>("SELECT * FROM Promotions");
            OutputTextBox.Text += "Усі акційні товари:\n";
            foreach (var promotion in promotions)
            {
                OutputTextBox.Text += $"{promotion.PromotionID}: {promotion.SectionID}, {promotion.Country}, {promotion.StartDate}, {promotion.EndDate}\n";
            }
        }

        private void DisplayAllCities(SqlConnection connection)
        {
            var cities = connection.Query<string>("SELECT DISTINCT City FROM Customers");
            OutputTextBox.Text += "Усі міста:\n";
            foreach (var city in cities)
            {
                OutputTextBox.Text += city + "\n";
            }
        }

        private void DisplayAllCountries(SqlConnection connection)
        {
            var countries = connection.Query<string>("SELECT DISTINCT Country FROM Customers");
            OutputTextBox.Text += "Усі країни:\n";
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
                OutputTextBox.Text = $"Помилка підключення: {ex.Message}";
            }
        }

        private void DisplayCustomersByCity(SqlConnection connection, string city)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers WHERE City = @City", new { City = city });
            OutputTextBox.Text += $"Покупці з міста {city}:\n";
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
                OutputTextBox.Text = $"Помилка підключення: {ex.Message}";
            }
        }

        private void DisplayCustomersByCountry(SqlConnection connection, string country)
        {
            var customers = connection.Query<Customer>("SELECT * FROM Customers WHERE Country = @Country", new { Country = country });
            OutputTextBox.Text += $"Покупці з країни {country}:\n";
            foreach (var customer in customers)
            {
                OutputTextBox.Text += $"{customer.CustomerID}: {customer.FullName}, {customer.Email}\n";
            }
        }

        private void CountryPromotionsButton_Click(object sender, RoutedEventArgs e)
        {
            string country = CountryTextBox.Text;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    DisplayPromotionsByCountry(connection, country);
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Помилка підключення: {ex.Message}";
            }
        }

        private void DisplayPromotionsByCountry(SqlConnection connection, string country)
        {
            var promotions = connection.Query<Promotion>("SELECT * FROM Promotions WHERE Country = @Country", new { Country = country });
            OutputTextBox.Text += $"Акції для країни {country}:\n";
            foreach (var promotion in promotions)
            {
                OutputTextBox.Text += $"{promotion.PromotionID}: {promotion.SectionID}, {promotion.Country}, {promotion.StartDate}, {promotion.EndDate}\n";
            }
        }
    }
}