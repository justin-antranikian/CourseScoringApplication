"server only"

import { ApiFetch } from "../api"
import { RaceLeaderboardDto, EventSearchResultDto } from "./definitions"

const baseUrl = "races"

export default (apiFetch: ApiFetch) => ({
  details: async (id: string | number): Promise<RaceLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${id}`)
    return await response.json()
  },
  search: async (): Promise<EventSearchResultDto[]> => {
    const response = await apiFetch(`${baseUrl}/search`)
    return await response.json()
  },
  bySlug: async (slug: string): Promise<Response> => {
    return await apiFetch(`${baseUrl}/by-slug?slug=${slug}`)
  },
})
