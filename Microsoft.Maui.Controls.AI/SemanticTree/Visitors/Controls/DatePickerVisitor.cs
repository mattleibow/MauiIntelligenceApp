using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class DatePickerVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is DatePicker;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var datePicker = (DatePicker)element;

        WriteStartElement("DatePicker", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Date", datePicker.Date, writer);
        WriteAttribute("IsEnabled", datePicker.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
