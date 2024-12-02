import { config } from "@/config"
import React from "react"
import { AthleteSearchResultDto } from "./definitions"
import Content from "./_components/Content"

const getData = async (): Promise<AthleteSearchResultDto[]> => {
  const url = `${config.apiHost}/athleteSearchApi`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page() {
  const athletes = await getData()

  return (
    <div className="flex gap-1">
      <div className="w-1/4">Directory</div>
      <div className="w-3/4">
        <div className="flex flex-wrap -mx-2">
          <Content apiHost={config.apiHost} athletes={athletes} />
        </div>
      </div>
    </div>
  )
}
