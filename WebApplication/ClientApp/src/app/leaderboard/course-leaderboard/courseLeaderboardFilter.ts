import { removeUndefinedKeyValues } from "../../_common/jsonHelpers"

export class CourseLeaderboardFilter {
  public courseId: number
  public bracketId: number
  public intervalId: number | undefined

  constructor(courseId: number, bracketId: number, intervalId: number | undefined) {
    this.courseId = courseId
    this.bracketId = bracketId
    this.intervalId = intervalId
  }

  public getAsParams() {

    const params = {
      bracketId: this.bracketId,
      intervalId: this.intervalId
    }

    return removeUndefinedKeyValues(params)
  }
}
