"use client"

import React, { useState } from "react"
import { EventSearchResultDto } from "../definitions"
import Card from "./Card"

export default function CardContainer({
  events,
}: {
  events: EventSearchResultDto[]
}) {
  const [dialogOpen, setDialogOpen] = useState(false)

  const handleViewMoreClicked = async (id: number): Promise<void> => {
    console.log(id)
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
    </>
  )
}
