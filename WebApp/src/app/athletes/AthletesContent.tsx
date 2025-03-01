"use client"

import React, { useState } from "react"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { Dialog } from "@/components/ui/dialog"
import { Ellipsis } from "lucide-react"
import { Card, CardContent } from "@/components/ui/card"
import { ContextMenu, ContextMenuContent, ContextMenuItem, ContextMenuTrigger } from "@/components/ui/context-menu"
import QuickViewDialogContent from "./QuickViewDialogContent"
import ComparePane from "@/app/athletes/ComparePane"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { AthleteSearchResultDto, ArpDto } from "@/app/_api/athletes/definitions"
import { getAthleteDetails } from "@/app/_api/serverActions"
import AthleteImage from "../_components/AthleteImage"
import { Button } from "@/components/ui/button"
import { twMerge } from "tailwind-merge"

export default function AthletesContent({
  athletes,
  directoryTree,
  athleteSearch,
}: {
  athletes: AthleteSearchResultDto[]
  directoryTree: React.ReactNode
  athleteSearch: React.ReactNode
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [arp, setArp] = useState<ArpDto | null>(null)
  const [selectedAthletes, setSelectedAthletes] = useState<AthleteSearchResultDto[]>([])
  const [showComparePane, setShowComparePane] = useState(false)

  const handleViewMoreClicked = async (athlete: AthleteSearchResultDto) => {
    const arp = await getAthleteDetails(athlete.id)
    setArp(arp)
    setDialogOpen(true)
  }

  const getSelectedIds = () => selectedAthletes.map((athlete) => athlete.id)

  const handleCompareClicked = (athlete: AthleteSearchResultDto) => {
    const selectedAthleteIds = getSelectedIds()

    if (selectedAthleteIds.includes(athlete.id)) {
      setSelectedAthletes(selectedAthletes.filter((selectedAthlete) => selectedAthlete.id !== athlete.id))
      return
    }

    setSelectedAthletes([...selectedAthletes, athlete])
  }

  const selectedIds = getSelectedIds()

  const queryParams = new URLSearchParams()
  selectedIds.forEach((id) => queryParams.append("ids", id.toString()))
  const compareUrl = `/athletes/compare?${queryParams.toString()}`

  return (
    <>
      <div className="flex gap-1">
        <div className="w-1/4">{directoryTree}</div>
        <div className="w-3/4">
          <div className="flex justify-between item-center mb-4 px-2">
            <div>
              <Button onClick={() => setShowComparePane(!showComparePane)}>Toggle Compare</Button>
            </div>
            <div>{athleteSearch}</div>
          </div>
          <div className="flex flex-wrap">
            {athletes.map((athlete, index) => (
              <div
                key={index}
                className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4 cursor-pointer"
                onClick={() => handleCompareClicked(athlete)}
              >
                <Card
                  className={twMerge(
                    "rounded shadow",
                    showComparePane && !selectedIds.includes(athlete.id) ? "hover:border hover:border-blue-300" : "",
                    showComparePane && selectedIds.includes(athlete.id) ? "border border-blue-800" : "",
                  )}
                >
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
                            <TooltipProvider>
                              <Tooltip>
                                <TooltipTrigger>
                                  <Ellipsis onClick={() => handleViewMoreClicked(athlete)} />
                                </TooltipTrigger>
                                <TooltipContent>
                                  <p>Quick View</p>
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
            {showComparePane ? (
              <ComparePane
                setShowComparePane={setShowComparePane}
                selectedAthletes={selectedAthletes}
                setSelectedAthletes={setSelectedAthletes}
                url={compareUrl}
              />
            ) : null}
            <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
              {arp ? <QuickViewDialogContent arp={arp} /> : null}
            </Dialog>
          </div>
        </div>
      </div>
    </>
  )
}
