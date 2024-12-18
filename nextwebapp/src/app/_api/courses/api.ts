"server only"

import { ApiFetch } from "../api"
import { CourseLeaderboardDto, PodiumAward } from "./definitions"

export default (apiFetch: ApiFetch) => ({
  awards: async (courseId: string | number): Promise<PodiumAward[]> => {
    const response = await apiFetch(`awardsPodiumApi/${courseId}`)
    return await response.json()
  },
  leaderboard: async (courseId: string | number): Promise<CourseLeaderboardDto> => {
    const response = await apiFetch(`courseLeaderboardApi/${courseId}`)
    return await response.json()
  },
})
