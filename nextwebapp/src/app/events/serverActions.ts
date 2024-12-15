"use server"

import { apiCaller } from "../_api/api"

const api = apiCaller()

export const getLeaderboard = (id: string | number) => {
  return api.races.leaderboard(id)
}
