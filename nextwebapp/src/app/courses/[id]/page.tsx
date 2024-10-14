import { config } from "@/config"
import React from "react"
import {
  CourseLeaderboardByIntervalDto,
  CourseLeaderboardDto,
} from "./definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"

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
      <div className="w-1/3">
        <div className="mt-4 text-2xl font-bold">
          {courseLeaderboard.raceName}
        </div>
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
      <div className="w-2/3">
        {courseLeaderboard.leaderboards.map((leaderboard, index) => (
          <div className="mt-12" key={index}>
            {leaderboard.intervalName}
            <LeaderBoard leaderboard={leaderboard} />
          </div>
        ))}
      </div>
    </div>
  )
}

const LeaderBoard = ({
  leaderboard,
}: {
  leaderboard: CourseLeaderboardByIntervalDto
}) => {
  return (
    <table className="table-auto w-full">
      <thead>
        <tr>
          <th className="w-[5%]"></th>
          <th className="w-[5%]"></th>
          <th className="w-[10%]">Bib</th>
          <th className="w-[20%]">Name</th>
          <th className="w-[10%]">Overall</th>
          <th className="w-[10%]">Gender</th>
          <th className="w-[10%]">Division</th>
          <th className="w-[15%]">Time</th>
          <th className="w-[10%]">Pace</th>
          <th className="w-[5%]"></th>
        </tr>
      </thead>
      <tbody>
        {leaderboard.results.map((irp) => (
          <tr key={irp.athleteCourseId}>
            <td className="text-left">
              <a href={`/results/${irp.athleteCourseId}`}>View</a>
            </td>
            <td>
              <i
                className="fa fa-plus-circle cursor-pointer"
                title="view more"
              ></i>
            </td>
            <td>
              <span className="bg-gray-800 text-white p-2 rounded" title="bib">
                {irp.bib}
              </span>
            </td>
            <td>
              <div>
                <a
                  className="font-bold bg-secondary text-black"
                  href={`/athletes/${irp.athleteId}`}
                >
                  {irp.fullName}
                </a>
              </div>
              <div>
                {irp.genderAbbreviated} | {irp.raceAge}
              </div>
            </td>
            <td className="font-bold text-gray-500">{irp.overallRank}</td>
            <td className="font-bold text-gray-500">{irp.genderRank}</td>
            <td className="font-bold text-gray-500">{irp.divisionRank}</td>
            <td className="font-bold">
              {irp.paceWithTimeCumulative.timeFormatted}
            </td>
            <td>
              <div className="font-bold">
                {irp.paceWithTimeCumulative.paceValue || "--"}
              </div>
              {irp.paceWithTimeCumulative.paceLabel}
            </td>
            <td>
              <button
                type="button"
                title="Compare Result"
                className="btn btn-outline-secondary btn-sm"
              >
                <i className="fa fa-compress" aria-hidden="true"></i>
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  )
}
