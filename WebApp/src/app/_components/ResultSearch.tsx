"use client"

import { useState } from "react"
import { getResultSearchResults } from "../_api/serverActions"
import { IrpSearchResult } from "../_api/results/definitions"
import { Input } from "@/components/ui/input"

export default function ResultSearch({ raceId, courseId }: { raceId: string | number; courseId?: string | number }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [results, setResults] = useState<IrpSearchResult[]>([])

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const results = await getResultSearchResults({
      raceId,
      courseId,
      searchTerm,
    })

    setResults(results)
  }

  const Results = ({ results }: { results: IrpSearchResult[] }) => {
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
          <a className="hover:underline" href={`/athletes/${result.athleteId}`}>
            {result.firstName} {result.lastName}
          </a>
        </div>
        <div>
          <a className="hover:underline" href={`/results/${result.id}`}>
            {result.gender} | {result.bib}
          </a>
        </div>
        <div className="text-green-500 text-sm">{result.courseName}</div>
      </div>
    ))
  }

  return (
    <div className="grid justify-items-end">
      <div className="relative group w-80">
        <Input placeholder="bib or name" value={searchTerm} onChange={handleInputChange} />
        <div className="absolute top-full left-0 z-50 w-full p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200">
          {searchTerm === "" ? "Please enter the search term" : <Results results={results} />}
        </div>
      </div>
    </div>
  )
}
