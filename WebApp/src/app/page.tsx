import { Sheet, SheetContent, SheetDescription, SheetHeader, SheetTitle, SheetTrigger } from "@/components/ui/sheet"

export const dynamic = "force-dynamic"

export default function Home() {
  return (
    <>
      <Sheet>
        <SheetTrigger>Open</SheetTrigger>
        <SheetContent className="w-[400px] sm:w-[540px]" side={"left"}>
          <SheetHeader>
            <SheetTitle>Are you absolutely sure?</SheetTitle>
            <SheetDescription>
              This action cannot be undone. This will permanently delete your account and remove your data from our
              servers.
            </SheetDescription>
          </SheetHeader>
        </SheetContent>
      </Sheet>
    </>
  )
}
