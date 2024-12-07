"use client"

import React, { useMemo, useState } from "react"
import { AthleteSearchResultDto } from "../definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import { Dialog } from "@/components/ui/dialog"
import { ArpDto } from "../[id]/definitions"
import { BadgePlus, Ellipsis, Scale } from "lucide-react"
import { Card, CardContent } from "@/components/ui/card"
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
import QuickViewDialogContent from "./QuickViewDialogContent"

export default function Content({
  apiHost,
  athletes,
}: {
  apiHost: string
  athletes: AthleteSearchResultDto[]
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [arp, setArp] = useState<ArpDto | null>(null)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const [hideComparePane, setHideComparePane] = useState(false)

  const handleViewMoreClicked = async (arpResult: AthleteSearchResultDto) => {
    const url = `${apiHost}/arpApi/${arpResult.id}`
    const response = await fetch(url)
    const result = (await response.json()) as ArpDto

    setArp(result)
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

  const ComparePane = () => {
    if (hideComparePane) {
      return (
        <div className="fixed bottom-0 left-0 w-full bg-gray-200 bg-opacity-90 text-black px-4 text-right">
          <span
            onClick={() => setHideComparePane(false)}
            className="cursor-pointer"
          >
            open
          </span>
        </div>
      )
    }

    return (
      <div className="fixed bottom-0 left-0 py-3 w-full bg-gray-200 bg-opacity-90 text-black flex items-center justify-between px-4">
        <div className="text-center flex-1">
          <a href={`/athletes/compare?ids=${idsEncoded}`}>
            Compare ({selectedIds.length})
          </a>
        </div>
        <div className="text-right">
          <span
            className="cursor-pointer"
            onClick={() => setHideComparePane(true)}
          >
            x
          </span>
        </div>
      </div>
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
                    <div>
                    <Scale
                      className="cursor-pointer"
                      onClick={() => handleCompareClicked(athlete.id)}
                      size={12}
                    />
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
      {selectedIds.length > 0 ? <ComparePane /> : null}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {arp ? <QuickViewDialogContent arp={arp} apiHost={apiHost} /> : null}
      </Dialog>
    </>
  )
}
