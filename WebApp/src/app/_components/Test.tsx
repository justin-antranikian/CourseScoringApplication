"use client"

import React, { useCallback } from "react"
import { debounce } from "lodash"

export default function Test() {
  const handleInputChange = (event: any) => {
    console.log("Searching for:", event.target.value)
  }

  const debouncedChangeHandler = useCallback(debounce(handleInputChange, 300), [])

  return <input type="text" onChange={debouncedChangeHandler} placeholder="Search..." />
}
