import { config } from "@/config"

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
  athleteCourseId: number;
  raceId: number;
  raceName: string;
  raceSeriesType: any;  // Assuming RaceSeriesType is defined elsewhere
  courseId: number;
  courseName: string;
  state: string;
  city: string;
  overallRank: number;
  genderRank: number;
  primaryDivisionRank: number;
  overallCount: number;
  genderCount: number;
  primaryDivisionCount: number;
  paceWithTimeCumulative: any;  // Assuming PaceWithTime is defined elsewhere
}

const getArpData = async (id: string): Promise<ArpDto> => {
  const url = `${config.apiHost}/arpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const arp = await getArpData(id)

  return (
    <div className="flex gap-1">
      <div className="bg-gray-200 w-1/3">
        <div className="mt-4 text-2xl font-bold">{arp.fullName}</div>
        <div className="text-lg">
          {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {arp.genderAbbreviated} | {arp.age}
        </div>
      </div>
      <div className="bg-gray-400 w-2/3">
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
            {arp.results.map(result => {
              return (
                <tr>
                  <td></td>
                  <td></td>
                  <td>{result.courseName}</td>
                  <td>Ba</td>
                  <td>Ba</td>
                  <td>Ba</td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}
