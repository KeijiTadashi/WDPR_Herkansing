using Microsoft.AspNetCore.Identity;

namespace api;
// https://www.yogihosting.com/aspnet-core-identity-setup/
public class Gebruiker : IdentityUser //In het ERR Login
{
    /*
     * IdentityUser heeft al veel data:
     * Id :string
     * Email :string?
     * UserName :string?
     * NormalizedEmail :string?
     * PasswordHash :string?
     * PhoneNumber :string?
     * NormalizedUserName :string?
     * En nog wat meer die niet in de ERR staan en we mogelijk/waarschijnlijk niet nodig hebben
     */
    public string AccountType { get; init; }
}