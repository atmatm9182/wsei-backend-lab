namespace WebAPI.Dto;

public class NewQuizItemDto
{
    public string Question { get; set; }
    public IEnumerable<string> Options { get; set; }
    public int CorrectOptionsIndex { get; set; }
}
