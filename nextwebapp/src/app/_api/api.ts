"server only"

import { config } from "@/config"
import athletes from "./athletes"
import results from "./results"
import races from "./races"
import courses from "./courses"

export type ApiFetch = (url: string, requestInit?: RequestInit) => Promise<Response>

export const getPostRequestInit = (body: any): RequestInit => {
  const requestInit: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body),
  }

  return requestInit
}

const { apiHost } = config

export const apiCaller = () => {
  const apiFetch = (url: string, requestInit?: RequestInit) => {
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
  }
}
