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
    public partial class Register : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 32 "C:\Users\Edgar Muñoz\source\repos\AseregeHTMLFramework-Razor_Branch\AseregeBarcelonaWeb\Pages\Components\Register.razor"
 
    private User user { get; set; } = new User();
    private string PasswordRepeat { get; set; } = "";
    private bool isShow = false;

    private int SecurityLevelPassword = 0;
    private Color SecurityLevelColor = Color.Default;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    private PasswordSecurityLevel passSecure;

    //para hacer visible la contraseña
    void OnPasswordVisibleClick()
    {
        isShow = !isShow;
        switch (isShow)
        {
            case false:                
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
                break;
            case true:                
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
                break;
        }
    }

    void ChangeVariant(string message, Variant variant)
    {

        SnackbarService.Add($"Normal {message}", Severity.Normal);
        SnackbarService.Add($"Info {message}", Severity.Info);
        SnackbarService.Add($"Success {message}", Severity.Success);
        SnackbarService.Add($"Warning {message}", Severity.Warning);
        SnackbarService.Add($"Error {message}", Severity.Error);
    }


    private async Task RegisterAsync()
    {                
        SnackbarService.Configuration.MaxDisplayedSnackbars = 5;

        if (PasswordRepeat == user.Passwordseguro)
        {                       
            PasswordSecurityLevel passSecure = PasswordSecurity.Check(PasswordRepeat);    

            switch (passSecure)
            {
                case PasswordSecurityLevel.VeryStrong:
                    SecurityLevelPassword = 100;                                                                            
                    SnackbarService.Add("La contraseña es muy segura. Puedes seguir adelante con el registro.", Severity.Success);
                    SecurityLevelColor = Color.Success;
                    break;

                case PasswordSecurityLevel.Strong:
                    SecurityLevelPassword = 75;                                        
                    SnackbarService.Add("La contraseña es segura. Recomendamos agregar algunos caracteres especiales.", Severity.Info);
                    SecurityLevelColor = Color.Info;
                    break;

                case PasswordSecurityLevel.Medium:
                    SecurityLevelPassword = 50;                                        
                    SnackbarService.Add("La contraseña es débil. Recomendamos agregar números y caracteres especiales.", Severity.Warning);
                    SecurityLevelColor = Color.Warning;
                    break;

                case PasswordSecurityLevel.Weak:
                    SecurityLevelPassword = 25;
                    SnackbarService.Add("La contraseña es muy débil. Recomendamos agregar mayúsculas, números y caracteres especiales.", Severity.Normal);
                    SecurityLevelColor = Color.Error;
                    break;

                default:
                    SecurityLevelPassword = 8;
                    SnackbarService.Add("Ha ocurrido un error al evaluar la seguridad de la contraseña. Inténtalo de nuevo más tarde.", Severity.Error);
                    SecurityLevelColor = Color.Error;
                    break;
            }
            using(Manager.MySQLManager manager = new Manager.MySQLManager())
            {
                Authorize authorize = new Authorize();
                authorize.Name = user.Nombre;
                authorize.Password = user.Passwordseguro;
                if (manager.GetUser(authorize)==null)
                {
                    manager.InsertUser(user);
                }
                else
                {
                    SnackbarService.Add("El usuario ya existe y no se puede insertar. Por favor, intentalo de nuevo", Severity.Info);
                }

                //PAGEMANAGER
                StateHasChanged();
            }
        }
        else
        {
            SecurityLevelPassword = 8;
            SnackbarService.Add("Las contraseñas no coinciden. Por por favor, asegurase de comprovarlas o cambia de password", Severity.Error);
            SecurityLevelColor = Color.Error;
        }
        StateHasChanged();
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
