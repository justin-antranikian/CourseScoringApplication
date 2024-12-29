"server only"

import { ApiFetch, getPostRequestInit } from "../api"
import { ArpDto, AthleteSearchResultDto, CompareAthletesAthleteInfoDto } from "./definitions"

const baseUrl = "athletes"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<ArpDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  compare: async (athleteIds: string[]): Promise<CompareAthletesAthleteInfoDto[]> => {
    const response = await apiFetch(`${baseUrl}/compare`, getPostRequestInit(athleteIds))
    return await response.json()
  },
  search: async (
    locationId?: number,
    locationType?: string,
    searchTerm?: string,
  ): Promise<AthleteSearchResultDto[]> => {
    const params = new URLSearchParams()

    if (locationId) {
      params.append("locationId", locationId.toString())
    }
    if (locationType) {
      params.append("locationType", locationType)
    }
    if (searchTerm) {
      params.append("searchTerm", searchTerm)
    }

    const url = `${baseUrl}/search?${params.toString()}`
    const response = await apiFetch(url)
    return await response.json()
  },
})
