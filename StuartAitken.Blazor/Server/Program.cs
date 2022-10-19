using Microsoft.OpenApi.Models;
using StuartAitken.Blazor.Server.ActionFilters;
using StuartAitken.Blazor.Server.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
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
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StuartAitken.Blazor API");
});

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
