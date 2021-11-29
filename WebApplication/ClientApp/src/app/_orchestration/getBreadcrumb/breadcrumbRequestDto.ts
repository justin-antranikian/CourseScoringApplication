import { removeUndefinedKeyValues } from "src/app/_common/jsonHelpers"

export enum BreadcrumbNavigationLevel {
  All,
  State,
  Area,
  City,
  ArpOrRaceSeriesDashboard,
  RaceLeaderboard,
  CourseLeaderboard,
  Irp
}

export class BreadcrumbRequestDto {

  constructor(
    public breadcrumbNavigationLevel: BreadcrumbNavigationLevel,
    public searchTerm: string,
  ) {}

  public getAsParamsObject = () => {

    return removeUndefinedKeyValues({
      breadcrumbNavigationLevel: this.breadcrumbNavigationLevel,
      searchTerm: this.searchTerm,
    })
  }
}