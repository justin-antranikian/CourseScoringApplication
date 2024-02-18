using System.Threading;

namespace Orchestration.GetBreadcrumb;

public class GetAthleteBreadcrumbOrchestrator : GetBreadcrumbOrchestratorBase
{
    public BreadcrumbResultDto GetResult(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        Thread.Sleep(1000);

        var locationInfoWithUrl = GetLocationInfoWithUrl(breadcrumbRequestDto);
        return new BreadcrumbResultDto(locationInfoWithUrl);
    }
}
