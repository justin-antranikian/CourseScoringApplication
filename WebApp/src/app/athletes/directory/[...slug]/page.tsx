import { getApi } from "@/app/_api/api"
import { AthleteSearchResultDto } from "@/app/_api/athletes/definitions"
import React from "react"
import Content from "../../_components/Content"
import { LocationDto } from "@/app/_api/locations/definitions"
import {
  Breadcrumb,
  BreadcrumbList,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbSeparator,
  BreadcrumbPage,
} from "@/components/ui/breadcrumb"
import { TreeView } from "@/app/_components/TreeView"
import { LocationType } from "@/app/_components/LocationInfoRankings"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import { ChevronDownIcon } from "lucide-react"

interface Props {
  params: {
    slug: string[]
  }
}

interface SlugEntry {
  slug: string
  name: string
}

const formatName = (slugPart: string): string => {
  return slugPart.replace(/-/g, " ").replace(/\b\w/g, (char) => char.toUpperCase())
}

const api = getApi()

export default async function Page({ params: { slug } }: Props) {
  const routeSegment = slug.join("/")
  const locationResponse = await api.locations.bySlug(routeSegment)

  if (locationResponse.status === 404) {
    return <div>Could not find directory by {routeSegment}</div>
  }

  const location = (await locationResponse.json()) as LocationDto
  const response = await api.athletes.bySlug(routeSegment)
  const athletes = (await response.json()) as AthleteSearchResultDto[]
  const directory = await api.locations.directory(location.id)

  const slugEntries: SlugEntry[] = []
  const accumulatedSlug = []

  for (let i = 0; i < slug.length; i++) {
    accumulatedSlug.push(slug[i])
    slugEntries.push({
      slug: accumulatedSlug.join("/"),
      name: formatName(slug[i]),
    })
  }

  return (
    <>
      <div className="mb-5">
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <DropdownMenu>
                <DropdownMenuTrigger className="flex items-center gap-1">
                  View Races
                  <ChevronDownIcon />
                </DropdownMenuTrigger>
                <DropdownMenuContent align="start">
                  {slugEntries.map((slug) => {
                    return (
                      <DropdownMenuItem>
                        <a href={`/races/directory/${slug.slug}`}>{slug.name}</a>
                      </DropdownMenuItem>
                    )
                  })}
                </DropdownMenuContent>
              </DropdownMenu>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink href="/athletes">All Athletes</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            {slugEntries.slice(0, slugEntries.length - 1).map((slug) => {
              return (
                <>
                  <BreadcrumbItem>
                    <BreadcrumbLink href={`/athletes/directory/${slug.slug}`}>{slug.name}</BreadcrumbLink>
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
          <div className="max-w-md mx-auto">
            <ul className="list-none">
              <TreeView nodes={directory} locationType={LocationType.athletes} />
            </ul>
          </div>
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
