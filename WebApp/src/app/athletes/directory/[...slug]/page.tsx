import { getApi } from "@/app/_api/api"
import { AthleteSearchResultDto } from "@/app/_api/athletes/definitions"
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
  const response = await api.athletes.bySlug(routeSegment)

  if (response.status === 404) {
    return <div>Content not found</div>
  }

  const athletes = (await response.json()) as AthleteSearchResultDto[]

  return (
    <div className="flex gap-1">
      <div className="w-1/4">Directory</div>
      <div className="w-3/4">
        <div className="flex flex-wrap -mx-2">
          <Content athletes={athletes} />
        </div>
      </div>
    </div>
  )
}
