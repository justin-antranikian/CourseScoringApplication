import React from "react"
import { AthleteSearchResultDto } from "../_api/athletes/definitions"
import { Button } from "@/components/ui/button"

interface Props {
  setShowComparePane: React.Dispatch<React.SetStateAction<boolean>>
  selectedAthletes: AthleteSearchResultDto[]
  setSelectedAthletes: React.Dispatch<React.SetStateAction<AthleteSearchResultDto[]>>
  url: string
}

export default function ComparePane({ setShowComparePane, selectedAthletes, setSelectedAthletes, url }: Props) {
  const handleCompareClicked = (athlete: AthleteSearchResultDto) => {
    setSelectedAthletes(selectedAthletes.filter((selectedAthlete) => selectedAthlete.id !== athlete.id))
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
            onClick={() => setSelectedAthletes([])}
            className="border border-gray-400 p-2 shadow cursor-pointer rounded"
          >
            Clear
          </div>
        </div>
        <div className="flex justify-center gap-4 items-center">
          {selectedAthletes
            .sort(
              (a: AthleteSearchResultDto, b: AthleteSearchResultDto) =>
                a.locationInfoWithRank.overallRank - b.locationInfoWithRank.overallRank,
            )
            .map((athlete) => (
              <div
                onClick={() => handleCompareClicked(athlete)}
                className="text-center text-sm border border-gray-400 hover:border-red-500 px-2 py-1 shadow cursor-pointer rounded"
                key={athlete.id}
              >
                <div>{athlete.fullName}</div>
                <div>
                  {athlete.age} | {athlete.genderAbbreviated}
                </div>
              </div>
            ))}
          <div>
            <Button>
              <a href={url}>Compare ({selectedAthletes.length})</a>
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}
