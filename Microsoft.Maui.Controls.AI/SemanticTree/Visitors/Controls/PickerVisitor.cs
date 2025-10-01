using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class PickerVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Picker;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var picker = (Picker)element;

        WriteStartElement("Picker", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Title", picker.Title, writer);
        WriteAttribute("SelectedIndex", picker.SelectedIndex, writer);
        WriteAttribute("SelectedItem", picker.SelectedItem, writer);
        WriteAttribute("IsEnabled", picker.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
