"use client"

import React, { useState } from "react"
import { Dialog } from "@/components/ui/dialog"
import { RaceSearchResultDto, RaceLeaderboardDto } from "@/app/_api/races/definitions"
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

export default function RacesContent({ races }: { races: RaceSearchResultDto[] }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [raceLeaderboard, setRaceLeaderboard] = useState<RaceLeaderboardDto | null>(null)

  const handleViewMoreClicked = async ({ upcomingRaceId }: RaceSearchResultDto) => {
    const leaderboard = await getRaceLeaderboard(upcomingRaceId)
    setRaceLeaderboard(leaderboard)
    setDialogOpen(true)
  }

  return (
    <>
      {races.map((race) => (
        <div key={race.id} className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4">
          <Card className="rounded shadow">
            <ContextMenu>
              <ContextMenuTrigger>
                <CardContent className="p-0">
                  <div>
                    <RaceSeriesImage raceSeriesType={race.raceSeriesType} />
                  </div>
                  <div className="bg-purple-200 text-center text-base py-2">
                    <a href={`/races/${race.upcomingRaceId}`}>
                      <strong>{race.name}</strong>
                    </a>
                  </div>
                  <div className="p-2">
                    <div>{race.raceKickOffDate}</div>
                    <div className="my-3">
                      <LocationInfoRankings
                        locationInfoWithRank={race.locationInfoWithRank}
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
                            <a href={`/races/${race.upcomingRaceId}`}>Leaderboard</a>
                          </DropdownMenuItem>
                          <DropdownMenuSeparator />
                          {race.courses.map((course) => {
                            return (
                              <DropdownMenuItem key={course.id}>
                                <a href={`/courses/${course.id}`}>{course.displayName}</a>
                              </DropdownMenuItem>
                            )
                          })}
                          <DropdownMenuSeparator />
                          <DropdownMenuItem className="cursor-pointer" onClick={() => handleViewMoreClicked(race)}>
                            <BadgePlus size={10} color="black" />
                            Quick View
                          </DropdownMenuItem>
                        </DropdownMenuContent>
                      </DropdownMenu>
                    </div>
                  </div>
                </CardContent>
              </ContextMenuTrigger>
              <ContextMenuContent>
                <ContextMenuItem onClick={() => handleViewMoreClicked(race)}>Quick View</ContextMenuItem>
              </ContextMenuContent>
            </ContextMenu>
          </Card>
        </div>
      ))}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {raceLeaderboard ? <QuickViewDialogContent raceLeaderboard={raceLeaderboard} /> : null}
      </Dialog>
    </>
  )
}
