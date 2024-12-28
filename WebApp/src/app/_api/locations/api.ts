"server only"

import { ApiFetch } from "../api"
import { LocationDto } from "./definitions"

const baseUrl = "locations"

export default (apiFetch: ApiFetch) => ({
  bySlug: async (slug: string): Promise<Response> => {
    return await apiFetch(`${baseUrl}/by-slug?slug=${slug}`)
  },
  directory: async (): Promise<LocationDto[]> => {
    const response = await apiFetch(baseUrl)
    return await response.json()
  },
})
