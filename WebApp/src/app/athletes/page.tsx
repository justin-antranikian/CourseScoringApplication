import React from "react"
import Content from "./_components/Content"
import { getApi } from "../_api/api"
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbPage } from "@/components/ui/breadcrumb"
import { DirectoryTreeView } from "../_components/DirectoryTreeView"
import { LocationType } from "../_components/LocationInfoRankings"
import AthleteSearch from "../_components/AthleteSearch"

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
          <DirectoryTreeView locations={directory} locationType={LocationType.athletes} />
        </div>
        <div className="w-3/4">
          <Content athletes={athletes} />
        </div>
      </div>
    </>
  )
}
