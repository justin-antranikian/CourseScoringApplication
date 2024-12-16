import React from "react"
import { apiCaller } from "../_api/api"
import Content from "./_components/Content"

const api = apiCaller()

export default async function Page() {
  const events = await api.races.search()

  return (
    <div className="flex gap-1">
      <div className="w-1/4">Directory</div>
      <div className="w-3/4">
        <div className="flex flex-wrap -mx-2">
          <Content events={events} />
        </div>
      </div>
    </div>
  )
}
