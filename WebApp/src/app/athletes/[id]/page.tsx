import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { BracketRank } from "@/app/_components/BracketRank"
import RankWithTime from "../_components/RankWithTime"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
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
  const arp = await api.athletes.details(id)
  const directory = await api.locations.directory()
  const { locationInfoWithRank } = arp

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <DropdownMenu>
                <DropdownMenuTrigger className="flex items-center gap-1">
                  <DirectorySheetView locations={directory} locationType={LocationType.athletes} />
                </DropdownMenuTrigger>
              </DropdownMenu>
            </BreadcrumbItem>
            <BreadcrumbItem>
              <BreadcrumbLink href="/athletes">All Athletes</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/athletes/directory/${locationInfoWithRank.stateUrl}`}>
                {locationInfoWithRank.state}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/athletes/directory/${locationInfoWithRank.areaUrl}`}>
                {locationInfoWithRank.area}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href={`/athletes/directory/${locationInfoWithRank.cityUrl}`}>
                {locationInfoWithRank.city}
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>{arp.fullName}</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div>
            <img style={{ width: "75%", height: 125 }} src="/Athlete.png" />
          </div>
          <div className="mt-2 text-2xl font-bold">{arp.fullName}</div>
          <div className="text-lg">
            {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {arp.genderAbbreviated} | {arp.age}
          </div>
          <LocationInfoRankings locationInfoWithRank={arp.locationInfoWithRank} locationType={LocationType.athletes} />
          <hr className="my-5" />
          <div className="my-3">{arp.firstName}'s training and diet</div>
          <ul className="list-disc pl-5 text-xs">
            {arp.wellnessTrainingAndDiet.map((entry, index) => (
              <li key={index}>{entry.description}</li>
            ))}
          </ul>
          <div className="my-3">{arp.firstName}'s goals</div>
          <ul className="list-disc pl-5 text-xs">
            {arp.wellnessGoals.map((entry, index) => (
              <li key={index}>{entry.description}</li>
            ))}
          </ul>
          <div className="my-3">{arp.firstName}'s inspiration</div>
          {arp.wellnessMotivationalList.map((entry) => {
            return <div className="pl-5 text-xs">{entry.description}</div>
          })}
        </div>
        <div className="w-3/4">
          <div className="mb-12 bold text-2xl text-purple-500">Results</div>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-[20%]">Event Name</TableHead>
                <TableHead className="w-[20%]">Overall</TableHead>
                <TableHead className="w-[20%]">Gender</TableHead>
                <TableHead className="w-[20%]">Division</TableHead>
                <TableHead className="w-[20%]">Total Time</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {arp.results.map((result) => {
                return (
                  <TableRow>
                    <TableCell>
                      <div>
                        <a href={`/races/${result.raceId}`}>{result.raceName}</a>
                      </div>
                      <div>
                        <a href={`/courses/${result.courseId}`}>{result.courseName}</a>
                      </div>
                      <div>
                        <a href={`/results/${result.athleteCourseId}`}>View</a>
                      </div>
                    </TableCell>
                    <TableCell>
                      <BracketRank rank={result.overallRank} total={result.overallCount} />
                    </TableCell>
                    <TableCell>
                      <BracketRank rank={result.genderRank} total={result.genderCount} />
                    </TableCell>
                    <TableCell>
                      <BracketRank rank={result.primaryDivisionRank} total={result.primaryDivisionCount} />
                    </TableCell>
                    <TableCell>
                      <RankWithTime paceTime={result.paceWithTimeCumulative} />
                    </TableCell>
                  </TableRow>
                )
              })}
            </TableBody>
          </Table>
        </div>
      </div>
    </>
  )
}
