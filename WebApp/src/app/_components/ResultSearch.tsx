"use client"

import { useCallback, useState } from "react"
import { searchResults } from "../_api/serverActions"
import { IrpSearchResult } from "../_api/results/definitions"
import { Input } from "@/components/ui/input"
import SearchResults, { NoResults } from "./SearchResults"
import { debounce } from "lodash"

export default function ResultSearch({ raceId, courseId }: { raceId: string | number; courseId?: string | number }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState("")
  const [results, setResults] = useState<IrpSearchResult[]>([])

  const fetchData = useCallback(
    debounce(async (searchTerm: string) => {
      setDebouncedSearchTerm(searchTerm)
      const results = searchTerm === "" ? [] : await searchResults({ raceId, courseId, searchTerm })
      setResults(results)
    }, 300),
    [],
  )

  const handleInputChange = async ({ target: { value: searchTerm } }: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(searchTerm)
    await fetchData(searchTerm)
  }

  const Results = () => {
    if (results.length === 0) {
      return <NoResults searchTerm={debouncedSearchTerm} />
    }

    return (
      <>
        <div className="font-bold mb-2 underline">Results ({results.length})</div>
        {results.map((result) => (
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
        ))}
      </>
    )
  }

  return (
    <div className="w-80">
      <SearchResults
        searchTerm={debouncedSearchTerm}
        inputComponent={<Input placeholder="bib or name" value={searchTerm} onChange={handleInputChange} />}
      >
        <Results />
      </SearchResults>
    </div>
  )
}
