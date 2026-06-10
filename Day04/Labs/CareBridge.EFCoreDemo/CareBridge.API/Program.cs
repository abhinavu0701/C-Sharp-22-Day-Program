using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext.
// ASP.NET Core will automatically create and inject it when needed.
builder.Services.AddDbContext<CareBridgeScaffoldContext>();

// Add Swagger support.
// Swagger gives us a testing screen for APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Vue.js running on another port
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS.
app.UseCors();

// Simple health-check endpoint.
app.MapGet("/", () =>{
    return "CareBridge API is running";
});

// R
app.MapGet("/api/patients",
    (CareBridgeScaffoldContext db) =>
    {
        var result =
            from e in db.Encounters
            join d in db.Departments
                on e.DepartmentId equals d.DepartmentId

            group e by d.Name into g

            select new
            {
                DepartmentName = g.Key,

                InPatientCount = g.Count(x => x.EncounterType == "InPatient"),
                OutPatientCount = g.Count(x => x.EncounterType == "OutPatient"),
                EDCount = g.Count(x => x.EncounterType == "ED"),

                TotalCount = g.Count()
            };

        return result
            .OrderByDescending(x => x.TotalCount)
            
            .ToList();
    });


app.Run();
