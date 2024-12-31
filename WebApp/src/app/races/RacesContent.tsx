"use client"

import React, { useState } from "react"
import { Dialog } from "@/components/ui/dialog"
import { EventSearchResultDto, RaceLeaderboardDto } from "@/app/_api/races/definitions"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { Card, CardContent } from "@/components/ui/card"
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu"
import { Ellipsis, BadgePlus } from "lucide-react"
import { ContextMenu, ContextMenuContent, ContextMenuItem, ContextMenuTrigger } from "@/components/ui/context-menu"
import { getRaceLeaderboard } from "@/app/_api/serverActions"
import QuickViewDialogContent from "./QuickViewDialogContent"
import RaceSeriesImage from "../_components/RaceSeriesImage"

export default function RacesContent({ events }: { events: EventSearchResultDto[] }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [leaderboard, setLeaderboard] = useState<RaceLeaderboardDto | null>(null)

  const handleViewMoreClicked = async ({ upcomingRaceId }: EventSearchResultDto) => {
    const leaderboard = await getRaceLeaderboard(upcomingRaceId)

    setLeaderboard(leaderboard)
    setDialogOpen(true)
  }

  return (
    <>
      <div className="flex flex-wrap">
        {events.map((event, index) => (
          <div key={index} className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4">
            <Card className="rounded shadow">
              <ContextMenu>
                <ContextMenuTrigger>
                  <CardContent className="p-0">
                    <div>
                      <RaceSeriesImage raceSeriesType={event.raceSeriesType} />
                    </div>
                    <div className="bg-purple-200 text-center text-base py-2">
                      <a href={`/races/${event.upcomingRaceId}`}>
                        <strong>{event.name}</strong>
                      </a>
                    </div>
                    <div className="p-2">
                      <div>{event.raceKickOffDate}</div>
                      <div className="my-3">
                        <LocationInfoRankings
                          locationInfoWithRank={event.locationInfoWithRank}
                          locationType={LocationType.races}
                        />
                      </div>
                      <div>
                        <DropdownMenu>
                          <DropdownMenuTrigger title="Actions">
                            <Ellipsis />
                          </DropdownMenuTrigger>
                          <DropdownMenuContent>
                            <DropdownMenuItem>
                              <a href={`/races/${event.upcomingRaceId}`}>Leaderboard</a>
                            </DropdownMenuItem>
                            <DropdownMenuSeparator />
                            {event.courses.map((course) => {
                              return (
                                <DropdownMenuItem key={course.id}>
                                  <a href={`/courses/${course.id}`}>{course.displayName}</a>
                                </DropdownMenuItem>
                              )
                            })}
                            <DropdownMenuSeparator />
                            <DropdownMenuItem onClick={() => handleViewMoreClicked(event)}>
                              <BadgePlus className="cursor-pointer" size={10} color="black" strokeWidth={1.5} />
                              Quick View
                            </DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </div>
                    </div>
                  </CardContent>
                </ContextMenuTrigger>
                <ContextMenuContent>
                  <ContextMenuItem onClick={() => handleViewMoreClicked(event)}>Quick View</ContextMenuItem>
                </ContextMenuContent>
              </ContextMenu>
            </Card>
          </div>
        ))}
      </div>
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {leaderboard ? <QuickViewDialogContent leaderboard={leaderboard} /> : null}
      </Dialog>
    </>
  )
}
