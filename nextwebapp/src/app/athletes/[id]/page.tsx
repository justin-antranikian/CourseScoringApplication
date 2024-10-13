import { config } from "@/config"
import { BracketRank } from "./BracketRank"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

interface ArpDto {
  age: number
  allEventsGoal: any
  firstName: string
  fullName: string
  genderAbbreviated: string
  goals: any[]
  locationInfoWithRank: any
  results: ArpResultDto[]
  tags: string[]
  wellnessTrainingAndDiet: any[]
  wellnessGoals: any[]
  wellnessGearList: any[]
  wellnessMotivationalList: any[]
}

interface ArpResultDto {
  athleteCourseId: number
  raceId: number
  raceName: string
  raceSeriesType: any // Assuming RaceSeriesType is defined elsewhere
  courseId: number
  courseName: string
  state: string
  city: string
  overallRank: number
  genderRank: number
  primaryDivisionRank: number
  overallCount: number
  genderCount: number
  primaryDivisionCount: number
  paceWithTimeCumulative: PaceWithTime
}

interface PaceWithTime {
  timeFormatted: string
  hasPace: boolean
  paceValue?: string | null
  paceLabel: string | null
}

const getArpData = async (id: string): Promise<ArpDto> => {
  const url = `${config.apiHost}/arpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

const RankWithTime = ({ paceTime }: { paceTime: PaceWithTime }) => {
  return (
    <>
      <div className="text-lg font-bold">{paceTime.timeFormatted}</div>
      {paceTime.hasPace && (
        <div>
          <strong className="mr-1">{paceTime.paceValue || "N/A"}</strong>
          {paceTime.paceLabel}
        </div>
      )}
    </>
  )
}

export default async function Page({ params: { id } }: Props) {
  const arp = await getArpData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/3">
        <div className="mt-4 text-2xl font-bold">{arp.fullName}</div>
        <div className="text-lg">
          {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {arp.genderAbbreviated} | {arp.age}
        </div>
      </div>
      <div className="w-2/3">
        <div className="mb-12">Results</div>
        <table className="my-5 table-auto w-full">
          <thead>
            <tr>
              <th className="w-[5%]" scope="col"></th>
              <th className="w-[15%]" scope="col"></th>
              <th className="w-[30%] text-left" scope="col">
                Event Name
              </th>
              <th className="w-[10%]" scope="col">
                Overall
              </th>
              <th className="w-[10%]" scope="col">
                Gender
              </th>
              <th className="w-[10%]" scope="col">
                Division
              </th>
              <th className="w-[20%]" scope="col">
                Total Time
              </th>
            </tr>
          </thead>
          <tbody>
            {arp.results.map((result) => {
              return (
                <tr>
                  <td></td>
                  <td></td>
                  <td>{result.courseName}</td>
                  <td>
                    <BracketRank
                      rank={result.overallRank}
                      total={result.overallCount}
                    />
                  </td>
                  <td>
                    <BracketRank
                      rank={result.genderRank}
                      total={result.genderCount}
                    />
                  </td>
                  <td>
                    <BracketRank
                      rank={result.primaryDivisionRank}
                      total={result.primaryDivisionCount}
                    />
                  </td>
                  <td>
                    <RankWithTime paceTime={result.paceWithTimeCumulative} />
                  </td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}
