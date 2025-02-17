using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ClinicaMedica.Client.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "Token");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var claims = ParseClaimsFromJwt(token);
                    identity = new ClaimsIdentity(claims, "jwt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el token: {ex.Message}");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            try
            {
                var payload = jwt.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al parsear el JWT: {ex.Message}");
                return new List<Claim>();
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
