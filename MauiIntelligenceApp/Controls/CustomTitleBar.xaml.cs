namespace MauiIntelligenceApp.Controls;

public partial class CustomTitleBar : ContentView
{
    public static readonly BindableProperty SearchTextProperty =
        BindableProperty.Create(nameof(SearchText), typeof(string), typeof(CustomTitleBar), string.Empty,
            BindingMode.TwoWay, propertyChanged: OnSearchTextPropertyChanged);

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public event EventHandler<string>? SearchTextChanged;
    public event EventHandler? MenuButtonClicked;
    public event EventHandler? OptionsButtonClicked;

    public CustomTitleBar()
    {
        InitializeComponent();
    }

    private static void OnSearchTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomTitleBar)bindable;
        control.SearchEntry.Text = newValue as string;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchText = e.NewTextValue;
        ClearButton.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        SearchTextChanged?.Invoke(this, e.NewTextValue);
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        SearchEntry.Text = string.Empty;
        SearchText = string.Empty;
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        MenuButtonClicked?.Invoke(this, EventArgs.Empty);
        
        // Default behavior: toggle flyout or navigate back
        if (Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
        }
    }

    private void OnOptionsButtonClicked(object sender, EventArgs e)
    {
        OptionsButtonClicked?.Invoke(this, EventArgs.Empty);
    }
}
