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
                <DropdownMenuLabel>Events</DropdownMenuLabel>
                <DropdownMenuSeparator />
                {event.courses.map((course) => {
                  return (
                    <DropdownMenuItem key={course.id}>
                      <Link href={`/races/${course.id}`}>
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
        </CardContent>
      </Card>
    </div>
  )
}
