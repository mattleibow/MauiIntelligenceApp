using Azure;
using Azure.AI.OpenAI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.AI;
using Microsoft.Extensions.AI;
using Syncfusion.Maui.Toolkit.Hosting;
using System.Reflection;
using MauiIntelligenceApp.AI;

namespace MauiIntelligenceApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit(s => s.SetShouldEnableSnackbarOnWindows(true))
			.ConfigureSyncfusionToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
				fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
			});

		// Add configuration from appsettings.json
		builder.Configuration
			.AddOptionalJsonFile("appsettings.json");
#if DEBUG
		builder.Configuration
			.AddOptionalJsonFile("appsettings.Development.json");
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif


		builder.Services.AddSingleton<DataAccessFunctions>();
		builder.Services.AddSingleton<NavigationFunctions>();
		builder.Services.AddSingleton<NotificationService>();
		builder.Services.AddSingleton<IAIFunctionProvider>(static sp => sp.GetRequiredService<DataAccessFunctions>());
		builder.Services.AddSingleton<IAIFunctionProvider>(static sp => sp.GetRequiredService<NavigationFunctions>());
		// builder.Services.AddSingleton<IAIFunctionProvider>(static sp => sp.GetRequiredService<NotificationService>());

		builder.Services.AddSingleton<ProjectRepository>();
		builder.Services.AddSingleton<TaskRepository>();
		builder.Services.AddSingleton<CategoryRepository>();
		builder.Services.AddSingleton<TagRepository>();
		builder.Services.AddSingleton<SeedDataService>();
		builder.Services.AddSingleton<ModalErrorHandler>();
		builder.Services.AddSingleton<ISemanticTreeService, SemanticTreeService>();
		builder.Services.AddSingleton<MainPageModel>();
		builder.Services.AddSingleton<ProjectListPageModel>();
		builder.Services.AddSingleton<ManageMetaPageModel>();

		builder.Services.AddTransient<MainWindow>();

		builder.Services.AddTransientWithShellRoute<ProjectDetailPage, ProjectDetailPageModel>("project");
		builder.Services.AddTransientWithShellRoute<TaskDetailPage, TaskDetailPageModel>("task");

		{
			var ai = builder.Configuration.GetSection("AI");

			var azureClient = new AzureOpenAIClient(
				new Uri(ai["Endpoint"]!),
				new AzureKeyCredential(ai["ApiKey"]!));

			var azureChatClient = azureClient.GetChatClient(ai["DeploymentName"]!);

			var chatClient = azureChatClient.AsIChatClient();

			builder.Services.AddSingleton(chatClient);
		}

		return builder.Build();
	}

	private static IConfigurationBuilder AddOptionalJsonFile(this IConfigurationBuilder builder, string path)
	{
		var assembly = Assembly.GetExecutingAssembly();

		var stream = assembly.GetManifestResourceStream("MauiIntelligenceApp." + path);
		if (stream is not null)
			return builder.AddJsonStream(stream);

		return builder;
	}
}
