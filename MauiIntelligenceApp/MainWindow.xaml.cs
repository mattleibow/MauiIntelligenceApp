using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using MauiIntelligenceApp.AI;
using Microsoft.Extensions.AI;

namespace MauiIntelligenceApp;

public partial class MainWindow : Window
{
	private readonly ISemanticTreeService semanticTreeService;
	private readonly IChatClient chatClient;
	private readonly IEnumerable<IAIFunctionProvider> functionProviders;

	public MainWindow(
		ISemanticTreeService semanticTreeService,
		IChatClient chatClient,
		IEnumerable<IAIFunctionProvider> functionProviders)
	{
		InitializeComponent();

		this.semanticTreeService = semanticTreeService;
		this.chatClient = chatClient;
		this.functionProviders = functionProviders;
	}

	private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
	{
		var systemPrompt =
			"""
			You are an AI assistant that helps users manage their tasks using functions.

			The user will ask you to do things or help them do things.
			You are to use the functions provided to interact with the app.

			The user cannot see the responses you give, only the results of your actions.
			If the user asks a question, you need to select a function to perform an action
			that will help the user get the information they need.

			If it is not a function call, the user cannot see the response. Do not respond
			with words. They will be lost. The user will be confused.

			Do not make up any information, only use the functions provided.
			Always call a function to get information, do not try to answer on your own.

			Some functions have a notification message that will be shown to the user.
			This should be used to inform the user of what you have done and not to share
			information you have learned from the functions.

			In order to update the current page, you may need to call functions to set properties
			or perform actions on the current page. These do NOT happen automatically.

			One thing to always remember, no saving happens automatically and the user will
			step in to do this. Do just enough to display data and enter/modify properties.

			The USER will save AFTER they are happy.

			The user may wish to make further changes before saving. 

			Get the user to a good place and let them take over.
			""";

		var funcPrompt =
			$"""
			Use the functions provided to help the user accomplish their request.
			Respond with a function call to perform an action or get information.

			Do not respond with words, only respond with a function call.
			""";

		var prompt =
			$"""
			The user has entered the following request:

			```
			{TitleBarSearchBox.Text}
			```
			""";

		var requestOptions = new ChatOptions()
		{
			// MaxCompletionTokens = 13107,
			Temperature = 1.0f,
			TopP = 1.0f,
			FrequencyPenalty = 0.0f,
			PresencePenalty = 0.0f,

			ToolMode = ChatToolMode.Auto,
			Tools = [
				.. functionProviders.SelectMany(static p => p.GetFunctions())
			]
		};

		var messages = new List<ChatMessage>()
		{
			new ChatMessage(ChatRole.System, systemPrompt),
			new ChatMessage(ChatRole.User, funcPrompt),
			new ChatMessage(ChatRole.User, prompt),
		};

		var client = new FunctionInvokingChatClient(chatClient);

		try
		{
			var response = await client.GetResponseAsync(messages, requestOptions);

			Debug.WriteLine(response.ToString());

			Debug.WriteLine("Messages:");
			foreach (var message in response.Messages)
			{
				Debug.WriteLine($"- {message.Role}: {message.ToDebugString()}");
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error: {ex.Message}");
		}
	}

	private void DebugSemanticTreeButton_Clicked(object sender, EventArgs e)
	{
		// Dump to debug output
		// SemanticTreeDebugger.DumpSemanticTree(this);

		// var fields = typeof(Routing).GetFields(
		// 	System.Reflection.BindingFlags.DeclaredOnly |
		// 	System.Reflection.BindingFlags.Static |
		// 	System.Reflection.BindingFlags.NonPublic);
		// foreach (var field in fields)
		// {
		// 	var value = field.GetValue(null);
		// 	Debug.WriteLine($"{field.Name}: {value}");

		// 	if (field.IsLiteral)
		// 		continue;

		// 	if (value is IDictionary dic)
		// 		{
		// 			foreach (var key in dic.Keys)
		// 			{
		// 				Debug.WriteLine($"- {key}: {dic[key]}");
		// 			}
		// 		}
		// 		else if (value is IEnumerable objects)
		// 		{
		// 			foreach (var obj in objects)
		// 			{
		// 				Debug.WriteLine($"- {obj}");
		// 			}
		// 		}
		// }

		// Shell.Current.GoToAsync("//projects");
	}


		// var tree = semanticTreeService.GetSemanticTree(this);

		
		// var systemPrompt = """
		// 	You are an AI assistant that helps users interact with a graphical user
		// 	interface (GUI) of an application. The GUI is represented as a hierarchical
		// 	tree structure, where each node contains information about a UI element,
		// 	including its type, text content, and other relevant properties.

		// 	Your task is to understand the user's request and provide clear instructions on
		// 	how to interact with the GUI to accomplish the desired action. You should
		// 	consider the hierarchy and relationships between UI elements when formulating your response.

		// 	When responding, please follow these guidelines:
		// 	1. Identify the most relevant UI elements based on the user's request.
		// 	2. Provide step-by-step instructions on how to navigate through the GUI to achieve the desired outcome.
		// 	3. If multiple options are available, suggest the most appropriate one based on common usage patterns.
		// 	4. If the request is unclear or cannot be fulfilled with the available UI elements, 
		// 	   ask for clarification or inform the user of any limitations.

		// 	Here is an example of how to interpret a user's request:
		// 	User Request: "I want to create a new project."
		// 	Response: "To create a new project, please follow these steps:
		// 	1. Click on the 'File' menu located at the top left corner of the application window.
		// 	2. Select 'New Project' from the dropdown menu.
		// 	3. Fill in the required details in the 'New Project' dialog box that appears.
		// 	4. Click 'Create' to finalize and create your new project."

		// 	Remember to always refer to the provided semantic tree structure when crafting your responses.
		// 	""";

		// var uiPrompt = $"""
		// 	Here is the semantic tree representation of the GUI:

		// 	{tree}
		// 	""";
}
