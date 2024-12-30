import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getApi } from "@/app/_api/api"
import { CompareIrpsIntervalDto } from "@/app/_api/results/definitions"
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

interface Props {
  searchParams: {
    ids: string
  }
}

export const dynamic = "force-dynamic"

const api = getApi()

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const irpsToCompare = await api.results.compare(ids)
  const intervalNames = irpsToCompare[0].compareIrpsIntervalDtos.map((inteval) => inteval.intervalName)
  const courseId = irpsToCompare[0].courseId

  const courseDetails = await api.courses.details(courseId)
  const directory = await api.locations.directory()
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
              <BreadcrumbLink href={`/courses/${courseDetails.courseId}`}>{courseDetails.courseName}</BreadcrumbLink>
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
          {irpsToCompare.map((athleteInfo) => {
            return (
              <TableRow key={athleteInfo.athleteCourseId}>
                <TableCell>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap text-xl">{athleteInfo.fullName}</div>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap">
                    {athleteInfo.city}, {athleteInfo.state}
                  </div>
                  <div className="text-xs">
                    {athleteInfo.genderAbbreviated} | {athleteInfo.raceAge}
                  </div>
                </TableCell>
                {athleteInfo.compareIrpsIntervalDtos.map((result) => {
                  return (
                    <TableCell>
                      <div>{result.crossingTime ? result.crossingTime : "--"}</div>
                      <PaceContent result={result} />
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

const PaceContent = ({ result }: { result: CompareIrpsIntervalDto }) => {
  if (!result.paceWithTime) {
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
      <span className="text-sm font-bold mr-1">{result.paceWithTime.timeFormatted}</span>
      <Pace paceWithTime={result.paceWithTime} />
    </div>
  )
}
