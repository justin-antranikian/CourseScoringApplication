"use client"

import { useState } from "react"
import { searchAthletes } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { AthleteSearchResultDto } from "../_api/athletes/definitions"

export default function AthleteSearch({ locationId, locationType }: { locationId?: number; locationType?: string }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [athletes, setAthletes] = useState<AthleteSearchResultDto[]>([])

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const athletes = await searchAthletes(locationId, locationType, searchTerm)
    setAthletes(athletes)
  }

  const Results = ({ results }: { results: AthleteSearchResultDto[] }) => {
    if (results.length === 0) {
      return (
        <div>
          There were no results found for: <strong>{searchTerm}</strong>
        </div>
      )
    }

    return (
      <>
        <div className="font-bold mb-2 underline">Athletes ({athletes.length})</div>
      </>
    )

    return results.map((result) => (
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
    ))
  }

  return (
    <div className="grid justify-items-end">
      <div className="relative group w-80">
        <Input placeholder="name" value={searchTerm} onChange={handleInputChange} />
        {searchTerm === "" ? null : (
          <div className="absolute top-full left-0 z-50 w-full p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200 max-h-[400px] overflow-y-auto">
            <Results results={athletes} />
          </div>
        )}
      </div>
    </div>
  )
}
