using System;

namespace SortOrders.API.Models;

public class AppSettings
{
    public string InputFile{get;set;} = "C:\\temp\\Input.txt";
    public string OutputFile{get;set;} = "C:\\temp\\Output.txt";
    public string LogFile{get;set;} = "C:\\temp\\Log.log";
    public char Delimeter{get;set;} = ',';
}
