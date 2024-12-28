"use client"

import { LocationDto } from "@/app/_api/locations/definitions"
import { Minus, Plus } from "lucide-react"
import { useState } from "react"
import { LocationType } from "./LocationInfoRankings"

export const TreeView: React.FC<{ nodes: LocationDto[]; locationType: LocationType }> = ({ nodes, locationType }) => {
  return nodes.map((node) => {
    return <TreeNodeComponent node={node} locationType={locationType} />
  })
}

export const TreeNodeComponent: React.FC<{ node: LocationDto; locationType: LocationType }> = ({
  node,
  locationType,
}) => {
  const [isExpanded, setIsExpanded] = useState(false)

  const hasChildren = node.childLocations.length > 0

  return (
    <li>
      <div
        className={`flex items-center gap-2 ${
          !hasChildren ? "pl-6" : "" // Add extra left padding if no children
        }`}
      >
        {hasChildren && (
          <button onClick={() => setIsExpanded(!isExpanded)} className="p-1 text-gray-500 hover:text-gray-700">
            {isExpanded ? <Minus size={16} /> : <Plus size={16} />}
          </button>
        )}
        <a href={`/${locationType}/directory/${node.slug}`}>
          <span className="text-gray-700 font-bold">{node.name}</span>
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
