import "server-only"

import { ApiFetch } from "../api"
import { getLocationBasedSearchParams } from "../utils"
import { RaceLeaderboardDto, RaceSearchResultDto } from "./definitions"

const baseUrl = "races"

export interface SearchParams {
  locationId?: number
  locationType?: string
  searchTerm?: string
  latitude?: number
  longitude?: number
}

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<RaceLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  search: async (searchParams: SearchParams): Promise<RaceSearchResultDto[]> => {
    const url = `${baseUrl}/search?${getLocationBasedSearchParams(searchParams)}`
    const response = await apiFetch(url)
    return await response.json()
  },
})
