using System.ComponentModel;
using Microsoft.Extensions.AI;

namespace MauiIntelligenceApp.AI;

public interface IPageModelRepresentation
{
    string Name { get; }

    string Capabilities { get; }

    string Properties { get; }

    object? GetValue(string propertyName);

    void SetValue(string propertyName, object? value);
    
    public string GetSummary() =>
        $"""
        You are currently on the "{Name}" page.
        
        Capabilities:
        {Capabilities}
        
        Properties:
        {Properties}
        """;
}
