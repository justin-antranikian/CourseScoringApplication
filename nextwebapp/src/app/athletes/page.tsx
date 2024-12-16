import React from "react"
import Content from "./_components/Content"
import { useApi } from "../_api/api"

const api = useApi()

export default async function Page() {
  const athletes = await api.athletes.search()

  return (
    <div className="flex gap-1">
      <div className="w-1/4">Directory</div>
      <div className="w-3/4">
        <div className="flex flex-wrap -mx-2">
          <Content athletes={athletes} />
        </div>
      </div>
    </div>
  )
}
