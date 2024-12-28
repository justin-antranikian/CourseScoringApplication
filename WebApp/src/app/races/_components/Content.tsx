"use client"

import React, { useEffect, useState } from "react"
import { Dialog } from "@/components/ui/dialog"
import { EventSearchResultDto, RaceLeaderboardDto } from "@/app/_api/races/definitions"
import LocationInfoRankings, { LocationType } from "@/app/_components/LocationInfoRankings"
import { Card, CardContent } from "@/components/ui/card"
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu"
import { Ellipsis, BadgePlus } from "lucide-react"
import { ContextMenu, ContextMenuContent, ContextMenuItem, ContextMenuTrigger } from "@/components/ui/context-menu"
import QuickViewDialogContent from "./QuickViewDialogContent"
import { getImage } from "@/app/utils"
import { getRaceLeaderboard } from "@/app/_api/serverActions"

export default function Content({ events }: { events: EventSearchResultDto[] }) {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [leaderboard, setLeaderboard] = useState<RaceLeaderboardDto | null>(null)

  const handleViewMoreClicked = async ({ upcomingRaceId }: EventSearchResultDto) => {
    const leaderboard = await getRaceLeaderboard(upcomingRaceId)

    setLeaderboard(leaderboard)
    setDialogOpen(true)
  }

  return (
    <>
      <div className="flex flex-wrap -mx-2">
        {events.map((event, index) => (
          <div key={index} className="w-full sm:w-1/2 md:w-1/3 lg:w-1/4 px-2 mb-4">
            <Card className="rounded shadow">
              <ContextMenu>
                <ContextMenuTrigger>
                  <CardContent className="p-0">
                    <div>
                      <img style={{ width: "100%", height: 125 }} src={getImage(event.raceSeriesTypeName)} />
                    </div>
                    <div className="bg-purple-200 text-center text-base py-2">
                      <a href={`/races/${event.upcomingRaceId}`}>
                        <strong>{event.name}</strong>
                      </a>
                    </div>
                    <div className="p-2">
                      <div className="my-3">
                        <LocationInfoRankings
                          locationInfoWithRank={event.locationInfoWithRank}
                          locationType={LocationType.races}
                        />
                      </div>
                      <div>
                        <DropdownMenu>
                          <DropdownMenuTrigger>
                            <Ellipsis />
                          </DropdownMenuTrigger>
                          <DropdownMenuContent>
                            <DropdownMenuItem>
                              <a href={`/races/${event.upcomingRaceId}`}>Leaderboard</a>
                            </DropdownMenuItem>
                            <DropdownMenuSeparator />
                            {event.courses.map((course) => {
                              return (
                                <DropdownMenuItem key={course.id}>
                                  <a href={`/courses/${course.id}`}>{course.displayName}</a>
                                </DropdownMenuItem>
                              )
                            })}
                            <DropdownMenuSeparator />
                            <DropdownMenuItem onClick={() => handleViewMoreClicked(event)}>
                              <BadgePlus className="cursor-pointer" size={10} color="black" strokeWidth={1.5} />
                              Quick View
                            </DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </div>
                    </div>
                  </CardContent>
                </ContextMenuTrigger>
                <ContextMenuContent>
                  <ContextMenuItem onClick={() => handleViewMoreClicked(event)}>Quick View</ContextMenuItem>
                </ContextMenuContent>
              </ContextMenu>
            </Card>
          </div>
        ))}
      </div>
      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        {leaderboard ? <QuickViewDialogContent leaderboard={leaderboard} /> : null}
      </Dialog>
    </>
  )
}

// const SearchComponent = () => {
//   const [query, setQuery] = useState("")
//   const [results, setResults] = useState([])
//   const [isSearching, setIsSearching] = useState(false)

//   // Debounce the search input
//   useEffect(() => {
//     const debounceTimeout = setTimeout(() => {
//       if (query.trim()) {
//         setIsSearching(true)
//         fetchSearchResults(query).then((data) => {
//           setResults(data as any)
//           setIsSearching(false)
//         })
//       } else {
//         setResults([])
//       }
//     }, 2000) // 2 seconds debounce time

//     return () => clearTimeout(debounceTimeout) // Clean up on component unmount or query change
//   }, [query])

//   // Simulate a search result fetch
//   const fetchSearchResults = async (query: any) => {
//     // Simulate an API call with a delay
//     await new Promise((resolve) => setTimeout(resolve, 500)) // Simulating delay
//     return [`Result for ${query} 1`, `Result for ${query} 2`, `Result for ${query} 3`]
//   }

//   return (
//     <div className="relative">
//       <input
//         type="text"
//         value={query}
//         onChange={(e) => setQuery(e.target.value)}
//         placeholder="Search..."
//         className="w-full p-2 border border-gray-300 rounded"
//       />
//       {results.length > 0 && (
//         <div className="absolute top-full left-0 w-full max-h-48 overflow-y-auto bg-white border border-gray-300 z-10 mt-1">
//           {isSearching ? (
//             <div className="p-2 text-gray-500">Loading...</div>
//           ) : (
//             results.map((result, index) => (
//               <div key={index} className="p-2 hover:bg-gray-100">
//                 {result}
//               </div>
//             ))
//           )}
//         </div>
//       )}
//     </div>
//   )
// }
