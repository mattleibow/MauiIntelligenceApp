using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class EditorVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Editor;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var editor = (Editor)element;

        WriteStartElement("Editor", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", editor.Text, writer);
        WriteAttribute("Placeholder", editor.Placeholder, writer);
        WriteAttribute("IsEnabled", editor.IsEnabled, writer);
        WriteAttribute("IsReadOnly", editor.IsReadOnly, writer);
        WriteEndElement(writer);

        return true;
    }
}
