using System;
using Microsoft.Extensions.Options;

namespace SortOrders.API.Models;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string Path;
    public FileLoggerProvider(string path)
    {
        Path = path;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(Path);
    }
    public void Dispose()
    {

    }
}

public static class FileLoggerExtensions
{
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
    {
        factory.AddProvider(new FileLoggerProvider(filePath));
        return factory;
    }
}