import { PaceWithTime } from "@/app/_components/IntervalTime"
import { config } from "@/config"
import React from "react"

interface Props {
  params: {
    id: string
  }
}

interface PodiumAward {
  bracketName: string
  firstPlaceAthlete: AwardWinnerDto | null
  secondPlaceAthlete: AwardWinnerDto | null
  thirdPlaceAthlete: AwardWinnerDto | null
}

interface AwardWinnerDto {
  athleteId: number
  athleteCourseId: number
  fullName: string
  finishTime: string
  paceWithTime: PaceWithTime
}

const getData = async (id: string) => {
  const url = `${config.apiHost}/awardsPodiumApi/${id}`
  const response = await fetch(url, { cache: "no-store" })
  return (await response.json()) as PodiumAward[]
}

export default async function Page({ params: { id } }: Props) {
  const awards = await getData(id)

  return (
    <>
      <div className="mb-8 text-purple-500 bold text-2xl">Awards</div>
      <table className="table w-full">
        <thead>
          <tr>
            <th className="text-left py-2 border-b border-black" scope="col">
              Bracket Name
            </th>
            <th className="text-left py-2 border-b border-black" scope="col">
              First Place
            </th>
            <th className="text-left py-2 border-b border-black" scope="col">
              Second Place
            </th>
            <th className="text-left py-2 border-b border-black" scope="col">
              Third Place
            </th>
          </tr>
        </thead>
        <tbody>
          {awards.map((awardPodium) => (
            <tr key={awardPodium.bracketName}>
              <td
                className="text-left py-2 border-b border-gray-200"
                scope="col"
              >
                <strong>{awardPodium.bracketName}</strong>
              </td>
              <td
                className="text-left py-2 border-b border-gray-200"
                scope="col"
              >
                <AthleteTemplate athlete={awardPodium.firstPlaceAthlete} />
              </td>
              <td
                className="text-left py-2 border-b border-gray-200"
                scope="col"
              >
                <AthleteTemplate athlete={awardPodium.secondPlaceAthlete} />
              </td>
              <td
                className="text-left py-2 border-b border-gray-200"
                scope="col"
              >
                <AthleteTemplate athlete={awardPodium.thirdPlaceAthlete} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

const AthleteTemplate = ({ athlete }: { athlete: AwardWinnerDto | null }) => {
  if (!athlete) {
    return <div>--</div>
  }

  return (
    <div>
      <div>
        <a href={`/athletes/${athlete.athleteId}`}>{athlete.fullName}</a>
      </div>
      <div>
        <span className="mr-1">
          <strong>{athlete.paceWithTime.timeFormatted}</strong>
        </span>
        {athlete.paceWithTime.hasPace && (
          <span>
            ({athlete.paceWithTime.paceValue} {athlete.paceWithTime.paceLabel})
          </span>
        )}
      </div>
      <div>
        <a
          href={`/results/${athlete.athleteCourseId}`}
          className="font-12"
          title="view result"
        >
          View
        </a>
      </div>
    </div>
  )
}
