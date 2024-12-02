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
      <body className={`antialiased`}>
        <nav className="bg-purple-600 p-4">
          <div className="container mx-auto flex justify-between">
            <div className="text-white font-bold text-xl">Course Scoring</div>
            <div className="space-x-4">
              <Link href="/athletes" className="text-white hover:text-gray-300">
                Athletes
              </Link>
              <Link href="/events" className="text-white hover:text-gray-300">
                Events
              </Link>
            </div>
          </div>
        </nav>
        <div className="p-3">{children}</div>
      </body>
    </html>
  )
}
