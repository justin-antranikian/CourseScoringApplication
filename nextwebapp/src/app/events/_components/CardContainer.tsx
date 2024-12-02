"use client"

import React, { useState } from "react"
import { EventSearchResultDto } from "../definitions"
import { Dialog, DialogContent } from "@/components/ui/dialog"
import { RaceLeaderboardDto } from "@/app/races/[id]/definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { Card, CardTitle, CardContent } from "@/components/ui/card"
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu"
import { Ellipsis, BadgePlus, Info } from "lucide-react"
import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "@/components/ui/context-menu"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { LeaderboardResultDto } from "@/app/courses/[id]/definitions"

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

  const handlePopoverChange = (open: boolean, irp: LeaderboardResultDto) => {
    if (!open) {
      return
    }

    console.log(open)
    console.log(irp)
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
              <div>
                <div className="mb-8 text-purple-500 bold text-2xl">
                  {board.courseName}
                </div>

                <table className="table-auto w-full mb-8">
                  <thead className="text-lg">
                    <tr className="border-b border-black">
                      <th className="w-[15%] text-left py-2" scope="col"></th>
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
                    {board.results.map((irp) => (
                      <tr
                        key={irp.athleteCourseId}
                        className="border-b border-gray-300"
                      >
                        <td className="text-left py-2">
                          <a href={`/results/${irp.athleteCourseId}`}>View</a>
                          <span className="ml-2">
                            <Popover
                              onOpenChange={(open) =>
                                handlePopoverChange(open, irp)
                              }
                            >
                              <PopoverTrigger
                                onClick={() => console.log("aaa")}
                              >
                                <Info size={12} />
                              </PopoverTrigger>
                              <PopoverContent>
                                Place content for the popover here.
                              </PopoverContent>
                            </Popover>
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
                    ))}
                  </tbody>
                </table>
              </div>
            )
          })}
        </>
      )
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
            <LeaderboardContent />
          </div>
        </div>
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
          <Card className="rounded shadow">
            <CardTitle>
              <div className="bg-purple-200 text-center text-base py-2">
                <a href={`/races/${event.upcomingRaceId}`}>
                  <strong>{event.name}</strong>
                </a>
              </div>
            </CardTitle>
            <ContextMenu>
              <ContextMenuTrigger>
                <CardContent>
                  <div className="my-3">
                    <LocationInfoRankings
                      locationInfoWithRank={event.locationInfoWithRank}
                    />
                  </div>
                  <div>
                    <DropdownMenu>
                      <DropdownMenuTrigger>
                        <Ellipsis />
                      </DropdownMenuTrigger>
                      <DropdownMenuContent>
                        <DropdownMenuItem>
                          <a href={`/races/${event.upcomingRaceId}`}>
                            Leaderboard
                          </a>
                        </DropdownMenuItem>
                        <DropdownMenuSeparator />
                        {event.courses.map((course) => {
                          return (
                            <DropdownMenuItem key={course.id}>
                              <a href={`/courses/${course.id}`}>
                                {course.displayName}
                              </a>
                            </DropdownMenuItem>
                          )
                        })}
                        <DropdownMenuSeparator />
                        <DropdownMenuItem
                          onClick={() => handleViewMoreClicked(event)}
                        >
                          {" "}
                          <BadgePlus
                            className="cursor-pointer"
                            size={10}
                            color="black"
                            strokeWidth={1.5}
                          />
                          Quick View
                        </DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </div>
                  <div></div>
                </CardContent>
              </ContextMenuTrigger>
              <ContextMenuContent>
                <ContextMenuItem onClick={() => handleViewMoreClicked(event)}>
                  Quick View
                </ContextMenuItem>
              </ContextMenuContent>
            </ContextMenu>
          </Card>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <QuickViewDialogContent />
      </Dialog>
    </>
  )
}
