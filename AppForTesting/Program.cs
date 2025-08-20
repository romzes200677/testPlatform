using CSharpTestApp.Services;
using CSharpTestApp.Services.Generators;

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
builder.Services.AddTransient<IQuestionService,QuestionService>();
builder.Services.AddTransient<ICodeTester, CodeTester>();
builder.Services.AddTransient<ICodeExecutionService, CodeExecutionService>();
builder.Services.AddTransient<InputOutputQuestionGenerator>();
builder.Services.AddTransient<VariableDeclarationQuestionGenerator>();
builder.Services.AddTransient<ConditionQuestionGenerator>();
builder.Services.AddTransient<LoopQuestionGenerator>();
builder.Services.AddTransient<StringsAndSymbolsQuestionGenerator>();
builder.Services.AddTransient<FunctionsQuestionGenerator>();
builder.Services.AddTransient<ArraysQuestionGenerator>();

builder.Services.AddTransient<IQuestionGenerator>(serviceProvider =>
{
    var generators = new List<IQuestionGenerator>
    {
        serviceProvider.GetRequiredService<InputOutputQuestionGenerator>(),
        serviceProvider.GetRequiredService<VariableDeclarationQuestionGenerator>(),
        serviceProvider.GetRequiredService<ConditionQuestionGenerator>(),
        serviceProvider.GetRequiredService<LoopQuestionGenerator>(),
        serviceProvider.GetRequiredService<StringsAndSymbolsQuestionGenerator>(),
        serviceProvider.GetRequiredService<FunctionsQuestionGenerator>(),
        serviceProvider.GetRequiredService<ArraysQuestionGenerator>()
    };
    return new CompositeQuestionGenerator(() => generators);
});

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