"use client"

import { LocationDto } from "@/app/_api/locations/definitions"
import { LocationType } from "@/app/_components/LocationInfoRankings"
import { TreeView } from "@/app/_components/TreeView"
import { BreadcrumbEllipsis } from "@/components/ui/breadcrumb"
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet"
import React, { useState } from "react"

export default function DirectorySheetView({
  locations,
  locationType,
}: {
  locations: LocationDto[]
  locationType: LocationType
}) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)

  return (
    <>
      <BreadcrumbEllipsis onClick={() => setSheetOpen(true)} title="Directory Quick View" className="h-4 w-4" />
      <span className="sr-only">Toggle menu</span>

      <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
        <SheetContent side={"left"}>
          <SheetHeader>
            <SheetTitle>Directory</SheetTitle>
          </SheetHeader>
          <ul className="list-none mt-3">
            <TreeView nodes={locations} locationType={locationType} />
          </ul>
        </SheetContent>
      </Sheet>
    </>
  )
}
