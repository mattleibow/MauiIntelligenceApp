using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class NavigationPageVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is NavigationPage;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var navigationPage = (NavigationPage)element;

        WriteStartElement("NavigationPage", writer);
        WriteCommonAttributes(navigationPage, writer);
        WriteAttribute("Title", navigationPage.Title, writer);

        if (navigationPage.CurrentPage != null)
            processChild(navigationPage.CurrentPage);

        WriteEndElement(writer);
        return true;
    }
}
