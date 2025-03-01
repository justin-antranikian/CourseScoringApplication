import React from "react"
import { Button } from "@/components/ui/button"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"

interface Props {
  setShowComparePane: React.Dispatch<React.SetStateAction<boolean>>
  selectedResults: LeaderboardResultDto[]
  setSelectedResults: React.Dispatch<React.SetStateAction<LeaderboardResultDto[]>>
  url: string
}

export default function ComparePane({ setShowComparePane, selectedResults, setSelectedResults, url }: Props) {
  const handleCompareClicked = (irp: LeaderboardResultDto) => {
    const filteredResults = selectedResults.filter(({ athleteCourseId }) => athleteCourseId !== irp.athleteCourseId)
    setSelectedResults(filteredResults)
  }

  return (
    <div className="fixed bottom-0 left-0 py-2 w-full bg-gray-200 text-black px-4">
      <div className="flex justify-around items-center">
        <div className="flex gap-2 items-center">
          <div
            onClick={() => setShowComparePane(false)}
            className="border border-gray-400 p-2 shadow cursor-pointer rounded"
          >
            Close
          </div>
          <div
            onClick={() => setSelectedResults([])}
            className="border border-gray-400 p-2 shadow cursor-pointer rounded"
          >
            Clear
          </div>
        </div>
        <div className="flex justify-center gap-4 items-center">
          {selectedResults.map((athlete) => (
            <div
              onClick={() => handleCompareClicked(athlete)}
              className="text-center text-sm border border-gray-400 hover:border-red-500 px-2 py-1 shadow cursor-pointer rounded"
              key={athlete.athleteCourseId}
            >
              <div>{athlete.fullName}</div>
              <div>
                {athlete.raceAge} | {athlete.genderAbbreviated}
              </div>
            </div>
          ))}
          <div>
            <Button>
              <a href={url}>Compare ({selectedResults.length})</a>
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}
