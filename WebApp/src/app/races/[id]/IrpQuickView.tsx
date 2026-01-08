import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import Intervals from "@/app/_components/Intervals"
import { DialogContent, DialogTitle } from "@/components/ui/dialog"
import React from "react"
import { Irp } from "@/app/_api/results/definitions"
// import { VisuallyHidden } from "@radix-ui/react-visually-hidden"

export default function IrpQuickView({ irp }: { irp: Irp }) {
  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <DialogTitle>Irp Quick View</DialogTitle>
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl bold">{irp.fullName}</div>
          <div>
            {irp.locationInfoWithRank.city}, {irp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {irp.genderAbbreviated} | {irp.raceAge}
          </div>
          <LocationInfoRankings locationInfoWithRank={irp.locationInfoWithRank} locationType={LocationType.races} />
        </div>
        <div className="flex-[3]">
          <div className="mb-8 text-purple-500 bold text-2xl">Results</div>
          <Intervals irp={irp} />
        </div>
      </div>
    </DialogContent>
  )
}
