
import React from 'react'

interface Props {
  params: {
    slug: string[]
  }
}

export default function Page({ params: { slug }}: Props) {
  console.log(slug.length)
  return (
    <div>
      
    </div>
  )
}
