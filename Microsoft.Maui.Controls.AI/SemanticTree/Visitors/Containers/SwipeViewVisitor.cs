using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class SwipeViewVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is SwipeView;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var swipeView = (SwipeView)element;

        WriteStartElement("SwipeView", writer);
        WriteCommonAttributes(swipeView, writer);

        if (swipeView.Content != null)
            processChild(swipeView.Content);

        WriteEndElement(writer);
        return true;
    }
}
