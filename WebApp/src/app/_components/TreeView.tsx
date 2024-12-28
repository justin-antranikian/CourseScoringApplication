"use client"

import { LocationDto } from "@/app/_api/locations/definitions"
import { Minus, Plus } from "lucide-react"
import { useState } from "react"
import { LocationType } from "./LocationInfoRankings"
import { twMerge } from "tailwind-merge"

export const TreeView: React.FC<{ nodes: LocationDto[]; locationType: LocationType }> = ({ nodes, locationType }) => {
  return (
    <ul className="list-none">
      {nodes.map((node) => (
        <TreeNodeComponent node={node} locationType={locationType} />
      ))}
    </ul>
  )
  // return nodes.map((node) => <TreeNodeComponent node={node} locationType={locationType} />)
}

export const TreeNodeComponent: React.FC<{ node: LocationDto; locationType: LocationType }> = ({
  node,
  locationType,
}) => {
  const [isExpanded, setIsExpanded] = useState(node.isExpanded)

  const hasChildren = node.childLocations.length > 0

  return (
    <li>
      <div className={twMerge("flex items-center gap-2", !hasChildren && "pl-6")}>
        {hasChildren && (
          <button onClick={() => setIsExpanded(!isExpanded)} className="p-1 text-gray-500 hover:text-gray-700">
            {isExpanded ? <Minus size={16} /> : <Plus size={16} />}
          </button>
        )}
        <a href={`/${locationType}/directory/${node.slug}`}>
          <span
            className={twMerge(
              "text-gray-700 hover:underline",
              hasChildren && "font-bold",
              node.isSelected && "bg-gray-700 text-white px-1",
            )}
          >
            {node.name}
          </span>
        </a>
      </div>
      {hasChildren && isExpanded && (
        <ul className="pl-4 border-l-2 border-gray-300 ml-2">
          {node.childLocations.map((child, index) => (
            <TreeNodeComponent key={index} node={child} locationType={locationType} />
          ))}
        </ul>
      )}
    </li>
  )
}
