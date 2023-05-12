// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace AseregeBarcelonaWeb.Pages.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using MudBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Shared.Dialogs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Pages.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Model.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Manager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using AseregeBarcelonaWeb.Manager.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\_Imports.razor"
using BlazorMultiavatar;

#line default
#line hidden
#nullable disable
    public partial class PanelAdministracion : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 66 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\Pages\Components\PanelAdministracion.razor"
 
    private List<User> UserList;
    private bool Loading = true;
    private User user;        

    private async Task LoadUsers()
    {        
        await Task.Yield();

        using (Manager.MySQLManager mySQLManager = new Manager.MySQLManager())
        {
            UserList = await mySQLManager.SelectUsersAsync();
        }
        Loading = false;
        StateHasChanged();
    }

    private async Task DeleteAsync(int indentificator)
    {
        await Task.Yield();

        Manager.MySQLManager connection = new Manager.MySQLManager();
        {
            await connection.DeleteUserAsync(indentificator);
        }
        _ = LoadUsers();
    }

    private async Task EditAsync()
    {
        await Task.Yield();
        Manager.MySQLManager connection = new Manager.MySQLManager();
        {
            //await connection.EditUserAsync();
        }
        _ = LoadUsers();
    }

    //Crear una tarea sincrona cuya función llama a una tarea asincronica, 
    //cuyo objetivo es conectarse a la base de datos y ejecutar la consulta del Delete User Where ID = @parametro
    private Task OnDelete(int indentificator)
    {
        Loading = true;
        StateHasChanged();
        _ = DeleteAsync(indentificator); //Ignorar la tarea borrar una vez se haya acabado el ciclo (resultado sea diferente de NULL)     
        return Task.CompletedTask;
    }

    private async Task OnEditUser(User user)
    {

        //creación del dialogo (ficha para editar perfil)        

        DialogOptions options = new DialogOptions() 
        {
            CloseOnEscapeKey = true 
        };

        DialogParameters parameter = new DialogParameters()
        {
               {
                   "User", user
               }
        };

        DialogResult result = await DialogService.Show<EditUserDialog>("Editar usuario", parameter, options).Result;
        if (!result.Cancelled)
        {
            //En la variable UserInfo (es de clase User), 
            //almaceno los datos del result.Data (if true) convertido en un objeto User. En caso contrario, crear una nueva instancia de ese Objeto.
            User UserInfo = (User)(result.Data ?? default(User)); 

        }       
    }

    protected override Task OnInitializedAsync()
    {
        Loading = true;
        _ = LoadUsers();

        return base.OnInitializedAsync();
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {        
        if (firstRender)
        {
            string nombre = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "Name");
            string Password = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "Password");


            Authorize UserAuthorize = new Authorize()
            {
                Name = nombre, 
                Password = Password
            };

            //Comprobar si el usuario es correcto (autenticado) o no. Si es true, tiene los permisos , en caso contrario, se comprueba si el usuario existe para poder acceder a la web
            //Pd: El usuario que NO sea administrador, tiene Acceso denegado al panel de administración
            bool IsNull = String.IsNullOrWhiteSpace(UserAuthorize.Name) | String.IsNullOrWhiteSpace(UserAuthorize.Password);
            if (!IsNull)
            {
                Manager.MySQLManager mySQLManager = new Manager.MySQLManager();
                {
                    bool EsValido = mySQLManager.Login(UserAuthorize);
                    user = mySQLManager.GetUser(UserAuthorize);
                }
            }
            else
            {
                Console.WriteLine("Error");
            }

            StateHasChanged();
        }

        await Task.CompletedTask;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IDialogService DialogService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISnackbar SnackbarService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime jsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager Navigation { get; set; }
    }
}
#pragma warning restore 1591
