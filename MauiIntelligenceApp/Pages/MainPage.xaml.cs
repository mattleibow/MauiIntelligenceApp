using MauiIntelligenceApp.Models;
using MauiIntelligenceApp.PageModels;

namespace MauiIntelligenceApp.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}