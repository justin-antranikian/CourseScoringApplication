import { Irp, IrpResultByIntervalDto } from "@/app/_api/results/definitions"
import { BracketRank } from "@/app/_components/BracketRank"
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover"
import { CheckCircle, Circle } from "lucide-react"
import { ReactNode } from "react"

import React from "react"

export default function PizzaTracker({ irp }: { irp: Irp }) {
  const IntervalPopoverContent = ({
    intervalResult,
    children,
  }: {
    intervalResult: IrpResultByIntervalDto
    children: ReactNode
  }) => {
    return (
      <Popover>
        <PopoverTrigger asChild>{children}</PopoverTrigger>
        <PopoverContent className="p-4">
          <div className="mb-4">
            {intervalResult.intervalName} is <span className="underline">completed</span>
          </div>
          <hr className="mb-4" />
          <div>
            <span className="mr-2">Overall:</span>
            <BracketRank rank={intervalResult.overallRank} total={intervalResult.overallCount} indicator={undefined} />
          </div>
          <div>
            <span className="mr-2">Gender:</span>
            <BracketRank rank={intervalResult.genderRank} total={intervalResult.genderCount} indicator={undefined} />
          </div>
          <div className="mb-4">
            <span className="mr-2">Division:</span>
            <BracketRank
              rank={intervalResult.primaryDivisionRank}
              total={intervalResult.primaryDivisionCount}
              indicator={undefined}
            />
          </div>
          <hr className="mb-4" />
          <div>
            <span className="mr-2">Interval Time:</span>
            <span className="text-lg font-bold">{intervalResult.paceWithTimeIntervalOnly!.timeFormatted}</span>
          </div>
          <div>
            <span className="mr-2">Cumulative Time:</span>
            <span className="text-lg font-bold">{intervalResult.paceWithTimeCumulative!.timeFormatted}</span>
          </div>
        </PopoverContent>
      </Popover>
    )
  }

  return (
    <>
      {irp.intervalResults.map((intervalResult, index) => (
        <div key={index} className="flex flex-col items-center">
          {intervalResult.crossingTime ? (
            <IntervalPopoverContent intervalResult={intervalResult}>
              <button title={intervalResult.intervalName}>
                <CheckCircle className="w-6 h-6 text-green-500 cursor-pointer" />
              </button>
            </IntervalPopoverContent>
          ) : (
            <span title={`${intervalResult.intervalName} (not started)`}>
              <Circle className="w-6 h-6 text-gray-300" />
            </span>
          )}
        </div>
      ))}
    </>
  )
}
