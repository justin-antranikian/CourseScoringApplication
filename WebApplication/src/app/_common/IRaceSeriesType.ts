import { RaceSeriesType, RaceSeriesTypeToImageUrl } from "../_core/enums/raceSeriesType"

/**
 * raceSeriesType comes from the server, while raceSeriesImageUrl is set on the client.
 */
export interface IRaceSeriesType {
  raceSeriesType: RaceSeriesType
  /** DOES NOT come from the server. Client is responsible for populating */
  raceSeriesImageUrl: string | undefined
}

/**
* Sets the raceSeriesImageUrl. Keeps the rest of the values.
*/
export const mapRaceSeriesTypeToImageUrl = <T extends IRaceSeriesType>(value: T): T => ({
  ...value,
  raceSeriesImageUrl: RaceSeriesTypeToImageUrl.getImageUrl(value.raceSeriesType)
})
