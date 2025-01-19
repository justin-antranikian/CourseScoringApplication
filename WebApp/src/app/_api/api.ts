import "server-only"

import athletes from "./athletes/api"
import results from "./results/api"
import races from "./races/api"
import courses from "./courses/api"
import locations from "./locations/api"

export type ApiFetch = (url: string, requestInit?: RequestInit) => Promise<Response>

export const getPostRequestInit = <T>(body: T): RequestInit => ({
  method: "POST",
  headers: {
    "Content-Type": "application/json",
  },
  body: JSON.stringify(body),
})

export const getApi = () => {
  const apiFetch = (url: string, requestInit?: RequestInit): Promise<Response> => {
    const apiHost = process.env["API_HOST"]!

    return fetch(`${apiHost}/${url}`, {
      cache: "no-store",
      ...requestInit,
    })
  }

  return {
    athletes: athletes(apiFetch),
    results: results(apiFetch),
    races: races(apiFetch),
    courses: courses(apiFetch),
    locations: locations(apiFetch),
  }
}
