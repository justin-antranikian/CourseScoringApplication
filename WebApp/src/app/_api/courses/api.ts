"server only"

import { ApiFetch } from "../api"
import { CourseLeaderboardDto, PodiumAward } from "./definitions"

const baseUrl = "courses"

export default (apiFetch: ApiFetch) => ({
  details: async (courseId: string | number): Promise<CourseLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${courseId}`)
    return await response.json()
  },
  awards: async (courseId: string | number): Promise<PodiumAward[]> => {
    const response = await apiFetch(`${baseUrl}/${courseId}/awards`)
    return await response.json()
  },
})
