using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class RefreshViewVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is RefreshView;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var refreshView = (RefreshView)element;

        WriteStartElement("RefreshView", writer);
        WriteCommonAttributes(refreshView, writer);

        if (refreshView.Content != null)
            processChild(refreshView.Content);

        WriteEndElement(writer);
        return true;
    }
}
