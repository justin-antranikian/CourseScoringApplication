import { getApi } from "@/app/_api/api"
import React from "react"
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
import { DirectoryTree } from "../../../_components/DirectoryTree"
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
import RaceSearch from "@/app/_components/RaceSearch"
import RacesContent from "../../RacesContent"

export const dynamic = "force-dynamic"

const api = getApi()

export default async function Page({
  params: { slug },
}: {
  params: {
    slug: string[]
  }
}) {
  const routeSegment = slug.join("/")
  const locationResponse = await api.locations.bySlug(routeSegment)

  if (locationResponse.status === 404) {
    return <div>Could not find directory by {routeSegment}</div>
  }

  const location = (await locationResponse.json()) as LocationDto
  const races = await api.races.search(location.id, location.locationType)
  const directory = await api.locations.directory(location.id)

  const slugEntries = getSlugEntries(slug)

  return (
    <>
      <div className="mb-5">
        <div className="flex justify-between">
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
          <div>
            <RaceSearch locationId={location.id} locationType={location.locationType} />
          </div>
        </div>
      </div>
      <div className="flex gap-1">
        <div className="w-1/4">
          <DirectoryTree locations={directory} locationType={LocationType.races} />
        </div>
        <div className="w-3/4">
          {races.length === 0 ? (
            <div className="text-lg font-bold text-red-500">There are no races at this location</div>
          ) : (
            <RacesContent events={races} />
          )}
        </div>
      </div>
    </>
  )
}
