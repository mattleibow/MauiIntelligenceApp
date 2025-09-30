namespace MauiIntelligenceApp.Messages;

public class SearchMessage
{
    public string SearchText { get; }

    public SearchMessage(string searchText)
    {
        SearchText = searchText;
    }
}
