import { BreadcrumbItem, BreadcrumbLink, BreadcrumbSeparator } from "@/components/ui/breadcrumb"
import React from "react"
import { LocationInfoWithRank, LocationType } from "./LocationInfoRankings"

export default function LocationBreadcrumbs({
  locationInfoWithRank,
  locationType,
}: {
  locationInfoWithRank: LocationInfoWithRank
  locationType: LocationType
}) {
  return (
    <>
      <BreadcrumbItem>
        <BreadcrumbLink href={`/${locationType}/directory/${locationInfoWithRank.stateUrl}`}>
          {locationInfoWithRank.state}
        </BreadcrumbLink>
      </BreadcrumbItem>
      <BreadcrumbSeparator />
      <BreadcrumbItem>
        <BreadcrumbLink href={`/${locationType}/directory/${locationInfoWithRank.areaUrl}`}>
          {locationInfoWithRank.area}
        </BreadcrumbLink>
      </BreadcrumbItem>
      <BreadcrumbSeparator />
      <BreadcrumbItem>
        <BreadcrumbLink href={`/${locationType}/directory/${locationInfoWithRank.cityUrl}`}>
          {locationInfoWithRank.city}
        </BreadcrumbLink>
      </BreadcrumbItem>
      <BreadcrumbSeparator />
    </>
  )
}
