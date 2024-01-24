using DotNetProject1.Models;
using DotNetProject1.Models.Login;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login/login")]
        public IActionResult LoginProc()
        {


            return null;
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
    }
}
