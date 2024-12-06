import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime, { PaceWithTime } from "@/app/_components/IntervalTime"
import { Irp } from "@/app/results/[id]/definitions"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { InfoIcon } from "lucide-react"
import React from "react"

export default function IrpDetails({ irpDetails }: { irpDetails: Irp }) {
  return (
    <>
      <div className="my-5 text-purple-500 text-2xl">{irpDetails.bib}</div>
      <table className="table-auto w-full text-sm">
        <thead>
          <tr className="border-b border-black">
            <th className="w-[25%] text-left py-2" scope="col"></th>
            <th className="w-[5%] text-left py-2" scope="col"></th>
            <th className="w-[35%] text-left py-2" scope="col">
              Interval Time
            </th>
            <th className="w-[35%] text-left py-2" scope="col">
              Cumulative Time
            </th>
          </tr>
        </thead>
        <tbody>
          {irpDetails.intervalResults.map((intervalResult, index) => (
            <tr className="border-b border-gray-300" key={index}>
              <td className="py-2">{intervalResult.intervalName}</td>
              <td className="py-2">
                <Popover>
                  <PopoverTrigger>
                    <InfoIcon size={10} />
                  </PopoverTrigger>
                  <PopoverContent>
                    <table className="w-full">
                      <thead>
                        <tr className="border-b border-black">
                          <th className="w-[33%] text-left py-2">Overall</th>
                          <th className="w-[33%] text-left py-2">Gender</th>
                          <th className="w-[33%] text-left py-2">Division</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>
                            <BracketRank
                              rank={intervalResult.overallRank}
                              total={intervalResult.overallCount}
                              indicator={intervalResult.overallIndicator}
                            />
                          </td>
                          <td>
                            <BracketRank
                              rank={intervalResult.genderRank}
                              total={intervalResult.genderCount}
                              indicator={intervalResult.genderIndicator}
                            />
                          </td>
                          <td>
                            <BracketRank
                              rank={intervalResult.primaryDivisionRank}
                              total={intervalResult.primaryDivisionCount}
                              indicator={
                                intervalResult.primaryDivisionIndicator
                              }
                            />
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </PopoverContent>
                </Popover>
              </td>
              <td className="py-2">
                <IntervalTime
                  paceTime={intervalResult.paceWithTimeIntervalOnly}
                />
              </td>
              <td className="py-2">
                <IntervalTime
                  paceTime={intervalResult.paceWithTimeCumulative}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}
