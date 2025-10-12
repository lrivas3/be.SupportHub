namespace HelpPoint.Features.Support;

public class RecaptchaService(HttpClient http, IConfiguration config) : IRecaptchaService
{
    private readonly string? _secret = config["Recaptcha:SecretKey"];

    public async Task<bool> ValidateAsync(string token)
    {
        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string?>("secret", _secret),
            new KeyValuePair<string, string?>("response", token)
        ]);
        var resp = await http.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
        if (!resp.IsSuccessStatusCode)
        {
            return false;
        }

        var json = await resp.Content.ReadFromJsonAsync<RecaptchaResponse>();
        return json is { Success: true };
    }

    private class RecaptchaResponse
    {
        public bool Success { get; set; }
        public float Score { get; set; }
        public string? Action { get; set; }
        public DateTime Challenge_TS { get; set; }
        public string? Hostname { get; set; }
        public string[]? ErrorCodes { get; set; }
    }
}
