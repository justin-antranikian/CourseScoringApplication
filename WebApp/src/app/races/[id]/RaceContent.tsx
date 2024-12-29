"use client"

import React, { useState } from "react"
import { RaceLeaderboardDto } from "../../_api/races/definitions"
import { InfoIcon } from "lucide-react"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"
import { Dialog } from "@/components/ui/dialog"
import Link from "next/link"
import { Button } from "@/components/ui/button"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp } from "@/app/_api/serverActions"
import { Irp } from "@/app/_api/results/definitions"
import IrpQuickView from "./IrpQuickView"

export default function RaceContent({ raceLeaderboard }: { raceLeaderboard: RaceLeaderboardDto }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [irp, setIrp] = useState<Irp | null>(null)

  const handleQuickViewClicked = async ({ athleteCourseId }: LeaderboardResultDto): Promise<void> => {
    const irp = await getIrp(athleteCourseId)
    setIrp(irp)
    setDialogOpen(true)
  }

  return (
    <>
      {raceLeaderboard.leaderboards.map((leaderboard, index) => (
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">{leaderboard.courseName}</div>
          <Table>
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
            <TableBody>
              {leaderboard.results.map((irp) => (
                <TableRow key={irp.athleteCourseId}>
                  <TableCell>
                    <a href={`/results/${irp.athleteCourseId}`}>View</a>
                  </TableCell>
                  <TableCell>
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
                </TableRow>
              ))}
            </TableBody>
          </Table>
          <div className="my-8 text-right">
            <Button>
              <Link href={`/courses/${leaderboard.courseId}`}>View</Link>
            </Button>
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {irp ? <IrpQuickView irp={irp} /> : null}
      </Dialog>
    </>
  )
}
