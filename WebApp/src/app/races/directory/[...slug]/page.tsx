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
} from "@/components/ui/breadcrumb"
import { LocationDto } from "@/app/_api/locations/definitions"

interface Props {
  params: {
    slug: string[]
  }
}

const api = getApi()

interface SlugEntry {
  slug: string
  name: string
}

export default async function Page({ params: { slug } }: Props) {
  const routeSegment = slug.join("/")
  const locationResponse = await api.locations.bySlug(routeSegment)

  if (locationResponse.status === 404) {
    return <div>Could not find directory by {routeSegment}</div>
  }

  const location = (await locationResponse.json()) as LocationDto

  const response = await api.races.bySlug(routeSegment)
  const races = (await response.json()) as EventSearchResultDto[]

  const slugEntries: SlugEntry[] = []
  const accumulatedSlug = []

  // Helper function to format names
  const formatName = (slugPart: string): string =>
    slugPart
      .replace(/-/g, " ") // Replace hyphens with spaces
      .replace(/\b\w/g, (char) => char.toUpperCase()) // Capitalize each word

  for (let i = 0; i < slug.length - 1; i++) {
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
              <BreadcrumbLink href="/races">All Races</BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            {slugEntries.map((slug) => {
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
          <div className="max-w-md mx-auto">
            <ul className="list-none">
              <li>
                <span className="text-gray-700 font-bold">Colorado</span>
                <ul className="pl-4 border-l-2 border-gray-300 ml-2">
                  <li>
                    <span className="text-gray-600 font-semibold">Greater Colorado Springs Area</span>
                    <ul className="pl-4 border-l-2 border-gray-300 ml-2">
                      <li>
                        <span className="text-gray-500">Colorado Springs</span>
                      </li>
                    </ul>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </div>
        <div className="w-3/4">
          <div className="flex flex-wrap -mx-2">
            <Content events={races} />
          </div>
        </div>
      </div>
    </>
  )
}
