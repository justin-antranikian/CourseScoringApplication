"use server"

import { getApi } from "./api"
import { IrpSearchRequest } from "./results/definitions"

const api = getApi()

export const getIrp = async (athleteCourseId: string | number) => await api.results.details(athleteCourseId)

export const getRaceLeaderboard = async (raceId: string | number) => await api.races.details(raceId)

export const getAthleteDetails = async (athleteId: string | number) => await api.athletes.details(athleteId)

export const searchResults = async (request: IrpSearchRequest) => await api.results.search(request)

export const searchRaces = async (locationId?: number, locationType?: string, searchTerm?: string) => {
  return await api.races.search(locationId, locationType, searchTerm)
}

export const searchAthletes = async (locationId?: number, locationType?: string, searchTerm?: string) => {
  return await api.athletes.search(locationId, locationType, searchTerm)
}
