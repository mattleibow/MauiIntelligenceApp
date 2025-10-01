// using System.Diagnostics;
// using System.Xml.Linq;

// namespace MauiIntelligenceApp.Services;

// /// <summary>
// /// Example demonstrating how an AI agent would interpret the semantic tree.
// /// This class shows how to parse and understand the XML semantic tree structure.
// /// </summary>
// public class AIAgentExample
// {
//     private readonly ISemanticTreeService _SemanticTreeService;

//     public AIAgentExample(ISemanticTreeService SemanticTreeService)
//     {
//         _SemanticTreeService = SemanticTreeService;
//     }

//     /// <summary>
//     /// Example: AI agent interprets user request to add a task.
//     /// </summary>
//     public string InterpretAddTaskRequest()
//     {
//         var SemanticTree = _SemanticTreeService.GetCurrentSemanticTree();
//         var doc = XDocument.Parse(SemanticTree);

//         // Look for "Add" buttons or similar controls
//         var addButtons = doc.Descendants("Button")
//             .Where(b => 
//                 b.Attribute("SemanticDescription")?.Value.Contains("Add", StringComparison.OrdinalIgnoreCase) == true ||
//                 b.Attribute("Text")?.Value.Contains("Add", StringComparison.OrdinalIgnoreCase) == true)
//             .ToList();

//         if (addButtons.Any())
//         {
//             var button = addButtons.First();
//             return $"Found 'Add' button. AI would invoke this button to add a task.\n" +
//                    $"Button details: {button}";
//         }

//         return "No 'Add' button found in current UI.";
//     }

//     /// <summary>
//     /// Example: AI agent finds navigation to projects page.
//     /// </summary>
//     public string InterpretViewProjectsRequest()
//     {
//         var SemanticTree = _SemanticTreeService.GetCurrentSemanticTree();
//         var doc = XDocument.Parse(SemanticTree);

//         // Look for flyout items or navigation items related to projects
//         var projectNavigation = doc.Descendants("FlyoutItem")
//             .Where(f => 
//                 f.Attribute("Title")?.Value.Contains("Project", StringComparison.OrdinalIgnoreCase) == true)
//             .ToList();

//         if (projectNavigation.Any())
//         {
//             var navItem = projectNavigation.First();
//             var route = navItem.Attribute("Route")?.Value;
//             var title = navItem.Attribute("Title")?.Value;
            
//             return $"Found navigation to Projects page.\n" +
//                    $"Title: {title}\n" +
//                    $"Route: {route}\n" +
//                    $"AI would navigate to: {route}";
//         }

//         return "No Projects navigation found in current UI.";
//     }

//     /// <summary>
//     /// Example: AI agent gets all available navigation options.
//     /// </summary>
//     public List<NavigationOption> GetAvailableNavigation()
//     {
//         var SemanticTree = _SemanticTreeService.GetCurrentSemanticTree();
//         var doc = XDocument.Parse(SemanticTree);

//         var navigationOptions = new List<NavigationOption>();

//         // Get all flyout items
//         var flyoutItems = doc.Descendants("FlyoutItem");
//         foreach (var item in flyoutItems)
//         {
//             navigationOptions.Add(new NavigationOption
//             {
//                 Type = "Flyout",
//                 Title = item.Attribute("Title")?.Value ?? "Unknown",
//                 Route = item.Attribute("Route")?.Value,
//                 Description = item.Attribute("SemanticDescription")?.Value
//             });
//         }

//         // Get toolbar items
//         var toolbarItems = doc.Descendants("ToolbarItem");
//         foreach (var item in toolbarItems)
//         {
//             navigationOptions.Add(new NavigationOption
//             {
//                 Type = "Toolbar",
//                 Title = item.Attribute("Text")?.Value ?? "Unknown",
//                 Description = item.Attribute("SemanticDescription")?.Value
//             });
//         }

//         return navigationOptions;
//     }

//     /// <summary>
//     /// Example: AI agent gets all actionable items on current page.
//     /// </summary>
//     public List<ActionableItem> GetAvailableActions()
//     {
//         var SemanticTree = _SemanticTreeService.GetCurrentSemanticTree();
//         var doc = XDocument.Parse(SemanticTree);

//         var actions = new List<ActionableItem>();

//         // Get current page
//         var currentPage = doc.Descendants("CurrentPage").FirstOrDefault()
//                          ?? doc.Descendants("Page").FirstOrDefault();

//         if (currentPage == null)
//             return actions;

//         // Find all buttons
//         foreach (var button in currentPage.Descendants("Button"))
//         {
//             actions.Add(new ActionableItem
//             {
//                 Type = "Button",
//                 Text = button.Attribute("Text")?.Value,
//                 Description = button.Attribute("SemanticDescription")?.Value,
//                 Hint = button.Attribute("SemanticHint")?.Value,
//                 IsEnabled = button.Attribute("IsEnabled")?.Value != "False"
//             });
//         }

//         // Find all entries
//         foreach (var entry in currentPage.Descendants("Entry"))
//         {
//             actions.Add(new ActionableItem
//             {
//                 Type = "Entry",
//                 Text = entry.Attribute("Text")?.Value,
//                 Placeholder = entry.Attribute("Placeholder")?.Value,
//                 Description = entry.Attribute("SemanticDescription")?.Value,
//                 Hint = entry.Attribute("SemanticHint")?.Value,
//                 IsEnabled = entry.Attribute("IsEnabled")?.Value != "False"
//             });
//         }

//         // Find all checkboxes
//         foreach (var checkbox in currentPage.Descendants("CheckBox"))
//         {
//             actions.Add(new ActionableItem
//             {
//                 Type = "CheckBox",
//                 IsChecked = checkbox.Attribute("IsChecked")?.Value == "True",
//                 Description = checkbox.Attribute("SemanticDescription")?.Value,
//                 Hint = checkbox.Attribute("SemanticHint")?.Value,
//                 IsEnabled = checkbox.Attribute("IsEnabled")?.Value != "False"
//             });
//         }

//         // Find all pickers
//         foreach (var picker in currentPage.Descendants("Picker"))
//         {
//             var items = picker.Element("Items")?.Elements("Item")
//                 .Select(i => i.Value)
//                 .ToList();

//             actions.Add(new ActionableItem
//             {
//                 Type = "Picker",
//                 Text = picker.Attribute("SelectedItem")?.Value,
//                 Description = picker.Attribute("SemanticDescription")?.Value,
//                 AvailableOptions = items
//             });
//         }

//         return actions;
//     }

//     /// <summary>
//     /// Example: AI agent analyzes current page context.
//     /// </summary>
//     public PageContext GetCurrentPageContext()
//     {
//         var SemanticTree = _SemanticTreeService.GetCurrentSemanticTree();
//         var doc = XDocument.Parse(SemanticTree);

//         var currentPage = doc.Descendants("CurrentPage").Elements("Page").FirstOrDefault()
//                          ?? doc.Descendants("Page").FirstOrDefault();

//         if (currentPage == null)
//             return new PageContext { PageTitle = "Unknown" };

//         var context = new PageContext
//         {
//             PageTitle = currentPage.Attribute("Title")?.Value ?? "Unknown",
//             AvailableActions = GetAvailableActions(),
//             AvailableNavigation = GetAvailableNavigation()
//         };

//         // Determine page capabilities
//         context.CanAddItems = context.AvailableActions.Any(a => 
//             a.Type == "Button" && 
//             (a.Description?.Contains("Add", StringComparison.OrdinalIgnoreCase) == true ||
//              a.Text?.Contains("Add", StringComparison.OrdinalIgnoreCase) == true));

//         context.CanEditItems = context.AvailableActions.Any(a => a.Type == "Entry");
//         context.CanDeleteItems = context.AvailableActions.Any(a => 
//             a.Type == "Button" && 
//             (a.Description?.Contains("Delete", StringComparison.OrdinalIgnoreCase) == true ||
//              a.Text?.Contains("Delete", StringComparison.OrdinalIgnoreCase) == true));

//         context.HasSearchBar = doc.Descendants("SearchBar").Any();

//         return context;
//     }

//     /// <summary>
//     /// Demonstrates the AI agent interpretation workflow.
//     /// </summary>
//     public void DemonstrateAIInterpretation()
//     {
//         Debug.WriteLine("=== AI Agent Semantic Tree Interpretation Demo ===\n");

//         // Get current context
//         var context = GetCurrentPageContext();
//         Debug.WriteLine($"Current Page: {context.PageTitle}");
//         Debug.WriteLine($"Can Add Items: {context.CanAddItems}");
//         Debug.WriteLine($"Can Edit Items: {context.CanEditItems}");
//         Debug.WriteLine($"Can Delete Items: {context.CanDeleteItems}");
//         Debug.WriteLine($"Has Search: {context.HasSearchBar}\n");

//         // List available navigation
//         Debug.WriteLine("Available Navigation:");
//         foreach (var nav in context.AvailableNavigation)
//         {
//             Debug.WriteLine($"  - {nav.Type}: {nav.Title} (Route: {nav.Route})");
//         }
//         Debug.WriteLine();

//         // List available actions
//         Debug.WriteLine("Available Actions on Current Page:");
//         foreach (var action in context.AvailableActions)
//         {
//             var details = $"  - {action.Type}";
//             if (!string.IsNullOrEmpty(action.Text))
//                 details += $" (Text: {action.Text})";
//             if (!string.IsNullOrEmpty(action.Description))
//                 details += $" [Description: {action.Description}]";
//             Debug.WriteLine(details);
//         }
//         Debug.WriteLine();

//         // Test specific requests
//         Debug.WriteLine("=== Testing Specific User Requests ===\n");
        
//         Debug.WriteLine("User Request: 'Add a task'");
//         Debug.WriteLine(InterpretAddTaskRequest());
//         Debug.WriteLine();

//         Debug.WriteLine("User Request: 'View projects'");
//         Debug.WriteLine(InterpretViewProjectsRequest());
//         Debug.WriteLine();

//         Debug.WriteLine("=== End Demo ===");
//     }
// }

// /// <summary>
// /// Represents a navigation option available in the UI.
// /// </summary>
// public class NavigationOption
// {
//     public string Type { get; set; } = string.Empty;
//     public string Title { get; set; } = string.Empty;
//     public string? Route { get; set; }
//     public string? Description { get; set; }
// }

// /// <summary>
// /// Represents an actionable item in the UI.
// /// </summary>
// public class ActionableItem
// {
//     public string Type { get; set; } = string.Empty;
//     public string? Text { get; set; }
//     public string? Placeholder { get; set; }
//     public string? Description { get; set; }
//     public string? Hint { get; set; }
//     public bool IsEnabled { get; set; } = true;
//     public bool IsChecked { get; set; }
//     public List<string>? AvailableOptions { get; set; }
// }

// /// <summary>
// /// Represents the context of the current page.
// /// </summary>
// public class PageContext
// {
//     public string PageTitle { get; set; } = string.Empty;
//     public List<ActionableItem> AvailableActions { get; set; } = new();
//     public List<NavigationOption> AvailableNavigation { get; set; } = new();
//     public bool CanAddItems { get; set; }
//     public bool CanEditItems { get; set; }
//     public bool CanDeleteItems { get; set; }
//     public bool HasSearchBar { get; set; }
// }
