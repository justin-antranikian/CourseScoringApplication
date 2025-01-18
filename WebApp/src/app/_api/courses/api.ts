import "server-only"

import { ApiFetch } from "../api"
import { AwardsDto, CourseDetailsDto, CourseLeaderboardByIntervalDto } from "./definitions"

const baseUrl = "courses"

export default (apiFetch: ApiFetch) => ({
  details: async (courseId: string | number): Promise<CourseDetailsDto> => {
    const response = await apiFetch(`${baseUrl}/${courseId}`)
    return await response.json()
  },
  leaderboard: async (courseId: string | number): Promise<CourseLeaderboardByIntervalDto[]> => {
    const response = await apiFetch(`${baseUrl}/${courseId}/leaderboard`)
    return await response.json()
  },
  awards: async (courseId: string | number): Promise<AwardsDto[]> => {
    const response = await apiFetch(`${baseUrl}/${courseId}/awards`)
    return await response.json()
  },
})
