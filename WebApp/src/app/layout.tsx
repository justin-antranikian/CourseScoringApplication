import type { Metadata } from "next"
import "./globals.css"
import Link from "next/link"
import NavSearch from "./_components/NavSearch"
import Image from "next/image"

export const metadata: Metadata = {
  title: "Course Scoring",
  description: "Course Scoring custom app",
}

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="en">
      <head>
        <link rel="icon" type="image/png" sizes="16x16" href="/favicon.png" />
      </head>
      <body className="flex flex-col min-h-screen">
        <nav className="p-4 bg-[#24325a]">
          <div className="container mx-auto flex justify-between">
            <div>
              <a href="/">
                <Image src="/CourseScoring9.png" alt={"Course Scoring Logo"} />
              </a>
            </div>
            <div className="flex items-center space-x-4">
              <NavSearch />
              <Link href="/athletes" className="text-white hover:text-gray-300">
                Athletes
              </Link>
              <Link href="/races" className="text-white hover:text-gray-300">
                Races
              </Link>
            </div>
          </div>
        </nav>
        <div className="p-3 flex-grow">{children}</div>
        <div className="p-2 bg-[#24325a] flex justify-center text-white text-xs">
          Copyright: © 2025 Course Scoring. All rights reserved.
        </div>
      </body>
    </html>
  )
}
