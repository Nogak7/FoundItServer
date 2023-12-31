using System.Text.Json;
using System.Text.Json.Serialization;
using FountItBL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
#region DBCONTEXT
//load connection string
string connection = builder.Configuration.GetConnectionString("FoundItDBS"); 
//Add DBContext
builder.Services.AddDbContext<FoundItDbContext>(options=>options.UseSqlServer(connection));
#endregion

// Add services to the container.
#region Json Hendeling
builder.Services.AddControllers().
    AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region 2- Session Support
//Add Session support
builder.Services.AddDistributedMemoryCache(); builder.Services.AddSession(options =>
{
options.IdleTimeout = TimeSpan.FromMinutes(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();



#region Use FILES And Session. //use files 
app.UseStaticFiles(); 
app.UseRouting();
#region Use Session
app.UseSession();
#endregion 
#endregion

app.MapControllers();

app.Run();

