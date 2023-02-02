using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorMealOrdering.Client.CustomComponents.ModalComponents;

namespace BlazorMealOrdering.Client.Utils
{
    public class ModalManager
    {
        private readonly IModalService _modalService;

        public ModalManager(IModalService modalService)
        {
            _modalService = modalService;
        }

        public async Task ShowMessageAsync(string title, string message, int duration = 0)
        {
            ModalParameters modalParameters = new();
            modalParameters.Add("Message", message);

            var modalRef = _modalService.Show<ShowMessagePopupComponent>(title, modalParameters);

            if (duration > 0)
            {
                await Task.Delay(duration);
                modalRef.Close();
            }
        }

        public async Task<bool> ConfirmationAsync(string title, string message)
        {
            ModalParameters modalParameters = new();
            modalParameters.Add("Message", message);

            IModalReference modalRef = _modalService.Show<ConfirmationPopupModalComponent>(title, modalParameters);
            ModalResult modalResult = await modalRef.Result;

            return !modalResult.Cancelled;
        }
    }
}