using System.Xml;

namespace Microsoft.Maui.Controls.AI;

public abstract class NavigationElementVisitorBase : ElementVisitorBase
{
    protected void VisitMenuItem(MenuItem menuItem, XmlWriter writer)
    {
        WriteStartElement("MenuItem", writer);
        WriteAttribute("Text", menuItem.Text, writer);
        WriteAttribute("Icon", menuItem.IconImageSource, writer);
        WriteAttribute("IsEnabled", menuItem.IsEnabled, writer);
        WriteAttribute("AutomationId", menuItem.AutomationId, writer);
        WriteAttribute("ClassId", menuItem.ClassId, writer);
        WriteEndElement(writer);
    }

    protected void VisitToolbarItem(ToolbarItem toolbarItem, XmlWriter writer)
    {
        WriteStartElement("ToolbarItem", writer);
        WriteAttribute("Text", toolbarItem.Text, writer);
        WriteAttribute("Icon", toolbarItem.IconImageSource, writer);
        WriteAttribute("IsEnabled", toolbarItem.IsEnabled, writer);
        WriteAttribute("Order", toolbarItem.Order, writer);
        WriteAttribute("Priority", toolbarItem.Priority, writer);
        WriteAttribute("AutomationId", toolbarItem.AutomationId, writer);
        WriteAttribute("ClassId", toolbarItem.ClassId, writer);
        WriteEndElement(writer);
    }
}
