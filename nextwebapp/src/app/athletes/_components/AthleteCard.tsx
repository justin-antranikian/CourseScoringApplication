"use client"

import { CompareAthletesAthleteInfoDto, CompareAthletesStat } from "@/app/_api/athletes/definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { ContextMenuContent } from "@/components/ui/context-menu"
import { MoveLeft, MoveRight } from "lucide-react"
import React, { useState } from "react"

const raceSeriesNames = ["Running", "Triathalon", "Road Biking", "Mountain Biking", "Cross Country Skiing", "Swimming"]

const slideCount = 2

export default function AthleteCard({ athlete }: { athlete: CompareAthletesAthleteInfoDto }) {
  const [selectedIndex, setSelectedIndex] = useState(1)

  const decrementIndex = () => {
    if (selectedIndex === 1) {
      setSelectedIndex(slideCount)
      return
    }

    setSelectedIndex((previous: number) => previous - 1)
  }

  const incrementIndex = () => {
    if (selectedIndex === slideCount) {
      setSelectedIndex(1)
      return
    }

    setSelectedIndex((previous: number) => previous + 1)
  }

  const Content = () => {
    if (selectedIndex === 1) {
      return (
        <div>
          <div className="text-2xl">
            {athlete.age} | {athlete.genderAbbreviated}
          </div>
          <LocationInfoRankings locationInfoWithRank={athlete.locationInfoWithRank} />
        </div>
      )
    }

    return (
      <div>
        {raceSeriesNames.map((seriesName) => {
          const stat = athlete.stats.find((stat) => stat.raceSeriesTypeName === seriesName)
          return <StatLine key={seriesName} raceSeriesTypeName={seriesName} stat={stat} />
        })}
      </div>
    )
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
          <MoveLeft onClick={decrementIndex} className="cursor-pointer" />
        </div>
        <div className="flex-grow mx-3 text-center">
          <Content />
        </div>
        <div className="flex-shrink-0 flex justify-end">
          <MoveRight onClick={incrementIndex} className="cursor-pointer" />
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
