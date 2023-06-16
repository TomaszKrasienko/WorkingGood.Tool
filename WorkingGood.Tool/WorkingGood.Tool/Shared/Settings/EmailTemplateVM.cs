namespace WorkingGood.Tool.Shared.Settings;

public class EmailTemplateVM
{
    public Guid Id { get; set; }
    public string Destination { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}