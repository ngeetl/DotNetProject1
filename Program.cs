var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 요청이 들어올 때 헤더, 쿠키 등을 확인해 HttpContext.User 속성을 설정하여 현재 요청과 연관된 사용자의 신원을 나타냅니다.
app.UseAuthentication();
// UseAuthentication 바탕으로 API 엔드포인트에 접근할 권리가 있는지, 특정 페이지를 볼 수 있는지 등을 결정. 권한 부여 정책(예: [Authorize] 어트리뷰트)에 따라 작동
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
