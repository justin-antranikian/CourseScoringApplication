
import { config } from '@/config'
import React from 'react'
import { Irp, IrpResultByIntervalDto } from './definitions'
import { BracketRank } from '@/app/_components/BracketRank'
import IntervalTime from '@/app/_components/IntervalTime'

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getIrpData = async (id: string): Promise<Irp> => {
  const url = `${config.apiHost}/irpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const irp = await getIrpData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/3">
      <div className="mt-4 text-2xl font-bold">{irp.fullName}</div>
        <div className="text-lg">
          {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {irp.genderAbbreviated} | {irp.raceAge}
        </div>
      </div>
      <div className="w-2/3">
        <div className="mb-12">Finish Info</div>
        <table className="my-5 table-auto w-full">
      <thead>
        <tr>
          <th className="w-[15%]" scope="col"></th>
          <th className="w-[20%]" scope="col">
            Time <span className="text-sm">({irp.timeZoneAbbreviated})</span>
          </th>
          <th className="w-[10%]" scope="col">Overall</th>
          <th className="w-[10%]" scope="col">Gender</th>
          <th className="w-[10%]" scope="col">Division</th>
          <th className="w-[15%]" scope="col">Interval Time</th>
          <th className="w-[20%]" scope="col">Cumulative Time</th>
        </tr>
      </thead>
      <tbody>
        {irp.intervalResults.map((intervalResult, index) => (
          <Result result={intervalResult} />
        ))}
      </tbody>
    </table>
      </div>
    </div>
  )
}

const Result = ({ result }: { result: IrpResultByIntervalDto }) => {
  return (
    <tr>
      <td>
        <div className="truncate max-w-[100px]">{result.intervalName}</div>
      </td>
      <td><span className="text-lg">{result.crossingTime}</span>
      </td>
      <td>
        <BracketRank rank={result.overallRank} total={result.overallCount} indicator={result.overallIndicator} />
      </td>
      <td>
        <BracketRank rank={result.genderRank} total={result.genderCount} indicator={result.genderIndicator} />
      </td>
      <td>
        <BracketRank
          rank={result.primaryDivisionRank}
          total={result.primaryDivisionCount}
          indicator={result.primaryDivisionIndicator}
        />
      </td>
      <td><IntervalTime paceTime={result.paceWithTimeIntervalOnly} /></td>
      <td><IntervalTime paceTime={result.paceWithTimeCumulative} /></td>
    </tr>
  )
}