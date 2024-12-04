"use client"

import React, { useState } from "react"
import { AthleteSearchResultDto } from "../definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { Dialog, DialogContent } from "@/components/ui/dialog"
import { ArpDto } from "../[id]/definitions"
import { BadgePlus, Ellipsis } from "lucide-react"
import AtheleteResult from "../[id]/AtheleteResult"
import { Card, CardContent, CardTitle } from "@/components/ui/card"
import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "@/components/ui/context-menu"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"

export default function Content({
  apiHost,
  athletes,
}: {
  apiHost: string
  athletes: AthleteSearchResultDto[]
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [arp, setArp] = useState<ArpDto | null>(null)

  const handleViewMoreClicked = async (arpResult: AthleteSearchResultDto) => {
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
          <div className="mb-8 text-purple-500 bold text-2xl">Results</div>
          <table className="my-5 table-auto w-full">
            <thead>
              <tr className="border-b border-black">
                <th className="w-[10%] py-2" scope="col"></th>
                <th className="w-[30%] text-left py-2" scope="col">
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
                return <AtheleteResult result={result} />
              })}
            </tbody>
          </table>
        </>
      )
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
            <AthleteResultsContent />
          </div>
        </div>
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
          <Card className="rounded shadow">
            <ContextMenu>
              <ContextMenuTrigger>
                <CardContent className="p-0">
                  <div>
                    <img
                      style={{ width: "100%", height: 125 }}
                      src="/Athlete.png"
                    />
                  </div>
                  <div className="bg-purple-200 text-center text-base py-2">
                    <a href={`/athletes/${athlete.id}`}>
                      <strong>{athlete.fullName}</strong>
                    </a>
                  </div>

                  <div className="p-2">
                    <div className="my-3">
                      <LocationInfoRankings
                        locationInfoWithRank={athlete.locationInfoWithRank}
                      />
                    </div>
                    <div>
                      <DropdownMenu>
                        <DropdownMenuTrigger>
                          <Ellipsis />
                        </DropdownMenuTrigger>
                        <DropdownMenuContent>
                          <DropdownMenuItem
                            onClick={() => handleViewMoreClicked(athlete)}
                          >
                            <BadgePlus
                              className="cursor-pointer"
                              size={10}
                              color="black"
                              strokeWidth={1.5}
                            />
                            Quick View
                          </DropdownMenuItem>
                        </DropdownMenuContent>
                      </DropdownMenu>
                    </div>
                  </div>
                </CardContent>
              </ContextMenuTrigger>
              <ContextMenuContent>
                <ContextMenuItem onClick={() => handleViewMoreClicked(athlete)}>
                  Quick View
                </ContextMenuItem>
              </ContextMenuContent>
            </ContextMenu>
          </Card>
        </div>
      ))}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <QuickViewDialogContent />
      </Dialog>
    </>
  )
}
