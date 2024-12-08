"use client"

import {
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
  Tooltip,
} from "@/components/ui/tooltip"
import { InfoIcon } from "lucide-react"
import React from "react"

export default function BracketTooltip() {
  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger>
          <InfoIcon size={10} />
        </TooltipTrigger>
        <TooltipContent>
          <p>View Brackets</p>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
