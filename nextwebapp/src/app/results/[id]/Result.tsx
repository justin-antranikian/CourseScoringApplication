import React from "react"
import { IrpResultByIntervalDto } from "./definitions"
import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime from "@/app/_components/IntervalTime"

export default function Result({ result }: { result: IrpResultByIntervalDto }) {
  return (
    <tr className="border-b border-gray-300">
      <td className="py-2">
        <div className="truncate max-w-[100px]">{result.intervalName}</div>
      </td>
      <td className="py-2">
        <span className="text-lg">{result.crossingTime}</span>
      </td>
      <td className="py-2">
        <BracketRank
          rank={result.overallRank}
          total={result.overallCount}
          indicator={result.overallIndicator}
        />
      </td>
      <td className="py-2">
        <BracketRank
          rank={result.genderRank}
          total={result.genderCount}
          indicator={result.genderIndicator}
        />
      </td>
      <td className="py-2">
        <BracketRank
          rank={result.primaryDivisionRank}
          total={result.primaryDivisionCount}
          indicator={result.primaryDivisionIndicator}
        />
      </td>
      <td className="py-2">
        <IntervalTime paceTime={result.paceWithTimeIntervalOnly} />
      </td>
      <td className="py-2">
        <IntervalTime paceTime={result.paceWithTimeCumulative} />
      </td>
    </tr>
  )
}
