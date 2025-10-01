using System.Xml;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Controls.AI;

/// <summary>
/// Base visitor class that provides common functionality for element visitors.
/// </summary>
public abstract class ElementVisitorBase : IElementVisitor
{
    public abstract bool CanVisit(Element element);

    public abstract bool Visit(Element element, XmlWriter writer, Action<Element> processChild);

    protected void WriteStartElement(string name, XmlWriter writer)
    {
        writer.WriteStartElement(name);
    }

    protected void WriteEndElement(XmlWriter writer)
    {
        writer.WriteEndElement();
    }

    protected void WriteAttribute(string name, string value, XmlWriter writer)
    {
        if (!string.IsNullOrWhiteSpace(value))
            writer.WriteAttributeString(name, value);
    }

    protected void WriteAttribute(string name, object? value, XmlWriter writer)
    {
        if (value != null)
            writer.WriteAttributeString(name, value.ToString() ?? "");
    }

    /// <summary>
    /// Writes common attributes for all elements (AutomationId, ClassId, Semantic properties).
    /// </summary>
    protected void WriteCommonAttributes(Element element, XmlWriter writer)
    {
        // AutomationId
        WriteAttribute("AutomationId", element.AutomationId, writer);

        // ClassId
        WriteAttribute("ClassId", element.ClassId, writer);

        // Semantic Properties
        if (element is BindableObject obj)
        {
            var semanticDesc = SemanticProperties.GetDescription(obj);
            WriteAttribute("SemanticDescription", semanticDesc, writer);

            var semanticHint = SemanticProperties.GetHint(obj);
            WriteAttribute("SemanticHint", semanticHint, writer);

            var headingLevel = SemanticProperties.GetHeadingLevel(obj);
            if (headingLevel != SemanticHeadingLevel.None)
                WriteAttribute("HeadingLevel", headingLevel.ToString(), writer);
        }
    }
}
