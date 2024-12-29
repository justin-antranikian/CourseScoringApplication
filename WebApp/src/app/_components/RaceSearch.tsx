"use client"

import { useState } from "react"
import { searchRaces } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { EventSearchResultDto } from "../_api/races/definitions"

export default function RaceSearch({ locationId, locationType }: { locationId?: number; locationType?: string }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [results, setResults] = useState<EventSearchResultDto[]>([])

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const results = await searchRaces(locationId, locationType, searchTerm)
    setResults(results)
  }

  const Results = ({ results }: { results: EventSearchResultDto[] }) => {
    if (results.length === 0) {
      return (
        <div>
          There were no results found for: <strong>{searchTerm}</strong>
        </div>
      )
    }

    return results.map((result) => (
      <div className="mb-2" key={result.id}>
        <div className="font-bold">
          <a className="hover:underline" href={`/races/${result.upcomingRaceId}`}>
            {result.name}
          </a>
        </div>
        <div className="text-green-500 text-sm">{result.courses[0].displayName}</div>
      </div>
    ))
  }

  return (
    <div className="grid justify-items-end">
      <div className="relative group w-80">
        <Input placeholder="name" value={searchTerm} onChange={handleInputChange} />
        {searchTerm === "" ? null : (
          <div className="absolute top-full left-0 z-50 w-full p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200 max-h-[400px] overflow-y-auto">
            <Results results={results} />
          </div>
        )}
      </div>
    </div>
  )
}
