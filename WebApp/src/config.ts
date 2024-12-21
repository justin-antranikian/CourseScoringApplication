"server only"

export interface Config {
  apiHost: string
}

const apiHost = process.env["API_HOST"]

if (apiHost === null) {
  throw new Error("Env variable: API_HOST must be set")
}

export const config: Config = {
  apiHost: apiHost as string,
}
