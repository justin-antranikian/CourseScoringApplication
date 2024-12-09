import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { Irp } from "@/app/results/[id]/definitions"
import Intervals from "@/app/results/_components/Intervals"
import { DialogContent } from "@/components/ui/dialog"
import React from "react"

export default function IrpQuickView({ irp }: { irp: Irp | null }) {
  if (!irp) {
    return
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl bold">{irp.fullName}</div>
          <div>
            {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {irp.genderAbbreviated} | {irp.raceAge}
          </div>
          <LocationInfoRankings
            locationInfoWithRank={irp.locationInfoWithRank}
          />
        </div>
        <div className="flex-[3]">
          <div className="mb-8 text-purple-500 bold text-2xl">Results</div>
          <Intervals irp={irp} />
        </div>
      </div>
    </DialogContent>
  )
}
