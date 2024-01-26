using DotNetProject1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

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
            return View(TicketModel.GetList());
        }

        public IActionResult TicketChange([FromForm] TicketModel model)
        {
            model.Update();

            return Redirect("/home/ticketlist");
        }
        public IActionResult BoardList(string search)
        {

            return View(BoardModel.GetList(search));
        }

        [Authorize]
		public IActionResult BoardWrite()
		{
			return View();
		}

        [Authorize]
        public IActionResult BoardWrite_Input(string title, string contents)
        {
            var model = new BoardModel();

            model.Title = title;   
            model.Contents = contents;
            model.Reg_user = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.Reg_username = User.Identity.Name;

            model.Insert();

            return Redirect("/home/boardlist");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
