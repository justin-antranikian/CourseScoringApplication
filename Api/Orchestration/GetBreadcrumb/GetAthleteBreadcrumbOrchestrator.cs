﻿namespace Api.Orchestration.GetBreadcrumb;

public class GetAthleteBreadcrumbOrchestrator : GetBreadcrumbOrchestratorBase
{
    public BreadcrumbResultDto GetResult(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        var locationInfoWithUrl = GetLocationInfoWithUrl(breadcrumbRequestDto);
        return new BreadcrumbResultDto(locationInfoWithUrl);
    }
}
