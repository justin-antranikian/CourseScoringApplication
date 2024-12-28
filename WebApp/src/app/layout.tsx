import type { Metadata } from "next"
import "./globals.css"
import Link from "next/link"

export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
}

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="en">
      <body className="antialiased flex flex-col min-h-screen">
        <nav className="p-4 bg-[#24325a]">
          <div className="container mx-auto flex justify-between items-center">
            <div className="text-white font-bold text-xl">
              <span>
                <a href="/">
                  <img src="/CourseScoring9.png" />
                </a>
              </span>
            </div>
            <div className="space-x-4">
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
        <div className="p-2 bg-[#24325a] flex items-center justify-center text-white text-xs">
          Copyright: © 2025 Course Scorting. All rights reserved.
        </div>
      </body>
    </html>
  )
}
