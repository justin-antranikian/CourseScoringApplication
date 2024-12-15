"server only"

import { config } from "@/config"
import athletes from "./athletes"

export type ApiFetch = (
  url: string,
  requestInit?: RequestInit,
) => Promise<Response>

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
  }
}
