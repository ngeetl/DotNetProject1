using DotNetProject1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
			return View(TicketModel.GetList());
		}

		//public IActionResult TicketList()
		//{
		//	return View(TicketModel.GetList());
		//}

		public IActionResult TicketChange([FromBody] TicketModel model)
		{
			model.Update();

			return Json(model); // ajax는 JSON 형태로 반환 받음
								//return Redirect("/home/ticketlist");
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
		public IActionResult BoardView(uint idx)
		{

			return View(BoardModel.Get(idx));
		}

		[Authorize]
		public IActionResult BoardEdit(uint idx, string type)
		{
			var model = BoardModel.Get(idx);
			var userSeq = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

			if (model.Reg_user != userSeq)
			{
				throw new Exception("수정할 수 있는 권한이 없습니다.");
			}

			if (type == "update")
			{
				return View(model);
			}
			else if (type == "delete")
			{
				model.Delete();
				return Redirect("/home/boardlist");
			}

			throw new Exception("잘못된 요청입니다.");
		}

		[Authorize]
		public IActionResult BoardEdit_Input(uint idx, string title, string contents)
		{
			var model = BoardModel.Get(idx);
			var userSeq = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

			if (model.Reg_user != userSeq)
			{
				throw new Exception("수정할 수 있는 권한이 없습니다.");
			}

			model.Title = title;
			model.Contents = contents;

			model.Update();

			return Redirect("/home/boardlist");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
