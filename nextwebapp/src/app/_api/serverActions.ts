"use server"

import { apiCaller } from "./api"

const api = apiCaller()

export const getIrp = (athleteCourseId: string | number) => {
  return api.results.details(athleteCourseId)
}
