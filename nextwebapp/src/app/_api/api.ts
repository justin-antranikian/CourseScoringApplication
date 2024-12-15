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

export const apiCaller = () => {
  const apiFetch = (url: string, requestInit?: RequestInit) => {
    const _requestInit: RequestInit = {
      cache: "no-store",
      ...requestInit,
    }

    return fetch(`${config.apiHost}/${url}`, _requestInit)
  }

  return {
    athletes: athletes(apiFetch),
    results: results(apiFetch),
    races: races(apiFetch),
    courses: courses(apiFetch),
  }
}
