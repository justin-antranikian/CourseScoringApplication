import { config } from "@/config"
import React from "react"
import LocationInfoRankings from "../_components/LocationInfoRankings"
import { EventSearchResultDto } from "./definitions"
import Link from "next/link"

const getData = async (): Promise<EventSearchResultDto[]> => {
  const url = `${config.apiHost}/raceSeriesSearchApi`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page() {
  const events = await getData()

  return (
    <div className="flex gap-1">
      <div className="w-1/3">Directory</div>
      <div className="w-2/3">
        <div className="flex flex-wrap -mx-2">
          {events.map((event, index) => (
            <div
              key={index}
              className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4"
            >
              <div className="p-4 bg-gray-200 rounded shadow">
                <Card event={event} />
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}

const Card = ({ event }: { event: EventSearchResultDto }) => {
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
      </div>
    </div>
  )
}
