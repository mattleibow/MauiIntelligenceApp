using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ImageVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Image;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var image = (Image)element;

        WriteStartElement("Image", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Source", image.Source, writer);
        WriteAttribute("Aspect", image.Aspect, writer);
        WriteAttribute("IsOpaque", image.IsOpaque, writer);
        WriteEndElement(writer);

        return true;
    }
}
