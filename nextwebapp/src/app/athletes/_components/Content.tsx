"use client"

import React, { useState } from "react"
import { AthleteSearchResultDto } from "../definitions"
import Link from "next/link"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { ArpDto } from "../[id]/definitions"
import { Camera } from "lucide-react"
import AtheleteResult from "../[id]/AtheleteResult"

export default function Content({
  apiHost,
  athletes,
}: {
  apiHost: string
  athletes: AthleteSearchResultDto[]
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [arp, setArp] = useState<ArpDto | null>(null)

  const handleQuickViewClicked = async (arpResult: AthleteSearchResultDto) => {
    const url = `${apiHost}/arpApi/${arpResult.id}`
    const response = await fetch(url)
    const result = (await response.json()) as ArpDto

    setArp(result)
    setDialogOpen(true)
  }

  const QuickViewDialogContent = () => {
    if (!arp) {
      return
    }

    const AthleteResultsContent = () => {
      return (
        <>
          <div className="mb-12">Results</div>

          <table className="my-5 table-auto w-full">
            <thead>
              <tr>
                <th className="w-[20%]" scope="col"></th>
                <th className="w-[30%] text-left" scope="col">
                  Event Name
                </th>
                <th className="w-[10%]" scope="col">
                  Overall
                </th>
                <th className="w-[10%]" scope="col">
                  Gender
                </th>
                <th className="w-[10%]" scope="col">
                  Division
                </th>
                <th className="w-[20%]" scope="col">
                  Total Time
                </th>
              </tr>
            </thead>
            <tbody>
              {arp.results.map((result) => {
                return <AtheleteResult result={result} />
              })}
            </tbody>
          </table>
        </>
      )
    }

    return (
      <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle>Athlete Quick View</DialogTitle>
          <DialogDescription>
            <div className="flex mt-5">
              <div className="flex-[1]">{arp.fullName}</div>
              <div className="flex-[3]">
                <AthleteResultsContent />
              </div>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    )
  }

  return (
    <>
      {athletes.map((athlete, index) => (
        <div
          key={index}
          className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4"
        >
          <div className="p-4 bg-gray-200 rounded shadow">
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
                  <Camera
                    size={14}
                    onClick={() => handleQuickViewClicked(athlete)}
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <QuickViewDialogContent />
      </Dialog>
    </>
  )
}