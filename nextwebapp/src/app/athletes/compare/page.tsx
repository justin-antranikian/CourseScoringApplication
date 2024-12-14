import { config } from "@/config"
import React from "react"
import { CompareAthletesAthleteInfoDto } from "./definitions"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"

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
      <div className="mb-8 text-purple-500 bold text-2xl">Athlete Compare</div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Athlete Info</TableHead>
            {raceSeriesName.map((seriesName) => {
              return <TableHead key={seriesName}>{seriesName}</TableHead>
            })}
          </TableRow>
        </TableHeader>
        <TableBody>
          {athletes.map((athleteInfo, index) => {
            return (
              <TableRow key={index}>
                <TableCell>
                  <div>{athleteInfo.fullName}</div>
                  <div className="text-sm">
                    {athleteInfo.genderAbbreviated} | {athleteInfo.age}
                  </div>
                </TableCell>
                {raceSeriesName.map((seriesName) => {
                  const stat = athleteInfo.stats.find(
                    (stat) => stat.raceSeriesTypeName === seriesName,
                  )

                  return (
                    <TableCell key={seriesName}>
                      {stat?.actualTotal ?? "--"}
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
