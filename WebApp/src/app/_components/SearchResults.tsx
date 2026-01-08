import { ReactNode } from "react"
import { twMerge } from "tailwind-merge"

export default function SearchResults({
  searchTerm,
  className = "w-full",
  inputComponent,
  children,
}: {
  searchTerm: string
  className?: string
  inputComponent: ReactNode
  children: ReactNode
}) {
  return (
    <div className="relative group">
      {inputComponent}
      {searchTerm === "" ? null : (
        <div
          className={twMerge(
            "absolute z-50 p-2 bg-white border border-gray-300 rounded shadow-lg invisible group-hover:visible max-h-[400px] overflow-y-auto",
            className,
          )}
        >
          {children}
        </div>
      )}
    </div>
  )
}

export const NoResults = ({ searchTerm }: { searchTerm: string }) => (
  <div>
    There were no results found for: <strong>{searchTerm}</strong>
  </div>
)
