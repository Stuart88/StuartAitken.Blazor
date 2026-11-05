using System.IO.Compression;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using StuartAitken.Blazor.Server.ActionFilters;
using StuartAitken.Blazor.Server.DataAccess;
using StuartAitken.Blazor.Server.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Blazor WASM's own build-time Brotli precompression of _framework/* already covers most of
// the first-load payload, but this middleware additionally compresses the API JSON responses,
// Razor Pages, Swagger document, and any static asset not already precompressed - and acts as
// a safety net if the host ever serves _framework files without the precompressed .br/.gz sidecars.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/wasm", "application/octet-stream" }
    );
});
builder.Services.Configure<BrotliCompressionProviderOptions>(
    options => options.Level = CompressionLevel.Fastest
);
builder.Services.Configure<GzipCompressionProviderOptions>(
    options => options.Level = CompressionLevel.Fastest
);

builder.Services.AddDbContext<StuartAitkenContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("StuartAitkenContext"))
);

builder.Services.AddScoped<ProjectsService>();
builder.Services.AddScoped<ProjectImageService>();
builder.Services.AddScoped<SecureDataService>();

builder.Services.AddControllersWithViews(opts =>
{
    //opts.Filters.AddService<CustomAuthorise>();
});
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AuthHeaderSwaggerFilter>();
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Basic",
            //BearerFormat = "JWT",
            In = ParameterLocation.Header,
            //Description = "JWT Authorization header. \r\n\r\n Enter the token in the text input below.,
        }
    );
});

builder.Services.AddScoped<AdminAuthorise>();

// Behind a reverse proxy (IIS, nginx, a CDN), Connection.RemoteIpAddress is the proxy's own
// address for every request unless the proxy's forwarded headers are trusted and applied here.
// KnownNetworks/KnownProxies are cleared because the proxy's address isn't fixed/known ahead of
// time; if this app is ever exposed directly to the internet without a proxy in front, pin those
// back down (or remove this block) - otherwise a client could forge X-Forwarded-For itself.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// Shared across every [AdminAuthorise]-protected endpoint (not just the login check, so a brute
// force can't just move to a different admin route to keep guessing), keyed by client IP: 10
// permits per 3 minutes, refilled automatically by the window. AdminAuthorise only draws from it
// on a wrong password (see the comment there), and the limiter itself owns all the per-key
// bookkeeping and eviction - nothing hand-rolled.
builder.Services.AddSingleton(
    PartitionedRateLimiter.Create<string, string>(clientKey =>
        RateLimitPartition.GetFixedWindowLimiter(
            clientKey,
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(3),
                QueueLimit = 0,
                AutoReplenishment = true
            }
        )
    )
);

var app = builder.Build();

// Must run before anything that inspects the scheme or the client's address.
app.UseForwardedHeaders();

// Must run before any middleware that writes to the response (static files, MVC, Swagger, etc.)
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Swagger exposes the full API surface - only serve it in Development.
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StuartAitken.Blazor API");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
