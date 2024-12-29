import React from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import RaceContent from "./RaceContent"
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
import { DropdownMenu, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import DirectorySheet from "../../_components/DirectorySheet"
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
  const raceLeaderboard = await api.races.details(id)
  const directory = await api.locations.directory()
  const { locationInfoWithRank } = raceLeaderboard

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
                <BreadcrumbPage>{raceLeaderboard.raceName}</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
          <div>
            <ResultSearch raceId={id} />
          </div>
        </div>
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
          <RaceContent raceLeaderboard={raceLeaderboard} />
        </div>
      </div>
    </>
  )
}
