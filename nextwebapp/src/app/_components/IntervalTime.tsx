import React from 'react'

export interface PaceWithTime {
  timeFormatted: string,
  hasPace: boolean,
  paceValue: string | null,
  paceLabel: string | null,
}

export default function IntervalTime({ paceTime }: { paceTime: PaceWithTime | null }) {

  if (!paceTime) {
    return <div>--</div>
  }

  return (
    <div>
      <div className="text-lg font-bold">{paceTime.timeFormatted}</div>
      {paceTime.hasPace && (
        <div>
          <strong className="mr-1">{paceTime.paceValue || 'N/A'}</strong>
          {paceTime.paceLabel}
        </div>
      )}
    </div>
  );
}
