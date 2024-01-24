using DotNetProject1.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Diagnostics;

namespace DotNetProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TicketList()
        {
            var dataTable = new DataTable();

            // db 연결
            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
            {

                try
                {
                    connect.Open();
                    Console.WriteLine("Connection State: " + connect.State); // 연결 상태 출력
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("An error occurred connecting to the database: " + ex.Message);
                }

                using (var cmd = new MySqlCommand())
                {
                    // string statuss = "In Progress";
                    cmd.Connection = connect;
                    cmd.CommandText = "SELECT * FROM t_ticket";
                    // cmd.Parameters.AddWithValue("@statuss", statuss);

                    var reader = cmd.ExecuteReader();
                    dataTable.Load(reader);

                }
            }
            Console.WriteLine(dataTable);
            ViewData["dataTable"] = dataTable;
            return View();
        }

        public IActionResult TicketChange(int ticket_id, string title)
        {
            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
            {
                try
                {
                    connect.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("An error occurred connecting to the database: " + ex.Message);
                }

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connect;
                    cmd.CommandText = @"
UPDATE t_ticket
SET
    title = @title
WHERE
	ticket_id = @ticket_id
";
                    cmd.Parameters.AddWithValue("@ticket_id", ticket_id);
                    cmd.Parameters.AddWithValue("@title", title);

                    cmd.ExecuteNonQuery();
                }
            }

            return Json(new { msg = "OK" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
