"server only"

import { CompareIrpsAthleteInfoDto } from "../compare-results/definitions"
import { Irp } from "../results/[id]/definitions"
import { ApiFetch, getPostRequestInit } from "./api"

export default (apiFetch: ApiFetch) => ({
  details: async (athleteCourseId: string | number): Promise<Irp> => {
    const response = await apiFetch(`irpApi/${athleteCourseId}`)
    return await response.json()
  },
  compare: async (athleteCourseIds: string[]): Promise<CompareIrpsAthleteInfoDto[]> => {
    const response = await apiFetch("compareIrpApi", getPostRequestInit({ athleteCourseIds }))
    return await response.json()
  },
})
