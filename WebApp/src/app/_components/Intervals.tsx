import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime from "@/app/_components/IntervalTime"
import { Irp } from "../_api/results/definitions"

export default function Intervals({ irp }: { irp: Irp }) {
  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead></TableHead>
          <TableHead>Time ({irp.timeZoneAbbreviated})</TableHead>
          <TableHead>Overall</TableHead>
          <TableHead>Gender</TableHead>
          <TableHead>Division</TableHead>
          <TableHead>Interval Time</TableHead>
          <TableHead>Cumulative Time</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {irp.intervalResults.map((intervalResult, index) => (
          <TableRow key={index}>
            <TableCell>
              <div className="truncate max-w-[100px]">{intervalResult.intervalName}</div>
            </TableCell>
            <TableCell>
              <span className="text-lg">{intervalResult.crossingTime}</span>
            </TableCell>
            <TableCell>
              <BracketRank
                rank={intervalResult.overallRank}
                total={intervalResult.overallCount}
                indicator={intervalResult.overallIndicator}
              />
            </TableCell>
            <TableCell>
              <BracketRank
                rank={intervalResult.genderRank}
                total={intervalResult.genderCount}
                indicator={intervalResult.genderIndicator}
              />
            </TableCell>
            <TableCell>
              <BracketRank
                rank={intervalResult.primaryDivisionRank}
                total={intervalResult.primaryDivisionCount}
                indicator={intervalResult.primaryDivisionIndicator}
              />
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
  )
}
