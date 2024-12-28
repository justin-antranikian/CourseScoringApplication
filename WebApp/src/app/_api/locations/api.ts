"server only"

import { ApiFetch } from "../api"
import { LocationDto } from "./definitions"

const baseUrl = "locations"

export default (apiFetch: ApiFetch) => ({
  bySlug: async (slug: string): Promise<Response> => {
    return await apiFetch(`${baseUrl}/by-slug?slug=${slug}`)
  },
  directory: async (locationId?: number | null): Promise<LocationDto[]> => {
    const url = locationId != null ? `${baseUrl}/directory?locationId=${locationId}` : `${baseUrl}/directory`
    const response = await apiFetch(url)
    return await response.json()
  },
})
