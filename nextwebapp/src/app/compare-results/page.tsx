import { config } from "@/config"
import React from "react"
import {
  CompareIrpsAthleteInfoDto,
  CompareIrpsIntervalDto,
} from "./definitions"
import { InfoIcon } from "lucide-react"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { BracketRank } from "../_components/BracketRank"
import { PaceWithTime } from "../_components/IntervalTime"

interface Props {
  searchParams: {
    ids: string
  }
}

const getData = async (
  athleteCourseIds: string[],
): Promise<CompareIrpsAthleteInfoDto[]> => {
  const url = `${config.apiHost}/compareIrpApi`

  const requestInit: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ athleteCourseIds }),
    cache: "no-store",
  }

  const response = await fetch(url, requestInit)
  return await response.json()
}

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const irpsToCompare = await getData(ids)

  const intervalNames = irpsToCompare[0].compareIrpsIntervalDtos.map(
    (inteval) => inteval.intervalName,
  )

  return (
    <>
      <div className="mb-8 text-purple-500 bold text-2xl">
        Result Comparision
      </div>
      <table className="table-auto w-full">
        <thead>
          <tr>
            <th className="text-left py-2 border-b border-black" scope="col">
              Athlete Info
            </th>
            {intervalNames.map((interval: string, index: number) => (
              <th
                key={index}
                className="text-left py-2 border-b border-black"
                scope="col"
              >
                {interval}
              </th>
            ))}
          </tr>
        </thead>
        <thead>
          {irpsToCompare.map((athleteInfo) => {
            return (
              <tr key={athleteInfo.athleteCourseId}>
                <td
                  className="text-left py-2 border-b border-gray-200"
                  scope="col"
                >
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap text-2xl">
                    {athleteInfo.fullName}
                  </div>
                  <div className="overflow-hidden text-ellipsis whitespace-nowrap">
                    {athleteInfo.city}, {athleteInfo.state}
                  </div>
                  <div className="text-xs">
                    {athleteInfo.genderAbbreviated} | {athleteInfo.raceAge}
                  </div>
                </td>
                {athleteInfo.compareIrpsIntervalDtos.map((result) => {
                  return (
                    <td
                      className="text-left py-2 border-b border-gray-200"
                      scope="col"
                    >
                      <div>
                        {result.crossingTime ? result.crossingTime : "--"}
                      </div>
                      <PaceContent result={result} />
                      <div className="mt-1">
                        <Popover>
                          <PopoverTrigger>
                            <InfoIcon size={10} />
                          </PopoverTrigger>
                          <PopoverContent>
                            <table className="w-full">
                              <thead>
                                <tr className="border-b border-black">
                                  <th className="w-[33%] text-left py-2">
                                    Overall
                                  </th>
                                  <th className="w-[33%] text-left py-2">
                                    Gender
                                  </th>
                                  <th className="w-[33%] text-left py-2">
                                    Division
                                  </th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr>
                                  <td>
                                    <BracketRank
                                      rank={result.overallRank}
                                      total={0}
                                    />
                                  </td>
                                  <td>
                                    <BracketRank
                                      rank={result.genderRank}
                                      total={0}
                                    />
                                  </td>
                                  <td>
                                    <BracketRank
                                      rank={result.primaryDivisionRank}
                                      total={0}
                                    />
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </PopoverContent>
                        </Popover>
                      </div>
                    </td>
                  )
                })}
              </tr>
            )
          })}
        </thead>
      </table>
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

    return (
      <span className="text-sm">
        ({`${paceWithTime.paceValue} ${paceWithTime.paceLabel}`})
      </span>
    )
  }

  return (
    <div>
      <span className="text-sm font-bold mr-1">
        {result.paceWithTime.timeFormatted}
      </span>
      <Pace paceWithTime={result.paceWithTime} />
    </div>
  )
}
