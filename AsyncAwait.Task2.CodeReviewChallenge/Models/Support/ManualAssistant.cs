using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudServices.Interfaces;

namespace AsyncAwait.Task2.CodeReviewChallenge.Models.Support;

public class ManualAssistant : IAssistant
{
    private readonly ISupportService _supportService;

    public ManualAssistant(ISupportService supportService)
    {
        _supportService = supportService ?? throw new ArgumentNullException(nameof(supportService));
    }    

    public async Task<string> RequestAssistanceAsync(string requestInfo)
    {
        try
        {
            // Await the registration of the support request
            await _supportService.RegisterSupportRequestAsync(requestInfo).ConfigureAwait(false);

            // Instead of sleeping, we can directly fetch the support info
            return await _supportService.GetSupportInfoAsync(requestInfo).ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            return $"Failed to register assistance request. Please try later. {ex.Message}";
        }
        catch (Exception ex)
        {
            // Handle other exceptions that may arise
            return $"An unexpected error occurred: {ex.Message}";
        }
    }
}
