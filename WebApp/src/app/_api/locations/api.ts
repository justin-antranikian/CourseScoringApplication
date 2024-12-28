"server only"

import { ApiFetch } from "../api"

const baseUrl = "locations"

export default (apiFetch: ApiFetch) => ({
  bySlug: async (slug: string): Promise<Response> => {
    return await apiFetch(`${baseUrl}/by-slug?slug=${slug}`)
  },
})
