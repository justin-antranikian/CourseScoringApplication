"use client"

import { CourseDetailsDto } from "@/app/_api/courses/definitions"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import RaceSeriesImage from "@/app/_components/RaceSeriesImage"
import { BreadcrumbEllipsis } from "@/components/ui/breadcrumb"
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet"
import React, { useState } from "react"

export default function CourseDetailsSheetView({ course }: { course: CourseDetailsDto }) {
  const [sheetOpen, setSheetOpen] = useState<boolean>(false)

  return (
    <>
      <BreadcrumbEllipsis onClick={() => setSheetOpen(true)} title="View Course Details" className="h-4 w-4" />
      <span className="sr-only">Toggle menu</span>

      <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
        <SheetContent>
          <SheetHeader>
            <SheetTitle>Course Details</SheetTitle>
          </SheetHeader>
          <div className="mt-3">
            <div>
              <RaceSeriesImage raceSeriesType={course.raceSeriesType} height={150} />
            </div>
            <div className="mt-2 text-2xl font-bold">{course.raceName}</div>
            <div className="text-lg text-blue-500 font-bold">{course.name}</div>
            <div className="text-sm mb-2">
              <div>
                {course.locationInfoWithRank.city}, {course.locationInfoWithRank.state}
              </div>
              <div>Distance: {course.distance}</div>
              <div>
                {course.courseDate} at <strong>{course.courseTime}</strong>
              </div>
            </div>
            <LocationInfoRankings
              locationInfoWithRank={course.locationInfoWithRank}
              locationType={LocationType.races}
            />
          </div>
        </SheetContent>
      </Sheet>
    </>
  )
}
