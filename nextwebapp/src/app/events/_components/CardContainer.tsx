"use client"

import React, { useState } from "react"
import { EventSearchResultDto } from "../definitions"
import Card from "./Card"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { RaceLeaderboardDto } from "@/app/races/[id]/definitions"

export default function CardContainer({
  events,
  apiHost,
}: {
  events: EventSearchResultDto[]
  apiHost: string
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [leaderboard, setLeaderboard] = useState<RaceLeaderboardDto | null>(
    null,
  )

  const handleViewMoreClicked = async (
    event: EventSearchResultDto,
  ): Promise<void> => {
    const raceId = event.upcomingRaceId
    const response = await fetch(`${apiHost}/raceLeaderboardApi/${raceId}`)
    const result = (await response.json()) as RaceLeaderboardDto

    setLeaderboard(result)
    setDialogOpen(true)
  }

  const QuickViewDialogContent = () => {
    if (!leaderboard) {
      return
    }

    const LeaderboardContent = () => {
      return (
        <>
          {leaderboard.leaderboards.map((board) => {
            return (
              <>
                <div>
                  <strong>{board.courseName}</strong>
                </div>

                <table className="table-auto w-full">
                  <thead>
                    <tr>
                      <th className="w-[10%]"></th>
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
                    {board.results.map((irp) => (
                      <tr key={irp.athleteCourseId}>
                        <td className="text-left">
                          <a href={`/results/${irp.athleteCourseId}`}>View</a>
                        </td>
                        <td>
                          <span
                            className="bg-gray-800 text-white p-2 rounded"
                            title="bib"
                          >
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
                        <td className="font-bold text-gray-500">
                          {irp.overallRank}
                        </td>
                        <td className="font-bold text-gray-500">
                          {irp.genderRank}
                        </td>
                        <td className="font-bold text-gray-500">
                          {irp.divisionRank}
                        </td>
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
              </>
            )
          })}
        </>
      )
    }

    return (
      <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle>Race Leaderboard Quick View</DialogTitle>
          <DialogDescription>
            <div className="flex mt-5">
              <div className="flex-[1]">{leaderboard.raceName}</div>
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
      {events.map((event, index) => (
        <div
          key={index}
          className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4"
        >
          <div className="p-4 bg-gray-200 rounded shadow">
            <Card event={event} clickHandler={handleViewMoreClicked} />
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <QuickViewDialogContent />
      </Dialog>
    </>
  )
}
