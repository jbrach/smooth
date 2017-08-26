
using System;

public class CreateDateAndCustomTextStrategy : IFileNameStrategy
{
    private readonly string _customText;

    public CreateDateAndCustomTextStrategy(string customText) => _customText = customText;

    public string GenerateName()
    {
        throw new NotImplementedException();
    }
}