using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class TabbedPageVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is TabbedPage;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var tabbedPage = (TabbedPage)element;

        WriteStartElement("TabbedPage", writer);
        WriteCommonAttributes(tabbedPage, writer);
        WriteAttribute("Title", tabbedPage.Title, writer);

        foreach (var page in tabbedPage.Children)
            processChild(page);

        WriteEndElement(writer);
        return true;
    }
}
