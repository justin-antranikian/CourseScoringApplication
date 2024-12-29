import React from "react"
import { getApi } from "../_api/api"
import Content from "./_components/Content"
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbPage } from "@/components/ui/breadcrumb"
import { DirectoryTree } from "../_components/DirectoryTree"
import { LocationType } from "../_components/LocationInfoRankings"
import RaceSearch from "../_components/RaceSearch"

const api = getApi()

export default async function Page() {
  const events = await api.races.search()
  const directory = await api.locations.directory()

  return (
    <>
      <div className="mb-5">
        <div className="flex justify-between">
          {/* <div> */}
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem>
                <BreadcrumbPage>All Races</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
          {/* </div> */}
          <div>
            <RaceSearch />
          </div>
        </div>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <DirectoryTree locations={directory} locationType={LocationType.races} />
        </div>
        <div className="w-3/4">
          <Content events={events} />
        </div>
      </div>
    </>
  )
}
