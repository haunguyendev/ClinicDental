using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRN221.ClinicDental.Business.Services;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Data.UnitOfWork;
using PRN221.ClinicDental.Presentation.Extensions;
using System.Text;
using PRN221.ClinicDental.Services.Interfaces;
using PRN221.ClinicDental.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"dentalclinic-cloud-storage.json");

builder.Services.AddSingleton<ICloudStorage>(s => new CloudStorage(StorageClient.Create()));

builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Accounts/Login";
            options.AccessDeniedPath = "/Accounts/AccessDenied";
        });
builder.Services.AddDbContext<ClinicDentalDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
    options.AddPolicy("ClinicOwnerOnly", policy => policy.RequireRole("ClinicOwner"));
    options.AddPolicy("DentistOnly", policy => policy.RequireRole("Dentist"));
    // Thêm các chính sách khác nếu cần
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("0"));
    options.AddPolicy("StaffPolicy", policy => policy.RequireRole("1"));
    options.AddPolicy("LecturePolicy", policy => policy.RequireRole("2"));
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IClinicRepository, ClinicRepository>();
builder.Services.AddTransient<IClinicServicesRepository, ClinicServicesRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IDentistDetailRepository, DentistDetailRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    // Log body yêu cầu
    if (context.Request.Method == "POST" || context.Request.Method == "PUT")
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        Console.WriteLine($"Request Body: {body}");
    }

    await next.Invoke();

    // Log thông tin phản hồi
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});


app.MapRazorPages();


app.Run();
