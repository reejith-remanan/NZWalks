using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
<<<<<<< Updated upstream
=======
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
>>>>>>> Stashed changes

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
<<<<<<< Updated upstream

=======
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();// switch data sources easily

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
>>>>>>> Stashed changes
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
