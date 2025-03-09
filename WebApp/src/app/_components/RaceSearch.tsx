"use client"

import { useCallback, useState } from "react"
import { searchRaces } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { RaceSearchResultDto } from "../_api/races/definitions"
import SearchResults, { NoResults } from "./SearchResults"
import { debounce } from "lodash"

export default function RaceSearch({ locationId, locationType }: { locationId?: number; locationType?: string }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState("")
  const [races, setRaces] = useState<RaceSearchResultDto[]>([])

  const fetchData = useCallback(
    debounce(async (searchTerm: string) => {
      setDebouncedSearchTerm(searchTerm)
      const races = searchTerm === "" ? [] : await searchRaces(locationId, locationType, searchTerm)
      setRaces(races)
    }, 300),
    [],
  )

  const handleInputChange = async ({ target: { value: searchTerm } }: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(searchTerm)
    await fetchData(searchTerm)
  }

  const Results = () => {
    if (races.length === 0) {
      return <NoResults searchTerm={debouncedSearchTerm} />
    }

    return (
      <>
        <div className="font-bold mb-2 underline">Races ({races.length})</div>
        {races.map((race, index) => (
          <div className="mb-2" key={index}>
            <div className="font-bold">
              <a className="hover:underline" href={`/races/${race.upcomingRaceId}`}>
                {race.name}
              </a>
            </div>
            <div>{race.raceKickOffDate}</div>
            <div className="text-green-500 text-sm">
              {race.locationInfoWithRank.city}, {race.locationInfoWithRank.state}
            </div>
          </div>
        ))}
      </>
    )
  }

  return (
    <div className="w-80">
      <SearchResults
        searchTerm={debouncedSearchTerm}
        inputComponent={<Input placeholder="name" value={searchTerm} onChange={handleInputChange} />}
      >
        <Results />
      </SearchResults>
    </div>
  )
}
