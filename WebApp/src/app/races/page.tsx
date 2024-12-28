import React from "react"
import { getApi } from "../_api/api"
import Content from "./_components/Content"
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbPage } from "@/components/ui/breadcrumb"
import { TreeView } from "../_components/TreeView"
import { LocationType } from "../_components/LocationInfoRankings"

const api = getApi()

export default async function Page() {
  const events = await api.races.search()
  const directory = await api.locations.directory()

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbPage>All Races</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <div className="max-w-md mx-auto">
            <ul className="list-none">
              <TreeView nodes={directory} locationType={LocationType.races} />
            </ul>
          </div>
        </div>
        <div className="w-3/4">
          <Content events={events} />
        </div>
      </div>
    </>
  )
}
