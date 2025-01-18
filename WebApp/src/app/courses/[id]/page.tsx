import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import CourseContent from "./CourseContent"
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
import RaceSeriesImage from "@/app/_components/RaceSeriesImage"

export const dynamic = "force-dynamic"

const api = getApi()

export default async function Page({ params }: { params: Promise<{ id: string }> }) {
  const { id } = await params

  const [courseLeaderboard, courseDetails, directory] = await Promise.all([
    api.courses.leaderboard(id),
    api.courses.details(id),
    api.locations.directory(),
  ])

  const { locationInfoWithRank } = courseDetails

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
                <BreadcrumbLink href={`/races/${courseDetails.raceId}`}>{courseDetails.raceName}</BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
              <BreadcrumbItem>
                <BreadcrumbPage>{courseDetails.name}</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
          <div>
            <ResultSearch raceId={courseDetails.raceId} courseId={id} />
          </div>
        </div>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div>
            <RaceSeriesImage raceSeriesType={courseDetails.raceSeriesType} width="75%" />
          </div>
          <div className="mt-2 text-2xl font-bold">{courseDetails.raceName}</div>
          <div className="text-lg text-blue-500 font-bold">{courseDetails.name}</div>
          <div className="text-sm mb-2">
            <div>
              {locationInfoWithRank.city}, {locationInfoWithRank.state}
            </div>
            <div>Distance: {courseDetails.distance}</div>
            <div>
              {courseDetails.courseDate} at <strong>{courseDetails.courseTime}</strong>
            </div>
          </div>
          <LocationInfoRankings locationInfoWithRank={locationInfoWithRank} locationType={LocationType.races} />
          <div className="mt-5">
            <a href={`/courses/${id}/awards`}>
              <Button>Awards</Button>
            </a>
          </div>
        </div>
        <div className="w-3/4">
          <CourseContent courseLeaderboards={courseLeaderboard} />
        </div>
      </div>
    </>
  )
}
