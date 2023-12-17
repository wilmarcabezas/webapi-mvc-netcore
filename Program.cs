using Microsoft.EntityFrameworkCore;
using apibackend.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiwebnetContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));


builder.Services.AddControllers().AddJsonOptions(opt =>
   { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
);

var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();


//dotnet-ef dbcontext scaffold "server=198.49.75.133; Database=apiwebnet;user=sa;Pwd=Asus8426*;Trust Server Certificate=true" Microsoft.EntityFrameworkCore.SqlServer -o Models