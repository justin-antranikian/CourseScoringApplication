export interface DirectoryDto {
  id: number
  isSelected: boolean
  isExpanded: boolean
  locationType: string
  name: string
  slug: string
  childLocations: DirectoryDto[]
}

export interface LocationDto {
  id: number
  locationType: string
  name: string
  slug: string
}
