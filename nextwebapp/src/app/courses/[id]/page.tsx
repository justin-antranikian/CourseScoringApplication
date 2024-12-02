import { config } from "@/config"
import React from "react"
import { CourseLeaderboardDto } from "./definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Content from "./_components/Content"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getData = async (id: string): Promise<CourseLeaderboardDto> => {
  const url = `${config.apiHost}/courseLeaderboardApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const courseLeaderboard = await getData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/4">
        <div className="text-2xl font-bold">{courseLeaderboard.raceName}</div>
        <div className="text-lg text-blue-500 font-bold">
          {courseLeaderboard.courseName}
        </div>
        <div className="text-sm mb-2">
          <div>
            {courseLeaderboard.locationInfoWithRank.city},{" "}
            {courseLeaderboard.locationInfoWithRank.state}
          </div>
          <div>Distance: {courseLeaderboard.courseDistance}</div>
          <div>
            {courseLeaderboard.courseDate} at{" "}
            <strong>{courseLeaderboard.courseTime}</strong>
          </div>
        </div>
        <LocationInfoRankings
          locationInfoWithRank={courseLeaderboard.locationInfoWithRank}
        />
      </div>
      <div className="w-3/4">
        <Content
          courseLeaderboard={courseLeaderboard}
          apiHost={config.apiHost}
        />
      </div>
    </div>
  )
}
