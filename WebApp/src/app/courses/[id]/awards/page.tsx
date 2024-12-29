import React from "react"
import { getApi } from "@/app/_api/api"
import { AwardWinnerDto } from "@/app/_api/courses/definitions"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
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

export const dynamic = "force-dynamic"

const api = getApi()

interface Props {
  params: {
    id: string
  }
}

export default async function Page({ params: { id } }: Props) {
  const awards = await api.courses.awards(id)
  const courseDetails = await api.courses.details(id)

  const { locationInfoWithRank } = courseDetails

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
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
              <BreadcrumbPage>Awards</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="mb-8 text-purple-500 bold text-2xl">Awards</div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className="w-[25%]">Bracket Name</TableHead>
            <TableHead className="w-[25%]">First Place</TableHead>
            <TableHead className="w-[25%]">Second Place</TableHead>
            <TableHead className="w-[25%]">Third Place</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {awards.map((awardPodium) => (
            <TableRow key={awardPodium.bracketName}>
              <TableCell>
                <strong>{awardPodium.bracketName}</strong>
              </TableCell>
              <TableCell>
                <Athlete athlete={awardPodium.firstPlaceAthlete} />
              </TableCell>
              <TableCell>
                <Athlete athlete={awardPodium.secondPlaceAthlete} />
              </TableCell>
              <TableCell>
                <Athlete athlete={awardPodium.thirdPlaceAthlete} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  )
}

const Athlete = ({ athlete }: { athlete: AwardWinnerDto | null }) => {
  if (!athlete) {
    return <div>--</div>
  }

  return (
    <div>
      <div>
        <a href={`/athletes/${athlete.athleteId}`}>{athlete.fullName}</a>
      </div>
      <div>
        <span className="mr-1">
          <strong>{athlete.paceWithTime.timeFormatted}</strong>
        </span>
        {athlete.paceWithTime.hasPace && (
          <span>
            ({athlete.paceWithTime.paceValue} {athlete.paceWithTime.paceLabel})
          </span>
        )}
      </div>
      <div>
        <a href={`/results/${athlete.athleteCourseId}`} className="font-12" title="view result">
          View
        </a>
      </div>
    </div>
  )
}
