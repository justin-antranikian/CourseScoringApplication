import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import CourseContent from "./CourseContent"
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
import DirectorySheet from "@/app/_components/DirectorySheet"
import ResultSearch from "@/app/_components/ResultSearch"
import LocationBreadcrumbs from "@/app/_components/LocationBreadcrumbs"

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
        <div className="flex justify-between">
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem>
                <DropdownMenu>
                  <DropdownMenuTrigger className="flex items-center gap-1">
                    <DirectorySheet locations={directory} locationType={LocationType.races} />
                  </DropdownMenuTrigger>
                </DropdownMenu>
              </BreadcrumbItem>
              <BreadcrumbItem>
                <BreadcrumbLink href="/races">All Races</BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
              <LocationBreadcrumbs locationInfoWithRank={locationInfoWithRank} locationType={LocationType.races} />
              <BreadcrumbItem>
                <BreadcrumbLink href={`/races/${courseLeaderboard.raceId}`}>
                  {courseLeaderboard.raceName}
                </BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
              <BreadcrumbItem>
                <BreadcrumbPage>{courseLeaderboard.courseName}</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
          <div>
            <ResultSearch raceId={courseLeaderboard.raceId} courseId={id} />
          </div>
        </div>
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
          <CourseContent courseLeaderboard={courseLeaderboard} />
        </div>
      </div>
    </>
  )
}
