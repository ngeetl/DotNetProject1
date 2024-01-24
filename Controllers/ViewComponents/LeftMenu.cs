using Microsoft.AspNetCore.Mvc;

namespace DotNetProject1.Controllers.ViewComponents
{
	public class LeftMenu : ViewComponent
	{
		// 생성자
		public LeftMenu()
		{

		}

		// 뷰 컴포넌트가 호출될 때 실행되는 메서드
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
