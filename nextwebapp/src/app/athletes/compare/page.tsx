import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { config } from "@/config"
import Link from "next/link"
import React from "react"
import {
  CompareAthletesAthleteInfoDto,
  CompareAthletesResult,
} from "./definitions"

interface Props {
  searchParams: {
    ids: string
  }
}

const getData = async (
  athleteIds: string[],
): Promise<CompareAthletesAthleteInfoDto[]> => {
  const url = `${config.apiHost}/compareAthletesApi`

  const requestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ athleteIds }),
  }

  const response = await fetch(url, requestInit)
  return await response.json()
}

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const athletes = await getData(ids)

  return (
    <>
      <div className="flex flex-wrap gap-8">
        {athletes.map((athleteInfo) => (
          <div key={athleteInfo.fullName} className="flex-1">
            <div className="text-2xl font-bold">{athleteInfo.fullName}</div>
            <div className="text-lg">
              {athleteInfo.locationInfoWithRank.city},{" "}
              {athleteInfo.locationInfoWithRank.state}
            </div>
            <div className="text-sm">
              {athleteInfo.genderAbbreviated} | {athleteInfo.age}
            </div>

            <hr className="my-2" />
            <LocationInfoRankings
              locationInfoWithRank={athleteInfo.locationInfoWithRank}
            />
            <hr className="my-2" />
          </div>
        ))}
      </div>

      <div className="flex flex-wrap gap-8">
        {athletes.map((athleteInfo) => (
          <div key={athleteInfo.fullName} className="flex-1">
            {athleteInfo.stats.map((stat) => (
              <div key={stat.raceSeriesTypeName} className="mb-2">
                <div className="font-bold">{stat.raceSeriesTypeName}</div>
                <div className="text-sm leading-3">
                  Total distance: <i>{stat.totalDistance}</i>
                </div>
              </div>
            ))}
          </div>
        ))}
      </div>

      <div className="row my-4 flex flex-wrap gap-8">
        {athletes.map((athleteInfo) => (
          <div key={athleteInfo.fullName} className="flex-1">
            <table className="table-auto w-full border-collapse">
              <thead>
                <tr>
                  <th
                    className="text-left"
                    style={{ width: "81%" }}
                    scope="col"
                  ></th>
                  <th title="Overall" style={{ width: "7%" }} scope="col">
                    O
                  </th>
                  <th title="Gender" style={{ width: "7%" }} scope="col">
                    G
                  </th>
                  <th title="Division" style={{ width: "7%" }} scope="col">
                    D
                  </th>
                </tr>
              </thead>
              <tbody>
                {athleteInfo.results.map((result) => (
                  <Result result={result} />
                ))}
              </tbody>
            </table>
          </div>
        ))}
      </div>
    </>
  )
}

const Result = ({ result }: { result: CompareAthletesResult }) => {
  return (
    <tr key={result.athleteCourseId}>
      <td className="text-left">
        <div className="text-lg font-bold">{result.raceName}</div>
        <div>
          <span className="text-sm font-bold">
            <strong>{result.paceWithTime.timeFormatted}</strong>
          </span>
          {result.paceWithTime.hasPace && (
            <span className="text-xs">
              ({result.paceWithTime.paceValue} {result.paceWithTime.paceLabel})
            </span>
          )}
        </div>
        <div>
          <Link
            className="text-xs text-blue-500 hover:underline"
            title="view result"
            href={`/results/${result.athleteCourseId}`}
          >
            View
          </Link>
        </div>
      </td>
      <td>{result.overallRank}</td>
      <td>{result.genderRank}</td>
      <td>{result.divisionRank}</td>
    </tr>
  )
}
