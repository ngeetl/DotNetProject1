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
    }
}
