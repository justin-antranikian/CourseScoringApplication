"use server"

import { apiCaller } from "../_api/api"

const api = apiCaller()

export const getAthleteDetails = (id: string | number) => {
  return api.athletes.details(id)
}

export const getIrp = (id: string | number) => {
  return api.results.details(id)
}
