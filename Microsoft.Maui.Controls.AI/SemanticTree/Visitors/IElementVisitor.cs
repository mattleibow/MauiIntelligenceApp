using System.Xml;

namespace Microsoft.Maui.Controls.AI;

public interface IElementVisitor
{
    bool CanVisit(Element element);
    bool Visit(Element element, XmlWriter writer, Action<Element> processChildElement);
}
