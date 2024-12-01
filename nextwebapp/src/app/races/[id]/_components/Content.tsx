"use client"

import React, { useState } from "react"
import { RaceLeaderboardByCourseDto, RaceLeaderboardDto } from "../definitions"
import { Camera } from "lucide-react"
import { LeaderboardResultDto } from "@/app/courses/[id]/definitions"
import { Irp } from "@/app/results/[id]/definitions"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import Link from "next/link"
import Result from "@/app/results/[id]/Result"

export default function Content({
  apiHost,
  raceLeaderboard,
}: {
  apiHost: string
  raceLeaderboard: RaceLeaderboardDto
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [irp, setIrp] = useState<Irp | null>(null)

  const handleQuickViewClicked = async (
    irpResult: LeaderboardResultDto,
  ): Promise<void> => {
    const response = await fetch(
      `${apiHost}/irpApi/${irpResult.athleteCourseId}`,
    )
    const result = (await response.json()) as Irp
    setIrp(result)
    setDialogOpen(true)
  }

  const QuickViewDialogContent = () => {
    if (!irp) {
      return
    }

    const LeaderboardContent = () => {
      return (
        <>
          <table className="my-5 table-auto w-full">
            <thead>
              <tr>
                <th className="w-[15%]" scope="col"></th>
                <th className="w-[20%]" scope="col">
                  Time{" "}
                  <span className="text-sm">({irp.timeZoneAbbreviated})</span>
                </th>
                <th className="w-[10%]" scope="col">
                  Overall
                </th>
                <th className="w-[10%]" scope="col">
                  Gender
                </th>
                <th className="w-[10%]" scope="col">
                  Division
                </th>
                <th className="w-[15%]" scope="col">
                  Interval Time
                </th>
                <th className="w-[20%]" scope="col">
                  Cumulative Time
                </th>
              </tr>
            </thead>
            <tbody>
              {irp.intervalResults.map((intervalResult, index) => (
                <Result result={intervalResult} />
              ))}
            </tbody>
          </table>
        </>
      )
    }

    return (
      <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle>Irp Quick View</DialogTitle>
          <DialogDescription>
            <div className="flex mt-5">
              <div className="flex-[1]">{irp.fullName}</div>
              <div className="flex-[3]">
                <LeaderboardContent />
              </div>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    )
  }

  return (
    <>
      {raceLeaderboard.leaderboards.map((leaderboard, index) => (
        <div className="mt-12" key={index}>
          {leaderboard.courseName}
          <LeaderBoard
            leaderboard={leaderboard}
            clickHandler={handleQuickViewClicked}
          />
          <div className="my-5 text-right">
            <Link href={`/courses/${leaderboard.courseId}`}>View</Link>
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <QuickViewDialogContent />
      </Dialog>
    </>
  )
}

const LeaderBoard = ({
  leaderboard,
  clickHandler,
}: {
  leaderboard: RaceLeaderboardByCourseDto
  clickHandler: (irpResult: LeaderboardResultDto) => Promise<void>
}) => {
  return (
    <table className="table-auto w-full">
      <thead>
        <tr>
          <th className="w-[5%]"></th>
          <th className="w-[5%]"></th>
          <th className="w-[15%]">Bib</th>
          <th className="w-[20%]">Name</th>
          <th className="w-[10%]">Overall</th>
          <th className="w-[10%]">Gender</th>
          <th className="w-[10%]">Division</th>
          <th className="w-[15%]">Time</th>
          <th className="w-[10%]">Pace</th>
        </tr>
      </thead>
      <tbody>
        {leaderboard.results.map((irp) => (
          <tr key={irp.athleteCourseId}>
            <td className="text-left">
              <a href={`/results/${irp.athleteCourseId}`}>View</a>
            </td>
            <td>
              <Camera
                size={14}
                color="blue"
                strokeWidth={1.5}
                onClick={() => clickHandler(irp)}
              />
            </td>
            <td>
              <span className="bg-gray-800 text-white p-2 rounded" title="bib">
                {irp.bib}
              </span>
            </td>
            <td>
              <div>
                <a
                  className="font-bold text-black bg-secondary"
                  href={`/athletes/${irp.athleteId}`}
                >
                  {irp.fullName}
                </a>
              </div>
              <div>
                {irp.genderAbbreviated} | {irp.raceAge}
              </div>
            </td>
            <td className="font-bold text-gray-500">{irp.overallRank}</td>
            <td className="font-bold text-gray-500">{irp.genderRank}</td>
            <td className="font-bold text-gray-500">{irp.divisionRank}</td>
            <td className="font-bold">
              {irp.paceWithTimeCumulative.timeFormatted}
            </td>
            <td>
              <div className="font-bold">
                {irp.paceWithTimeCumulative.paceValue || "--"}
              </div>
              {irp.paceWithTimeCumulative.paceLabel}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  )
}
