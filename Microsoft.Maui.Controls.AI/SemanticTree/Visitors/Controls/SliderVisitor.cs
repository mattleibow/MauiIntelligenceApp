using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class SliderVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Slider;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var slider = (Slider)element;

        WriteStartElement("Slider", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Value", slider.Value.ToString("F2"), writer);
        WriteAttribute("Minimum", slider.Minimum, writer);
        WriteAttribute("Maximum", slider.Maximum, writer);
        WriteAttribute("IsEnabled", slider.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
