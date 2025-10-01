using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class LabelVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Label;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var label = (Label)element;

        WriteStartElement("Label", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", label.Text, writer);
        WriteEndElement(writer);

        return true;
    }
}
