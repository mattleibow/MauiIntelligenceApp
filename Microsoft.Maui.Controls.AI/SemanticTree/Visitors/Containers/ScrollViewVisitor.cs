using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ScrollViewVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is ScrollView;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var scrollView = (ScrollView)element;

        WriteStartElement("ScrollView", writer);
        WriteCommonAttributes(scrollView, writer);

        if (scrollView.Content != null)
            processChild(scrollView.Content);

        WriteEndElement(writer);

        return true;
    }
}
