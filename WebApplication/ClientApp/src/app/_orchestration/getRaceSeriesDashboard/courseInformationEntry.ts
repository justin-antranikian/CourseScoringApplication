
export enum CourseInformationType {
  Description,
  Promotional,
  HowToPrepare
}

export interface CourseInformationEntry {
  id: number
  courseId: number
  courseInformationType: CourseInformationType
  description: string
}