"use client"

import React, { useState } from "react"
import { RaceLeaderboardDto } from "../definitions"
import { InfoIcon } from "lucide-react"
import { LeaderboardResultDto } from "@/app/courses/[id]/definitions"
import { Irp } from "@/app/results/[id]/definitions"
import { Dialog } from "@/components/ui/dialog"
import Link from "next/link"
import { Button } from "@/components/ui/button"
import IrpQuickView from "./IrpQuickView"

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

  return (
    <>
      {raceLeaderboard.leaderboards.map((leaderboard, index) => (
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">
            {leaderboard.courseName}
          </div>
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
                <tr
                  key={irp.athleteCourseId}
                  className="border-b border-gray-300"
                >
                  <td className="text-left py-2">
                    <a href={`/results/${irp.athleteCourseId}`}>View</a>
                  </td>
                  <td className="py-2">
                    <InfoIcon
                      className="cursor-pointer"
                      size={14}
                      color="black"
                      strokeWidth={1.5}
                      onClick={() => handleQuickViewClicked(irp)}
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
              ))}
            </tbody>
          </table>
          <div className="my-8 text-right">
            <Button>
              <Link href={`/courses/${leaderboard.courseId}`}>View</Link>
            </Button>
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <IrpQuickView irp={irp} />
      </Dialog>
    </>
  )
}
