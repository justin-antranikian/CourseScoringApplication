import { CompareAthletesAthleteInfoDto } from "../athletes/compare/definitions"
import { ApiFetch } from "./api"

export default (apiFetch: ApiFetch) => {
  return {
    compare: async (
      athleteIds: string[],
    ): Promise<CompareAthletesAthleteInfoDto[]> => {
      const requestInit: RequestInit = {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ athleteIds }),
      }

      const response = await apiFetch("compareAthletesApi", requestInit)
      return await response.json()
    },
  }
}
