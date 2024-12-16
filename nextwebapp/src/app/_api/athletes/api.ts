"server only"

import { ApiFetch, getPostRequestInit } from "../api"
import { ArpDto, AthleteSearchResultDto, CompareAthletesAthleteInfoDto } from "./definitions"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<ArpDto> => {
    const response = await apiFetch(`arpApi/${id}`)
    return await response.json()
  },
  search: async (): Promise<AthleteSearchResultDto[]> => {
    const response = await apiFetch("athleteSearchApi")
    return await response.json()
  },
  compare: async (athleteIds: string[]): Promise<CompareAthletesAthleteInfoDto[]> => {
    const response = await apiFetch("compareAthletesApi", getPostRequestInit({ athleteIds }))
    return await response.json()
  },
})
