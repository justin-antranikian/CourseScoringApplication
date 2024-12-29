"use server"

import { getApi } from "./api"
import { IrpSearchRequest } from "./results/definitions"

const api = getApi()

export const getIrp = (athleteCourseId: string | number) => api.results.details(athleteCourseId)

export const getRaceLeaderboard = (raceId: string | number) => api.races.details(raceId)

export const getAthleteDetails = (athleteId: string | number) => api.athletes.details(athleteId)

export const getResultSearchResults = (request: IrpSearchRequest) => api.results.search(request)

export const searchRaces = (locationId?: number, locationType?: string, searchTerm?: string) => {
  return api.races.search(locationId, locationType, searchTerm)
}

export const searchAthletes = (locationId?: number, locationType?: string, searchTerm?: string) => {
  return api.athletes.search(locationId, locationType, searchTerm)
}
