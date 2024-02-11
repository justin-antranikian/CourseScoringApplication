import { removeUndefinedKeyValues } from "../_common/jsonHelpers"

export class SearchAthletesRequestDto {
  constructor(
    public state: string | null,
    public area: string | null,
    public city: string | null,
    public searchTerm: string | null,
  ) {}

  public getAsParamsObject = () => {

    const params = {
      searchTerm: this.searchTerm,
      state: this.state,
      area: this.area,
      city: this.city,
    }

    return removeUndefinedKeyValues(params)
  }
}