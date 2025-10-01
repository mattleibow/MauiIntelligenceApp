using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class WindowVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Window;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var window = (Window)element;

        WriteStartElement("Window", writer);
        WriteCommonAttributes(window, writer);

        if (window.Page != null)
            processChild(window.Page);

        WriteEndElement(writer);
        return true;
    }
}
