import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { AthleteSearchResultDto } from '../../_core/athleteSearchResultDto';

export abstract class AthletesComponentBase extends BreadcrumbComponent {

  public athleteSearchResultsChunked!: any[][]
  public dashboardInfoResponseDto!: any

  public title: any
  public isLanding = false

  public athleteIdsToCompare: number[] = []
  public athleteIdsToCompareString: any = null
  public showToast = false;

  public onCompareClicked = ({ id }: AthleteSearchResultDto) => {
    const athleteIds = this.athleteIdsToCompare

    if (!athleteIds.includes(id)) {
      athleteIds.push(id)
    } else {
      const index = athleteIds.indexOf(id);
      athleteIds.splice(index, 1);
    }

    this.athleteIdsToCompareString = JSON.stringify(athleteIds)
    this.showToast = true
  }

  public onCloseCompareClicked = () => {
    this.athleteIdsToCompare = []
    this.showToast = false
  }
}