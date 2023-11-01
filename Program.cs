using coreAPI.Data;
using coreAPI.Mappings;
using coreAPI.Repositories.Regions;
using coreAPI.Repositories.Walks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting and Providing ConnectionString for managing all the instance of dbConext class
builder.Services.AddDbContext<CoreDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CoreAPIConnectionString"))
);

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalksRepository, SQLWalkRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
