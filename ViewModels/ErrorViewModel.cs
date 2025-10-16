namespace FederalBonds.ViewModels
{
    // ============================================================
    // ===== ViewModel used for displaying error information.
    // ===== Provides details about the current request for debugging.
    // ============================================================
    public class ErrorViewModel
    {
        // ===== Unique identifier of the failed request (for tracking)
        public string? RequestId { get; set; }

        // ===== Indicates whether the RequestId should be displayed
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
