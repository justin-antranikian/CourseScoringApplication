export interface LocationDto {
  name: string
  slug: string
  childLocations: LocationDto[]
}
