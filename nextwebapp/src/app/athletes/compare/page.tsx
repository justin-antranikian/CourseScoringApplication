import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { apiCaller } from "@/app/_api/api"

interface Props {
  searchParams: {
    ids: string
  }
}

const api = apiCaller()

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const athletes = await api.athletes.compare(ids)
  const raceSeriesName = ["Running", "Triathalon", "Road Biking", "Mountain Biking", "Cross Country Skiing", "Swimming"]

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
                  const stat = athleteInfo.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
                  return <TableCell key={seriesName}>{stat?.actualTotal ?? "--"}</TableCell>
                })}
              </TableRow>
            )
          })}
        </TableBody>
      </Table>
    </>
  )
}
