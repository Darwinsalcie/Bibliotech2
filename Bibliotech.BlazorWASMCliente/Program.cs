using Bibliotech.BlazorWASMCliente;
using Bibliotech.BlazorWASMCliente.Services;
using Bibliotech.BlazorWASMCliente.Services.Libros;
using Bibliotech.BlazorWASMCliente.Services.Notificaciones;
using Bibliotech.BlazorWASMCliente.Services.Reservas;
using Bibliotech.BlazorWASMCliente.Services.Roles;
using Bibliotech.BlazorWASMCliente.Services.Usuarios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;






var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5253") });

builder.Services.AddScoped<ILibrosService, LibroService>();
builder.Services.AddScoped<IUsuariosService, UsuarioService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<INotificacionesService, NotificacionesService>();
builder.Services.AddScoped<IReservasService, ReservasService>();
builder.Services.AddSweetAlert2();


await builder.Build().RunAsync();
