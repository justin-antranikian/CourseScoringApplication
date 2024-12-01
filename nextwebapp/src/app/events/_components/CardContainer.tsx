"use client"

import React, { useState } from "react"
import { EventSearchResultDto } from "../definitions"
import Card from "./Card"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { RaceLeaderboardDto } from "@/app/races/[id]/definitions"

export default function CardContainer({
  events,
  apiHost,
}: {
  events: EventSearchResultDto[]
  apiHost: string
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [leaderboard, setLeaderboard] = useState<RaceLeaderboardDto | null>(
    null,
  )

  const handleViewMoreClicked = async (
    event: EventSearchResultDto,
  ): Promise<void> => {
    const raceId = event.upcomingRaceId
    const response = await fetch(`${apiHost}/raceLeaderboardApi/${raceId}`)

    const result = (await response.json()) as RaceLeaderboardDto
    setLeaderboard(result)
    setDialogOpen(true)
  }

  const LeaderboardDialogContent = () => {
    if (!leaderboard) {
      return
    }

    return (
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{leaderboard.raceName}</DialogTitle>
          <DialogDescription>
            This action cannot be undone. This will permanently delete your
            account and remove your data from our servers.
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    )
  }

  return (
    <>
      {events.map((event, index) => (
        <div
          key={index}
          className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4"
        >
          <div className="p-4 bg-gray-200 rounded shadow">
            <Card event={event} clickHandler={handleViewMoreClicked} />
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <LeaderboardDialogContent />
      </Dialog>
    </>
  )
}
