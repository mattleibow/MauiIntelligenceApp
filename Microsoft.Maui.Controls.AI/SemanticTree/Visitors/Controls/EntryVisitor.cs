using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class EntryVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Entry;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var entry = (Entry)element;

        WriteStartElement("Entry", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", entry.Text, writer);
        WriteAttribute("Placeholder", entry.Placeholder, writer);
        WriteAttribute("IsEnabled", entry.IsEnabled, writer);
        WriteAttribute("IsReadOnly", entry.IsReadOnly, writer);
        WriteEndElement(writer);

        return true;
    }
}
