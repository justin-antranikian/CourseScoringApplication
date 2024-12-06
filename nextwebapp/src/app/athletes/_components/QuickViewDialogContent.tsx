import { DialogContent } from "@/components/ui/dialog"
import React, { useEffect, useState } from "react"
import { ArpDto, ArpResultDto } from "../[id]/definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import Link from "next/link"
import { Info } from "lucide-react"
import { BracketRank } from "@/app/_components/BracketRank"
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
} from "@/components/ui/sheet"
import { Irp } from "@/app/results/[id]/definitions"
import RankWithTime from "./RankWithTime"
import IrpDetails from "./IrpDetails"

export default function QuickViewDialogContent({
  arp,
  apiHost,
}: {
  arp: ArpDto
  apiHost: string
}) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)
  const [irpDetails, setIrpDetails] = useState<Irp | null>(null)

  useEffect(() => {
    if (irpDetails) {
      setSheetOpen(true)
    }
  }, [irpDetails])

  const getIrpData = async (irp: ArpResultDto) => {
    const url = `${apiHost}/irpApi/${irp.athleteCourseId}`
    const response = await fetch(url)
    const result = (await response.json()) as Irp
    setIrpDetails(result)
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl font-bold">{arp.fullName}</div>
          <div className="text-lg">
            {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {arp.genderAbbreviated} | {arp.age}
          </div>
          <LocationInfoRankings
            locationInfoWithRank={arp.locationInfoWithRank}
          />
        </div>
        <div className="flex-[3]">
          <div className="mb-8 text-purple-500 bold text-2xl">Results</div>
          <table className="my-5 table-auto w-full">
            <thead>
              <tr className="border-b border-black">
                <th className="w-[10%] py-2" scope="col"></th>
                <th className="w-[5%] py-2" scope="col"></th>
                <th className="w-[25%] text-left py-2" scope="col">
                  Event Name
                </th>
                <th className="w-[13%] text-left py-2" scope="col">
                  Overall
                </th>
                <th className="w-[13%] text-left py-2" scope="col">
                  Gender
                </th>
                <th className="w-[14%] text-left py-2" scope="col">
                  Division
                </th>
                <th className="w-[20%] text-left py-2" scope="col">
                  Total Time
                </th>
              </tr>
            </thead>
            <tbody>
              {arp.results.map((result) => {
                return (
                  <tr className="border-b border-gray-300">
                    <td className="py-2">
                      <Link href={`/results/${result.athleteCourseId}`}>
                        View
                      </Link>
                    </td>
                    <td className="text-left py-2">
                      <span className="cursor-pointer">
                        <Info size={14} onClick={() => getIrpData(result)} />
                      </span>
                    </td>
                    <td className="py-2">
                      <div>
                        <Link href={`/races/${result.raceId}`}>
                          {result.raceName}
                        </Link>
                      </div>
                      <div>
                        <Link href={`/courses/${result.courseId}`}>
                          {result.courseName}
                        </Link>
                      </div>
                    </td>
                    <td className="py-2">
                      <BracketRank
                        rank={result.overallRank}
                        total={result.overallCount}
                      />
                    </td>
                    <td className="py-2">
                      <BracketRank
                        rank={result.genderRank}
                        total={result.genderCount}
                      />
                    </td>
                    <td className="py-2">
                      <BracketRank
                        rank={result.primaryDivisionRank}
                        total={result.primaryDivisionCount}
                      />
                    </td>
                    <td className="py-2">
                      <RankWithTime paceTime={result.paceWithTimeCumulative} />
                    </td>
                  </tr>
                )
              })}
            </tbody>
          </table>
        </div>
      </div>
      <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
        <SheetContent>
          <SheetHeader>
            <SheetTitle>Irp Details</SheetTitle>
          </SheetHeader>
          {irpDetails ? <IrpDetails irpDetails={irpDetails} /> : null}
        </SheetContent>
      </Sheet>
    </DialogContent>
  )
}
