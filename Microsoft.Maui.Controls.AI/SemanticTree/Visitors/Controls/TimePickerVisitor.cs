using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class TimePickerVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is TimePicker;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var timePicker = (TimePicker)element;

        WriteStartElement("TimePicker", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Time", timePicker.Time, writer);
        WriteAttribute("IsEnabled", timePicker.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
