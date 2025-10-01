using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class FlyoutPageVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is FlyoutPage;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var flyoutPage = (FlyoutPage)element;

        WriteStartElement("FlyoutPage", writer);
        WritePageAttributes(flyoutPage, writer);

        WriteStartElement("Flyout", writer);
        if (flyoutPage.Flyout != null)
            processChild(flyoutPage.Flyout);
        WriteEndElement(writer);

        WriteStartElement("Detail", writer);
        if (flyoutPage.Detail != null)
            processChild(flyoutPage.Detail);
        WriteEndElement(writer);

        WriteEndElement(writer);
        return true;
    }

    private void WritePageAttributes(Page page, XmlWriter writer)
    {
        WriteCommonAttributes(page, writer);
        WriteAttribute("Title", page.Title, writer);
    }
}
