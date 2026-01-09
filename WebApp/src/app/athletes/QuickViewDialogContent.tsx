import { DialogContent, DialogTitle } from "@/components/ui/dialog"
import React, { useState } from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import Link from "next/link"
import { Info } from "lucide-react"
import { BracketRank } from "@/app/_components/BracketRank"
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet"
import RankWithTime from "./RankWithTime"
import IrpDetails from "../_components/IrpDetails"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp } from "@/app/_api/serverActions"
import { ArpDto, ArpResultDto } from "@/app/_api/athletes/definitions"
import { Irp } from "@/app/_api/results/definitions"
// import { VisuallyHidden } from "@radix-ui/react-visually-hidden"

export default function QuickViewDialogContent({ arp }: { arp: ArpDto }) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)
  const [irpDetails, setIrpDetails] = useState<Irp | null>(null)

  const getIrpData = async ({ athleteCourseId }: ArpResultDto) => {
    const irp = await getIrp(athleteCourseId)
    setIrpDetails(irp)
    setSheetOpen(true)
  }

  return (
    <DialogContent className="w-[90%] max-w-screen-lg h-[90vh] overflow-y-auto">
      {/* <VisuallyHidden> */}
      <DialogTitle>Athlete Quick View</DialogTitle>
      {/* </VisuallyHidden> */}
      <div className="flex mt-5">
        <div className="flex-[1]">
          <div className="text-2xl font-bold">{arp.fullName}</div>
          <div className="text-lg">
            {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
          </div>
          <div className="mb-3 text-xs">
            {arp.genderAbbreviated} | {arp.age}
          </div>
          <LocationInfoRankings locationInfoWithRank={arp.locationInfoWithRank} locationType={LocationType.athletes} />
        </div>
        <div className="flex-[3]">
          <div className="mb-8 text-purple-500 bold text-2xl">Results</div>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-[10%]"></TableHead>
                <TableHead className="w-[5%]"></TableHead>
                <TableHead className="w-[25%]">Event Name</TableHead>
                <TableHead className="w-[13%]">Overall</TableHead>
                <TableHead className="w-[13%]">Gender</TableHead>
                <TableHead className="w-[14%]">Division</TableHead>
                <TableHead className="w-[20%]">Total Time</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {arp.results.map((result) => (
                <TableRow key={result.athleteCourseId}>
                  <TableCell>
                    <Link href={`/results/${result.athleteCourseId}`}>View</Link>
                  </TableCell>
                  <TableCell>
                    <span className="cursor-pointer">
                      <Info size={14} onClick={() => getIrpData(result)} />
                    </span>
                  </TableCell>
                  <TableCell>
                    <div>
                      <Link href={`/races/${result.raceId}`}>{result.raceName}</Link>
                    </div>
                    <div>
                      <Link href={`/courses/${result.courseId}`}>{result.courseName}</Link>
                    </div>
                  </TableCell>
                  <TableCell>
                    <BracketRank rank={result.overallRank} total={result.overallCount} />
                  </TableCell>
                  <TableCell>
                    <BracketRank rank={result.genderRank} total={result.genderCount} />
                  </TableCell>
                  <TableCell>
                    <BracketRank rank={result.primaryDivisionRank} total={result.primaryDivisionCount} />
                  </TableCell>
                  <TableCell>
                    <RankWithTime paceTime={result.paceWithTimeCumulative} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      </div>
      <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
        <SheetContent>
          <SheetHeader>
            <SheetTitle>Result Details</SheetTitle>
          </SheetHeader>
          {irpDetails ? <IrpDetails irpDetails={irpDetails} /> : null}
        </SheetContent>
      </Sheet>
    </DialogContent>
  )
}
