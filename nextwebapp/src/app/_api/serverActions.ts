"use server"

import { useApi } from "./api"

const api = useApi()

export const getIrp = (athleteCourseId: string | number) => api.results.details(athleteCourseId)

export const getRaceLeaderboard = (raceId: string | number) => api.races.leaderboard(raceId)

export const getAthleteDetails = (athleteId: string | number) => api.athletes.details(athleteId)
