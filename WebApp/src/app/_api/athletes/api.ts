import "server-only"

import { ApiFetch, getPostRequestInit } from "../api"
import { getLocationBasedSearchParams } from "../utils"
import { ArpDto, AthleteSearchResultDto, AthleteCompareDto } from "./definitions"

const baseUrl = "athletes"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<ArpDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  compare: async (athleteIds: string[]): Promise<AthleteCompareDto[]> => {
    const response = await apiFetch(`${baseUrl}/compare`, getPostRequestInit(athleteIds))
    return await response.json()
  },
  search: async (
    locationId?: number,
    locationType?: string,
    searchTerm?: string,
  ): Promise<AthleteSearchResultDto[]> => {
    const searchParams = getLocationBasedSearchParams({ locationId, locationType, searchTerm })
    const url = `${baseUrl}/search?${searchParams}`
    const response = await apiFetch(url)
    return await response.json()
  },
})
