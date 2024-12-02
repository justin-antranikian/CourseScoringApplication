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
import { Button } from "@/components/ui/button"

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
    const url = `${apiHost}/irpApi/${irpResult.athleteCourseId}`
    const response = await fetch(url)
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
                <th className="w-[15%] text-left" scope="col"></th>
                <th className="w-[20%] text-left" scope="col">
                  Time{" "}
                  <span className="text-sm">({irp.timeZoneAbbreviated})</span>
                </th>
                <th className="w-[10%] text-left" scope="col">
                  Overall
                </th>
                <th className="w-[10%] text-left" scope="col">
                  Gender
                </th>
                <th className="w-[10%] text-left" scope="col">
                  Division
                </th>
                <th className="w-[15%] text-left" scope="col">
                  Interval Time
                </th>
                <th className="w-[20%] text-left" scope="col">
                  Cumulative Time
                </th>
              </tr>
            </thead>
            <tbody>
              {irp.intervalResults.map((intervalResult, index) => (
                <Result result={intervalResult} key={index} />
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
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">
            {leaderboard.courseName}
          </div>
          <LeaderBoard
            leaderboard={leaderboard}
            clickHandler={handleQuickViewClicked}
          />
          {/* <div className="border-b">
  This has a bottom border.
</div> */}
          <div className="my-8 text-right">
            <Button>
              <Link href={`/courses/${leaderboard.courseId}`}>View</Link>
            </Button>
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
      <thead className="text-lg">
        <tr className="border-b border-black">
          <th className="w-[5%] text-left py-2" scope="col"></th>
          <th className="w-[5%] text-left py-2" scope="col"></th>
          <th className="w-[15%] text-left py-2" scope="col">
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
        {leaderboard.results.map((irp) => (
          <tr key={irp.athleteCourseId} className="border-b border-gray-300">
            <td className="text-left py-2">
              <a href={`/results/${irp.athleteCourseId}`}>View</a>
            </td>
            <td className="py-2">
              <Camera
                className="cursor-pointer"
                size={14}
                color="black"
                strokeWidth={1.5}
                onClick={() => clickHandler(irp)}
              />
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
                <a href={`/athletes/${irp.athleteId}`}>{irp.fullName}</a>
              </div>
              <div>
                {irp.genderAbbreviated} | {irp.raceAge}
              </div>
            </td>
            <td className="font-bold text-gray-500 py-2">{irp.overallRank}</td>
            <td className="font-bold text-gray-500 py-2">{irp.genderRank}</td>
            <td className="font-bold text-gray-500 py-2">{irp.divisionRank}</td>
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
        ))}
      </tbody>
    </table>
  )
}
