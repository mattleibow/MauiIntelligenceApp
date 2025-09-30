using CommunityToolkit.Mvvm.Input;
using MauiIntelligenceApp.Models;

namespace MauiIntelligenceApp.PageModels;

public interface IProjectTaskPageModel
{
	IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
	bool IsBusy { get; }
}