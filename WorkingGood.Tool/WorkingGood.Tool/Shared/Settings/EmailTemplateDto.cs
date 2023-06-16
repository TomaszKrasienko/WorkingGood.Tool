namespace WorkingGood.Tool.Shared.Settings;

public class EmailTemplateDto
{
    public string Destination { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}