using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Nikaman.Pages.OffersPanel
{
    public class IndexModel : PageModel
    {
        public List<Offer> clients = new List<Offer>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-AU6MOVM\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Offer";
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Offer client = new Offer();
                                client.Id = reader.GetInt32(0);
                                client.Email = reader.GetString(1);
                                client.Instagram = reader.GetString(2);
                                client.Offered_At = reader.GetDateTime(3);
                                client.Files = reader.GetString(4);
                                client.AddInfo = reader.GetString(5);
                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class Offer
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Instagram { get; set; }
        public string? AddInfo { get; set; }
        public DateTime Offered_At { get; set; }
        public string? Files { get; set; }
    }
}
