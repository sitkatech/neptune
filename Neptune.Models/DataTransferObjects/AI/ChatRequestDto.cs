namespace Neptune.Models.DataTransferObjects;

public class ChatRequestDto
{
    public List<ChatMessageDto> Messages { get; set; } = new();
    public Dictionary<string, object> Filters { get; set; } = new();
}