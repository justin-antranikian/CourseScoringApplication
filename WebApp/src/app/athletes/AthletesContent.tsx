"use client"

import React, { useMemo, useState } from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { Dialog } from "@/components/ui/dialog"
import { BadgePlus, ChartBarStacked, Ellipsis } from "lucide-react"
import { Card, CardContent } from "@/components/ui/card"
import { ContextMenu, ContextMenuContent, ContextMenuItem, ContextMenuTrigger } from "@/components/ui/context-menu"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import QuickViewDialogContent from "./QuickViewDialogContent"
import ComparePane from "@/app/_components/ComparePane"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { AthleteSearchResultDto, ArpDto } from "@/app/_api/athletes/definitions"
import { getAthleteDetails } from "@/app/_api/serverActions"
import AthleteImage from "../_components/AthleteImage"

export default function AthletesContent({ athletes }: { athletes: AthleteSearchResultDto[] }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [arp, setArp] = useState<ArpDto | null>(null)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const [hideComparePane, setHideComparePane] = useState(false)

  const handleViewMoreClicked = async (athlete: AthleteSearchResultDto) => {
    const arp = await getAthleteDetails(athlete.id)
    setArp(arp)
    setDialogOpen(true)
  }

  const handleCompareClicked = (id: number) => {
    setSelectedIds((prevSelectedResults) => {
      return prevSelectedResults.includes(id)
        ? prevSelectedResults.filter((resultId) => resultId !== id)
        : [...prevSelectedResults, id]
    })
  }

  const idsEncoded = useMemo(() => {
    return encodeURIComponent(`[${selectedIds.join(",")}]`)
  }, [selectedIds])

  return (
    <div className="flex flex-wrap">
      {athletes.map((athlete, index) => (
        <div key={index} className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4">
          <Card className="rounded shadow">
            <ContextMenu>
              <ContextMenuTrigger>
                <CardContent className="p-0">
                  <div>
                    <AthleteImage width="100%" />
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
                        locationType={LocationType.athletes}
                      />
                    </div>
                    <div>
                      <DropdownMenu>
                        <DropdownMenuTrigger>
                          <Ellipsis />
                        </DropdownMenuTrigger>
                        <DropdownMenuContent>
                          <DropdownMenuItem onClick={() => handleViewMoreClicked(athlete)}>
                            <BadgePlus className="cursor-pointer" size={10} color="black" strokeWidth={1.5} />
                            Quick View
                          </DropdownMenuItem>
                        </DropdownMenuContent>
                      </DropdownMenu>
                    </div>
                    <div>
                      <TooltipProvider>
                        <Tooltip>
                          <TooltipTrigger>
                            <ChartBarStacked
                              className="cursor-pointer"
                              onClick={() => handleCompareClicked(athlete.id)}
                              size={15}
                              color="green"
                            />
                          </TooltipTrigger>
                          <TooltipContent>
                            <p>Compare Athletes</p>
                          </TooltipContent>
                        </Tooltip>
                      </TooltipProvider>
                    </div>
                  </div>
                </CardContent>
              </ContextMenuTrigger>
              <ContextMenuContent>
                <ContextMenuItem onClick={() => handleViewMoreClicked(athlete)}>Quick View</ContextMenuItem>
              </ContextMenuContent>
            </ContextMenu>
          </Card>
        </div>
      ))}
      {selectedIds.length > 0 ? (
        <ComparePane
          hideComparePane={hideComparePane}
          setHideComparePane={setHideComparePane}
          idsEncoded={idsEncoded}
          selectedIds={selectedIds}
          url={"athletes/compare"}
          thickOpactity={true}
        />
      ) : null}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {arp ? <QuickViewDialogContent arp={arp} /> : null}
      </Dialog>
    </div>
  )
}
