using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class BorderVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Border;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var border = (Border)element;

        WriteStartElement("Border", writer);
        WriteCommonAttributes(border, writer);

        if (border.Content != null)
            processChild(border.Content);

        WriteEndElement(writer);

        return true;
    }
}
