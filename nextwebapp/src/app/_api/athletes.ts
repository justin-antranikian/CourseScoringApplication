"server only"

import { ArpDto } from "../athletes/[id]/definitions"
import { CompareAthletesAthleteInfoDto } from "../athletes/compare/definitions"
import { AthleteSearchResultDto } from "../athletes/definitions"
import { ApiFetch, getPostRequestInit } from "./api"

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
