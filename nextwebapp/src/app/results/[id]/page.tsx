import React from "react"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Link from "next/link"
import Intervals from "../../_components/Intervals"
import { getApi } from "@/app/_api/api"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const api = getApi()

export default async function Page({ params: { id } }: Props) {
  const irp = await api.results.details(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/4">
        <div>
          <img style={{ width: "75%", height: 125 }} src="/Athlete.png" />
        </div>
        <div className="mt-2 text-2xl font-bold">
          <Link href={`/athletes/${irp.athleteId}`}>{irp.fullName}</Link>
        </div>
        <div className="text-lg">
          {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {irp.genderAbbreviated} | {irp.raceAge}
        </div>
        <LocationInfoRankings locationInfoWithRank={irp.locationInfoWithRank} />
        <hr className="mt-5" />
        <div className="mt-5">
          {irp.tags.map((tag, index) => (
            <span key={index} className="text-lg bg-blue-500 text-white py-1 px-3 rounded-lg mr-2 mb-2 inline-block">
              {tag}
            </span>
          ))}
        </div>
        <div className="mt-3">{irp.firstName}'s training</div>
        <ul className="mt-3">
          {irp.trainingList.map((training, index) => (
            <li key={index} className="text-xs">
              {training}
            </li>
          ))}
        </ul>

        <div className="mt-3">{irp.firstName}'s personal goal</div>
        <div className="mt-3 text-xs italic">"{irp.personalGoalDescription}"</div>

        <div className="mt-3">{irp.firstName}'s course goal</div>
        <div className="mt-3 text-xs italic text-blue-500">
          <strong>"{irp.courseGoalDescription}"</strong>
        </div>
      </div>
      <div className="w-3/4">
        <div className="mb-12 bold text-2xl text-purple-500">Finish Info</div>
        <div className="my-5 flex flex-wrap">
          <div className="w-full sm:w-1/3">
            <div>Time</div>
            <div className="text-xl font-bold">{irp.paceWithTimeCumulative.timeFormatted}</div>
          </div>
          <div className="w-full sm:w-1/3">
            <div>Pace ({irp.paceWithTimeCumulative.paceLabel})</div>
            <div className="text-xl font-bold">{irp.paceWithTimeCumulative.paceValue || "--"}</div>
          </div>
          <div className="w-full sm:w-1/3">
            <div>Finish Time ({irp.timeZoneAbbreviated})</div>
            <div className="text-xl font-bold">{irp.finishTime ? irp.finishTime : "--"}</div>
          </div>
        </div>
        <hr className="my-5" />
        <div className="flex space-x-4">
          {irp.bracketResults.map((bracket) => (
            <div className="flex-1" key={bracket.rank}>
              <div className="truncate" title={bracket.name}>
                {bracket.name}
              </div>
              <div className="mt-1 text-2xl font-bold text-primary">
                {bracket.rank} of {bracket.totalRacers}
              </div>
            </div>
          ))}
        </div>

        <hr className="my-5" />
        <div className="mb-12 bold text-2xl text-purple-500">Intervals</div>
        <Intervals irp={irp} />
      </div>
    </div>
  )
}
