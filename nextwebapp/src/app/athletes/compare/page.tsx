import { config } from "@/config"
import React from "react"
import { CompareAthletesAthleteInfoDto } from "./definitions"

interface Props {
  searchParams: {
    ids: string
  }
}

const getData = async (
  athleteIds: string[],
): Promise<CompareAthletesAthleteInfoDto[]> => {
  const url = `${config.apiHost}/compareAthletesApi`

  const requestInit: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ athleteIds }),
    cache: "no-store",
  }

  const response = await fetch(url, requestInit)
  return await response.json()
}

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const athletes = await getData(ids)

  const raceSeriesName = [
    "Running",
    "Triathalon",
    "Road Biking",
    "Mountain Biking",
    "Cross Country Skiing",
    "Swimming",
  ]

  return (
    <>
      <div className="mb-8 text-purple-500 bold text-2xl">
        Athlete Comparision
      </div>
      <table className="table-auto w-full">
        <thead>
          <tr>
            <th className="text-left py-2 border-b border-black" scope="col">
              Athlete Info
            </th>
            {raceSeriesName.map((seriesName) => {
              return (
                <td
                  key={seriesName}
                  className="text-left py-2 border-b border-black"
                  scope="col"
                >
                  {seriesName}
                </td>
              )
            })}
          </tr>
        </thead>
        <tbody>
          {athletes.map((athleteInfo, index) => {
            return (
              <tr key={index}>
                <td
                  className="text-left py-2 border-b border-gray-200"
                  scope="col"
                >
                  <div>{athleteInfo.fullName}</div>
                  <div className="text-sm">
                    {athleteInfo.genderAbbreviated} | {athleteInfo.age}
                  </div>
                </td>
                {raceSeriesName.map((seriesName) => {
                  const stat = athleteInfo.stats.find(
                    (stat) => stat.raceSeriesTypeName === seriesName,
                  )

                  return (
                    <td
                      key={seriesName}
                      className="text-left py-2 border-b border-gray-200"
                      scope="col"
                    >
                      {stat?.actualTotal ?? "--"}
                    </td>
                  )
                })}
              </tr>
            )
          })}
        </tbody>
      </table>
    </>
  )
}
