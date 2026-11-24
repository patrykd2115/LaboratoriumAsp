using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class IsbnAttribute : ValidationAttribute
{
    public IsbnAttribute() => ErrorMessage = "Niepoprawny format ISBN";
    
    public override bool IsValid(object? value)
    {
        if (value is not string)
        {
            ErrorMessage = "ISBN nie jest łańcuchem";
            return false;
        }
        
        string isbn = value as string;
        
        if (isbn.Length != 10 && isbn.Length !=13)
        {
            ErrorMessage = "ISBN ma niepoprawną długość";
            return false;
        }

        if (!isbn.ToCharArray().All(c => char.IsDigit(c)))
        {
            ErrorMessage = "ISBN powinien zabierać same cyfry!";
            return false;
        }
        
        return true;
    }
}