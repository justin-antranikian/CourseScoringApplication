"use client"

import React from "react"
import { EventSearchResultDto } from "../definitions"
import Link from "next/link"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { BadgePlus, Ellipsis } from "lucide-react"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Card, CardContent, CardTitle } from "@/components/ui/card"
import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "@/components/ui/context-menu"

export default function EventCard({
  event,
  clickHandler,
}: {
  event: EventSearchResultDto
  clickHandler: (event: EventSearchResultDto) => Promise<void>
}) {
  return (
    <div>
      <Card className="rounded shadow">
        <CardTitle>
          <div className="bg-purple-200 text-center text-base py-2">
            <Link href={`/races/${event.upcomingRaceId}`}>
              <strong>{event.name}</strong>
            </Link>
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
                    <DropdownMenuSeparator />
                    <DropdownMenuLabel>Events</DropdownMenuLabel>
                    <DropdownMenuItem>
                      <Link href={`/races/${event.upcomingRaceId}`}>
                        Leaderboard
                      </Link>
                    </DropdownMenuItem>
                    <DropdownMenuSeparator />

                    {event.courses.map((course) => {
                      return (
                        <DropdownMenuItem key={course.id}>
                          <Link href={`/courses/${course.id}`}>
                            {course.displayName}
                          </Link>
                        </DropdownMenuItem>
                      )
                    })}
                    <DropdownMenuSeparator />
                    <DropdownMenuItem onClick={() => clickHandler(event)}>
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
            <ContextMenuItem onClick={() => clickHandler(event)}>
              Quick View
            </ContextMenuItem>
          </ContextMenuContent>
        </ContextMenu>
      </Card>
    </div>
  )
}
