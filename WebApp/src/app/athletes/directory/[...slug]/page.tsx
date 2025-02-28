import { getApi } from "@/app/_api/api"
import React from "react"
import AthletesContent from "../../AthletesContent"
import { LocationDto } from "@/app/_api/locations/definitions"
import {
  Breadcrumb,
  BreadcrumbList,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbSeparator,
  BreadcrumbPage,
  BreadcrumbEllipsis,
} from "@/components/ui/breadcrumb"
import { DirectoryTree } from "@/app/_components/DirectoryTree"
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
import AthleteSearch from "@/app/_components/AthleteSearch"

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

  const [athletes, directory] = await Promise.all([
    api.athletes.search(location.id, location.locationType),
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
                    <BreadcrumbEllipsis title="Race Quick Navigation" className="h-4 w-4" />
                    <span className="sr-only">Toggle menu</span>
                  </DropdownMenuTrigger>
                  <DropdownMenuContent align="start">
                    <DropdownMenuLabel>View Races</DropdownMenuLabel>
                    <DropdownMenuSeparator />
                    {slugEntries.map(({ slug, name }, index) => {
                      return (
                        <DropdownMenuItem key={index}>
                          <a href={`/races/directory/${slug}`}>{name}</a>
                        </DropdownMenuItem>
                      )
                    })}
                  </DropdownMenuContent>
                </DropdownMenu>
              </BreadcrumbItem>
              <BreadcrumbItem key={2}>
                <BreadcrumbLink href="/athletes">All Athletes</BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
              {slugEntries.slice(0, slugEntries.length - 1).map(({ slug, name }) => {
                return (
                  <>
                    <BreadcrumbItem key={slug}>
                      <BreadcrumbLink href={`/athletes/directory/${slug}`}>{name}</BreadcrumbLink>
                    </BreadcrumbItem>
                    <BreadcrumbSeparator />
                  </>
                )
              })}
              <BreadcrumbItem key={3}>
                <BreadcrumbPage>{location.name}</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
        </div>
      </div>
      <AthletesContent
        athletes={athletes}
        directoryTree={<DirectoryTree locations={directory} locationType={LocationType.athletes} />}
        athleteSearch={<AthleteSearch locationId={location.id} locationType={location.locationType} />}
      />
    </>
  )
}
