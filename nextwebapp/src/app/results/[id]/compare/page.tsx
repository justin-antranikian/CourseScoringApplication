import { config } from "@/config"
import React from "react"
import { CompareIrpsAthleteInfoDto } from "./definitions"
import Link from "next/link"

interface Props {
  searchParams: {
    ids: string
  }
}

const getData = async (
  athleteCourseIds: string[],
): Promise<CompareIrpsAthleteInfoDto[]> => {
  const url = `${config.apiHost}/compareIrpApi`

  const requestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ athleteCourseIds }),
  }

  const response = await fetch(url, requestInit)
  return await response.json()
}

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const irpsToCompare = await getData(ids)

  return (
    <>
      <div className="flex flex-wrap gap-8">
        {irpsToCompare.map((athleteInfo) => (
          <div key={athleteInfo.athleteCourseId} className="flex-1">
            <div className="overflow-hidden text-ellipsis whitespace-nowrap font-bold text-2xl">
              {athleteInfo.fullName}
            </div>
            <div className="overflow-hidden text-ellipsis whitespace-nowrap text-lg">
              {athleteInfo.city}, {athleteInfo.state}
            </div>
            <div className="text-sm">
              {athleteInfo.genderAbbreviated} | {athleteInfo.raceAge}
            </div>
            <hr className="my-2" />
            <div className="mb-3">
              <Link
                href={`/results/${athleteInfo.athleteCourseId}`}
                className="btn btn-success"
                title="view result"
              >
                {athleteInfo.compareIrpsRank === 0 && "Best Result"}
                {athleteInfo.compareIrpsRank === 1 && "2nd Best Result"}
                {athleteInfo.compareIrpsRank === 2 && "3rd Best Result"}
                {athleteInfo.compareIrpsRank === 3 && "4th Best Result"}
              </Link>
            </div>

            {athleteInfo.finishInfo ? (
              <>
                <div>{athleteInfo.finishInfo.finishTime}</div>
                <div>
                  <span className="font-bold text-xl">
                    {
                      athleteInfo.finishInfo.paceWithTimeCumulative
                        .timeFormatted
                    }
                  </span>
                  {athleteInfo.finishInfo.paceWithTimeCumulative.hasPace && (
                    <span>
                      ({athleteInfo.finishInfo.paceWithTimeCumulative.paceValue}{" "}
                      {athleteInfo.finishInfo.paceWithTimeCumulative.paceLabel})
                    </span>
                  )}
                </div>
              </>
            ) : (
              <div>--</div>
            )}
          </div>
        ))}
      </div>

      <div className="flex flex-wrap gap-8">
        {irpsToCompare.map((athleteInfo) => (
          <div key={athleteInfo.athleteId} className="flex-1">
            <table className="table-auto w-full">
              <thead>
                <tr>
                  <th className="text-left w-3/4"></th>
                  <th className="w-1/12" title="Overall">
                    O
                  </th>
                  <th className="w-1/12" title="Gender">
                    G
                  </th>
                  <th className="w-1/12" title="Division">
                    D
                  </th>
                </tr>
              </thead>
              <tbody>
                {athleteInfo.compareIrpsIntervalDtos.map((result, index) => (
                  <tr key={index}>
                    <td className="text-left py-2">
                      <div className="text-lg font-bold">
                        {result.intervalName}
                      </div>
                      <div className="text-primary font-bold">
                        {result.crossingTime ? result.crossingTime : "--"}
                      </div>
                      {result.paceWithTime ? (
                        <div>
                          <span className="text-sm font-bold">
                            {result.paceWithTime.timeFormatted}
                          </span>
                          {result.paceWithTime.hasPace && (
                            <span className="text-sm">
                              ({result.paceWithTime.paceValue}{" "}
                              {result.paceWithTime.paceLabel})
                            </span>
                          )}
                        </div>
                      ) : (
                        <div>--</div>
                      )}
                    </td>
                    <td className="py-1 text-center">
                      {result.overallRank ? result.overallRank : "--"}
                    </td>
                    <td className="py-1 text-center">
                      {result.genderRank ? result.genderRank : "--"}
                    </td>
                    <td className="py-1 text-center">
                      {result.primaryDivisionRank
                        ? result.primaryDivisionRank
                        : "--"}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ))}
      </div>
    </>
  )
}
