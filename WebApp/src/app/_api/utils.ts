export const getLocationBasedSearchParams = (locationId?: number, locationType?: string, searchTerm?: string) => {
  const params = new URLSearchParams()

  if (locationId) {
    params.append("locationId", locationId.toString())
  }
  if (locationType) {
    params.append("locationType", locationType)
  }
  if (searchTerm) {
    params.append("searchTerm", searchTerm)
  }

  return params.toString()
}
