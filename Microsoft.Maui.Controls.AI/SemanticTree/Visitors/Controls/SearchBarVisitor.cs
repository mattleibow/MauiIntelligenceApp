using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class SearchBarVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is SearchBar;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var searchBar = (SearchBar)element;

        WriteStartElement("SearchBar", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", searchBar.Text, writer);
        WriteAttribute("Placeholder", searchBar.Placeholder, writer);
        WriteAttribute("IsEnabled", searchBar.IsEnabled, writer);
        WriteEndElement(writer);

        return true;
    }
}
