"server only"

import { ApiFetch, getPostRequestInit } from "../api"
import { CompareIrpsAthleteInfoDto, Irp, ResultSearchRequest, ResultSearchResponse } from "./definitions"

const baseUrl = "results"

export default (apiFetch: ApiFetch) => ({
  details: async (athleteCourseId: string | number): Promise<Irp> => {
    const response = await apiFetch(`${baseUrl}/${athleteCourseId}`)
    return await response.json()
  },
  compare: async (athleteCourseIds: string[]): Promise<CompareIrpsAthleteInfoDto[]> => {
    const response = await apiFetch(`${baseUrl}/compare`, getPostRequestInit({ athleteCourseIds }))
    return await response.json()
  },
  search: async (request: ResultSearchRequest): Promise<ResultSearchResponse[]> => {
    const response = await apiFetch(`${baseUrl}/search`, getPostRequestInit(request))
    return await response.json()
  },
})
