using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ContentViewVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is IContentView;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var contentView = (IContentView)element;

        // ContentView is a container, just process content without writing element
        if (contentView.PresentedContent is Element presented)
            processChild(presented);

        return true;
    }
}
