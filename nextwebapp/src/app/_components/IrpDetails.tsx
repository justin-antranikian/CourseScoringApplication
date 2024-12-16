import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime from "@/app/_components/IntervalTime"
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { InfoIcon } from "lucide-react"
import React from "react"
import { Irp } from "../_api/results/definitions"

export default function IrpDetails({ irpDetails }: { irpDetails: Irp }) {
  return (
    <>
      <div className="my-5 text-purple-500 text-2xl">{irpDetails.bib}</div>
      <Table className="table-fixed w-full">
        <TableHeader>
          <TableRow>
            <TableHead className="w-4/12 pl-0">Athlete Info</TableHead>
            <TableHead className="w-4/12">Interval Time</TableHead>
            <TableHead className="w-4/12">Cumulative Time</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {irpDetails.intervalResults.map((intervalResult, index) => (
            <TableRow key={index}>
              <TableCell className="pl-0">
                <div>{intervalResult.intervalName}</div>
                <div className="mt-2">
                  <Popover>
                    <PopoverTrigger>
                      <InfoIcon size={10} />
                    </PopoverTrigger>
                    <PopoverContent>
                      <table className="w-full table-fixed">
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
                                indicator={intervalResult.primaryDivisionIndicator}
                              />
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </PopoverContent>
                  </Popover>
                </div>
              </TableCell>
              <TableCell>
                <IntervalTime paceTime={intervalResult.paceWithTimeIntervalOnly} />
              </TableCell>
              <TableCell>
                <IntervalTime paceTime={intervalResult.paceWithTimeCumulative} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  )
}
