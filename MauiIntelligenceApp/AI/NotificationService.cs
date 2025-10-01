using System.ComponentModel;
using Microsoft.Extensions.AI;
using Microsoft.Maui.ApplicationModel;

namespace MauiIntelligenceApp.AI;

public class NotificationService : IAIFunctionProvider
{
    private AIFunction? showNotificationFunction;
    private IReadOnlyList<AIFunction>? allFunctions;

    public IReadOnlyList<AIFunction> GetFunctions() =>
        allFunctions ??=
        [
            showNotificationFunction ??= AIFunctionFactory.Create(ShowNotificationAsync)
        ];

    [Description(
        """
        Displays a toast or snackbar notification with the provided short message.
        """)]
    public async Task<string> ShowNotificationAsync(
        [Description("The short message to display to the user. (max 100 chars)")] string message,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "Notification skipped because the message was empty.";
        }

        cancellationToken.ThrowIfCancellationRequested();
        var trimmedMessage = message.Trim();

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (OperatingSystem.IsWindows())
            {
                await AppShell.DisplaySnackbarAsync(trimmedMessage);
            }
            else
            {
                await AppShell.DisplayToastAsync(trimmedMessage);
            }
        });

        return $"Notification displayed: {trimmedMessage}";
    }
}
