import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import Content from "./_components/Content"
import { getImageNonFormatted } from "@/app/utils"
import { getApi } from "@/app/_api/api"
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const api = getApi()

export default async function Page({ params: { id } }: Props) {
  const raceLeaderboard = await api.races.details(id)
  const { locationInfoWithRank } = raceLeaderboard

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink href="/races">All Races</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/races/directory/${locationInfoWithRank.stateUrl}`}>
                {locationInfoWithRank.state}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/races/directory/${locationInfoWithRank.areaUrl}`}>
                {locationInfoWithRank.area}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/races/directory/${locationInfoWithRank.cityUrl}`}>
                {locationInfoWithRank.city}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>{raceLeaderboard.raceName}</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div>
            <img style={{ width: "75%", height: 125 }} src={getImageNonFormatted(raceLeaderboard.raceSeriesTypeName)} />
          </div>
          <div className="mt-2 text-2xl font-bold">{raceLeaderboard.raceName}</div>
          <div className="text-lg text-blue-500 font-bold">All Courses</div>

          <div className="text-sm mb-2">
            <div>
              {raceLeaderboard.locationInfoWithRank.city}, {raceLeaderboard.locationInfoWithRank.state}
            </div>
            <div className="font-bold">{raceLeaderboard.raceKickOffDate}</div>
          </div>
          <LocationInfoRankings
            locationInfoWithRank={raceLeaderboard.locationInfoWithRank}
            locationType={LocationType.races}
          />
        </div>
        <div className="w-3/4">
          <Content raceLeaderboard={raceLeaderboard} />
        </div>
      </div>
    </>
  )
}
