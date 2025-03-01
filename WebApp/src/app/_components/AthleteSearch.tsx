"use client"

import { useState } from "react"
import { searchAthletes } from "../_api/serverActions"
import { Input } from "@/components/ui/input"
import { AthleteSearchResultDto } from "../_api/athletes/definitions"
import SearchResults, { NoResults } from "./SearchResults"

export default function AthleteSearch({ locationId, locationType }: { locationId?: number; locationType?: string }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [athletes, setAthletes] = useState<AthleteSearchResultDto[]>([])

  const handleInputChange = async ({ target: { value: searchTerm } }: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(searchTerm)
    const athletes = searchTerm === "" ? [] : await searchAthletes(locationId, locationType, searchTerm)
    setAthletes(athletes)
  }

  const Results = () => {
    if (athletes.length === 0) {
      return <NoResults searchTerm={searchTerm} />
    }

    return (
      <>
        <div className="font-bold mb-2 underline">Athletes ({athletes.length})</div>
        {athletes.map((athlete, index) => {
          return (
            <div className="mb-2" key={index}>
              <div className="font-bold">
                <a className="hover:underline" href={`/athletes/${athlete.id}`}>
                  {athlete.fullName}
                </a>
              </div>
              <div>
                {athlete.genderAbbreviated} | {athlete.age}
              </div>
              <div className="text-green-500 text-sm">
                {athlete.locationInfoWithRank.city}, {athlete.locationInfoWithRank.state}
              </div>
            </div>
          )
        })}
      </>
    )
  }

  return (
    <div className="w-80">
      <SearchResults
        searchTerm={searchTerm}
        inputComponent={<Input placeholder="name" value={searchTerm} onChange={handleInputChange} />}
      >
        <Results />
      </SearchResults>
    </div>
  )
}
