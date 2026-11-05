using System.IO.Compression;
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

var app = builder.Build();

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
