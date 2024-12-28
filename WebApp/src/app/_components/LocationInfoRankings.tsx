import React from "react"

export enum LocationType {
  athletes = "athletes",
  races = "races",
}

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
  locationType,
}: {
  locationInfoWithRank: LocationInfoWithRank
  locationType: LocationType
}) {
  return (
    <div className="text-xs">
      <div>
        <a href={`/${locationType}`}>
          <strong>#{locationInfoWithRank.overallRank}</strong> <span className="text-green-700 underline">Overall</span>
        </a>
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.stateRank}
          display={locationInfoWithRank.state}
          url={locationInfoWithRank.stateUrl}
          locationType={locationType}
        />
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.areaRank}
          display={locationInfoWithRank.area}
          url={locationInfoWithRank.areaUrl}
          locationType={locationType}
        />
      </div>
      <div>
        <RankLink
          rank={locationInfoWithRank.cityRank}
          display={locationInfoWithRank.city}
          url={locationInfoWithRank.cityUrl}
          locationType={locationType}
        />
      </div>
    </div>
  )
}

const RankLink = ({
  rank,
  display,
  url,
  locationType,
}: {
  rank: number
  display: string
  url: string
  locationType: LocationType
}) => {
  return (
    <a href={`/${locationType}/directory/${url}`}>
      <strong>#{rank}</strong> from <span className="text-green-700 underline">{display}</span>
    </a>
  )
}
