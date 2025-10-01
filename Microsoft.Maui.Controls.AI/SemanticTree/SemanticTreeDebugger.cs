using System.Diagnostics;

namespace Microsoft.Maui.Controls.AI;

public static class SemanticTreeDebugger
{
    public static void DumpSemanticTree(Window window)
    {
        var service = new SemanticTreeService();
        var tree = service.GetSemanticTree(window);

        if (OperatingSystem.IsWindows())
        {
            // On Windows, use Debug.WriteLine for better readability in Output window
            Debug.WriteLine("=== Semantic Tree Dump ===");
            Debug.WriteLine(tree);
            Debug.WriteLine("=== End Semantic Tree Dump ===");
        }
        else
        {
            Console.WriteLine("=== Semantic Tree Dump ===");
            Console.WriteLine(tree);
            Console.WriteLine("=== End Semantic Tree Dump ===");
        }
    }
}
