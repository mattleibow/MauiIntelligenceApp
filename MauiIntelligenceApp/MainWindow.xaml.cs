namespace MauiIntelligenceApp;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
	{
	}

	private void DebugSemanticTreeButton_Clicked(object sender, EventArgs e)
	{
		// Dump to debug output
		SemanticTreeDebugger.DumpSemanticTree(this);
	}
}
