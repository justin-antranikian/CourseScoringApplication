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
    const response = await apiFetch(`${baseUrl}/compare`, getPostRequestInit({ athleteIds }))
    return await response.json()
  },
  search: async (): Promise<AthleteSearchResultDto[]> => {
    const response = await apiFetch(`${baseUrl}/search`)
    return await response.json()
  },
  bySlug: async (slug: string): Promise<Response> => {
    return await apiFetch(`${baseUrl}/by-slug?slug=${slug}`)
  },
})
