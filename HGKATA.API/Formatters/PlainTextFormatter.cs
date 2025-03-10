using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;

public class PlainTextFormatter : TextInputFormatter
{
    public PlainTextFormatter()
    {
        SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Text.Plain));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.ASCII);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        using var reader = new StreamReader(context.HttpContext.Request.Body, encoding);
        var plainText = await reader.ReadToEndAsync();
        
        return await InputFormatterResult.SuccessAsync(plainText);
    }

    protected override bool CanReadType(Type type) => type == typeof(string);
}