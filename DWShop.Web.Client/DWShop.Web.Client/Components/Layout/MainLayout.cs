using DWShop.Web.Infrastructure.Settings;
using MudBlazor;


namespace DWShop.Web.Client.Components.Layout
{
    public partial class MainLayout
    {


        private MudTheme currentTheme;

        public async Task DarkMode()
        {
            bool isDarkMode
                = await _clientPreference.ToogleDarkModeAsync();

            currentTheme = isDarkMode
                ? DWTheme.DefaultTheme
                : DWTheme.DarkTheme;
        }


    
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {

                if (!firstRender)
                    return;
                currentTheme = await _clientPreference.GetCurrentThemeAsync();
                await base.OnAfterRenderAsync(firstRender);
                StateHasChanged();

            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
