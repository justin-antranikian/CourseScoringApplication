import { getApi } from "@/app/_api/api"
import { EventSearchResultDto } from "@/app/_api/races/definitions"
import React from "react"
import Content from "../../_components/Content"

interface Props {
  params: {
    slug: string[]
  }
}

const api = getApi()

export default async function Page({ params: { slug } }: Props) {
  const routeSegment = slug.join("/")
  const response = await api.races.bySlug(routeSegment)

  if (response.status === 404) {
    return <div>Could not find directory by {routeSegment}</div>
  }

  const races = (await response.json()) as EventSearchResultDto[]

  return (
    <div className="flex gap-1">
      <div className="w-1/4">Directory</div>
      <div className="w-3/4">
        <div className="flex flex-wrap -mx-2">
          <Content events={races} />
        </div>
      </div>
    </div>
  )
}
