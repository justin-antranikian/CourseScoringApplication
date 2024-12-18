import React from "react"
import { useApi } from "@/app/_api/api"
import { ContextMenu, ContextMenuTrigger } from "@/components/ui/context-menu"
import AthleteCard from "../_components/AthleteCard"

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
      <div className="w-full border border-gray-200 rounded-md shadow-sm">
        <div className="bg-gray-100 text-sm font-medium text-gray-700">
          <div className="flex divide-x divide-gray-200">
            <div className="p-3 flex-1">Athlete Info</div>
            {raceSeriesNames.map((seriesName) => (
              <div key={seriesName} className="p-3 flex-1">
                {seriesName}
              </div>
            ))}
          </div>
        </div>
        <div className="divide-y divide-gray-200">
          {athletes.map((athlete, index) => (
            <ContextMenu key={index}>
              <ContextMenuTrigger>
                <div className="flex items-center divide-x divide-gray-200 hover:bg-gray-50">
                  <div className="p-3 flex-1">
                    <div>{athlete.fullName}</div>
                    <div className="text-sm text-gray-500">
                      {athlete.genderAbbreviated} | {athlete.age}
                    </div>
                  </div>
                  {raceSeriesNames.map((seriesName) => {
                    const stat = athlete.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
                    return (
                      <div key={seriesName} className="p-3 flex-1 text-center">
                        {stat?.actualTotal ?? "--"}
                      </div>
                    )
                  })}
                </div>
              </ContextMenuTrigger>
              <AthleteCard athlete={athlete} />
            </ContextMenu>
          ))}
        </div>
      </div>
    </>
  )
}
