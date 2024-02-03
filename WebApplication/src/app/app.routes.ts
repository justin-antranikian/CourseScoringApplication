import { Route, Routes } from '@angular/router';
import { IrpComponent } from './irp/irp.component';
import { RouteConstants } from './_common/routeConstants';
import { EventsAllComponent } from './dashboards/events/events-all.component';
import { ArpComponent } from './arp/arp.component';
import { AthletesAllComponent } from './dashboards/athletes/athletes-all.component';
import { RaceLeaderboardComponent } from './leaderboard/race-leaderboard/race-leaderboard.component';
import { CourseLeaderboardComponent } from './leaderboard/course-leaderboard/course-leaderboard.component';
import { RaceSeriesDashboardComponent } from './race-series-dashboard/race-series-dashboard.component';
import { EventsCityComponent } from './dashboards/events/events-city.component';
import { EventsStateComponent } from './dashboards/events/events-state.component';
import { EventsAreaComponent } from './dashboards/events/events-area.component';
import { IrpCompareComponent } from './irp-compare/irp-compare.component';
import { AthletesStateComponent } from './dashboards/athletes/athletes-state.component';
import { AthletesAreaComponent } from './dashboards/athletes/athletes-area.component';
import { AthletesCityComponent } from './dashboards/athletes/athletes-city.component';
import { AwardsPodiumComponent } from './leaderboard/awards-podium/awards-podium.component';
import { ArpCompareComponent } from './arp-compare/arp-compare.component';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';

const rootRoute: Route = { 
  path: '',
  redirectTo: `${RouteConstants.EventsPage}`,
  pathMatch: 'full'
}

const irpRoute: Route = {
  path: `${RouteConstants.IndividualResultPage}/:id`,
  component: IrpComponent
}

const athletesAllPage: Route = {
  path: `${RouteConstants.AthletesPage}`,
  component: AthletesAllComponent,
  pathMatch: 'full'
}

const athleteStateRoute: Route = { 
  path: `${RouteConstants.AthletesStatePage}/:state`, 
  component: AthletesStateComponent,
  pathMatch: 'full'
}

const athleteAreaRoute: Route = {
  path: `${RouteConstants.AthletesAreaPage}/:area`,
  component: AthletesAreaComponent,
  pathMatch: 'full'
}

const athleteCityRoute: Route = {
  path: `${RouteConstants.AthletesCityPage}/:city`,
  component: AthletesCityComponent,
  pathMatch: 'full'
}

const stateEventsRoute: Route = { 
  path: `${RouteConstants.EventsStatePage}/:state`,
  component: EventsStateComponent,
  pathMatch: 'full'
}

const cityEventsRoute: Route = { 
  path: `${RouteConstants.EventsCityPage}/:city`,
  component: EventsCityComponent,
  pathMatch: 'full' 
}

const areaEventsRoute: Route = { 
  path: `${RouteConstants.EventsAreaPage}/:area`,
  component: EventsAreaComponent,
  pathMatch: 'full'
}

const atheletePage: Route = {
  path: `${RouteConstants.AthletePage}/:id`,
  component: ArpComponent,
}

const raceSeriesDashboardPage: Route = { 
  path: `${RouteConstants.RaceSeriesDashboardPage}/:id`, 
  component: RaceSeriesDashboardComponent
}

const raceLeaderboardPage: Route = { 
  path: `${RouteConstants.RaceLeaderboardPage}/:id`, 
  component: RaceLeaderboardComponent
}

const courseLeaderboardPage: Route = { 
  path: `${RouteConstants.CourseLeaderboardPage}/:id`,
  component: CourseLeaderboardComponent
}

const eventsPage: Route = { 
  path: `${RouteConstants.EventsPage}`,
  component: EventsAllComponent,
  pathMatch: 'full'
}

const irpComparePage: Route = { 
  path: `${RouteConstants.IndividualResultComparePage}/:id`,
  component: IrpCompareComponent 
}

const awardsPodium: Route = { 
  path: `${RouteConstants.AwardsPodiumPage}/:id`,
  component: AwardsPodiumComponent
}

const athleteComparePage: Route = { 
  path: `${RouteConstants.AthleteComparePage}`,
  component: ArpCompareComponent
}

const advancedSearchPage: Route = {
  path: `${RouteConstants.AdvancedSearch}`,
  component: AdvancedSearchComponent,
  pathMatch: 'full'
}

export const routes: Routes = [
  rootRoute,
  eventsPage,
  stateEventsRoute,
  cityEventsRoute,
  areaEventsRoute,
  athletesAllPage,
  athleteStateRoute,
  athleteAreaRoute,
  athleteCityRoute,
  raceSeriesDashboardPage,
  raceLeaderboardPage,
  courseLeaderboardPage,
  atheletePage,
  irpRoute,
  irpComparePage,
  awardsPodium,
  athleteComparePage,
  advancedSearchPage,
];
