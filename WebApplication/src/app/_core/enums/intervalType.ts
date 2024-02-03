
export enum IntervalType {
  Swim,
  Bike,
  Run,
  Transition,
  FullCourse,
  MountainBike,
  CrossCountrySki
}

export class IntervalTypeToImageUrl {

  /**
  * A helper method that takes a IntervalType and returns the corresponding image path.
  * @param intervalName
  * @returns image path
  */
  public static getImageUrl = (intervalType: IntervalType): string => {
    switch (intervalType) {
      case IntervalType.Swim: {
        return '/assets/img/SwimmingClipArt.png'
      }
      case IntervalType.Bike: {
        return '/assets/img/BikingClipArt.jpg'
      }
      case IntervalType.Run: {
        return '/assets/img/RunningClipArt.png'
      }
      case IntervalType.Transition: {
        return '/assets/img/TransitionClipArt.jpg'
      }
      case IntervalType.FullCourse: {
        return '/assets/img/FullCourse.jpg'
      }
      case IntervalType.MountainBike: {
        return '/assets/img/MountainBike.jpg'
      }
      case IntervalType.CrossCountrySki: {
        return '/assets/img/CrossCountrySkiing.png'
      }
    }
  }
}