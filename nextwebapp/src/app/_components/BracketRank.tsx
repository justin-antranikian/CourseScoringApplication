export enum BetweenIntervalTimeIndicator {
  StartingOrSame,
  Improving,
  GettingWorse,
  NotStarted,
}

export const BracketRank = ({
  rank,
  total,
  indicator,
}: {
  rank: number | null
  total: number
  indicator?: BetweenIntervalTimeIndicator
}) => {
  if (!rank) {
    return <div>--</div>
  }

  const Indicator = () => {
    if (!indicator) {
      return null
    }

    if (indicator == BetweenIntervalTimeIndicator.Improving) {
      return <div className="text-xs text-green-500">(Better)</div>
    }

    if (indicator == BetweenIntervalTimeIndicator.GettingWorse) {
      return <div className="text-xs text-red-500">(Worse)</div>
    }
  }

  return (
    <>
      <span className="text-lg font-bold">{rank}</span>
      <span className="text-sm mx-1">of {total}</span>
      <Indicator />
    </>
  )
}
