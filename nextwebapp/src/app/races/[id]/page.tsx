import { config } from "@/config"
import React from "react"
import { RaceLeaderboardByCourseDto, RaceLeaderboardDto } from "./definitions"
import Link from "next/link"

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
        <div className="mt-4 text-2xl font-bold">
          {raceLeaderboard.raceName}
        </div>
        <div className="text-lg text-blue-500 font-bold">All Courses</div>

        <div className="text-sm mb-2">
          <div>
            {raceLeaderboard.locationInfoWithRank.city},{" "}
            {raceLeaderboard.locationInfoWithRank.state}
          </div>
          <div className="font-bold">{raceLeaderboard.raceKickOffDate}</div>
        </div>
      </div>
      <div className="w-2/3">
        {raceLeaderboard.leaderboards.map((leaderboard, index) => (
          <div className="mt-12" key={index}>
            {leaderboard.courseName}
            <LeaderBoard leaderboard={leaderboard} />
            <div className="my-5 text-right">
              <Link href={`/cources/${leaderboard.courseId}`}>View</Link>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

const LeaderBoard = ({
  leaderboard,
}: {
  leaderboard: RaceLeaderboardByCourseDto
}) => {
  return (
    <table className="table-auto w-full">
      <thead>
        <tr>
          <th className="w-[5%]"></th>
          <th className="w-[5%]"></th>
          <th className="w-[15%]">Bib</th>
          <th className="w-[20%]">Name</th>
          <th className="w-[10%]">Overall</th>
          <th className="w-[10%]">Gender</th>
          <th className="w-[10%]">Division</th>
          <th className="w-[15%]">Time</th>
          <th className="w-[10%]">Pace</th>
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
                // onClick={() => onViewIrpClicked(irp.athleteCourseId)}
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
                  className="font-bold text-black bg-secondary"
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
          </tr>
        ))}
      </tbody>
    </table>
  )
}
