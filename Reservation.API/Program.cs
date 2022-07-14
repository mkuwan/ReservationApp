using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.IServices;
using Reservation.Application.Services;
using Reservation.Domain.Models.ScheduleModels.Services;
using Reservation.Domain.Repositories;
using Reservation.Infrastructure.DbContexts;
using Reservation.Infrastructure.DummyData;
using Reservation.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ISchedulerRepository, SchedulerRepository>();
builder.Services.AddTransient<ICheckDuplicateRepository, CheckDuplicateRepository>();
builder.Services.AddTransient<IManagerRepository, ManagerRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ICheckDuplicateService, CheckDuplicateService>();
builder.Services.AddTransient<ISchedulerService, SchedulerService>();

builder.Services.AddDbContext<ReservationDbContext>(options =>
{
    options.UseSqlite("Data Source=reservation.db");
  
});

// 認可によって保護されたユーザー データを使って ASP.NET Core Web アプリを作成する
// https://docs.microsoft.com/ja-jp/aspnet/core/security/authorization/secure-data?view=aspnetcore-6.0#rau
// 権限: ロールサービス
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ReservationDbContext>();

// 認証されたユーザーを要求するフォールバック認可ポリシーの設定
// このフォールバック認可ポリシーにより、認可属性を持つ Razor Pages、コントローラー、またはアクション メソッドを除き、
// "すべての" ユーザーの認証が要求されます。
// たとえば、[AllowAnonymous] や [Authorize(PolicyName="MyPolicy")] を使用する Razor Pages、コントローラー、
// またはアクション メソッドでは、フォールバック認可ポリシーではなく適用された認可属性が使用されます。
//builder.Services.AddAuthorization(options =>
//{
//    // ASP.NET Core でのポリシー ベースの認可
//    // https://docs.microsoft.com/ja-jp/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

// CORS
string AllowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()   // WithOrigins("https://localhost:7093", "https://localhost:7093")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});



// Authorization Handler
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingServerAuthenticationStateProvider>();





builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data to DB
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ReservationDbContext>();

    await context.Database.EnsureDeletedAsync();
    //await context.Database.EnsureCreatedAsync();
    context.Database.Migrate();

    // UserSecret を使う方法: ローカルPC内の指定のディレクトリ内で管理します
    // これはあくまでも開発環境用としてして行います
    // (参照)「ASP.NET Core での開発におけるアプリ シークレットの安全な保存」
    // https://docs.microsoft.com/ja-jp/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
    // dotnet user-secrets set --project Reservation.API ManagerPW <pw>
    // dotnet user-secrets set --project Reservation.API ClientPW <pw>　
    // 　としてローカル環境にシークレットパスワードを作成しておく
    var initialManagerPw = builder.Configuration.GetValue<string>("ManagerPW");
    var unitialClientPw = builder.Configuration.GetValue<string>("ClientPW");

    await SeedData.DeleteDate(context);
    await SeedData.InitializeAspNetUsersManaber(serviceProvider, initialManagerPw);
    await SeedData.InitializeAspNetUsersClient(serviceProvider, unitialClientPw);
    await SeedData.InitializeSchedule(context);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseCors(AllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

