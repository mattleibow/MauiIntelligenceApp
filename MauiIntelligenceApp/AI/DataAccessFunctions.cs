using System.ComponentModel;
using System.Text;
using MauiIntelligenceApp.Data;
using MauiIntelligenceApp.Models;
using Microsoft.Extensions.AI;

namespace MauiIntelligenceApp.AI;

public record DataLineItem(int Id, string Name, string Description = "");

public class DataAccessFunctions(
    CategoryRepository categories,
    ProjectRepository projects,
    TaskRepository tasks,
    TagRepository tags) : IAIFunctionProvider
{
    private AIFunction? getProjectsFunction;
    private AIFunction? getTasksFunction;
    private List<AIFunction>? allFunctions;

    public IReadOnlyList<AIFunction> GetFunctions() =>
        allFunctions ??=
        [
            getProjectsFunction ??= AIFunctionFactory.Create(GetProjectsAsync),
            getTasksFunction ??= AIFunctionFactory.Create(GetTasksAsync)
        ];

    [Description(
        """
        Lists all projects with their ID, Name, and Description, optionally filtered by a project ID.

        If a project ID is provided, returns the specific project.
        If no project ID is provided, returns all projects.
        """)]
    public async Task<IList<DataLineItem>> GetProjectsAsync(
        [Description("The optional project ID to filter by.")] int? projectId = null)
    {
        if (projectId is int projectIdValue)
        {
            var item = await projects.GetAsync(projectIdValue);
            return item is null ? [] : [new DataLineItem(item.ID, item.Name, item.Description)];
        }

        var items = await projects.ListAsync();
        return [.. items.Select(p => new DataLineItem(p.ID, p.Name, p.Description))];
    }

    [Description(
        """
        Lists all tasks with their ID and Title, optionally filtered by a task ID or a project ID.

        If a task ID is provided, returns the specific task.
        If a project ID is provided, returns all tasks associated with that project.
        If neither is provided, returns all tasks.
        """)]
    public async Task<IList<DataLineItem>> GetTasksAsync(
        [Description("The optional task ID to filter by.")] int? taskId = null,
        [Description("The optional project ID to filter tasks by.")] int? projectId = null)
    {
        if (taskId is int taskIdValue)
        {
            var item = await tasks.GetAsync(taskIdValue);
            return item is null ? [] : [new DataLineItem(item.ID, item.Title)];
        }

        if (projectId is int projectIdValue)
        {
            var projectItems = await tasks.ListAsync(projectIdValue);
            return [.. projectItems.Select(p => new DataLineItem(p.ID, p.Title))];
        }

        var items = await tasks.ListAsync();
        return [.. items.Select(p => new DataLineItem(p.ID, p.Title))];
    }
}
