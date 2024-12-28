"use client"

import React, { useMemo, useState } from "react"
import { CourseLeaderboardDto, LeaderboardResultDto } from "../../../_api/courses/definitions"
import { ChartBarStacked, InfoIcon } from "lucide-react"
import { Dialog } from "@/components/ui/dialog"
import IrpQuickView from "@/app/races/[id]/IrpQuickView"
import ComparePane from "@/app/_components/ComparePane"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { getIrp, getResultSearchResults } from "@/app/_api/serverActions"
import { Irp, ResultSearchResponse } from "@/app/_api/results/definitions"

export default function Content({
  courseLeaderboard,
  courseId,
}: {
  courseLeaderboard: CourseLeaderboardDto
  courseId: string | number
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

  const handleQuickViewClicked = async ({ athleteCourseId }: LeaderboardResultDto): Promise<void> => {
    const irp = await getIrp(athleteCourseId)
    setIrp(irp)
    setDialogOpen(true)
  }

  return (
    <>
      <InputTracker courseLeaderboard={courseLeaderboard} courseId={courseId} />
      {courseLeaderboard.leaderboards.map((leaderboard, index) => (
        <div key={index}>
          <div className="mb-8 text-purple-500 bold text-2xl">{leaderboard.intervalName}</div>
          <Table className="mb-8">
            <TableHeader>
              <TableRow>
                <TableHead></TableHead>
                <TableHead></TableHead>
                <TableHead>Name</TableHead>
                <TableHead>Overall</TableHead>
                <TableHead>Gender</TableHead>
                <TableHead>Division</TableHead>
                <TableHead>Time</TableHead>
                <TableHead>Pace</TableHead>
                <TableHead></TableHead>
              </TableRow>
            </TableHeader>
            <TableBody className="text-sm">
              {leaderboard.results.map((irp) => (
                <TableRow key={irp.athleteCourseId} className="border-b border-gray-300">
                  <TableCell className="text-left py-2">
                    <a href={`/results/${irp.athleteCourseId}`}>View</a>
                  </TableCell>
                  <TableCell className="py-2">
                    <InfoIcon
                      className="cursor-pointer"
                      size={14}
                      color="black"
                      strokeWidth={1.5}
                      onClick={() => handleQuickViewClicked(irp)}
                    />
                  </TableCell>
                  <TableCell>
                    <div>
                      <a href={`/athletes/${irp.athleteId}`}>{irp.fullName}</a>
                    </div>
                    <div>
                      {irp.genderAbbreviated} | {irp.bib}
                    </div>
                  </TableCell>
                  <TableCell>{irp.overallRank}</TableCell>
                  <TableCell>{irp.genderRank}</TableCell>
                  <TableCell>{irp.divisionRank}</TableCell>
                  <TableCell className="font-bold">{irp.paceWithTimeCumulative.timeFormatted}</TableCell>
                  <TableCell>
                    <div className="font-bold">{irp.paceWithTimeCumulative.paceValue || "--"}</div>
                    {irp.paceWithTimeCumulative.paceLabel}
                  </TableCell>
                  <TableCell>
                    <TooltipProvider>
                      <Tooltip>
                        <TooltipTrigger>
                          <ChartBarStacked
                            className="cursor-pointer"
                            onClick={() => handleCompareClicked(irp.athleteCourseId)}
                            size={15}
                            color="green"
                          />
                        </TooltipTrigger>
                        <TooltipContent>
                          <p>Compare Results</p>
                        </TooltipContent>
                      </Tooltip>
                    </TooltipProvider>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      ))}
      {selectedIds.length > 0 ? (
        <ComparePane
          hideComparePane={hideComparePane}
          setHideComparePane={setHideComparePane}
          idsEncoded={idsEncoded}
          url={"results/compare"}
          selectedIds={selectedIds}
        />
      ) : null}
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {irp ? <IrpQuickView irp={irp} /> : null}
      </Dialog>
    </>
  )
}

const InputTracker = ({
  courseLeaderboard,
  courseId,
}: {
  courseLeaderboard: CourseLeaderboardDto
  courseId: string | number
}) => {
  const [searchTerm, setSearchTerm] = useState("")
  const [results, setResults] = useState<ResultSearchResponse[] | null>(null)

  const handleInputChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = event.target.value
    setSearchTerm(searchTerm)

    const results = await getResultSearchResults({
      raceId: courseLeaderboard.raceId,
      courseId,
      searchTerm,
    })

    setResults(results)
  }

  const Results = ({ results }: { results: ResultSearchResponse[] }) => {
    if (results.length === 0) {
      return <div>There were no results for {searchTerm}</div>
    }

    return results.map((result) => (
      <div className="mb-2" key={result.id}>
        <div>
          <a className="hover:underline" href={`/athletes/${result.athleteId}`}>
            {result.firstName} {result.lastName}
          </a>
        </div>
        <div>
          <a className="hover:underline" href={`/results/${result.id}`}>
            {result.bib}
          </a>
        </div>
      </div>
    ))
  }

  return (
    <div className="relative flex flex-col items-end group">
      <input
        type="text"
        placeholder="Search on bib, or first/last name"
        className="shad-cn p-2 border rounded shadow-md w-[300px]"
        value={searchTerm}
        onChange={handleInputChange}
      />
      {searchTerm.length > 0 && (
        <div className="absolute top-full mt-2 bg-white border border-gray-300 rounded shadow-lg z-50 p-4 min-w-[350px] opacity-0 group-hover:opacity-100 transition-opacity">
          {results ? <Results results={results} /> : <>... Waiting for results</>}
        </div>
      )}
    </div>
  )
}
