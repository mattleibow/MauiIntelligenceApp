using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiIntelligenceApp.AI;
using MauiIntelligenceApp.Data;
using MauiIntelligenceApp.Models;
using MauiIntelligenceApp.Services;

namespace MauiIntelligenceApp.PageModels;

public partial class ProjectListPageModel : ObservableObject, IPageModelRepresentation
{
	string IPageModelRepresentation.Name => "Project List Page";
	string IPageModelRepresentation.Capabilities =>
		"""
		- Can display a list of projects.
		- Can navigate to project detail pages.
		- Can create new projects.
		""";
	string IPageModelRepresentation.Properties =>
		"""
		None.
		""";
	void IPageModelRepresentation.SetValue(string propertyName, object? value) { }
	object? IPageModelRepresentation.GetValue(string propertyName) => null;

	private readonly ProjectRepository _projectRepository;

	[ObservableProperty]
	private List<Project> _projects = [];

	public ProjectListPageModel(ProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}

	[RelayCommand]
	private async Task Appearing()
	{
		PageModelManipulationFunctions.CurrentPage = this;

		Projects = await _projectRepository.ListAsync();
	}

	[RelayCommand]
	Task NavigateToProject(Project project)
		=> Shell.Current.GoToAsync($"project?id={project.ID}");

	[RelayCommand]
	async Task AddProject()
	{
		await Shell.Current.GoToAsync($"project");
	}
}