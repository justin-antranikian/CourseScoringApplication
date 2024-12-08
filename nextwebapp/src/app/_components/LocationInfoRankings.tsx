import React from "react"

export interface LocationInfoWithRank {
  area: string
  areaRank: number
  areaUrl: string
  city: string
  cityRank: number
  cityUrl: string
  overallRank: number
  state: string
  stateRank: number
  stateUrl: string
}

export default function LocationInfoRankings({
  locationInfoWithRank,
}: {
  locationInfoWithRank: LocationInfoWithRank
}) {
  return (
    <div className="text-xs">
      <div>
        <strong>#{locationInfoWithRank.overallRank}</strong>
        <a> Overall</a>
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.stateRank}
          display={locationInfoWithRank.state}
        />
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.areaRank}
          display={locationInfoWithRank.area}
        />
        <div>ok</div>
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.cityRank}
          display={locationInfoWithRank.city}
        />
      </div>
    </div>
  )
}

const RankLink = ({ rank, display }: { rank: number; display: string }) => {
  return (
    <>
      <strong>#{rank}</strong> from {display}
      {/* <Link href={`${url}/${link}`} title={display} className="text-success">
        {display}
      </Link> */}
    </>
  )
}
