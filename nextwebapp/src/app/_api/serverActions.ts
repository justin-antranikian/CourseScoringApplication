"use server"

import { apiCaller } from "./api"

const api = apiCaller()

export const getIrp = (athleteCourseId: string | number) => api.results.details(athleteCourseId)

export const getLeaderboard = (id: string | number) => api.races.leaderboard(id)

export const getAthleteDetails = (id: string | number) => api.athletes.details(id)
