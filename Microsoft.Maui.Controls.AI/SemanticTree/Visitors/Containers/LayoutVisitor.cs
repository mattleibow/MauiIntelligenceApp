using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class LayoutVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Microsoft.Maui.ILayout;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var layout = (Microsoft.Maui.ILayout)element;

        // Layout is a container, just process children without writing element
        foreach (var child in layout)
        {
            if (child is Element childElement)
                processChild(childElement);
        }

        return true;
    }
}
