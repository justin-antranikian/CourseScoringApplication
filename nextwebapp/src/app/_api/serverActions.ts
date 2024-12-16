"use server"

import { apiCaller } from "./api"

const api = apiCaller()

export const getIrp = (athleteCourseId: string | number) => {
  return api.results.details(athleteCourseId)
}

export const getLeaderboard = (id: string | number) => {
  return api.races.leaderboard(id)
}
