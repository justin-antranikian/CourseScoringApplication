"use client"

import { useEffect, useState } from "react"
import { Input } from "@/components/ui/input"

export default function HomeContent() {
  const [customValue, setCustomValue] = useState("")

  useEffect(() => {
    console.log(customValue)
  }, [customValue])

  return (
    <div className="relative group w-80">
      <Input placeholder="Enter a value" value={customValue} onChange={(e) => setCustomValue(e.target.value)} />
      <div className="absolute top-full left-0 z-50 w-full p-2 bg-white border border-gray-300 rounded shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200">
        {customValue === "" ? "Please enter the search term" : "Content"}
      </div>
    </div>
  )
}
