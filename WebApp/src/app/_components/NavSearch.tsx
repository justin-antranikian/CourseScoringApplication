"use client"

import { useState } from "react"
import { searchAthletes, searchRaces } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { AthleteSearchResultDto } from "../_api/athletes/definitions"
import { EventSearchResultDto } from "../_api/races/definitions"

export default function NavSearch() {
  const [searchTerm, setSearchTerm] = useState("")
  const [athletes, setAthletes] = useState<AthleteSearchResultDto[]>([])
  const [races, setRaces] = useState<EventSearchResultDto[]>([])

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const athletes = await searchAthletes(undefined, undefined, searchTerm)
    const races = await searchRaces(undefined, undefined, searchTerm)

    setAthletes(athletes)
    setRaces(races)
  }

  const Results = () => {
    if (athletes.length === 0 && races.length === 0) {
      return (
        <div>
          There were no results found for: <strong>{searchTerm}</strong>
        </div>
      )
    }

    return (
      <div className="flex">
        <div className="flex-1">
          <div className="font-bold mb-2 underline">Races ({races.length})</div>
          {races.map((result) => (
            <div className="mb-2" key={result.id}>
              <div className="font-bold">
                <a className="hover:underline" href={`/races/${result.upcomingRaceId}`}>
                  {result.name}
                </a>
              </div>
              <div className="text-green-500 text-sm">{result.courses[0].displayName}</div>
            </div>
          ))}
        </div>
        <div className="flex-1">
          <div className="font-bold mb-2 underline">Athletes ({athletes.length})</div>
          {athletes.map((result) => {
            return (
              <div className="mb-2" key={result.id}>
                <div className="font-bold">
                  <a className="hover:underline" href={`/athletes/${result.id}`}>
                    {result.fullName}
                  </a>
                </div>
                <div>
                  {result.genderAbbreviated} | {result.age}
                </div>
                <div className="text-green-500 text-sm">
                  {result.locationInfoWithRank.city}, {result.locationInfoWithRank.state}
                </div>
              </div>
            )
          })}
        </div>
      </div>
    )

    return
  }

  return (
    <div className="grid justify-items-end">
      <div className="relative group">
        <Input placeholder="athlete or race" className="h-7 w-60" value={searchTerm} onChange={handleInputChange} />
        {searchTerm === "" ? null : (
          <div className="absolute top-full right-0 z-50 w-96 p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200 max-h-[400px] overflow-y-auto">
            <Results />
          </div>
        )}
      </div>
    </div>
  )
}
