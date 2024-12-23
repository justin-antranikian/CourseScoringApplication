"server only"

import { ApiFetch } from "../api"
import { CourseLeaderboardDto, AwardsDto } from "./definitions"

const baseUrl = "courses"

export default (apiFetch: ApiFetch) => ({
  details: async (courseId: string | number): Promise<CourseLeaderboardDto> => {
    const response = await apiFetch(`${baseUrl}/${courseId}`)
    return await response.json()
  },
  awards: async (courseId: string | number): Promise<AwardsDto[]> => {
    const response = await apiFetch(`${baseUrl}/${courseId}/awards`)
    return await response.json()
  },
})
