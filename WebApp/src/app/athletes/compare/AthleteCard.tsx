"use client"

import { AthleteCompareDto, AthleteCompareStatDto } from "@/app/_api/athletes/definitions"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { ContextMenuContent } from "@/components/ui/context-menu"
import { MoveLeft, MoveRight } from "lucide-react"
import React, { useState } from "react"
import { raceSeriesTypeNameMap } from "../../definitions"
import AthleteImage from "../../_components/AthleteImage"

export default function AthleteCard({ athlete }: { athlete: AthleteCompareDto }) {
  const [slideNumber, setSlideNumber] = useState(1)

  const slides = [
    <div key={1}>
      <div className="text-2xl">
        {athlete.age} | {athlete.genderAbbreviated}
      </div>
      <LocationInfoRankings locationInfoWithRank={athlete.locationInfoWithRank} locationType={LocationType.athletes} />
    </div>,
    <div key={2}>
      {Object.entries(raceSeriesTypeNameMap).map(([raceSeriesType, seriesName]) => {
        const stat = athlete.stats.find((stat) => stat.raceSeriesType === raceSeriesType)
        return <StatLine key={raceSeriesType} raceSeriesType={seriesName} stat={stat} />
      })}
    </div>,
  ]

  const slideCount = slides.length

  const previousSlide = () => {
    setSlideNumber((previous: number) => (previous === 1 ? slideCount : previous - 1))
  }

  const nextSlide = () => {
    setSlideNumber((previous: number) => (previous === slideCount ? 1 : previous + 1))
  }

  return (
    <ContextMenuContent className="w-[400px] min-w-[400px] p-3">
      <div>
        <AthleteImage width="65%" />
      </div>
      <div className="bg-purple-200 text-center text-base py-2 mb-3">
        <a href={`/athletes/${athlete.id}`}>
          <strong>{athlete.fullName}</strong>
        </a>
      </div>
      <div className="flex items-center">
        <div className="flex-shrink-0 flex justify-start">
          <MoveLeft onClick={previousSlide} className="cursor-pointer" />
        </div>
        <div className="flex-grow mx-3 text-center">{slides[slideNumber - 1]}</div>
        <div className="flex-shrink-0 flex justify-end">
          <MoveRight onClick={nextSlide} className="cursor-pointer" />
        </div>
      </div>
    </ContextMenuContent>
  )
}

const StatLine = ({ raceSeriesType, stat }: { raceSeriesType: string; stat: AthleteCompareStatDto | undefined }) => {
  const Content = () => {
    if (!stat) {
      return <>No stats available</>
    }

    if (!stat.goalTotal) {
      return <>{stat.actualTotal} (total events)</>
    }

    return (
      <>
        {stat.actualTotal} of {stat.goalTotal}
      </>
    )
  }

  return (
    <div key={raceSeriesType}>
      <strong>{raceSeriesType}</strong>: <Content />
    </div>
  )
}
