"server only"

import { EventSearchResultDto } from "../events/definitions"
import { RaceLeaderboardDto } from "../races/[id]/definitions"
import { ApiFetch } from "./api"

export default (apiFetch: ApiFetch) => ({
  leaderboard: async (id: string | number): Promise<RaceLeaderboardDto> => {
    const response = await apiFetch(`raceLeaderboardApi/${id}`)
    return await response.json()
  },
  search: async (): Promise<EventSearchResultDto[]> => {
    const response = await apiFetch("raceSeriesSearchApi")
    return await response.json()
  },
})
