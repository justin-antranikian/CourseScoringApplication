import { HttpParams } from "@angular/common/http"

export const getHttpParams = (fromObject: any) => {
  const httpParams = new HttpParams({ fromObject })
  return { params: httpParams }
}
