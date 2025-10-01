using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class StepperVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Stepper;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var stepper = (Stepper)element;

        WriteStartElement("Stepper", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Value", stepper.Value, writer);
        WriteAttribute("Minimum", stepper.Minimum, writer);
        WriteAttribute("Maximum", stepper.Maximum, writer);
        WriteAttribute("IsEnabled", stepper.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
