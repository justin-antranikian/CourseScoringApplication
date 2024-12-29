"use client"

import { LocationDto } from "@/app/_api/locations/definitions"
import { Minus, Plus } from "lucide-react"
import { useState } from "react"
import { LocationType } from "./LocationInfoRankings"
import { twMerge } from "tailwind-merge"

export const DirectoryTree: React.FC<{ locations: LocationDto[]; locationType: LocationType }> = ({
  locations,
  locationType,
}) => {
  return (
    <ul className="list-none">
      {locations.map((location) => (
        <LocationNode location={location} locationType={locationType} />
      ))}
    </ul>
  )
}

const LocationNode: React.FC<{ location: LocationDto; locationType: LocationType }> = ({ location, locationType }) => {
  const [isExpanded, setIsExpanded] = useState(location.isExpanded)
  const hasChildren = location.childLocations.length > 0

  return (
    <li>
      <div className={twMerge("flex items-center gap-2", !hasChildren && "pl-6")}>
        {hasChildren && (
          <button onClick={() => setIsExpanded(!isExpanded)} className="p-1 text-gray-500 hover:text-gray-700">
            {isExpanded ? <Minus size={16} /> : <Plus size={16} />}
          </button>
        )}
        <a href={`/${locationType}/directory/${location.slug}`}>
          <span
            className={twMerge(
              "text-gray-700 hover:underline",
              hasChildren && "font-bold",
              location.isSelected && "bg-gray-700 text-white px-1",
            )}
          >
            {location.name}
          </span>
        </a>
      </div>
      {hasChildren && isExpanded && (
        <ul className="pl-4 border-l-2 border-gray-300 ml-2">
          {location.childLocations.map((child, index) => (
            <LocationNode key={index} location={child} locationType={locationType} />
          ))}
        </ul>
      )}
    </li>
  )
}
