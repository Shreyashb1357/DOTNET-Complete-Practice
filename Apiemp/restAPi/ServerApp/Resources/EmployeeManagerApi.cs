using System.IdentityModel.Tokens.Jwt;
using ServerApp.security;

namespace ServerApp.Resources;

public class EmployeeManagerApi
{
    public static async Task<IResult> SignIn(string id , int passcode)
    {
        string managerMail = id + "@gmail.com";
        if(passcode == 9820)
        {
            await OtpHelper.MailPasscodeAsync(managerMail , "admin@gmail.com");
            return Results.Ok("OTP sent successfully");
        }
        if(OtpHelper.VerifyPasscode(managerMail , passcode))
        {
            var token = JwtHelper.CreateToken(id);
            return Results.Text(token);
        }
        return Results.Unauthorized();
    }
}