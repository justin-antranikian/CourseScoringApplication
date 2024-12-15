"server only"

import { PaceWithTime } from "../_components/IntervalTime"
import { CourseLeaderboardDto } from "../courses/[id]/definitions"
import { ApiFetch } from "./api"

export interface PodiumAward {
  bracketName: string
  firstPlaceAthlete: AwardWinnerDto | null
  secondPlaceAthlete: AwardWinnerDto | null
  thirdPlaceAthlete: AwardWinnerDto | null
}

export interface AwardWinnerDto {
  athleteId: number
  athleteCourseId: number
  fullName: string
  finishTime: string
  paceWithTime: PaceWithTime
}

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
