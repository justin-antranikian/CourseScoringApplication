"use client"

import { CompareAthletesAthleteInfoDto, CompareAthletesStat } from "@/app/_api/athletes/definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { ContextMenuContent } from "@/components/ui/context-menu"
import { MoveLeft, MoveRight } from "lucide-react"
import React, { useState } from "react"
import { raceSeriesNames } from "../definitions"

export default function AthleteCard({ athlete }: { athlete: CompareAthletesAthleteInfoDto }) {
  const [slideNumber, setSlideNumber] = useState(1)

  const slides = [
    <div>
      <div className="text-2xl">
        {athlete.age} | {athlete.genderAbbreviated}
      </div>
      <LocationInfoRankings locationInfoWithRank={athlete.locationInfoWithRank} />
    </div>,
    <div>
      {raceSeriesNames.map((seriesName) => {
        const stat = athlete.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
        return <StatLine key={seriesName} raceSeriesTypeName={seriesName} stat={stat} />
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
        <img style={{ width: "50%", height: 125 }} src="/Athlete.png" />
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

const StatLine = ({
  raceSeriesTypeName,
  stat,
}: {
  raceSeriesTypeName: string
  stat: CompareAthletesStat | undefined
}) => {
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
    <div key={raceSeriesTypeName}>
      <strong>{raceSeriesTypeName}</strong>: <Content />
    </div>
  )
}
