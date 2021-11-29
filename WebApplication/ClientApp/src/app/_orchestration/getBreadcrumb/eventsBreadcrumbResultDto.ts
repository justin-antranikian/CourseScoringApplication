import { DisplayNameWithIdDto } from "src/app/_orchestration/displayNameWithIdDto";
import { BreadcrumbResultDto } from "./breadcrumbResultDto";

export interface EventsBreadcrumbResultDto extends BreadcrumbResultDto {
  raceSeriesDisplayWithId?: DisplayNameWithIdDto | null
  raceDisplayWithId?: DisplayNameWithIdDto | null
  courseDisplayWithId?: DisplayNameWithIdDto | null
  irpDisplayWithId?: DisplayNameWithIdDto | null
}