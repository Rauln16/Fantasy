using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories;
using Fantasy.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace Fantasy.Frontend.Pages.Teams
{
    public partial class TeamCreate
    {
        private TeamsForm? teamForm;
        private TeamDTO teamDTO = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/teams/full", teamDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Equipo creado");
        }

        private void Return()
        {
            teamForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/teams");
        }
    }
}