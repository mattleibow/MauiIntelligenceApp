namespace MauiIntelligenceApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState) =>
		activationState?.Context.Services.GetRequiredService<MainWindow>() ??
		throw new InvalidOperationException("MainWindow not registered in DI container.");
}