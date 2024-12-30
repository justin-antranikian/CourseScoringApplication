import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import Link from "next/link"
import Intervals from "../../_components/Intervals"
import { getApi } from "@/app/_api/api"
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"
import { DropdownMenu, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import CourseDetailsSheetView from "./CourseDetailsSheetView"
import DirectorySheet from "@/app/_components/DirectorySheet"
import LocationBreadcrumbs from "@/app/_components/LocationBreadcrumbs"
import { RaceSeriesTypeForAthleteNames } from "@/app/definitions"
import AthleteImage from "@/app/_components/AthleteImage"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const api = getApi()

export default async function Page({ params: { id } }: Props) {
  const irp = await api.results.details(id)
  const courseDetails = await api.courses.details(irp.courseId)
  const directory = await api.locations.directory()

  const { raceSeriesLocationInfoWithRank: locationInfoWithRank } = irp

  return (
    <>
      <div className="mb-5">
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
              <BreadcrumbLink href={`/races/${irp.raceId}`}>{irp.raceName}</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/courses/${irp.courseId}`}>{irp.courseName}</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>{irp.bib}</BreadcrumbPage>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <DropdownMenu>
                <DropdownMenuTrigger className="flex items-center gap-1">
                  <CourseDetailsSheetView course={courseDetails} />
                </DropdownMenuTrigger>
              </DropdownMenu>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div>
            <AthleteImage width="75%" />
          </div>
          <div className="mt-2 text-2xl font-bold">
            <Link href={`/athletes/${irp.athleteId}`}>{irp.fullName}</Link>
          </div>
          <div className="text-lg">
            {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {irp.genderAbbreviated} | {irp.raceAge}
          </div>
          <LocationInfoRankings locationInfoWithRank={irp.locationInfoWithRank} locationType={LocationType.athletes} />
          <hr className="mt-5" />
          <div className="mt-5">
            {irp.tags.map((tag, index) => (
              <span key={index} className="text-lg bg-blue-500 text-white py-1 px-3 rounded-lg mr-2 mb-2 inline-block">
                {RaceSeriesTypeForAthleteNames[tag]}
              </span>
            ))}
          </div>
          <div className="mt-3">{irp.firstName}'s training</div>
          <ul className="mt-3">
            {irp.trainingList.map((training, index) => (
              <li key={index} className="text-xs">
                {training}
              </li>
            ))}
          </ul>

          <div className="mt-3">{irp.firstName}'s personal goal</div>
          <div className="mt-3 text-xs italic">"{irp.personalGoalDescription}"</div>

          <div className="mt-3">{irp.firstName}'s course goal</div>
          <div className="mt-3 text-xs italic text-blue-500">
            <strong>"{irp.courseGoalDescription}"</strong>
          </div>
        </div>
        <div className="w-3/4">
          <div className="mb-12 bold text-2xl text-purple-500">Finish Info</div>
          <div className="my-5 flex flex-wrap">
            <div className="w-full sm:w-1/3">
              <div>Time</div>
              <div className="text-xl font-bold">{irp.paceWithTimeCumulative.timeFormatted}</div>
            </div>
            <div className="w-full sm:w-1/3">
              <div>Pace ({irp.paceWithTimeCumulative.paceLabel})</div>
              <div className="text-xl font-bold">{irp.paceWithTimeCumulative.paceValue || "--"}</div>
            </div>
            <div className="w-full sm:w-1/3">
              <div>Finish Time ({irp.timeZoneAbbreviated})</div>
              <div className="text-xl font-bold">{irp.finishTime ? irp.finishTime : "--"}</div>
            </div>
          </div>
          <hr className="my-5" />
          <div className="flex space-x-4">
            {irp.bracketResults.map((bracket) => (
              <div className="flex-1" key={bracket.rank}>
                <div className="truncate" title={bracket.name}>
                  {bracket.name}
                </div>
                <div className="mt-1 text-2xl font-bold text-primary">
                  {bracket.rank} of {bracket.totalRacers}
                </div>
              </div>
            ))}
          </div>

          <hr className="my-5" />
          <div className="mb-12 bold text-2xl text-purple-500">Intervals</div>
          <Intervals irp={irp} />
        </div>
      </div>
    </>
  )
}
