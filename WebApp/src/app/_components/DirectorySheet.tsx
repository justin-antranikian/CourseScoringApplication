"use client"

import { LocationDto } from "@/app/_api/locations/definitions"
import { LocationType } from "@/app/_components/LocationInfoRankings"
import { DirectoryTree } from "@/app/_components/DirectoryTree"
import { BreadcrumbEllipsis } from "@/components/ui/breadcrumb"
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet"
import React, { useState } from "react"

export default function DirectorySheet({
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
          <div className="mt-3">
            <DirectoryTree locations={locations} locationType={locationType} />
          </div>
        </SheetContent>
      </Sheet>
    </>
  )
}
