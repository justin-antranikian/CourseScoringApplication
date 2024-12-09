import { config } from "@/config"
import React from "react"
import { Irp } from "./definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Link from "next/link"
import Result from "./Result"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime from "@/app/_components/IntervalTime"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getData = async (id: string): Promise<Irp> => {
  const url = `${config.apiHost}/irpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const irp = await getData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/4">
        <div>
          <img style={{ width: "75%", height: 125 }} src="/Athlete.png" />
        </div>
        <div className="mt-2 text-2xl font-bold">
          <Link href={`/athletes/${irp.athleteId}`}>{irp.fullName}</Link>
        </div>
        <div className="text-lg">
          {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {irp.genderAbbreviated} | {irp.raceAge}
        </div>
        <LocationInfoRankings locationInfoWithRank={irp.locationInfoWithRank} />
        <hr className="mt-5" />
        <div className="mt-5">
          {irp.tags.map((tag, index) => (
            <span
              key={index}
              className="text-lg bg-blue-500 text-white py-1 px-3 rounded-lg mr-2 mb-2 inline-block"
            >
              {tag}
            </span>
          ))}
        </div>
        <div className="mt-3">{irp.firstName}'s training</div>
        <ul className="mt-3">
          {irp.trainingList.map((training, index) => (
            <li key={index} className="text-xs">
              {training}
            </li>
          ))}
        </ul>

        <div className="mt-3">{irp.firstName}'s personal goal</div>
        <div className="mt-3 text-xs italic">
          "{irp.personalGoalDescription}"
        </div>

        <div className="mt-3">{irp.firstName}'s course goal</div>
        <div className="mt-3 text-xs italic text-blue-500">
          <strong>"{irp.courseGoalDescription}"</strong>
        </div>
      </div>
      <div className="w-3/4">
        <div className="mb-12 bold text-2xl text-purple-500">Finish Info</div>
        <div className="my-5 flex flex-wrap">
          <div className="w-full sm:w-1/3">
            <div>Time</div>
            <div className="text-xl font-bold">
              {irp.paceWithTimeCumulative.timeFormatted}
            </div>
          </div>
          <div className="w-full sm:w-1/3">
            <div>Pace ({irp.paceWithTimeCumulative.paceLabel})</div>
            <div className="text-xl font-bold">
              {irp.paceWithTimeCumulative.paceValue || "--"}
            </div>
          </div>
          <div className="w-full sm:w-1/3">
            <div>Finish Time ({irp.timeZoneAbbreviated})</div>
            <div className="text-xl font-bold">
              {irp.finishTime ? irp.finishTime : "--"}
            </div>
          </div>
        </div>
        <hr className="my-5" />
        <div className="flex space-x-4">
          {irp.bracketResults.map((bracket) => (
            <div className="flex-1" key={bracket.rank}>
              <div className="truncate" title={bracket.name}>
                {bracket.name}
              </div>
              <div className="mt-1 text-2xl font-bold text-primary">
                {bracket.rank} of {bracket.totalRacers}
              </div>
            </div>
          ))}
        </div>

        <hr className="my-5" />
        <div className="mb-12 bold text-2xl text-purple-500">Intervals</div>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead></TableHead>
              <TableHead>
                Time{" "}
                <span className="text-sm">({irp.timeZoneAbbreviated})</span>
              </TableHead>
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
                  <div className="truncate max-w-[100px]">
                    {intervalResult.intervalName}
                  </div>
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
                  <IntervalTime
                    paceTime={intervalResult.paceWithTimeIntervalOnly}
                  />
                </TableCell>
                <TableCell>
                  <IntervalTime
                    paceTime={intervalResult.paceWithTimeCumulative}
                  />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </div>
  )
}
