import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { useApi } from "@/app/_api/api"
import { ContextMenu, ContextMenuContent, ContextMenuTrigger } from "@/components/ui/context-menu"
import { InfoIcon } from "lucide-react"
import { CompareAthletesStat } from "@/app/_api/athletes/definitions"

interface Props {
  searchParams: {
    ids: string
  }
}

const api = useApi()

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const athletes = await api.athletes.compare(ids)
  const raceSeriesNames = [
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
            {raceSeriesNames.map((seriesName) => {
              return <TableHead key={seriesName}>{seriesName}</TableHead>
            })}
          </TableRow>
        </TableHeader>
        <TableBody>
          {athletes.map((athleteInfo, index) => {
            return (
              <TableRow key={index}>
                <TableCell>
                  <ContextMenu>
                    <ContextMenuTrigger>
                      <div>{athleteInfo.fullName}</div>
                      <div className="text-sm">
                        {athleteInfo.genderAbbreviated} | {athleteInfo.age}
                      </div>
                      <div className="mt-2">
                        <InfoIcon size={14} />
                      </div>
                    </ContextMenuTrigger>
                    <ContextMenuContent className="w-64 p-3">
                      {raceSeriesNames.map((seriesName) => {
                        const stat = athleteInfo.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
                        return <StatLine raceSeriesTypeName={seriesName} stat={stat} />
                      })}
                    </ContextMenuContent>
                  </ContextMenu>
                </TableCell>
                {raceSeriesNames.map((seriesName) => {
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

const StatLine = ({
  raceSeriesTypeName,
  stat,
}: {
  raceSeriesTypeName: string
  stat: CompareAthletesStat | undefined
}) => {
  if (!stat) {
    return (
      <div>
        <strong>{raceSeriesTypeName}:</strong> No stats available
      </div>
    )
  }

  return (
    <div key={raceSeriesTypeName}>
      <strong>{raceSeriesTypeName}</strong>: {stat?.actualTotal ?? "--"} of {stat?.goalTotal ?? "--"}
    </div>
  )
}
