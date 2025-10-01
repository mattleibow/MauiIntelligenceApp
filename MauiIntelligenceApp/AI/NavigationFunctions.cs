using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.AI;
using Microsoft.Maui.Controls;

namespace MauiIntelligenceApp.AI;

public record RouteLineItem(string RouteTemplate, string Description);

public class NavigationFunctions(NotificationService notificationService) : IAIFunctionProvider
{
    private string? allRoutesDescriptions;
    private List<RouteLineItem>? allRoutes;

    private AIFunction? getRoutesFunction;
    private AIFunction? navigateFunction;
    private List<AIFunction>? allFunctions;

    public IReadOnlyList<AIFunction> GetFunctions() => 
        allFunctions ??=
        [
            getRoutesFunction ??= AIFunctionFactory.Create(GetRoutes),
            navigateFunction ??= AIFunctionFactory.Create(NavigateAsync)
        ];

    [Description(
        """
        Provides a list of available navigation routes in the application,
        including their templates and descriptions of their purpose.
        
        Some routes have placeholder parameters (e.g., <project-id>, <task-id>)
        that should be replaced with actual values when navigating.
        """)]
    public IReadOnlyList<RouteLineItem> GetRoutes() =>
        allRoutes ??=
        [
            // Root Navigation
            new RouteLineItem("//main", "Go to the main dashboard or home page."),
            new RouteLineItem("//projects", "View all projects."),
            // RouteLineItem("//manage", "..."),

            // Project Navigation
            new RouteLineItem("project", "Go to a page that will allow you to create a new project."),
            new RouteLineItem("project?id=<project-id>", "View and edit details about a specific project."),
            // new RouteLineItem("project?refresh=true", "Refresh the project list."),

            // Task Navigation
            new RouteLineItem("task", "Go to a page that will allow you to create a new task."),
            new RouteLineItem("task?id=<task-id>", "View and edit details about a specific task."),
            new RouteLineItem("task?project=<project-id>", "Go to a page that will allow you to create a new task in a specific project."),
        ];

    public string GetRoutesDescription()
    {
        return allRoutesDescriptions ??= BuildRouteDescriptions(GetRoutes());

        static string BuildRouteDescriptions(IReadOnlyList<RouteLineItem> routes)
        {
            var sb = new StringBuilder();

            sb.AppendLine(
                """
                These are the available routes in the application. Each route
                provides a template URI and a brief description of its purpose.

                Some routes have placeholder parameters (e.g., <project-id>, <task-id>)
                that should be replaced with actual values when navigating.

                Always preserve the template exactly as shown when specifying
                a route to navigate to, including the same number of slashes (/) as
                in the template.

                routes:
                """);
            foreach (var route in routes)
            {
                sb.AppendLine($"- template: '{route.RouteTemplate}'");
                sb.AppendLine($"  description: {route.Description}");
            }

            return sb.ToString();
        }
    }

    [Description(
        """
        Navigates to the specified route using Shell navigation.

        Provide the route in the same format as the available route templates.
        Optionally disable animation during the transition and provide a notification message
        to show after navigation completes.

        Always provide the exact route, including the same number of slashes (/) as in the template.
        """)]
    public async Task<string> NavigateAsync(
        [Description("The route to navigate to, including any query parameters.")] string route,
        [Description("Set to false to disable navigation animation.")] bool animate = true,
        [Description("Optional message to display as a toast/snackbar notification after navigation.")] string? notificationMessage = null)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            throw new ArgumentException("Route must be provided.", nameof(route));
        }

        var trimmedRoute = route.Trim();

        if (Shell.Current is not { } shell)
        {
            throw new InvalidOperationException("Navigation is unavailable because no Shell is active.");
        }

        var (success, response) = await shell.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                await shell.GoToAsync(trimmedRoute, animate);

                return (true, $"Navigated to route '{trimmedRoute}' successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Navigation to route '{trimmedRoute}' failed: {ex.Message}");
            }
        });

        if (success && !string.IsNullOrWhiteSpace(notificationMessage))
        {
            await notificationService.ShowNotificationAsync(notificationMessage);
        }

        return response;
    }
}
