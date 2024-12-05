
import { PaceWithTime } from '@/app/_components/IntervalTime'
import React from 'react'

export default function RankWithTime({ paceTime }: { paceTime: PaceWithTime }) {
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