using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ImageButtonVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is ImageButton;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var imageButton = (ImageButton)element;

        WriteStartElement("ImageButton", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Source", imageButton.Source, writer);
        WriteAttribute("IsEnabled", imageButton.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
