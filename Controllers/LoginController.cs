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

				// 로그인 작업

				// 클레임(claim)은 사용자에 대한 정보 조각 (쿠키 인증에서 사용하는 기본 스키마, 사용자의 이름에 대한 클레임을 나타내는 표준 URI, 사용자의 역할에 대한 클레임을 나타내는 표준 URI)
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.User_seq.ToString()));
				identity.AddClaim(new Claim(ClaimTypes.Name, user.User_name));
				identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

				var principal = new ClaimsPrincipal(identity);

				// 사용자를 인증하고, 현재 세션에 로그인 상태를 설정
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
				// 비밀번호 재입력 확인
				string password2 = Request.Form!["password2"];

				if (input.Password != password2)
				{
					throw new Exception("비밀번호를 확인해 주세요.");
				}

				// password 암호화
				input.ConvertPassword();
				// db에 저장
				input.Register();

				return Redirect("/login/login");
			}
			catch (Exception ex)
			{
				return Redirect($"/login/register?msg={HttpUtility.UrlEncode(ex.Message)}");
			}
		}

		[Authorize] // 인증된 사용자만 로그아웃 가능
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return Redirect("/");
		}
	}
}


// 쿠키 인증 참고: https://learn.microsoft.com/ko-kr/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0