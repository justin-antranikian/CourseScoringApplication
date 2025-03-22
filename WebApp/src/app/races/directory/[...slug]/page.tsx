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

const getLocation = async (slug: string[]): Promise<LocationDto | null> => {
  const locationResponse = await api.locations.bySlug(slug.join("/"))

  if (locationResponse.status === 404) {
    return null
  }

  return await locationResponse.json()
}

export default async function Page({ params }: { params: Promise<{ slug: string[] }> }) {
  const { slug } = await params
  const location = await getLocation(slug)

  if (!location) {
    return <div>Could not find location.</div>
  }

  const [races, directory] = await Promise.all([
    api.races.search({ locationId: location.id, locationType: location.locationType }),
    api.locations.directory(location.id),
  ])

  const slugEntries = getSlugEntries(slug)

  return (
    <>
      <div className="mb-5">
        <div className="flex justify-between">
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem key={1}>
                <DropdownMenu>
                  <DropdownMenuTrigger className="flex items-center gap-1">
                    <BreadcrumbEllipsis title="Athlete Quick Navigation" className="h-4 w-4" />
                    <span className="sr-only">Toggle menu</span>
                  </DropdownMenuTrigger>
                  <DropdownMenuContent align="start">
                    <DropdownMenuLabel>View Athletes</DropdownMenuLabel>
                    <DropdownMenuSeparator />
                    {slugEntries.map(({ slug, name }) => {
                      return (
                        <DropdownMenuItem key={"race-" + slug}>
                          <a href={`/athletes/directory/${slug}`}>{name}</a>
                        </DropdownMenuItem>
                      )
                    })}
                  </DropdownMenuContent>
                </DropdownMenu>
              </BreadcrumbItem>
              <BreadcrumbItem key={2}>
                <BreadcrumbLink href="/races">All Races</BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
              {slugEntries.slice(0, slugEntries.length - 1).map(({ slug, name }) => {
                return (
                  <React.Fragment key={"athlete-" + slug}>
                    <BreadcrumbItem>
                      <BreadcrumbLink href={`/races/directory/${slug}`}>{name}</BreadcrumbLink>
                    </BreadcrumbItem>
                    <BreadcrumbSeparator />
                  </React.Fragment>
                )
              })}
              <BreadcrumbItem key={3}>
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
        <div className="w-3/4 flex flex-wrap">
          {races.length === 0 ? (
            <div className="text-lg font-bold text-red-500">There are no races at this location</div>
          ) : (
            <RacesContent races={races} />
          )}
        </div>
      </div>
    </>
  )
}
