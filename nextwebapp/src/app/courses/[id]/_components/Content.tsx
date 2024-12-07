"use client"

import React, { useMemo, useState } from "react"
import { CourseLeaderboardDto, LeaderboardResultDto } from "../definitions"
import { InfoIcon } from "lucide-react"
import { Irp } from "@/app/results/[id]/definitions"
import { Dialog } from "@/components/ui/dialog"
import IrpQuickView from "@/app/races/[id]/_components/IrpQuickView"
import { Scale } from "lucide-react"

export default function Content({
  apiHost,
  courseLeaderboard,
}: {
  apiHost: string
  courseLeaderboard: CourseLeaderboardDto
}) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [irp, setIrp] = useState<Irp | null>(null)
  const [selectedIds, setSelectedIds] = useState<number[]>([])
  const [hideComparePane, setHideComparePane] = useState(false)

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

  const handleQuickViewClicked = async (
    irpResult: LeaderboardResultDto,
  ): Promise<void> => {
    const url = `${apiHost}/irpApi/${irpResult.athleteCourseId}`
    const response = await fetch(url)
    const result = (await response.json()) as Irp
    setIrp(result)
    setDialogOpen(true)
  }

  const ComparePane = () => {
    return (
      <div className="fixed bottom-0 left-0 py-5 w-full bg-gray-200 bg-opacity-50 text-black flex items-center justify-between px-4">
        {hideComparePane ? (
          <div className="text-right">
            <span onClick={() => setHideComparePane(false)}>open</span>
          </div>
        ) : (
          <>
            <div className="text-center flex-1">
              <a href={`/compare-results?ids=${idsEncoded}`}>
                Compare ({selectedIds.length})
              </a>
            </div>
            <div className="text-right">
              <span onClick={() => setHideComparePane(true)}>x</span>
            </div>
          </>
        )}
      </div>
    )
  }

  return (
    <>
      {courseLeaderboard.leaderboards.map((leaderboard, index) => (
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">
            {leaderboard.intervalName}
          </div>
          <table className="table-auto w-full mb-8">
            <thead className="text-lg">
              <tr className="border-b border-black">
                <th className="w-[5%] text-left py-2" scope="col"></th>
                <th className="w-[5%] text-left py-2" scope="col"></th>
                <th className="w-[10%] text-left py-2" scope="col">
                  Bib
                </th>
                <th className="w-[20%] text-left py-2" scope="col">
                  Name
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
                  Time
                </th>
                <th className="w-[10%] text-left py-2" scope="col">
                  Pace
                </th>
                <th className="w-[5%] text-left py-2" scope="col"></th>
              </tr>
            </thead>
            <tbody className="text-sm">
              {leaderboard.results.map((irp) => (
                <tr
                  key={irp.athleteCourseId}
                  className="border-b border-gray-300"
                >
                  <td className="text-left py-2">
                    <a href={`/results/${irp.athleteCourseId}`}>View</a>
                  </td>
                  <td className="py-2">
                    <InfoIcon
                      className="cursor-pointer"
                      size={14}
                      color="black"
                      strokeWidth={1.5}
                      onClick={() => handleQuickViewClicked(irp)}
                    />
                  </td>
                  <td className="py-2">
                    <span
                      className="bg-gray-800 text-white px-2 py-1 rounded"
                      title="bib"
                    >
                      {irp.bib}
                    </span>
                  </td>
                  <td className="py-2">
                    <div>
                      <a href={`/athletes/${irp.athleteId}`}>{irp.fullName}</a>
                    </div>
                    <div>
                      {irp.genderAbbreviated} | {irp.raceAge}
                    </div>
                  </td>
                  <td className="font-bold text-gray-500 py-2">
                    {irp.overallRank}
                  </td>
                  <td className="font-bold text-gray-500 py-2">
                    {irp.genderRank}
                  </td>
                  <td className="font-bold text-gray-500 py-2">
                    {irp.divisionRank}
                  </td>
                  <td className="font-bold py-2">
                    {irp.paceWithTimeCumulative.timeFormatted}
                  </td>
                  <td className="py-2">
                    <div className="font-bold">
                      {irp.paceWithTimeCumulative.paceValue || "--"}
                    </div>
                    {irp.paceWithTimeCumulative.paceLabel}
                  </td>
                  <td className="py-2">
                    <Scale
                      className="cursor-pointer"
                      onClick={() => handleCompareClicked(irp.athleteCourseId)}
                      size={12}
                    />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ))}
      {selectedIds.length > 0 ? <ComparePane /> : null}

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <IrpQuickView irp={irp} />
      </Dialog>
    </>
  )
}
