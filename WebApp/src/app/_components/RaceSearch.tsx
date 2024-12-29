"use client"

import { useState } from "react"
import { searchRaces } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { EventSearchResultDto } from "../_api/races/definitions"

export default function RaceSearch({ locationId, locationType }: { locationId?: number; locationType?: string }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [races, setRaces] = useState<EventSearchResultDto[]>([])

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const getRaces = async () => {
      if (searchTerm === "") {
        return []
      }

      return await searchRaces(locationId, locationType, searchTerm)
    }

    const races = await getRaces()
    setRaces(races)
  }

  const Results = () => {
    if (races.length === 0) {
      return (
        <div>
          There were no results found for: <strong>{searchTerm}</strong>
        </div>
      )
    }

    return (
      <>
        <div className="font-bold mb-2 underline">Races ({races.length})</div>
        {races.map((race) => {
          return (
            <div className="mb-2" key={race.id}>
              <div className="font-bold">
                <a className="hover:underline" href={`/races/${race.upcomingRaceId}`}>
                  {race.name}
                </a>
              </div>
              <div>{race.courses[0].displayName}</div>
              <div className="text-green-500 text-sm">
                {race.locationInfoWithRank.city}, {race.locationInfoWithRank.state}
              </div>
            </div>
          )
        })}
      </>
    )
  }

  return (
    <div className="grid justify-items-end">
      <div className="relative group w-80">
        <Input placeholder="name" value={searchTerm} onChange={handleInputChange} />
        {searchTerm === "" ? null : (
          <div className="absolute top-full left-0 z-50 w-full p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200 max-h-[400px] overflow-y-auto">
            <Results />
          </div>
        )}
      </div>
    </div>
  )
}
