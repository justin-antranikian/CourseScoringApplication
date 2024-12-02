import React from "react"
import { ArpResultDto } from "./definitions"
import Link from "next/link"
import { BracketRank } from "@/app/_components/BracketRank"
import { PaceWithTime } from "@/app/_components/IntervalTime"

export default function AtheleteResult({ result }: { result: ArpResultDto }) {
  return (
    <tr className="border-b border-gray-300">
      <td className="py-2">
        <Link href={`/results/${result.athleteCourseId}`}>View</Link>
      </td>
      <td className="py-2">
        <div>
          <Link href={`/races/${result.raceId}`}>{result.raceName}</Link>
        </div>
        <div>
          <Link href={`/courses/${result.courseId}`}>{result.courseName}</Link>
        </div>
        <div>
          {result.state}, {result.city}
        </div>
      </td>
      <td className="py-2">
        <BracketRank rank={result.overallRank} total={result.overallCount} />
      </td>
      <td className="py-2">
        <BracketRank rank={result.genderRank} total={result.genderCount} />
      </td>
      <td className="py-2">
        <BracketRank
          rank={result.primaryDivisionRank}
          total={result.primaryDivisionCount}
        />
      </td>
      <td className="py-2">
        <RankWithTime paceTime={result.paceWithTimeCumulative} />
      </td>
    </tr>
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
