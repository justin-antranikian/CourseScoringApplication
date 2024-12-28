export interface SlugEntry {
  slug: string
  name: string
}

const formatName = (slugPart: string): string => {
  return slugPart.replace(/-/g, " ").replace(/\b\w/g, (char) => char.toUpperCase())
}

export const getSlugEntries = (slug: string[]): SlugEntry[] => {
  return slug.map((slugPart, index) => {
    const accumulatedSlug = slug.slice(0, index + 1)
    return {
      slug: accumulatedSlug.join("/"),
      name: formatName(slugPart),
    }
  })
}
