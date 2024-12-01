"use client"

import React from "react"
import { EventSearchResultDto } from "../definitions"
import Link from "next/link"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { Camera } from "lucide-react"

export default function Card({
  event,
  clickHandler,
}: {
  event: EventSearchResultDto
  clickHandler: (event: EventSearchResultDto) => Promise<void>
}) {
  return (
    <div>
      <div className="py-2 text-center bg-secondary">
        <Link href={`/races/${event.upcomingRaceId}`}>
          <strong>{event.name}</strong>
        </Link>
      </div>

      <div className="mt-2 px-2">
        <LocationInfoRankings
          locationInfoWithRank={event.locationInfoWithRank}
        />
        <div className="text-right">
          <i className="fa fa-plus-circle cursor-pointer" title="view more"></i>
        </div>
        <div>
          <Camera
            size={14}
            color="blue"
            strokeWidth={1.5}
            onClick={() => clickHandler(event)}
          />
        </div>
      </div>
    </div>
  )
}
