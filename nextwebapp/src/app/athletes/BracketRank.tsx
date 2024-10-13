export const BracketRank = ({ rank, total }: { rank: number; total: number }) => {
  return (
    <>
      <span className="text-lg font-bold">{rank}</span>
      <span className="text-sm mx-1">of {total}</span>
    </>
  )
}