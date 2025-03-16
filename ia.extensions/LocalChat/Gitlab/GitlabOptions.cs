namespace LocalChat.Gitlab;

public class GitlabOptions
{
    public string BaseUrl { get; set; } = "https://gitlab.smsdata.com/";
    public string PrivateTokenName { get; set; } = "PRIVATE-TOKEN";
    public string PrivateTokenValue { get; set; } = "private-token-value";
}