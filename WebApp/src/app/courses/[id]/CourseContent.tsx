"use client"

import React, { useState } from "react"
import { ChartBarStacked, InfoIcon } from "lucide-react"
import { Dialog } from "@/components/ui/dialog"
import IrpQuickView from "@/app/races/[id]/IrpQuickView"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp } from "@/app/_api/serverFunctions"
import { Irp } from "@/app/_api/results/definitions"
import { CourseLeaderboardByIntervalDto, LeaderboardResultDto } from "@/app/_api/courses/definitions"
import ComparePane from "./ComparePane"
import { Button } from "@/components/ui/button"

export default function CourseContent({
  courseLeaderboards,
}: {
  courseLeaderboards: CourseLeaderboardByIntervalDto[]
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [irp, setIrp] = useState<Irp | null>(null)
  const [selecetedResults, setSelectedResults] = useState<LeaderboardResultDto[]>([])
  const [showComparePane, setShowComparePane] = useState(false)

  const getSelectedIds = () => selecetedResults.map((result) => result.athleteCourseId)

  const handleQuickViewClicked = async ({ athleteCourseId }: LeaderboardResultDto) => {
    const irp = await getIrp(athleteCourseId)
    setIrp(irp)
    setDialogOpen(true)
  }

  const handleCompareClicked = (irp: LeaderboardResultDto) => {
    const selectedAthleteIds = getSelectedIds()

    if (selectedAthleteIds.includes(irp.athleteCourseId)) {
      setSelectedResults(
        selecetedResults.filter((selectedResult) => selectedResult.athleteCourseId !== irp.athleteCourseId),
      )
      return
    }

    setSelectedResults([...selecetedResults, irp])
  }

  const selectedIds = getSelectedIds()

  const queryParams = new URLSearchParams()
  selectedIds.forEach((id) => queryParams.append("ids", id.toString()))
  const compareUrl = `/results/compare?${queryParams.toString()}`

  return (
    <>
      <div className="mb-4">
        <Button onClick={() => setShowComparePane(!showComparePane)}>Toggle Compare</Button>
      </div>
      {courseLeaderboards.map((leaderboard, index) => (
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">{leaderboard.intervalName}</div>
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
                <TableHead></TableHead>
              </TableRow>
            </TableHeader>
            <TableBody className="text-sm">
              {leaderboard.results.map((irp) => (
                <TableRow key={irp.athleteCourseId} className="border-b border-gray-300">
                  <TableCell className="text-left py-2">
                    <a href={`/results/${irp.athleteCourseId}`}>View</a>
                  </TableCell>
                  <TableCell className="py-2">
                    <InfoIcon
                      className="cursor-pointer"
                      size={14}
                      color="black"
                      strokeWidth={1.5}
                      onClick={() => handleQuickViewClicked(irp)}
                    />
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
                  <TableCell>
                    <button
                      title="Add to Compare"
                      className={selectedIds.includes(irp.athleteCourseId) ? "opacity-50" : "cursor-pointer"}
                      onClick={() => handleCompareClicked(irp)}
                      disabled={selectedIds.includes(irp.athleteCourseId)}
                    >
                      <ChartBarStacked size={15} color="green" />
                    </button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      ))}
      {showComparePane ? (
        <ComparePane
          setShowComparePane={setShowComparePane}
          selectedResults={selecetedResults}
          setSelectedResults={setSelectedResults}
          url={compareUrl}
        />
      ) : null}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {irp ? <IrpQuickView irp={irp} /> : null}
      </Dialog>
    </>
  )
}
