using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class SwitchVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Switch;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var switchControl = (Switch)element;

        WriteStartElement("Switch", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("IsToggled", switchControl.IsToggled, writer);
        WriteAttribute("IsEnabled", switchControl.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
