export interface LocationDto {
  id: number
  isSelected: boolean
  isExpanded: boolean
  name: string
  slug: string
  childLocations: LocationDto[]
}
