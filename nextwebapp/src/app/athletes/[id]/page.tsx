import { config } from "@/config"
import { ArpDto } from "./definitions"
import LocationInfoRankings from "@/app/_components/LocationInfoRankings"
import AtheleteResult from "./AtheleteResult"

export const dynamic = "force-dynamic"

interface Props {
  params: {
    id: string
  }
}

const getData = async (id: string): Promise<ArpDto> => {
  const url = `${config.apiHost}/arpApi/${id}`
  const response = await fetch(url)
  return await response.json()
}

export default async function Page({ params: { id } }: Props) {
  const arp = await getData(id)

  return (
    <div className="flex gap-1">
      <div className="w-1/4">
        <div>
          <img style={{ width: "75%", height: 125 }} src="/Athlete.png" />
        </div>
        <div className="mt-2 text-2xl font-bold">{arp.fullName}</div>
        <div className="text-lg">
          {arp.locationInfoWithRank.city}, {arp.locationInfoWithRank.state}
        </div>
        <div className="mb-3 text-xs">
          {arp.genderAbbreviated} | {arp.age}
        </div>
        <LocationInfoRankings locationInfoWithRank={arp.locationInfoWithRank} />
        <hr className="my-5" />
        <div className="my-3">{arp.firstName}'s training and diet</div>
        <ul className="list-disc pl-5 text-xs">
          {arp.wellnessTrainingAndDiet.map((entry, index) => (
            <li key={index}>{entry.description}</li>
          ))}
        </ul>
        <div className="my-3">{arp.firstName}'s goals</div>
        <ul className="list-disc pl-5 text-xs">
          {arp.wellnessGoals.map((entry, index) => (
            <li key={index}>{entry.description}</li>
          ))}
        </ul>
        <div className="my-3">{arp.firstName}'s inspiration</div>
        {arp.wellnessMotivationalList.map((entry) => {
          return <div className="pl-5 text-xs">{entry.description}</div>
        })}
      </div>
      <div className="w-3/4">
        <div className="mb-12 bold text-2xl text-purple-500">Results</div>
        <table className="my-5 table-auto w-full">
          <thead>
            <tr className="border-b border-black">
              <th className="w-[10%] py-2" scope="col"></th>
              <th className="w-[30%] text-left py-2" scope="col">
                Event Name
              </th>
              <th className="w-[13%] text-left py-2" scope="col">
                Overall
              </th>
              <th className="w-[13%] text-left py-2" scope="col">
                Gender
              </th>
              <th className="w-[14%] text-left py-2" scope="col">
                Division
              </th>
              <th className="w-[20%] text-left py-2" scope="col">
                Total Time
              </th>
            </tr>
          </thead>
          <tbody>
            {arp.results.map((result) => {
              return <AtheleteResult result={result} />
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}
