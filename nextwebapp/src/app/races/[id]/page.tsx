import { config } from "@/config"
import React from "react"
import { RaceLeaderboardDto } from "./definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Content from "./_components/Content"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getData = async (id: string): Promise<RaceLeaderboardDto> => {
  const url = `${config.apiHost}/raceLeaderboardApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const raceLeaderboard = await getData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/3">
        <div className="text-2xl font-bold">{raceLeaderboard.raceName}</div>
        <div className="text-lg text-blue-500 font-bold">All Courses</div>

        <div className="text-sm mb-2">
          <div>
            {raceLeaderboard.locationInfoWithRank.city},{" "}
            {raceLeaderboard.locationInfoWithRank.state}
          </div>
          <div className="font-bold">{raceLeaderboard.raceKickOffDate}</div>
        </div>
        <LocationInfoRankings
          locationInfoWithRank={raceLeaderboard.locationInfoWithRank}
        />
      </div>
      <div className="w-2/3">
        <Content apiHost={config.apiHost} raceLeaderboard={raceLeaderboard} />
      </div>
    </div>
  )
}
