"use client"

import React, { useMemo, useState } from "react"
import { CourseLeaderboardDto, LeaderboardResultDto } from "../definitions"
import { ChartBarStacked, InfoIcon } from "lucide-react"
import { Irp } from "@/app/results/[id]/definitions"
import { Dialog } from "@/components/ui/dialog"
import IrpQuickView from "@/app/races/[id]/_components/IrpQuickView"
import ComparePane from "@/app/_components/ComparePane"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp } from "@/app/_api/serverActions"

export default function Content({ courseLeaderboard }: { courseLeaderboard: CourseLeaderboardDto }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [irp, setIrp] = useState<Irp | null>(null)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const [hideComparePane, setHideComparePane] = useState(false)

  const handleCompareClicked = (id: number) => {
    setSelectedIds((prevSelectedResults) => {
      return prevSelectedResults.includes(id)
        ? prevSelectedResults.filter((resultId) => resultId !== id)
        : [...prevSelectedResults, id]
    })
  }

  const idsEncoded = useMemo(() => {
    return encodeURIComponent(`[${selectedIds.join(",")}]`)
  }, [selectedIds])

  const handleQuickViewClicked = async ({ athleteCourseId }: LeaderboardResultDto): Promise<void> => {
    const irp = await getIrp(athleteCourseId)
    setIrp(irp)
    setDialogOpen(true)
  }

  return (
    <>
      {courseLeaderboard.leaderboards.map((leaderboard, index) => (
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
                    <TooltipProvider>
                      <Tooltip>
                        <TooltipTrigger>
                          <ChartBarStacked
                            className="cursor-pointer"
                            onClick={() => handleCompareClicked(irp.athleteCourseId)}
                            size={15}
                            color="green"
                          />
                        </TooltipTrigger>
                        <TooltipContent>
                          <p>Compare Results</p>
                        </TooltipContent>
                      </Tooltip>
                    </TooltipProvider>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      ))}
      {selectedIds.length > 0 ? (
        <ComparePane
          hideComparePane={hideComparePane}
          setHideComparePane={setHideComparePane}
          idsEncoded={idsEncoded}
          url={"compare-results"}
          selectedIds={selectedIds}
        />
      ) : null}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {irp ? <IrpQuickView irp={irp} /> : null}
      </Dialog>
    </>
  )
}
