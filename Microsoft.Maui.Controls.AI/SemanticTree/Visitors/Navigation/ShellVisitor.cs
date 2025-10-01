using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

public class ShellVisitor : NavigationElementVisitorBase
{
    public override bool CanVisit(Element element) => element is Shell;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var shell = (Shell)element;

        WriteStartElement("Shell", writer);
        WriteCommonAttributes(shell, writer);

        // Process Shell FlyoutHeader
        if (shell.FlyoutHeader is Element headerElement)
        {
            WriteStartElement("FlyoutHeader", writer);
            processChild(headerElement);
            WriteEndElement(writer);
        }

        // Process Shell MenuItems
        WriteStartElement("MenuItems", writer);
        var shellMenuItems = shell.GetType()
            .GetProperty("MenuItems")?
            .GetValue(shell) as IEnumerable<MenuItem>;

        if (shellMenuItems != null)
        {
            foreach (var menuItem in shellMenuItems)
            {
                VisitMenuItem(menuItem, writer);
            }
        }
        WriteEndElement(writer);

        // Process Shell items
        WriteStartElement("ShellItems", writer);
        foreach (var item in shell.Items)
        {
            if (item is TabBar tabBar)
                VisitTabBar(tabBar, writer, processChild);
            else if (item is FlyoutItem flyoutItem)
                VisitFlyoutItem(flyoutItem, writer, processChild);
            else
                VisitShellItem(item, writer, processChild);
        }
        WriteEndElement(writer);


        // Process Shell FlyoutFooter
        if (shell.FlyoutFooter is Element footerElement)
        {
            WriteStartElement("FlyoutFooter", writer);
            processChild(footerElement);
            WriteEndElement(writer);
        }

        // Process CurrentPage
        WriteStartElement("CurrentPage", writer);
        if (shell.CurrentPage != null)
        {
            processChild(shell.CurrentPage);
        }
        WriteEndElement(writer);

        return true;
    }

    protected void VisitTabBar(TabBar tabBar, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("TabBar", writer);
        WriteAttribute("Route", tabBar.Route, writer);

        foreach (var item in tabBar.Items)
        {
            if (item is Tab tab)
                VisitTab(tab, writer, processChild);
            else if (item is ShellSection section)
                VisitShellSection(section, writer, processChild);
        }

        WriteEndElement(writer);
    }

    protected void VisitTab(Tab tab, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("Tab", writer);
        WriteAttribute("Title", tab.Title, writer);
        WriteAttribute("Route", tab.Route, writer);
        WriteAttribute("Icon", tab.Icon, writer);

        // Indicate if this is the current tab
        if (tab.Parent is TabBar tabBar && tabBar.Parent is Shell shell)
        {
            var isCurrentTab = ReferenceEquals(shell.CurrentItem, tab) ||
                              ReferenceEquals(shell.CurrentItem, tabBar);
            WriteAttribute("IsCurrent", isCurrentTab, writer);
        }
        else if (tab.Parent is FlyoutItem flyoutItem && flyoutItem.Parent is Shell shellFromFlyout)
        {
            var isCurrentTab = ReferenceEquals(shellFromFlyout.CurrentItem, flyoutItem) &&
                              ReferenceEquals(flyoutItem.CurrentItem, tab);
            WriteAttribute("IsCurrent", isCurrentTab, writer);
        }

        foreach (var content in tab.Items)
        {
            VisitShellContent(content, writer, processChild);
        }

        WriteEndElement(writer);
    }

    protected void VisitFlyoutItem(FlyoutItem flyoutItem, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("FlyoutItem", writer);
        WriteAttribute("Title", flyoutItem.Title, writer);
        WriteAttribute("Route", flyoutItem.Route, writer);
        WriteAttribute("Icon", flyoutItem.Icon, writer);

        if (flyoutItem.Parent is Shell shell)
        {
            var isCurrentItem = shell.CurrentItem == flyoutItem;
            WriteAttribute("IsCurrent", isCurrentItem, writer);
        }

        foreach (var section in flyoutItem.Items)
        {
            if (section is Tab tab)
                VisitTab(tab, writer, processChild);
            else
                VisitShellSection(section, writer, processChild);
        }

        WriteEndElement(writer);
    }

    protected void VisitShellItem(ShellItem shellItem, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("ShellItem", writer);
        WriteAttribute("Title", shellItem.Title, writer);
        WriteAttribute("Route", shellItem.Route, writer);

        foreach (var section in shellItem.Items)
            VisitShellSection(section, writer, processChild);

        WriteEndElement(writer);
    }

    protected void VisitShellSection(ShellSection section, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("ShellSection", writer);
        WriteAttribute("Title", section.Title, writer);
        WriteAttribute("Route", section.Route, writer);

        foreach (var content in section.Items)
            VisitShellContent(content, writer, processChild);

        WriteEndElement(writer);
    }

    protected void VisitShellContent(ShellContent content, XmlWriter writer, Action<Element> processChild)
    {
        WriteStartElement("ShellContent", writer);
        WriteAttribute("Title", content.Title, writer);
        WriteAttribute("Route", content.Route, writer);
        WriteAttribute("Icon", content.Icon, writer);

        // Indicate if this is the current content
        var parent = content.Parent;
        if (parent != null)
        {
            Element current = parent;
            Shell? shell = null;
            while (current != null)
            {
                if (current is Shell s)
                {
                    shell = s;
                    break;
                }
                current = current.Parent;
            }

            if (shell != null)
            {
                var isCurrentContent =
                    ReferenceEquals(shell.CurrentItem, content) ||
                    (parent is ShellSection section && ReferenceEquals(shell.CurrentItem, section) && ReferenceEquals(section.CurrentItem, content)) ||
                    (parent is Tab tab && ReferenceEquals(shell.CurrentItem, tab) && ReferenceEquals(tab.CurrentItem, content));
                WriteAttribute("IsCurrent", isCurrentContent, writer);
            }
        }

        // Process ShellContent MenuItems
        foreach (var menuItem in content.MenuItems)
        {
            VisitMenuItem(menuItem, writer);
        }

        if (content.Content is Element element)
            processChild(element);

        WriteEndElement(writer);
    }
}
