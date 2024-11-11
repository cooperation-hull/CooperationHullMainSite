using CooperationHullMainSite.Models.ConfigSections;
using CooperationHullMainSite.Services;
using NLog;
using NLog.Web;
using static CooperationHullMainSite.Models.ConfigSections.ActionNetworkConfig;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Base services
    builder.Services.AddRazorPages();


    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        // This lambda determines whether user consent for non-essential 
        // cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.Lax;
        options.ConsentCookieValue = "true";
    });


    builder.Services.AddControllersWithViews();

    //Custom config options
    builder.Services.Configure<ActionNetworkConfig>(
        builder.Configuration.GetSection("ActionNetworkConfig"));

    builder.Services.Configure<SanityCMSConfig>(
        builder.Configuration.GetSection("SanityCMSConfig"));

    builder.Configuration.AddEnvironmentVariables();

    //Custom services
    builder.Services.AddSingleton<IJsonFileReader, JsonFileReader>();
    builder.Services.AddSingleton<IActionNetworkCalls, ActionNetworkCalls>();
    builder.Services.AddSingleton<ISanityCMSCalls, SanityCMSCalls>();

    //NLog setup
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Host.UseNLog();


    var app = builder.Build();

    app.Logger.LogInformation(Environment.GetEnvironmentVariable("HELLO_FROM_GCP"));  //TODO: set this in cloud run and check build logs to see if it was accessible

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Flush();
    LogManager.Shutdown();
}


