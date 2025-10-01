using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ButtonVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Button;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var button = (Button)element;

        WriteStartElement("Button", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", button.Text, writer);
        WriteAttribute("IsEnabled", button.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
