using Microsoft.Extensions.AI;

namespace MauiIntelligenceApp.AI;

public interface IAIFunctionProvider
{
    IReadOnlyList<AIFunction> GetFunctions();
}
