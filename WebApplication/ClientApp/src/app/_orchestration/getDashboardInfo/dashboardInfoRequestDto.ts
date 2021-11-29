import { removeUndefinedKeyValues } from "../../_common/jsonHelpers"

export enum DashboardInfoType {
  Events,
  Athletes,
}

export enum DashboardInfoLocationType {
  All,
  State,
  Area,
  City
}

export class DashboardInfoRequestDto {

  constructor(
    private dashboardInfoType: DashboardInfoType,
    private dashboardInfoLocationType: DashboardInfoLocationType,
    private locationTermUrlFriendly: string | null = null,
  ) {}

  public getAsParamsObject = () => {

    const params = {
      dashboardInfoType: this.dashboardInfoType,
      dashboardInfoLocationType: this.dashboardInfoLocationType,
      locationTermUrlFriendly: this.locationTermUrlFriendly,
    }

    return removeUndefinedKeyValues(params)
  }
}