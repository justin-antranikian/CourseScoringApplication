import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import Content from "./_components/Content"
import { getImageNonFormatted } from "@/app/utils"
import { Button } from "@/components/ui/button"
import { getApi } from "@/app/_api/api"
import {
  Breadcrumb,
  BreadcrumbList,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbSeparator,
  BreadcrumbPage,
} from "@/components/ui/breadcrumb"
import { DropdownMenu, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import DirectorySheetView from "@/app/_components/DirectorySheetView"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const api = getApi()

export default async function Page({ params: { id } }: Props) {
  const courseLeaderboard = await api.courses.leaderboard(id)
  const directory = await api.locations.directory()
  const { locationInfoWithRank } = courseLeaderboard

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <DropdownMenu>
                <DropdownMenuTrigger className="flex items-center gap-1">
                  <DirectorySheetView locations={directory} locationType={LocationType.races} />
                </DropdownMenuTrigger>
              </DropdownMenu>
            </BreadcrumbItem>
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
              <BreadcrumbLink href={`/races/${courseLeaderboard.raceId}`}>{courseLeaderboard.raceName}</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>{courseLeaderboard.courseName}</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div>
            <img
              style={{ width: "75%", height: 125 }}
              src={getImageNonFormatted(courseLeaderboard.raceSeriesTypeName)}
            />
          </div>
          <div className="mt-2 text-2xl font-bold">{courseLeaderboard.raceName}</div>
          <div className="text-lg text-blue-500 font-bold">{courseLeaderboard.courseName}</div>
          <div className="text-sm mb-2">
            <div>
              {courseLeaderboard.locationInfoWithRank.city}, {courseLeaderboard.locationInfoWithRank.state}
            </div>
            <div>Distance: {courseLeaderboard.courseDistance}</div>
            <div>
              {courseLeaderboard.courseDate} at <strong>{courseLeaderboard.courseTime}</strong>
            </div>
          </div>
          <LocationInfoRankings
            locationInfoWithRank={courseLeaderboard.locationInfoWithRank}
            locationType={LocationType.races}
          />
          <div className="mt-5">
            <a href={`/courses/${id}/awards`}>
              <Button>Awards</Button>
            </a>
          </div>
        </div>
        <div className="w-3/4">
          <Content courseLeaderboard={courseLeaderboard} />
        </div>
      </div>
    </>
  )
}
