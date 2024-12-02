import { Irp } from "@/app/results/[id]/definitions"
import Result from "@/app/results/[id]/Result"
import { DialogContent, DialogHeader } from "@/components/ui/dialog"
import React from "react"

export default function IrpQuickView({ irp }: { irp: Irp | null }) {
  if (!irp) {
    return
  }

  const LeaderboardContent = () => {
    return (
      <>
        <table className="my-5 table-auto w-full">
          <thead>
            <tr className="border-b border-black">
              <th className="w-[15%] text-left py-2" scope="col"></th>
              <th className="w-[20%] text-left py-2" scope="col">
                Time{" "}
                <span className="text-sm">({irp.timeZoneAbbreviated})</span>
              </th>
              <th className="w-[10%] text-left py-2" scope="col">
                Overall
              </th>
              <th className="w-[10%] text-left py-2" scope="col">
                Gender
              </th>
              <th className="w-[10%] text-left py-2" scope="col">
                Division
              </th>
              <th className="w-[15%] text-left py-2" scope="col">
                Interval Time
              </th>
              <th className="w-[20%] text-left py-2" scope="col">
                Cumulative Time
              </th>
            </tr>
          </thead>
          <tbody>
            {irp.intervalResults.map((intervalResult, index) => (
              <Result result={intervalResult} key={index} />
            ))}
          </tbody>
        </table>
      </>
    )
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <DialogHeader>
        <div className="flex mt-5">
          <div className="flex-[1]">{irp.fullName}</div>
          <div className="flex-[3]">
            <LeaderboardContent />
          </div>
        </div>
      </DialogHeader>
    </DialogContent>
  )
}
