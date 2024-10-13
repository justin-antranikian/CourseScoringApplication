import { config } from "@/config"
import { BracketRank } from "../../_components/BracketRank"
import Link from "next/link"
import { ArpDto, ArpResultDto } from "./definitions"
import { PaceWithTime } from "@/app/_components/IntervalTime"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getData = async (id: string): Promise<ArpDto> => {
  const url = `${config.apiHost}/arpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const arp = await getData(id)

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
        <div className="my-3">{arp.firstName}'s training and diet</div>
        <ul className="list-disc pl-5">
          {arp.wellnessTrainingAndDiet.map((entry, index) => (
            <li key={index}>{entry.description}</li>
          ))}
        </ul>
        <div className="my-3">{arp.firstName}'s goals</div>
        <ul className="list-disc pl-5">
          {arp.wellnessGoals.map((entry, index) => (
            <li key={index}>{entry.description}</li>
          ))}
        </ul>
        <div className="my-3">{arp.firstName}'s inspiration</div>
        {arp.wellnessMotivationalList.map((entry) => {
          return <div className="pl-5">{entry.description}</div>
        })}
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
              return <Result result={result} />
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
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

const Result = ({ result }: { result: ArpResultDto }) => {
  return (
    <tr>
      <td></td>
      <td>
        <Link href={`/results/${result.athleteCourseId}`}>View</Link>
      </td>
      <td>
        <div>{result.raceName}</div>
        <div>
          <Link href={`/courses/${result.courseId}`}>{result.courseName}</Link>
        </div>
        <div>
          {result.state}, {result.city}
        </div>
      </td>
      <td>
        <BracketRank rank={result.overallRank} total={result.overallCount} />
      </td>
      <td>
        <BracketRank rank={result.genderRank} total={result.genderCount} />
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
}
