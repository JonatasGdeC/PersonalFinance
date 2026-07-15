using System.Security.Cryptography;
using PersonalFinance.Domain.Security.CodeGenerator;

namespace PersonalFinance.Infrastructure.Security.CodeGenerator;

internal class CodeGenerator : ICodeGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    public string Generate(int length = 6)
    {
        char[] code = new char[length];

        for (int i = 0; i < length; i++)
        {
            int index = RandomNumberGenerator.GetInt32(toExclusive: Chars.Length);
            code[i] = Chars[index: index];
        }

        return new string(value: code);
    }
}