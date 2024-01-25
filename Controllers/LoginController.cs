using DotNetProject1.Models;
using DotNetProject1.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;

namespace DotNetProject1.Controllers
{
	public class LoginController : Controller
	{
		public LoginController()
		{
		}

		public IActionResult Index()
		{
			return Redirect("/login/login");
		}

		[HttpGet]
		public IActionResult Login(string msg)
		{
			ViewData["msg"] = msg;

			return View();
		}

		[HttpPost]
		[Route("/login/login")]
		public async Task<IActionResult> LoginProc([FromForm] UserModel input)
		{
			try
			{
				input.ConvertPassword();
				var user = input.GetLoginUser();

				// �α��� �۾�

				// Ŭ����(claim)�� ����ڿ� ���� ���� ���� (��Ű �������� ����ϴ� �⺻ ��Ű��, ������� �̸��� ���� Ŭ������ ��Ÿ���� ǥ�� URI, ������� ���ҿ� ���� Ŭ������ ��Ÿ���� ǥ�� URI)
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.User_seq.ToString()));
				identity.AddClaim(new Claim(ClaimTypes.Name, user.User_name));
				identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

				var principal = new ClaimsPrincipal(identity);

				// ����ڸ� �����ϰ�, ���� ���ǿ� �α��� ���¸� ����
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
				{
					IsPersistent = false,
					ExpiresUtc = DateTime.UtcNow.AddHours(4),
					AllowRefresh = true
				});

				return Redirect("/");
			}
			catch (Exception ex)
			{
				return Redirect($"/login/login?msg={HttpUtility.UrlEncode(ex.Message)}");
			}
		}

		public IActionResult Register(string msg)
		{
			ViewData["msg"] = msg;

			return View();
		}

		[HttpPost]
		[Route("/login/register")]
		public IActionResult RegisterProc([FromForm] UserModel input)
		{
			try
			{
				// ��й�ȣ ���Է� Ȯ��
				string password2 = Request.Form!["password2"];

				if (input.Password != password2)
				{
					throw new Exception("��й�ȣ�� Ȯ���� �ּ���.");
				}

				// password ��ȣȭ
				input.ConvertPassword();
				// db�� ����
				input.Register();

				return Redirect("/login/login");
			}
			catch (Exception ex)
			{
				return Redirect($"/login/register?msg={HttpUtility.UrlEncode(ex.Message)}");
			}
		}

		[Authorize] // ������ ����ڸ� �α׾ƿ� ����
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return Redirect("/");
		}
	}
}


// ��Ű ���� ����: https://learn.microsoft.com/ko-kr/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0