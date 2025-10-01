using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ContentPageVisitor : NavigationElementVisitorBase
{
    public override bool CanVisit(Element element) => element is ContentPage;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var contentPage = (ContentPage)element;

        WriteStartElement("Page", writer);
        WriteCommonAttributes(contentPage, writer);
        WriteAttribute("Title", contentPage.Title, writer);

        // Process ToolbarItems
        foreach (var toolbarItem in contentPage.ToolbarItems)
        {
            VisitToolbarItem(toolbarItem, writer);
        }

        if (contentPage.Content != null)
            processChild(contentPage.Content);

        WriteEndElement(writer);
        return true;
    }
}
