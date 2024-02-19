import { removeUndefinedKeyValues } from "../../_common/jsonHelpers"

export enum SearchOnField {
  Bib,
  FirstName,
  LastName,
  FullName
}

export class SearchIrpsRequestDto {
  constructor(
    public searchOn: SearchOnField,
    public searchTerm: string,
    public raceId: number | null,
    public courseId: number | null
  ) {}

  public getAsParamsObject = () => {

    const params = {
      searchOn: this.searchOn,
      searchTerm: this.searchTerm,
      raceId: this.raceId,
      courseId: this.courseId,
    }

    return removeUndefinedKeyValues(params)
  }
}