import React from "react"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Content from "./_components/Content"
import { getImageNonFormatted } from "@/app/utils"
import { useApi } from "@/app/_api/api"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const api = useApi()

export default async function Page({ params: { id } }: Props) {
  const raceLeaderboard = await api.races.leaderboard(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/4">
        <div>
          <img style={{ width: "75%", height: 125 }} src={getImageNonFormatted(raceLeaderboard.raceSeriesTypeName)} />
        </div>
        <div className="mt-2 text-2xl font-bold">{raceLeaderboard.raceName}</div>
        <div className="text-lg text-blue-500 font-bold">All Courses</div>

        <div className="text-sm mb-2">
          <div>
            {raceLeaderboard.locationInfoWithRank.city}, {raceLeaderboard.locationInfoWithRank.state}
          </div>
          <div className="font-bold">{raceLeaderboard.raceKickOffDate}</div>
        </div>
        <LocationInfoRankings locationInfoWithRank={raceLeaderboard.locationInfoWithRank} />
      </div>
      <div className="w-3/4">
        <Content raceLeaderboard={raceLeaderboard} />
      </div>
    </div>
  )
}
