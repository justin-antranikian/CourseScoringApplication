import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getApi } from "@/app/_api/api"
import { ResultCompareIntervalDto } from "@/app/_api/results/definitions"
import { PaceWithTime } from "@/app/_components/IntervalTime"
import {
  Breadcrumb,
  BreadcrumbList,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbSeparator,
  BreadcrumbPage,
} from "@/components/ui/breadcrumb"
import LocationBreadcrumbs from "@/app/_components/LocationBreadcrumbs"
import { LocationType } from "@/app/_components/LocationInfoRankings"
import { DropdownMenu, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import DirectorySheet from "@/app/_components/DirectorySheet"

export const dynamic = "force-dynamic"

const api = getApi()

export default async function Page({
  searchParams: { ids },
}: {
  searchParams: {
    ids: string[]
  }
}) {
  const athletes = await api.results.compare(ids)
  const intervalNames = athletes[0].intervals.map((inteval) => inteval.intervalName)
  const courseId = athletes[0].courseId

  const [courseDetails, directory] = await Promise.all([api.courses.details(courseId), api.locations.directory()])
  const { locationInfoWithRank } = courseDetails

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
              <BreadcrumbLink href={`/races/${courseDetails.raceId}`}>{courseDetails.raceName}</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/courses/${courseDetails.id}`}>{courseDetails.name}</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>Compare</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="mb-8 text-purple-500 bold text-2xl">Result Compare</div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Athlete Info</TableHead>
            {intervalNames.map((interval: string, index: number) => (
              <TableHead key={index}>{interval}</TableHead>
            ))}
          </TableRow>
        </TableHeader>
        <TableBody>
          {athletes.map((athlete) => {
            return (
              <TableRow key={athlete.athleteCourseId}>
                <TableCell>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap text-xl">{athlete.fullName}</div>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap">
                    {athlete.locationInfoWithRank.city}, {athlete.locationInfoWithRank.state}
                  </div>
                  <div className="text-xs">
                    {athlete.genderAbbreviated} | {athlete.raceAge}
                  </div>
                </TableCell>
                {athlete.intervals.map((interval) => {
                  return (
                    <TableCell>
                      <div>{interval.crossingTime ? interval.crossingTime : "--"}</div>
                      <PaceContent interval={interval} />
                    </TableCell>
                  )
                })}
              </TableRow>
            )
          })}
        </TableBody>
      </Table>
    </>
  )
}

const PaceContent = ({ interval }: { interval: ResultCompareIntervalDto }) => {
  if (!interval.paceWithTime) {
    return <div>--</div>
  }

  const Pace = ({ paceWithTime }: { paceWithTime: PaceWithTime | null }) => {
    if (!paceWithTime) {
      return null
    }

    return <span className="text-sm">({`${paceWithTime.paceValue} ${paceWithTime.paceLabel}`})</span>
  }

  return (
    <div>
      <span className="text-sm font-bold mr-1">{interval.paceWithTime.timeFormatted}</span>
      <Pace paceWithTime={interval.paceWithTime} />
    </div>
  )
}
