
export interface NavigationItem {
  displayName: string
  displayNameUrl: string
  count: number
  isOpen: boolean
  isHighlighted: boolean
  items: NavigationItem[]
}

export interface DashboardInfoResponseDto {
  states: NavigationItem[]
  areas: NavigationItem[] | null
  stateNavigationItem: NavigationItem | null
  title: string | null
  titleUrl: string | null
  description: string | null
}