using System.Text;
using System.Xml;

namespace Microsoft.Maui.Controls.AI;

public class SemanticTreeBuilder
{
    private readonly List<IElementVisitor> _visitors =
    [
        // Syncfusion visitors
        new SfSegmentedControlVisitor(),
        new SfSegmentItemVisitor(),

        // Register interactable control visitors first (most specific)
        new ButtonVisitor(),
        new EntryVisitor(),
        new EditorVisitor(),
        new PickerVisitor(),
        new DatePickerVisitor(),
        new TimePickerVisitor(),
        new StepperVisitor(),
        new SliderVisitor(),
        new SwitchVisitor(),
        new CheckBoxVisitor(),
        new RadioButtonVisitor(),
        new SearchBarVisitor(),
        new ImageButtonVisitor(),
        new ImageVisitor(),
        new LabelVisitor(),

        // Register container visitors
        new SwipeViewVisitor(),
        new RefreshViewVisitor(),
        new ScrollViewVisitor(),
        new BorderVisitor(),
        new ContentViewVisitor(),
        new LayoutVisitor(),
        
        // Register navigation visitors
        new ShellVisitor(),
        new WindowVisitor(),
        new FlyoutPageVisitor(),
        new TabbedPageVisitor(),
        new NavigationPageVisitor(),
        new ContentPageVisitor(),
    ];

    public void RegisterVisitor(IElementVisitor visitor)
    {
        _visitors.Insert(0, visitor);
    }

    public string BuildTree(Element rootElement)
    {
        if (rootElement == null)
            return "<!-- No element provided -->";

        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            OmitXmlDeclaration = false,
            Encoding = Encoding.UTF8
        };

        using var stringWriter = new StringWriter();
        using var writer = XmlWriter.Create(stringWriter, settings);

        writer.WriteStartDocument();

        ProcessElement(rootElement, writer);

        writer.WriteEndDocument();
        writer.Flush();

        return stringWriter.ToString();
    }

    private void ProcessElement(Element element, XmlWriter writer)
    {
        if (element == null) return;

        var visitor = _visitors.FirstOrDefault(v => v.CanVisit(element));

        visitor?.Visit(element, writer, e => ProcessElement(e, writer));

        // If no visitor found, element is not interactable or navigational, so we skip it but log it
        
        if (visitor == null)
        {
            System.Diagnostics.Debug.WriteLine($"[SemanticTreeBuilder] Skipping element of type {element.GetType().FullName} - no visitor registered.");
        }
    }
}
