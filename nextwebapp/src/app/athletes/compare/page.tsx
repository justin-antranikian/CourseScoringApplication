import React from "react"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { useApi } from "@/app/_api/api"
import { ContextMenu, ContextMenuContent, ContextMenuTrigger } from "@/components/ui/context-menu"
import { CompareAthletesAthleteInfoDto, CompareAthletesStat } from "@/app/_api/athletes/definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { MoveLeft, MoveRight } from "lucide-react"

interface Props {
  searchParams: {
    ids: string
  }
}

const raceSeriesNames = ["Running", "Triathalon", "Road Biking", "Mountain Biking", "Cross Country Skiing", "Swimming"]

const api = useApi()

export default async function Page({ searchParams }: Props) {
  const ids = searchParams.ids ? JSON.parse(searchParams.ids) : []
  const athletes = await api.athletes.compare(ids)

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
          {athletes.map((athlete, index) => {
            return (
              <TableRow key={index}>
                <TableCell>
                  <ContextMenu>
                    <ContextMenuTrigger>
                      <div>{athlete.fullName}</div>
                      <div className="text-sm">
                        {athlete.genderAbbreviated} | {athlete.age}
                      </div>
                    </ContextMenuTrigger>
                    <AthleteContextMenuContent athlete={athlete} />
                  </ContextMenu>
                </TableCell>
                {raceSeriesNames.map((seriesName) => {
                  const stat = athlete.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
                  return (
                    <TableCell key={seriesName}>
                      <ContextMenu>
                        <ContextMenuTrigger>
                          <div className="w-full h-full">{stat?.actualTotal ?? "--"}</div>
                        </ContextMenuTrigger>
                        <AthleteContextMenuContent athlete={athlete} />
                      </ContextMenu>
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

const AthleteContextMenuContent = ({ athlete }: { athlete: CompareAthletesAthleteInfoDto }) => {
  return (
    <ContextMenuContent className="w-[400px] min-w-[400px] p-3">
      <div>
        <img style={{ width: "50%", height: 125 }} src="/Athlete.png" />
      </div>
      <div className="bg-purple-200 text-center text-base py-2">
        <a href={`/athletes/${athlete.id}`}>
          <strong>{athlete.fullName}</strong>
        </a>
      </div>
      <div className="flex items-center">
        <div className="flex-shrink-0 flex justify-start">
          <MoveLeft className="cursor-pointer" />
        </div>
        <div className="flex-grow mx-3 text-center">
          <div className="my-2 text-2xl">
            {athlete.age} | {athlete.genderAbbreviated}
          </div>
          <LocationInfoRankings locationInfoWithRank={athlete.locationInfoWithRank} />
          <div className="mt-3">
            {raceSeriesNames.map((seriesName) => {
              const stat = athlete.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
              return <StatLine key={seriesName} raceSeriesTypeName={seriesName} stat={stat} />
            })}
          </div>
        </div>
        <div className="flex-shrink-0 flex justify-end">
          <MoveRight className="cursor-pointer" />
        </div>
      </div>
    </ContextMenuContent>
  )
}

const StatLine = ({
  raceSeriesTypeName,
  stat,
}: {
  raceSeriesTypeName: string
  stat: CompareAthletesStat | undefined
}) => {
  const Content = () => {
    if (!stat) {
      return <>No stats available</>
    }

    if (!stat.goalTotal) {
      return <>{stat.actualTotal} (total events)</>
    }

    return (
      <>
        {stat.actualTotal} of {stat.goalTotal}
      </>
    )
  }

  return (
    <div key={raceSeriesTypeName}>
      <strong>{raceSeriesTypeName}</strong>: <Content />
    </div>
  )
}
