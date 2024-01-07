using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCOREgrp05.Data;
using SCOREgrp05.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MBContext") ?? throw new InvalidOperationException("Connection string 'MBContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
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
app.MapHub<MatchHub>("/MatchHub");
app.MapHub<ButHub>("/ButHub");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Matches}/{action=Index}/{id?}");

app.Run();
