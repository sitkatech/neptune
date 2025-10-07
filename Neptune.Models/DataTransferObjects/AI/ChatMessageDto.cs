namespace Neptune.Models.DataTransferObjects;

public class ChatMessageDto
{
    public string Role { get; set; } // e.g., "user", "assistant"
    public string Content { get; set; } // The message content
    public string Id { get; set; } // Optional, for RAG
}