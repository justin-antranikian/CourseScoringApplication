import { RouteConstants } from './_common/routeConstants';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
// sub components
import { NavMenuComponent } from './_subComponents/nav-menu/nav-menu.component';
import { BracketRankComponent } from './_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from './_subComponents/interval-time/interval-time.component';
// main components
import { RaceSeriesDashboardComponent } from './race-series-dashboard/race-series-dashboard.component';
import { RaceLeaderboardComponent } from './leaderboard/race-leaderboard/race-leaderboard.component';
import { CourseLeaderboardComponent } from './leaderboard/course-leaderboard/course-leaderboard.component';
import { IrpComponent } from './irp/irp.component';
import { ArpComponent } from './arp/arp.component';
// services
import { ApiRequestService } from './_services/api-request.service';
import { EventsAllComponent } from './dashboards/events/events-all.component';
import { EventsStateComponent } from './dashboards/events/events-state.component';
import { EventsAreaComponent } from './dashboards/events/events-area.component';
import { EventSearchResultComponent } from './dashboards/events/event-search-result/event-search-result.component';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { AthletesAllComponent } from './dashboards/athletes/athletes-all.component';
import { AthletesStateComponent } from './dashboards/athletes/athletes-state.component';
import { AthletesAreaComponent } from './dashboards/athletes/athletes-area.component';
import { AthleteSearchResultComponent } from './dashboards/athletes/athlete-search-result/athlete-search-result.component';
import { AthletesCityComponent } from './dashboards/athletes/athletes-city.component';
import { SmartNavigationComponent } from './dashboards/smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from './dashboards/smart-navigation-states/smart-navigation-states.component';
import { QuickSearchComponent } from './dashboards/quick-search/quick-search.component';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';
import { EventsCityComponent } from './dashboards/events/events-city.component';
import { IrpsSearchComponent } from './_subComponents/irps-search-component/irps-search.component';
import { IrpsSearchResultComponent } from './_subComponents/irps-search-component/irps-search-result.component';
import { EventsBreadcrumbComponent } from './_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { AthleteBreadcrumbComponent } from './_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { LocationInfoRankingsComponent } from './_subComponents/location-info-rankings/location-info-rankings.component';
import { ArpResultComponent } from './arp/arp-result.component';
import { IrpPizzaTrackerComponent } from './irp/irp-pizza-tracker.component';
import { IrpIntervalResultComponent } from './irp/irp-interval-result.component';
import { RaceSeriesParticipantComponent } from './race-series-dashboard/race-series-participant.component';
import { RaceSeriesCourseTabComponent } from './race-series-dashboard/race-series-course-tab.component';
import { LeaderboardResultComponent } from './_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { BreadcrumbsApiRequestService } from './_services/breadcrumbs-api-request.service';
import { AwardsPodiumComponent } from './leaderboard/awards-podium/awards-podium.component';
import { IrpCompareComponent } from './irp-compare/irp-compare.component';
import { IrpQuickViewComponent } from './_subComponents/leaderboard-results-grid/irp-quick-view.component';
import { ArpCompareComponent } from './arp-compare/arp-compare.component';
import { AthleteResultsReportComponent } from './athlete-results-report/athlete-results-report.component';
import { RacesByMonthReportComponent } from './races-by-month-report/races-by-month-report.component';

@NgModule({
  declarations: [
    AppComponent,
    // sub components
    NavMenuComponent,
    BracketRankComponent,
    IntervalTimeComponent,
    EventSearchResultComponent,
    AthleteSearchResultComponent,
    // main components
    AthletesAllComponent,
    AthletesStateComponent,
    AthletesAreaComponent,
    AthletesCityComponent,
    EventsAllComponent,
    EventsStateComponent,
    EventsAreaComponent,
    EventsCityComponent,
    RaceSeriesDashboardComponent,
    RaceLeaderboardComponent,
    CourseLeaderboardComponent,
    IrpComponent,
    IrpCompareComponent,
    ArpComponent,
    SmartNavigationComponent,
    SmartNavigationStatesComponent,
    QuickSearchComponent,
    AdvancedSearchComponent,
    IrpsSearchComponent,
    IrpsSearchResultComponent,
    EventsBreadcrumbComponent,
    AthleteBreadcrumbComponent,
    LocationInfoRankingsComponent,
    ArpResultComponent,
    ArpCompareComponent,
    IrpPizzaTrackerComponent,
    IrpIntervalResultComponent,
    RaceSeriesParticipantComponent,
    RaceSeriesCourseTabComponent,
    LeaderboardResultComponent,
    IrpQuickViewComponent,
    AwardsPodiumComponent,
    RacesByMonthReportComponent,
    AthleteResultsReportComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ChartsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: `${RouteConstants.EventsPage}`, pathMatch: 'full' },
      { path: `${RouteConstants.AdvancedSearch}`, component: AdvancedSearchComponent, pathMatch: 'full' },
      { path: `${RouteConstants.AthletesPage}`, component: AthletesAllComponent, pathMatch: 'full' },
      { path: `${RouteConstants.AthletesStatePage}/:state`, component: AthletesStateComponent, pathMatch: 'full' },
      { path: `${RouteConstants.AthletesAreaPage}/:area`, component: AthletesAreaComponent, pathMatch: 'full' },
      { path: `${RouteConstants.AthletesCityPage}/:city`, component: AthletesCityComponent, pathMatch: 'full' },
      { path: `${RouteConstants.EventsPage}`, component: EventsAllComponent, pathMatch: 'full' },
      { path: `${RouteConstants.EventsStatePage}/:state`, component: EventsStateComponent, pathMatch: 'full' },
      { path: `${RouteConstants.EventsAreaPage}/:area`, component: EventsAreaComponent, pathMatch: 'full' },
      { path: `${RouteConstants.EventsCityPage}/:city`, component: EventsCityComponent, pathMatch: 'full' },
      { path: `${RouteConstants.RaceSeriesDashboardPage}/:id`, component: RaceSeriesDashboardComponent },
      { path: `${RouteConstants.RaceLeaderboardPage}/:id`, component: RaceLeaderboardComponent },
      { path: `${RouteConstants.CourseLeaderboardPage}/:id`, component: CourseLeaderboardComponent },
      { path: `${RouteConstants.IndividualResultPage}/:id`, component: IrpComponent },
      { path: `${RouteConstants.IndividualResultComparePage}/:id`, component: IrpCompareComponent },
      { path: `${RouteConstants.AthletePage}/:id`, component: ArpComponent },
      { path: `${RouteConstants.AthleteComparePage}`, component: ArpCompareComponent },
      { path: `${RouteConstants.AwardsPodiumPage}/:id`, component: AwardsPodiumComponent },
      { path: `${RouteConstants.RacesByMonthReport}`, component: RacesByMonthReportComponent },
      { path: `${RouteConstants.AthleteResultsReport}`, component: AthleteResultsReportComponent },
    ])
  ],
  providers: [ApiRequestService, BreadcrumbsApiRequestService, ThemeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
