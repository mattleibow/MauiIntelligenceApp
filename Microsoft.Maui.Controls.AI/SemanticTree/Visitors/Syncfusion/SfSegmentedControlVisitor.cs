using System.Collections;
using System.Xml;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Toolkit.SegmentedControl;

namespace Microsoft.Maui.Controls.AI;

public class SfSegmentedControlVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is SfSegmentedControl;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var segmentedControl = (SfSegmentedControl)element;

        WriteStartElement("SegmentedControl", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("SelectedIndex", segmentedControl.SelectedIndex, writer);
        WriteAttribute("IsEnabled", segmentedControl.IsEnabled, writer);

        if (segmentedControl.ItemsSource is IEnumerable items)
        {
            foreach (var item in items.OfType<SfSegmentItem>())
            {
                processChild(item);
            }
        }

        WriteEndElement(writer);

        return true;
    }
}

public class SfSegmentItemVisitor : ElementVisitorBase
{
    public override bool CanVisit(Element element) => element is SfSegmentItem;

    public override bool Visit(Element element, XmlWriter writer, Action<Element> processChild)
    {
        var segmentItem = (SfSegmentItem)element;

        WriteStartElement("SegmentItem", writer);
        WriteCommonAttributes(element, writer);
        WriteAttribute("Text", segmentItem.Text, writer);
        WriteAttribute("IsEnabled", segmentItem.IsEnabled, writer);
        WriteAttribute("ImageSource", segmentItem.ImageSource, writer);
        WriteEndElement(writer);

        return true;
    }
}
