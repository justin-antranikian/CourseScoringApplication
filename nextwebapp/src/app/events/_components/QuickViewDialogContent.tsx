import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import IrpDetails from "@/app/_components/IrpDetails"
import { LeaderboardResultDto } from "@/app/courses/[id]/definitions"
import { RaceLeaderboardDto } from "@/app/races/[id]/definitions"
import { Irp } from "@/app/results/[id]/definitions"
import { DialogContent } from "@/components/ui/dialog"
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
} from "@/components/ui/sheet"
import { Info } from "lucide-react"
import React, { useEffect, useState } from "react"

export default function QuickViewDialogContent({
  apiHost,
  leaderboard,
}: {
  leaderboard: RaceLeaderboardDto | null
  apiHost: string
}) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)
  const [irpDetails, setIrpDetails] = useState<Irp | null>(null)

  useEffect(() => {
    if (irpDetails) {
      setSheetOpen(true)
    }
  }, [irpDetails])

  const getIrpData = async (irp: LeaderboardResultDto) => {
    const url = `${apiHost}/irpApi/${irp.athleteCourseId}`
    const response = await fetch(url)
    const result = (await response.json()) as Irp
    setIrpDetails(result)
  }

  if (!leaderboard) {
    return
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl bold">{leaderboard.raceName}</div>
          <div className="text-sm mb-2">
            <div>
              {leaderboard.locationInfoWithRank.city},{" "}
              {leaderboard.locationInfoWithRank.state}
            </div>
            <div className="font-bold">{leaderboard.raceKickOffDate}</div>
            <div className="mt-3">
              <LocationInfoRankings
                locationInfoWithRank={leaderboard.locationInfoWithRank}
              />
            </div>
          </div>
        </div>
        <div className="flex-[3]">
          {leaderboard.leaderboards.map((board) => {
            return (
              <div>
                <div className="mb-8 text-purple-500 bold text-2xl">
                  <a href={`/courses/${board.courseId}`}>{board.courseName}</a>
                </div>
                <table className="table-auto w-full mb-8">
                  <thead className="text-lg">
                    <tr className="border-b border-black">
                      <th className="w-[7%] text-left py-2" scope="col"></th>
                      <th className="w-[7%] text-left py-2" scope="col"></th>
                      <th className="w-[10%] text-left py-2" scope="col">
                        Bib
                      </th>
                      <th className="w-[20%] text-left py-2" scope="col">
                        Name
                      </th>
                      <th className="w-[10%] text-left py-2" scope="col">
                        Overall
                      </th>
                      <th className="w-[10%] text-left py-2" scope="col">
                        Gender
                      </th>
                      <th className="w-[10%] text-left py-2" scope="col">
                        Division
                      </th>
                      <th className="w-[15%] text-left py-2" scope="col">
                        Time
                      </th>
                      <th className="w-[10%] text-left py-2" scope="col">
                        Pace
                      </th>
                    </tr>
                  </thead>
                  <tbody className="text-sm">
                    {board.results.map((irp) => {
                      return (
                        <tr
                          key={irp.athleteCourseId}
                          className="border-b border-gray-300"
                        >
                          <td className="text-left py-2">
                            <a href={`/results/${irp.athleteCourseId}`}>View</a>
                          </td>
                          <td className="text-left py-2">
                            <span className="cursor-pointer">
                              <Info size={14} onClick={() => getIrpData(irp)} />
                            </span>
                          </td>
                          <td className="py-2">
                            <span
                              className="bg-gray-800 text-white px-2 py-1 rounded"
                              title="bib"
                            >
                              {irp.bib}
                            </span>
                          </td>
                          <td className="py-2">
                            <div>
                              <a href={`/athletes/${irp.athleteId}`}>
                                {irp.fullName}
                              </a>
                            </div>
                            <div>
                              {irp.genderAbbreviated} | {irp.raceAge}
                            </div>
                          </td>
                          <td className="font-bold text-gray-500 py-2">
                            {irp.overallRank}
                          </td>
                          <td className="font-bold text-gray-500 py-2">
                            {irp.genderRank}
                          </td>
                          <td className="font-bold text-gray-500 py-2">
                            {irp.divisionRank}
                          </td>
                          <td className="font-bold py-2">
                            {irp.paceWithTimeCumulative.timeFormatted}
                          </td>
                          <td className="py-2">
                            <div className="font-bold">
                              {irp.paceWithTimeCumulative.paceValue || "--"}
                            </div>
                            {irp.paceWithTimeCumulative.paceLabel}
                          </td>
                        </tr>
                      )
                    })}
                  </tbody>
                </table>
              </div>
            )
          })}
        </div>
      </div>
      <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
        <SheetContent>
          <SheetHeader>
            <SheetTitle>Irp Details</SheetTitle>
          </SheetHeader>
          {irpDetails ? <IrpDetails irpDetails={irpDetails} /> : null}
        </SheetContent>
      </Sheet>
    </DialogContent>
  )
}
