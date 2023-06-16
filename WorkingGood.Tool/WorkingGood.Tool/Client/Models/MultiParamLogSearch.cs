using System.Reflection.Metadata.Ecma335;

namespace WorkingGood.Tool.Client.Models;

public class MultiParamLogSearch
{
    public string? ServiceName { get; set; }
    public string? Level { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? SearchPhrase { get; set; }
}