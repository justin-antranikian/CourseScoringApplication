import { RaceSeriesType } from "../_core/enums/raceSeriesType"
import { removeUndefinedKeyValues } from "./jsonHelpers"

export class RaceTypeFilterModel {
  constructor (
    public running: boolean = true,
    public triathalon: boolean = true,
    public roadBiking: boolean = true,
    public mountainBiking: boolean = true,
    public crossCountrySkiing: boolean = true,
    public swim: boolean = true,
  ) {}

  /**
   * Gets the selected seriesType filters from the user as an array of RaceSeriesType.
   */
  public getSelectedTypes = (): RaceSeriesType[] => {
    const selectedTypes = [
      this.mapRunning(),
      this.mapTriathalon(),
      this.mapRoadBiking(),
      this.mapMountainBiking(),
      this.mapCrossCountrySkiing(),
      this.mapSwim()
    ]

    return selectedTypes.filter(oo => oo !== null)
  }

  public getSelectedTypesValues = (): any => {
    const selectedTypes = {
      running: this.running,
      triathalon: this.triathalon,
      roadBiking: this.roadBiking,
      mountainBiking: this.mountainBiking,
      crossCountrySkiing: this.crossCountrySkiing,
      swim: this.swim
    }

    return removeUndefinedKeyValues(selectedTypes)
  }

  public setFilterValue = (selected: boolean) => {
    this.running = this.triathalon = this.roadBiking = this.mountainBiking = this.crossCountrySkiing = this.swim = selected
  }

  private mapRunning = (): RaceSeriesType | null => this.running ? RaceSeriesType.Running : null
  private mapTriathalon = (): RaceSeriesType | null => this.triathalon ? RaceSeriesType.Triathalon : null
  private mapRoadBiking = (): RaceSeriesType | null => this.roadBiking ? RaceSeriesType.RoadBiking : null
  private mapMountainBiking = (): RaceSeriesType | null => this.mountainBiking ? RaceSeriesType.MountainBiking : null
  private mapCrossCountrySkiing = (): RaceSeriesType | null => this.crossCountrySkiing ? RaceSeriesType.CrossCountrySkiing : null
  private mapSwim = (): RaceSeriesType | null => this.swim ? RaceSeriesType.Swim : null
}
