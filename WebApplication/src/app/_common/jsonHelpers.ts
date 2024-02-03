import { pickBy } from 'lodash';

/**
* Primarly used before sending an http request so undefined or null values don't get put in the url.
* example { A: 'A', B: null }  =>  { A: 'A' }
* @params object of type: any
* @returns new object of type: any
*/
export const removeUndefinedKeyValues = (jsonObject: any): any => {
  return pickBy(jsonObject, (value: any) => value)
}