"use client"

import { useState } from "react"
import { getResultSearchResults } from "../_api/serverActions"
import { IrpSearchResult } from "../_api/results/definitions"

export default function ResultSearch({ raceId, courseId }: { raceId: string | number; courseId?: string | number }) {
  const [searchTerm, setSearchTerm] = useState("")
  const [results, setResults] = useState<IrpSearchResult[] | null>(null)
  const [isFocus, setIsFocus] = useState<boolean>(false)

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
      return <div>There were no results for {searchTerm}</div>
    }

    return results.map((result) => (
      <div className="mb-2" key={result.id}>
        <div>
          <a className="hover:underline" href={`/athletes/${result.athleteId}`}>
            {result.firstName} {result.lastName}
          </a>
        </div>
        <div>
          <a className="hover:underline" href={`/results/${result.id}`}>
            {result.bib}
          </a>
          {!courseId ? <span className="ml-2 text-green-500">({result.courseName})</span> : null}
        </div>
      </div>
    ))
  }

  return (
    <div className="relative flex flex-col items-end group">
      <input
        type="text"
        placeholder="bib or name"
        className="shad-cn p-2 border rounded shadow-md w-[300px]"
        value={searchTerm}
        onChange={handleInputChange}
        onFocus={() => setIsFocus(true)}
        onBlur={() => setIsFocus(false)}
      />
      {searchTerm.length > 0 && isFocus && (
        <div className="absolute top-full mt-2 bg-white border border-gray-300 rounded shadow-lg z-50 p-4 min-w-[350px] opacity-0 group-hover:opacity-100 transition-opacity">
          {results ? <Results results={results} /> : <>... Waiting for results</>}
        </div>
      )}
    </div>
  )
}
