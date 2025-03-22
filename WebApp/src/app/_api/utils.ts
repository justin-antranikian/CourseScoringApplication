import { SearchParams } from "./races/api"

export const getLocationBasedSearchParams = (searchParams: SearchParams) => {
  const params = new URLSearchParams()

  Object.entries(searchParams).forEach(([key, value]) => {
    if (value !== undefined) {
      params.append(key, value.toString())
    }
  })

  return params.toString()
}
