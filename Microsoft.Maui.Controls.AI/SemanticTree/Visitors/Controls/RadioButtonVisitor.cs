using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class RadioButtonVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is RadioButton;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var radioButton = (RadioButton)element;

        WriteStartElement("RadioButton", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Content", radioButton.Content, writer);
        WriteAttribute("IsChecked", radioButton.IsChecked, writer);
        WriteAttribute("IsEnabled", radioButton.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
