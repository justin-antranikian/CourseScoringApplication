"server only"

import { ApiFetch } from "../api"
import { RaceLeaderboardDto, EventSearchResultDto } from "./definitions"

const baseUrl = "races"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<RaceLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  search: async (locationId?: number, locationType?: string, searchTerm?: string): Promise<EventSearchResultDto[]> => {
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
