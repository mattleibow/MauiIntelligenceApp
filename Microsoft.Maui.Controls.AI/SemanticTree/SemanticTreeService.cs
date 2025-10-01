namespace Microsoft.Maui.Controls.AI;

/// <summary>
/// Implementation of ISemanticTreeService for building semantic trees from MAUI applications.
/// </summary>
public class SemanticTreeService : ISemanticTreeService
{
    private readonly SemanticTreeBuilder _builder;

    public SemanticTreeService()
    {
        _builder = new SemanticTreeBuilder();
    }

    /// <summary>
    /// Gets the semantic tree from a specific window.
    /// </summary>
    public string GetSemanticTree(Window window)
    {
        if (window == null)
            throw new ArgumentNullException(nameof(window));

        return _builder.BuildTree(window);
    }

    /// <summary>
    /// Gets the semantic tree from a specific page.
    /// </summary>
    public string GetSemanticTree(Page page)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        return _builder.BuildTree(page);
    }

    /// <summary>
    /// Gets the semantic tree from a specific element.
    /// </summary>
    public string GetSemanticTree(VisualElement element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        return _builder.BuildTree(element);
    }
}
