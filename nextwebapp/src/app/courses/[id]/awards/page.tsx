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

export default async function Page({ params: { id } }: Props) {
  const getData = async () => {
    const url = `${config.apiHost}/awardsPodiumApi/${id}`
    const response = await fetch(url, { cache: "no-store" })
    return (await response.json()) as PodiumAward[]
  }

  const awards = await getData()

  return (
    <table className="table w-full">
      <thead>
        <tr>
          <th className="text-left pb-2" style={{ width: "25%" }}></th>
          <th className="font-24 text-left pb-2" style={{ width: "25%" }}>
            First Place
          </th>
          <th className="font-24 text-left pb-2" style={{ width: "25%" }}>
            Second Place
          </th>
          <th className="font-24 text-left pb-2" style={{ width: "25%" }}>
            Third Place
          </th>
        </tr>
      </thead>
      <tbody>
        {awards.map((awardPodium) => (
          <tr key={awardPodium.bracketName}>
            <td className="text-left py-2">
              <strong>{awardPodium.bracketName}</strong>
            </td>
            <td className="text-left py-2">
              <AthleteTemplate athlete={awardPodium.firstPlaceAthlete} />
            </td>
            <td className="text-left py-2">
              <AthleteTemplate athlete={awardPodium.secondPlaceAthlete} />
            </td>
            <td className="text-left py-2">
              <AthleteTemplate athlete={awardPodium.thirdPlaceAthlete} />
            </td>
          </tr>
        ))}
      </tbody>
    </table>
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
      <div className="primary-color font-weight-bold">{athlete.finishTime}</div>
      <div>
        <span className="font-12 font-weight-bold">
          <strong>{athlete.paceWithTime.timeFormatted}</strong>
        </span>
        {athlete.paceWithTime.hasPace && (
          <span style={{ fontSize: "12px" }}>
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
