import React from "react"
import AthletesContent from "./AthletesContent"
import { getApi } from "../_api/api"
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbPage } from "@/components/ui/breadcrumb"
import { DirectoryTree } from "../_components/DirectoryTree"
import { LocationType } from "../_components/LocationInfoRankings"
import AthleteSearch from "../_components/AthleteSearch"

export const dynamic = "force-dynamic"

const api = getApi()

export default async function Page() {
  const athletes = await api.athletes.search()
  const directory = await api.locations.directory()

  return (
    <>
      <div className="mb-5">
        <div className="flex justify-between">
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem>
                <BreadcrumbPage>All Athletes</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
          <div>
            <AthleteSearch />
          </div>
        </div>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <DirectoryTree locations={directory} locationType={LocationType.athletes} />
        </div>
        <div className="w-3/4">
          <AthletesContent athletes={athletes} />
        </div>
      </div>
    </>
  )
}
