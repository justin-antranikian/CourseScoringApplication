import { CircleChevronDown, CircleChevronUp } from "lucide-react"
import React from "react"

interface Props {
  hideComparePane: boolean
  setHideComparePane: React.Dispatch<React.SetStateAction<boolean>>
  idsEncoded: string
  selectedIds: number[]
  thickOpactity?: boolean
}

export default function ComparePane({
  hideComparePane,
  setHideComparePane,
  idsEncoded,
  selectedIds,
  thickOpactity,
}: Props) {
  const opacity = thickOpactity ? "80" : "50"

  if (hideComparePane) {
    return (
      <div
        className={`fixed bottom-0 left-0 w-full bg-gray-200 bg-opacity-20 text-black px-4 py-1 flex justify-end`}
      >
        <span
          onClick={() => setHideComparePane(false)}
          className="cursor-pointer"
        >
          <CircleChevronUp size={14} />
        </span>
      </div>
    )
  }

  return (
    <div
      className={`fixed bottom-0 left-0 py-3 w-full bg-gray-200 bg-opacity-${opacity} text-black flex items-center justify-between px-4`}
    >
      <div className="text-center flex-1">
        <a href={`/compare-results?ids=${idsEncoded}`}>
          Compare ({selectedIds.length})
        </a>
      </div>
      <div className="text-right">
        <span
          className="cursor-pointer"
          onClick={() => setHideComparePane(true)}
        >
          <CircleChevronDown size={16} />
        </span>
      </div>
    </div>
  )
}
