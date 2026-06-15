using CareBridge.Api.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;


using CareBridge.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===================================================================
// REGISTER THE DATABASE CONTEXT
// 'AddDbContext' tells ASP.NET Core: "whenever any piece of code asks
// for a CareBridgeDbContext, create one for them, configured to talk
// to SQL Server using the connection string we just defined in
// appsettings.json (section 3.7)".
//
// This is called DEPENDENCY INJECTION - you saw this pattern on Day 8
// too. We are not creating the database connection ourselves anywhere
// in our controller code; we just ASK for it, and ASP.NET Core hands
// us a ready-to-use one.
// ===================================================================
builder.Services.AddDbContext<CareBridgeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CareBridgeDb")));


var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(
    TimeSpan.FromSeconds(2),
    TimeoutStrategy.Optimistic);



var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutRejectedException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt - 1)),
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            Console.WriteLine(
                $"[RETRY] Attempt {retryAttempt} after {timespan.TotalSeconds}s. " +
                $"Reason: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
        });



var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutRejectedException>()
    .CircuitBreakerAsync(
        handledEventsAllowedBeforeBreaking: 3,
        durationOfBreak: TimeSpan.FromSeconds(30),
        onBreak: (outcome, breakDelay) =>
        {
            // Fires the MOMENT the circuit flips from CLOSED to OPEN.
            // 'breakDelay' is how long it will STAY open (30 seconds,
            // as configured above).
            Console.WriteLine(
                $"[CIRCUIT BREAKER] OPENED for {breakDelay.TotalSeconds}s. " +
                $"Reason: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
        },
        onReset: () =>
        {
            // Fires when the circuit goes back to CLOSED - meaning
            // a HALF-OPEN test call SUCCEEDED, and normal operation
            // has resumed.
            Console.WriteLine("[CIRCUIT BREAKER] CLOSED - calls will be attempted again.");
        },
        onHalfOpen: () =>
        {
            // Fires when the 30-second cooldown ends, and Polly is
            // about to cautiously allow exactly ONE test call through.
            Console.WriteLine("[CIRCUIT BREAKER] HALF-OPEN - testing with next call.");
        });







builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// app.UseDefaultFiles() - if someone requests the 'root' address
// (e.g. just https://localhost:<port>/ with nothing after it),
// automatically serve wwwroot/index.html, without them having to
// type '/index.html' explicitly.
app.UseDefaultFiles();

// app.UseStaticFiles() - actually turns on the feature of serving
// files from the wwwroot folder at all. Without this line,
// requesting /index.html would return a 404 Not Found, even though
// the file physically exists on disk.
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
