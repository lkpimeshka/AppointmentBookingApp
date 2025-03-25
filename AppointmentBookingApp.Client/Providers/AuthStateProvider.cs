using AppointmentBookingApp.Application.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.JSInterop;


namespace AppointmentBookingApp.Client.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider, IAuthStateProvider
    {
        private readonly ProtectedSessionStorage _protectedSessionStorage;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
        private readonly IJSRuntime _jsRuntime;

        public AuthStateProvider(ProtectedSessionStorage protectedSessionStorage, IJSRuntime jsRuntime)
        {
            _protectedSessionStorage = protectedSessionStorage;
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var tokenResult = "";
            //var tokenResult = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImZiNmJmMjQ5LTA2NjYtNDZkNC04YmQzLWFmZGQwZThhYjRiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJGdWxsTmFtZSI6IkFkbWluIFVzZXIiLCJleHAiOjE3NDE0NDM3MTgsImlzcyI6IkFwcG9pbnRtZW50Qm9va2luZyIsImF1ZCI6IkFwcG9pbnRtZW50Qm9va2luZ1VzZXJzIn0.ZUsrLcrugjhbqjAI0FuUYb2PYCDik0DKZ0igLy4w-VI";
            try {
                var tokenResult = await _protectedSessionStorage.GetAsync<string>("authToken");

                if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(tokenResult.Value), "Bearer"));
                }
                else
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                }

                return new AuthenticationState(_currentUser);
            }
            catch (Exception ex)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            
        }

        /*public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenResult = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrEmpty(tokenResult))
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(tokenResult), "Bearer"));
            }
            else
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            return new AuthenticationState(_currentUser);
        }*/


        /*public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // This call should be deferred until after the component is rendered
            var authToken = await GetTokenAsync();

            var identity = string.IsNullOrEmpty(authToken) ?
                new ClaimsIdentity() :
                new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        private async Task<string> GetTokenAsync()
        {
            // Wait until after rendering before calling JavaScript interop
            var result = await _protectedSessionStorage.GetAsync<string>("authToken");
            return result.Success ? result.Value : null;
        }*/

        public async Task SetToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                await _protectedSessionStorage.SetAsync("authToken", token);
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "Bearer"));
            }
            else
            {
                await _protectedSessionStorage.DeleteAsync("authToken");
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsAuthenticated(string username)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "Bearer");
            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];

            try
            {
                var jsonBytes = WebEncoders.Base64UrlDecode(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
                return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            }
            catch
            {
                return new List<Claim>();
            }
        }

        public async Task<string> GetUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                return user.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            }

            return null; 
        }

        public async Task<string> GetUserRoleAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                return user.FindFirst(ClaimTypes.Role)?.Value; // Get the user's role
            }

            return null; 
        }


        public async Task Logout()
        {
            await _protectedSessionStorage.DeleteAsync("authToken");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

    }
}
