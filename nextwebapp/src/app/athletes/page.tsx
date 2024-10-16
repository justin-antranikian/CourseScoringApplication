import { config } from "@/config"
import React from "react"
import { AthleteSearchResultDto } from "./definitions"
import LocationInfoRankings from "../_components/LocationInfoRankings"
import Link from "next/link"

const getData = async (): Promise<AthleteSearchResultDto[]> => {
  const url = `${config.apiHost}/athleteSearchApi`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page() {
  const athletes = await getData()

  return (
    <div className="flex gap-1">
      <div className="w-1/3">Directory</div>
      <div className="w-2/3">
        <div className="flex flex-wrap -mx-2">
          {athletes.map((athlete, index) => (
            <div
              key={index}
              className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4"
            >
              <div className="p-4 bg-gray-200 rounded shadow">
                <Card athlete={athlete} />
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}

const Card = ({ athlete }: { athlete: AthleteSearchResultDto }) => {
  return (
    <div>
      <div className="py-2 text-center bg-secondary">
        <Link href={`/athletes/${athlete.id}`}>
          <strong>{athlete.fullName}</strong>
        </Link>
      </div>

      <div className="mt-2 px-2">
        <LocationInfoRankings
          locationInfoWithRank={athlete.locationInfoWithRank}
        />
        <div className="text-right">
          <i className="fa fa-plus-circle cursor-pointer" title="view more"></i>
        </div>
      </div>
      <div className="mb-2 px-2 text-right">
        <i
          // onClick={() => onCompareClicked(athlete)}
          className="fa fa-compress cursor-pointer"
          title="compare"
        ></i>
      </div>
    </div>
  )
}
