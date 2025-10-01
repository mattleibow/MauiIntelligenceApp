namespace Microsoft.Maui.Controls.AI;

/// <summary>
/// Service for building semantic trees from MAUI elements.
/// </summary>
public interface ISemanticTreeService
{
    /// <summary>
    /// Gets the semantic tree from a specific window.
    /// </summary>
    string GetSemanticTree(Window window);

    /// <summary>
    /// Gets the semantic tree from a specific page.
    /// </summary>
    string GetSemanticTree(Page page);

    /// <summary>
    /// Gets the semantic tree from a specific element.
    /// </summary>
    string GetSemanticTree(VisualElement element);
}
