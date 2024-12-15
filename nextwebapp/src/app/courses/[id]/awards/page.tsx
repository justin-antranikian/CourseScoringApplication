import { apiCaller } from "@/app/_api/api"
import { AwardWinnerDto } from "@/app/_api/courses"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import React from "react"

interface Props {
  params: {
    id: string
  }
}
const api = apiCaller()

export default async function Page({ params: { id } }: Props) {
  const awards = await api.courses.awards(id)

  return (
    <>
      <div className="mb-8 text-purple-500 bold text-2xl">Awards</div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Bracket Name</TableHead>
            <TableHead>First Place</TableHead>
            <TableHead>Second Place</TableHead>
            <TableHead>Third Place</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {awards.map((awardPodium) => (
            <TableRow key={awardPodium.bracketName}>
              <TableCell>
                <strong>{awardPodium.bracketName}</strong>
              </TableCell>
              <TableCell>
                <AthleteTemplate athlete={awardPodium.firstPlaceAthlete} />
              </TableCell>
              <TableCell>
                <AthleteTemplate athlete={awardPodium.secondPlaceAthlete} />
              </TableCell>
              <TableCell>
                <AthleteTemplate athlete={awardPodium.thirdPlaceAthlete} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  )
}

const AthleteTemplate = ({ athlete }: { athlete: AwardWinnerDto | null }) => {
  if (!athlete) {
    return <div>--</div>
  }

  return (
    <div>
      <div>
        <a href={`/athletes/${athlete.athleteId}`}>{athlete.fullName}</a>
      </div>
      <div>
        <span className="mr-1">
          <strong>{athlete.paceWithTime.timeFormatted}</strong>
        </span>
        {athlete.paceWithTime.hasPace && (
          <span>
            ({athlete.paceWithTime.paceValue} {athlete.paceWithTime.paceLabel})
          </span>
        )}
      </div>
      <div>
        <a href={`/results/${athlete.athleteCourseId}`} className="font-12" title="view result">
          View
        </a>
      </div>
    </div>
  )
}
