//using Api.DataModels;
//using Api.Orchestration.GetBreadcrumb;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.Controllers;

//[Route("[controller]")]
//public class EventsBreadCrumbsApiController(ScoringDbContext scoringDbContext) : ControllerBase
//{
//    [HttpGet]
//    public async Task<EventsBreadcrumbResultDto> Get([FromQuery] BreadcrumbRequestDto requestDto)
//    {
//        var orchestrator = new GetEventsBreadcrumbOrchestrator(scoringDbContext);
//        return await orchestrator.GetResult(requestDto);
//    }
//}
