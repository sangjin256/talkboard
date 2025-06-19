using NUnit.Framework;

public class PostContentSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }
    
    public bool IsSatisfiedBy(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            ErrorMessage = "글 내용은 반드시 입력되어야 합니다.";
            return false;
        }
        return true;
    }
}