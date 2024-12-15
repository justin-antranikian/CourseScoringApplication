import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip"
import { CircleChevronDown, CircleChevronUp } from "lucide-react"
import React from "react"
import { twMerge } from "tailwind-merge"

interface Props {
  hideComparePane: boolean
  setHideComparePane: React.Dispatch<React.SetStateAction<boolean>>
  idsEncoded: string
  selectedIds: number[]
  url: string
  thickOpactity?: boolean
}

export default function ComparePane({
  hideComparePane,
  setHideComparePane,
  idsEncoded,
  selectedIds,
  url,
  thickOpactity,
}: Props) {
  if (hideComparePane) {
    return (
      <div
        className={twMerge(
          "fixed bottom-0 left-0 w-full bg-gray-200 text-black px-4 py-1 flex justify-end",
          thickOpactity ? "bg-opacity-70" : "bg-opacity-30",
        )}
      >
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger>
              <span
                onClick={() => setHideComparePane(false)}
                className="cursor-pointer"
              >
                <CircleChevronUp size={14} />
              </span>
            </TooltipTrigger>
            <TooltipContent>
              <p>Expand Compare</p>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </div>
    )
  }

  return (
    <div
      className={twMerge(
        "fixed bottom-0 left-0 py-3 w-full bg-gray-200 text-black flex items-center justify-between px-4",
        thickOpactity ? "bg-opacity-70" : "bg-opacity-50",
      )}
    >
      <div className="text-center flex-1">
        <a href={`/${url}?ids=${idsEncoded}`}>Compare ({selectedIds.length})</a>
      </div>
      <div className="text-right">
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger>
              <span
                onClick={() => setHideComparePane(true)}
                className="cursor-pointer"
              >
                <CircleChevronDown size={16} />
              </span>
            </TooltipTrigger>
            <TooltipContent>
              <p>Hide Compare</p>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </div>
    </div>
  )
}
