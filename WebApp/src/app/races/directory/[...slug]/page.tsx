import { getApi } from "@/app/_api/api"
import { EventSearchResultDto } from "@/app/_api/races/definitions"
import React from "react"
import Content from "../../_components/Content"
import {
  Breadcrumb,
  BreadcrumbList,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbSeparator,
  BreadcrumbPage,
  BreadcrumbEllipsis,
} from "@/components/ui/breadcrumb"
import { LocationDto } from "@/app/_api/locations/definitions"
import { DirectoryTreeView } from "../../../_components/DirectoryTreeView"
import { LocationType } from "@/app/_components/LocationInfoRankings"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { getSlugEntries } from "@/utils"
import ResultSearch from "@/app/_components/ResultSearch"

interface Props {
  params: {
    slug: string[]
  }
}

const api = getApi()

export default async function Page({ params: { slug } }: Props) {
  const routeSegment = slug.join("/")
  const locationResponse = await api.locations.bySlug(routeSegment)

  if (locationResponse.status === 404) {
    return <div>Could not find directory by {routeSegment}</div>
  }

  const location = (await locationResponse.json()) as LocationDto
  const response = await api.races.bySlug(routeSegment)
  const races = (await response.json()) as EventSearchResultDto[]
  const directory = await api.locations.directory(location.id)

  const slugEntries = getSlugEntries(slug)

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <DropdownMenu>
                <DropdownMenuTrigger className="flex items-center gap-1">
                  <BreadcrumbEllipsis title="Athlete Quick Navigation" className="h-4 w-4" />
                  <span className="sr-only">Toggle menu</span>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="start">
                  <DropdownMenuLabel>View Athletes</DropdownMenuLabel>
                  <DropdownMenuSeparator />
                  {slugEntries.map((slug) => {
                    return (
                      <DropdownMenuItem>
                        <a href={`/athletes/directory/${slug.slug}`}>{slug.name}</a>
                      </DropdownMenuItem>
                    )
                  })}
                </DropdownMenuContent>
              </DropdownMenu>
            </BreadcrumbItem>
            <BreadcrumbItem>
              <BreadcrumbLink href="/races">All Races</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            {slugEntries.slice(0, slugEntries.length - 1).map((slug) => {
              return (
                <>
                  <BreadcrumbItem>
                    <BreadcrumbLink href={`/races/directory/${slug.slug}`}>{slug.name}</BreadcrumbLink>
                  </BreadcrumbItem>
                  <BreadcrumbSeparator />
                </>
              )
            })}
            <BreadcrumbItem>
              <BreadcrumbPage>{location.name}</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <DirectoryTreeView locations={directory} locationType={LocationType.races} />
        </div>
        <div className="w-3/4">
          <div className="mb-3">
            <ResultSearch raceId={1} />
          </div>
          <Content events={races} />
        </div>
      </div>
    </>
  )
}
