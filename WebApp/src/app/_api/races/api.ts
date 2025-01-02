"server only"

import { ApiFetch } from "../api"
import { getLocationBasedSearchParams } from "../utils"
import { RaceLeaderboardDto, RaceSearchResultDto } from "./definitions"

const baseUrl = "races"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<RaceLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  search: async (locationId?: number, locationType?: string, searchTerm?: string): Promise<RaceSearchResultDto[]> => {
    const searchParams = getLocationBasedSearchParams(locationId, locationType, searchTerm)
    const url = `${baseUrl}/search?${searchParams}`
    const response = await apiFetch(url)
    return await response.json()
  },
})
