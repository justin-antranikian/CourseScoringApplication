import React from "react"
import { IrpResultByIntervalDto } from "./definitions"
import { BracketRank } from "@/app/_components/BracketRank"
import IntervalTime from "@/app/_components/IntervalTime"

export default function Result({ result }: { result: IrpResultByIntervalDto }) {
  return (
    <tr>
      <td>
        <div className="truncate max-w-[100px]">{result.intervalName}</div>
      </td>
      <td>
        <span className="text-lg">{result.crossingTime}</span>
      </td>
      <td>
        <BracketRank
          rank={result.overallRank}
          total={result.overallCount}
          indicator={result.overallIndicator}
        />
      </td>
      <td>
        <BracketRank
          rank={result.genderRank}
          total={result.genderCount}
          indicator={result.genderIndicator}
        />
      </td>
      <td>
        <BracketRank
          rank={result.primaryDivisionRank}
          total={result.primaryDivisionCount}
          indicator={result.primaryDivisionIndicator}
        />
      </td>
      <td>
        <IntervalTime paceTime={result.paceWithTimeIntervalOnly} />
      </td>
      <td>
        <IntervalTime paceTime={result.paceWithTimeCumulative} />
      </td>
    </tr>
  )
}
