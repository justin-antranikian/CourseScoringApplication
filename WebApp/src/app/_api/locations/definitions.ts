export interface LocationDto {
  id: number
  isSelected: boolean
  isExpanded: boolean
  locationType: string
  name: string
  slug: string
  childLocations: LocationDto[]
}
