using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfHarmony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string Host = "http://localhost:5092";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            EditNameText.Text = FindNameText.Text;
            EditValueText.Text = FindValueText.Text;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (FindValueText.Text.Length > 0)
                Delete();
            FindNameText.Text = string.Empty;
            FindValueText.Text = string.Empty;
        }

        private void FindNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FindNameText.Text.Length > 0)
            {
                Search();
            }
            else
            {
                FindValueText.Text = string.Empty;
            }
        }
        private async Task Search()
        {
            var http = new HttpClient();
            var result = await http.GetStringAsync($"{Host}/GetItem?name={FindNameText.Text}");

            if (string.IsNullOrEmpty(result))
            {
                //Подписать уведомление
                return;
            }
            FindValueText.Text = result;
        }
        private async Task Add()
        {
            var http = new HttpClient();
            var Response = await http.PostAsync($"{Host}/AddItem", new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "name", EditNameText.Text },
                { "value", EditValueText.Text }
            }
            ));
            Console.WriteLine(await Response.Content.ReadAsStringAsync());
        }
        private async Task Delete()
        {
            var http = new HttpClient();
            await http.DeleteAsync($"{Host}/DeleteItem?name={FindNameText.Text}");
        }

    }
}
