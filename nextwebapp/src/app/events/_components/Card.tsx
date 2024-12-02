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

export default function Card({
  event,
  clickHandler,
}: {
  event: EventSearchResultDto
  clickHandler: (event: EventSearchResultDto) => Promise<void>
}) {
  return (
    <div>
      <div className="text-right">
        <DropdownMenu>
          <DropdownMenuTrigger>
            <Ellipsis />
          </DropdownMenuTrigger>
          <DropdownMenuContent>
            <DropdownMenuLabel>Events</DropdownMenuLabel>
            <DropdownMenuSeparator />
            {event.courses.map((course) => {
              return (
                <DropdownMenuItem>
                  <Link href={`/races/${course.id}`}>{course.displayName}</Link>
                </DropdownMenuItem>
              )
            })}
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      <div className="py-2 text-center bg-secondary">
        <Link href={`/races/${event.upcomingRaceId}`}>
          <strong>{event.name}</strong>
        </Link>
      </div>

      <div className="mt-2 px-2">
        <LocationInfoRankings
          locationInfoWithRank={event.locationInfoWithRank}
        />
        <div>
          <BadgePlus
            className="cursor-pointer"
            size={14}
            color="black"
            strokeWidth={1.5}
            onClick={() => clickHandler(event)}
          />
        </div>
      </div>
    </div>
  )
}
