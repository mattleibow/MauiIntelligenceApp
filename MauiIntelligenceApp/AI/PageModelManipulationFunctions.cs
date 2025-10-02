using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Extensions.AI;

namespace MauiIntelligenceApp.AI;

public class PageModelManipulationFunctions : IAIFunctionProvider
{
    private AIFunction? getValueFunction;
    private AIFunction? setValueFunction;
    private List<AIFunction>? allFunctions;
    private static IPageModelRepresentation? currentPage;

    public IReadOnlyList<AIFunction> GetFunctions() =>
        allFunctions ??=
        [
            getValueFunction ??= AIFunctionFactory.Create(GetPagePropertyValue),
            setValueFunction ??= AIFunctionFactory.Create(SetPagePropertyValue)
        ];

    public static IPageModelRepresentation? CurrentPage
    {
        get => currentPage;
        set
        {
            currentPage = value;
            Debug.WriteLine($"Current page set to: {currentPage?.Name ?? currentPage?.GetType().Name ?? "null"}");
        }
    }

    [Description(
        """
        Fetches the values of a specified property from the current page.

        This function can be used to retrieve data from the page model
        and is typically called from within the context of a page.

        This is useful for understanding the current state of the page
        and making decisions based on its properties.
        """)]
    public object? GetPagePropertyValue(
        [Description("The name of the property to fetch.")] string propertyName)
    {
        return CurrentPage?.GetValue(propertyName);
    }

    [Description(
        """
        Sets the value of a specified property on the current page.

        This function can be used to modify the state of the page model
        and is typically called from within the context of a page.

        This is useful for updating the page's properties in response to
        user actions or other events.
        """)]
    public void SetPagePropertyValue(
        [Description("The name of the property to set.")] string propertyName,
        [Description("The value to set for the property.")] object? value)
    {
        CurrentPage?.SetValue(propertyName, value);
    }
}
