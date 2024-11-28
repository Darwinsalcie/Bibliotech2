namespace Bibliotech.BlazorWASM.Services.Login
{
    namespace Bibliotech.BlazorWASM.Services.Login
    {
        public class AuthService
        {
            public bool IsLoggedIn { get; private set; }
            public string UserRole { get; private set; } // Nueva propiedad para el rol del usuario

            public void Login(string email, string password)
            {
                if (email == "user1@example.com" && password == "password1")
                {
                    IsLoggedIn = true;
                    UserRole = "RoleA"; // Rol para credenciales 1
                }
                else if (email == "user2@example.com" && password == "password2")
                {
                    IsLoggedIn = true;
                    UserRole = "RoleB"; // Rol para credenciales 2
                }
                else
                {
                    IsLoggedIn = false;
                    UserRole = string.Empty;
                }
            }

            public void Logout()
            {
                IsLoggedIn = false;
                UserRole = string.Empty;
            }
        }
    }


}
