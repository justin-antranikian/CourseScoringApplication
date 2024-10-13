import { config } from "@/config"
import React from "react"
import { Irp, IrpResultByIntervalDto } from "./definitions"
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
      <div className="w-1/3">
        <div className="mt-4 text-2xl font-bold">{irp.fullName}</div>
        <div className="text-lg">
          {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {irp.genderAbbreviated} | {irp.raceAge}
        </div>
        <div>
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
      <div className="w-2/3">
        <div className="mb-12">Finish Info</div>
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
        <table className="my-5 table-auto w-full">
          <thead>
            <tr>
              <th className="w-[15%]" scope="col"></th>
              <th className="w-[20%]" scope="col">
                Time{" "}
                <span className="text-sm">({irp.timeZoneAbbreviated})</span>
              </th>
              <th className="w-[10%]" scope="col">
                Overall
              </th>
              <th className="w-[10%]" scope="col">
                Gender
              </th>
              <th className="w-[10%]" scope="col">
                Division
              </th>
              <th className="w-[15%]" scope="col">
                Interval Time
              </th>
              <th className="w-[20%]" scope="col">
                Cumulative Time
              </th>
            </tr>
          </thead>
          <tbody>
            {irp.intervalResults.map((intervalResult, index) => (
              <Result result={intervalResult} />
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}

const Result = ({ result }: { result: IrpResultByIntervalDto }) => {
  return (
    <tr>
      <td>
        <div className="truncate max-w-[100px]">{result.intervalName}</div>
      </td>
      <td>
        <span className="text-lg">{result.crossingTime}</span>
      </td>
      <td>
        <BracketRank
          rank={result.overallRank}
          total={result.overallCount}
          indicator={result.overallIndicator}
        />
      </td>
      <td>
        <BracketRank
          rank={result.genderRank}
          total={result.genderCount}
          indicator={result.genderIndicator}
        />
      </td>
      <td>
        <BracketRank
          rank={result.primaryDivisionRank}
          total={result.primaryDivisionCount}
          indicator={result.primaryDivisionIndicator}
        />
      </td>
      <td>
        <IntervalTime paceTime={result.paceWithTimeIntervalOnly} />
      </td>
      <td>
        <IntervalTime paceTime={result.paceWithTimeCumulative} />
      </td>
    </tr>
  )
}
