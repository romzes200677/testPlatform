using CSharpTestApp.Infrastructure;
using CSharpTestApp.Services;

var builder = WebApplication.CreateBuilder(args);
// Добавление сервисов
// Регистрация сервисов
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<ICodeTester, CodeTester>();
builder.Services.AddScoped<CompilationResult>();
builder.Services.AddScoped<TestRunResult>();
builder.Services.AddScoped<ITestRepository, InMemoryTestRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration => 
{
    configuration.RootPath = "ClientApp/dist";
});

var app = builder.Build();
app.UseCors("AllowAll");
// Конфигурация middleware
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

// Для разработки: запускаем Vite вручную
if (app.Environment.IsDevelopment())
{
    // Запуск Vite через отдельный процесс
    StartViteDevServer();
}
else
{
    app.MapFallbackToFile("index.html");
}

app.Run();

void StartViteDevServer()
{
    var viteProcess = new System.Diagnostics.Process
    {
        StartInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "run dev",
            WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
            UseShellExecute = true,
            CreateNoWindow = false
        }
    };
    
    viteProcess.Start();
    
    // Закрыть процесс при завершении приложения
    app.Lifetime.ApplicationStopping.Register(() => 
    {
        if (!viteProcess.HasExited)
            viteProcess.Kill();
    });
}