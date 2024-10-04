using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }   

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        // Await the registration of the visit
        await _statisticService.RegisterVisitAsync(path).ConfigureAwait(false);

        // Update headers after the visit is registered
        var visitCount = await _statisticService.GetVisitsCountAsync(path).ConfigureAwait(false);
        context.Response.Headers.Add(
            CustomHttpHeaders.TotalPageVisits,
            visitCount.ToString());

        // Proceed to the next middleware/component in the pipeline
        await _next(context);
    }
}
