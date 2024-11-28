using Bibliotech.BlazorWASM;
using Bibliotech.BlazorWASM.Services.Libros;
using Bibliotech.BlazorWASM.Services.Notificaciones;
using Bibliotech.BlazorWASM.Services.Reservas;
using Bibliotech.BlazorWASM.Services.Roles;
using Bibliotech.BlazorWASM.Services.Usuarios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;






var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5253") });

builder.Services.AddScoped<ILibrosService,LibroService>();
builder.Services.AddScoped<IUsuariosService, UsuarioService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<INotificacionesService, NotificacionesService>();
builder.Services.AddScoped<IReservasService, ReservasService>();
builder.Services.AddSweetAlert2();


await builder.Build().RunAsync();
