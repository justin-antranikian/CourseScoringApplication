import React from "react"
import { InfoIcon } from "lucide-react"
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { apiCaller } from "@/app/_api/api"
import { CompareIrpsIntervalDto } from "@/app/_api/results/definitions"
import { BracketRank } from "@/app/_components/BracketRank"
import { PaceWithTime } from "@/app/_components/IntervalTime"

interface Props {
  searchParams: {
    ids: string
  }
}

const api = apiCaller()

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const irpsToCompare = await api.results.compare(ids)
  const intervalNames = irpsToCompare[0].compareIrpsIntervalDtos.map((inteval) => inteval.intervalName)

  return (
    <>
      <div className="mb-8 text-purple-500 bold text-2xl">Result Compare</div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Athlete Info</TableHead>
            {intervalNames.map((interval: string, index: number) => (
              <TableHead key={index}>{interval}</TableHead>
            ))}
          </TableRow>
        </TableHeader>
        <TableBody>
          {irpsToCompare.map((athleteInfo) => {
            return (
              <TableRow key={athleteInfo.athleteCourseId}>
                <TableCell>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap text-xl">{athleteInfo.fullName}</div>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap">
                    {athleteInfo.city}, {athleteInfo.state}
                  </div>
                  <div className="text-xs">
                    {athleteInfo.genderAbbreviated} | {athleteInfo.raceAge}
                  </div>
                </TableCell>
                {athleteInfo.compareIrpsIntervalDtos.map((result) => {
                  return (
                    <TableCell>
                      <div>{result.crossingTime ? result.crossingTime : "--"}</div>
                      <PaceContent result={result} />
                      <div className="mt-1">
                        <Popover>
                          <PopoverTrigger>
                            <span title="Bracket Details">
                              <InfoIcon size={10} />
                            </span>
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
                                    <BracketRank rank={result.overallRank} total={0} />
                                  </td>
                                  <td>
                                    <BracketRank rank={result.genderRank} total={0} />
                                  </td>
                                  <td>
                                    <BracketRank rank={result.primaryDivisionRank} total={0} />
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </PopoverContent>
                        </Popover>
                      </div>
                    </TableCell>
                  )
                })}
              </TableRow>
            )
          })}
        </TableBody>
      </Table>
    </>
  )
}

const PaceContent = ({ result }: { result: CompareIrpsIntervalDto }) => {
  if (!result.paceWithTime) {
    return <div>--</div>
  }

  const Pace = ({ paceWithTime }: { paceWithTime: PaceWithTime | null }) => {
    if (!paceWithTime) {
      return null
    }

    return <span className="text-sm">({`${paceWithTime.paceValue} ${paceWithTime.paceLabel}`})</span>
  }

  return (
    <div>
      <span className="text-sm font-bold mr-1">{result.paceWithTime.timeFormatted}</span>
      <Pace paceWithTime={result.paceWithTime} />
    </div>
  )
}
