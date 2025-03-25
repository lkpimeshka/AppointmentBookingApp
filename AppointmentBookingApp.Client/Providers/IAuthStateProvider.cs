using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AppointmentBookingApp.Client.Providers
{
    public interface IAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void MarkUserAsAuthenticated(string username);
        void MarkUserAsLoggedOut();
        Task<string> GetUserIdAsync();
        //IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
    }
}
