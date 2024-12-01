import { config } from "@/config"
import React from "react"
import { EventSearchResultDto } from "./definitions"
import CardContainer from "./_components/CardContainer"

const getData = async (): Promise<EventSearchResultDto[]> => {
  const url = `${config.apiHost}/raceSeriesSearchApi`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page() {
  const events = await getData()

  return (
    <div className="flex gap-1">
      <div className="w-1/3">Directory</div>
      <div className="w-2/3">
        <div className="flex flex-wrap -mx-2">
          <CardContainer events={events} apiHost={config.apiHost} />
        </div>
      </div>
    </div>
  )
}
