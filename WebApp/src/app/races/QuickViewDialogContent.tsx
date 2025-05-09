import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import IrpDetails from "@/app/_components/IrpDetails"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"
import { RaceLeaderboardDto } from "@/app/_api/races/definitions"
import { DialogContent, DialogTitle } from "@/components/ui/dialog"
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet"
import { Info } from "lucide-react"
import React, { useState } from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp } from "@/app/_api/serverActions"
import { Irp } from "@/app/_api/results/definitions"
import { VisuallyHidden } from "@radix-ui/react-visually-hidden"

export default function QuickViewDialogContent({ raceLeaderboard }: { raceLeaderboard: RaceLeaderboardDto }) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)
  const [irpDetails, setIrpDetails] = useState<Irp | null>(null)

  const getIrpData = async ({ athleteCourseId }: LeaderboardResultDto) => {
    const irp = await getIrp(athleteCourseId)
    setIrpDetails(irp)
    setSheetOpen(true)
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <VisuallyHidden>
        <DialogTitle>Race Quick View</DialogTitle>
      </VisuallyHidden>
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl bold">{raceLeaderboard.raceName}</div>
          <div className="text-sm mb-2">
            <div>
              {raceLeaderboard.locationInfoWithRank.city}, {raceLeaderboard.locationInfoWithRank.state}
            </div>
            <div className="font-bold">{raceLeaderboard.raceKickOffDate}</div>
            <div className="mt-3">
              <LocationInfoRankings
                locationInfoWithRank={raceLeaderboard.locationInfoWithRank}
                locationType={LocationType.races}
              />
            </div>
          </div>
        </div>
        <div className="flex-[3]">
          {raceLeaderboard.leaderboards.map((leaderboard) => {
            return (
              <div key={leaderboard.courseId}>
                <div className="mb-8 text-purple-500 bold text-2xl">
                  <a href={`/courses/${leaderboard.courseId}`}>{leaderboard.courseName}</a>
                </div>
                <Table className="mb-8">
                  <TableHeader>
                    <TableRow>
                      <TableHead></TableHead>
                      <TableHead></TableHead>
                      <TableHead>Name</TableHead>
                      <TableHead>Overall</TableHead>
                      <TableHead>Gender</TableHead>
                      <TableHead>Division</TableHead>
                      <TableHead>Time</TableHead>
                      <TableHead>Pace</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody className="text-sm">
                    {leaderboard.results.map((irp) => {
                      return (
                        <TableRow key={irp.athleteCourseId} className="border-b border-gray-300">
                          <TableCell>
                            <a href={`/results/${irp.athleteCourseId}`}>View</a>
                          </TableCell>
                          <TableCell>
                            <span className="cursor-pointer">
                              <Info size={14} onClick={() => getIrpData(irp)} />
                            </span>
                          </TableCell>
                          <TableCell>
                            <div>
                              <a href={`/athletes/${irp.athleteId}`}>{irp.fullName}</a>
                            </div>
                            <div>
                              {irp.genderAbbreviated} | {irp.bib}
                            </div>
                          </TableCell>
                          <TableCell>{irp.overallRank}</TableCell>
                          <TableCell>{irp.genderRank}</TableCell>
                          <TableCell>{irp.divisionRank}</TableCell>
                          <TableCell className="font-bold">{irp.paceWithTimeCumulative.timeFormatted}</TableCell>
                          <TableCell>
                            <div className="font-bold">{irp.paceWithTimeCumulative.paceValue || "--"}</div>
                            {irp.paceWithTimeCumulative.paceLabel}
                          </TableCell>
                        </TableRow>
                      )
                    })}
                  </TableBody>
                </Table>
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
