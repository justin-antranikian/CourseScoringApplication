import React from "react"
import Image from "next/image"

export default function AthleteImage({ width = "100%", height = 125 }: { width?: string; height?: number }) {
  return <Image style={{ width, height }} src="/Athlete.png" alt={"Athlete Image"} />
}
