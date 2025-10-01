using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class CheckBoxVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is CheckBox;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var checkBox = (CheckBox)element;

        WriteStartElement("CheckBox", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("IsChecked", checkBox.IsChecked, writer);
        WriteAttribute("IsEnabled", checkBox.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
