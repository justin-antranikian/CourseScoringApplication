import { Dialog } from "@/components/ui/dialog"
import React, { useState } from "react"
import CardContainer from "./CardContainer"
import { EventSearchResultDto } from "../definitions"

export default function DialogWrapper({
  events,
  apiHost,
}: {
  events: EventSearchResultDto[]
  apiHost: string
}) {
  const [dialogOpen, setDialogOpen] = useState(false)

  return (
    <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
      <CardContainer events={[]} apiHost={""} />
    </Dialog>
  )
}
