using LNU.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LNU.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                Query query = new Query
                {
                    Country = this.tb_country.Text,
                    Q = this.tb_q.Text,
                    Source = this.tb_source.Text,
                    Type = (
                    this.select_type.ItemContainerGenerator.ContainerFromIndex(this.select_type.SelectedIndex) as ListBoxItem).Content.ToString()
                };

                StringContent content = new StringContent(
                   JsonConvert.SerializeObject(query),
                   Encoding.UTF8,
                   "application/json");

                var response = client.PostAsync($"http://localhost:64438/api/values/Load", content).Result;
            }

            myWebBrowser.Refresh();
        }
    }
}
