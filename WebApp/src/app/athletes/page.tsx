import React from "react"
import Content from "./_components/Content"
import { getApi } from "../_api/api"
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbPage } from "@/components/ui/breadcrumb"
import { DirectoryTreeView } from "../_components/DirectoryTreeView"
import { LocationType } from "../_components/LocationInfoRankings"

const api = getApi()

export default async function Page() {
  const athletes = await api.athletes.search()
  const directory = await api.locations.directory()

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbPage>All Athletes</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <DirectoryTreeView locations={directory} locationType={LocationType.athletes} />
        </div>
        <div className="w-3/4">
          <div className="flex flex-wrap -mx-2">
            <Content athletes={athletes} />
          </div>
        </div>
      </div>
    </>
  )
}
