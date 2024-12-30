import React from "react"

export default function AthleteImage({ width = "100%", height = 125 }: { width?: string; height?: number }) {
  return <img style={{ width, height }} src="/Athlete.png" />
}
