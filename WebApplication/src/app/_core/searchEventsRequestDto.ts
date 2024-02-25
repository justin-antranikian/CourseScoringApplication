import { removeUndefinedKeyValues } from "../_common/jsonHelpers"
import { RaceSeriesType } from "./raceSeriesType"

export class SearchEventsRequestDto {
  constructor(
    public searchTerm: string | null,
    public state: string | null = null,
    public area: string | null = null,
    public city: string | null = null,
    public raceSeriesTypes: RaceSeriesType[] = [],
  ) {}

  public getAsParamsObject = () => {

    const params = {
      searchTerm: this.searchTerm,
      raceSeriesTypes: this.raceSeriesTypes,
      state: this.state,
      area: this.area,
      city: this.city,
    }

    return removeUndefinedKeyValues(params)
  }
}