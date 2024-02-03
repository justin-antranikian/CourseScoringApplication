import { IntervalType, IntervalTypeToImageUrl } from "../_core/enums/intervalType";

/**
 * intervalName comes from the server, while intervalTypeImageUrl is set on the client.
 */
export interface IIntervalType {
  intervalType: IntervalType
  /** DOES NOT come from the server. Client is responsible for populating */
  intervalTypeImageUrl: string | undefined
}

/**
* Sets the intervalTypeImageUrl. Keeps the rest of the values.
*/
export const mapIntervalTypeToImageUrl = <T extends IIntervalType>(value: T): T => ({
  ...value,
  intervalTypeImageUrl: IntervalTypeToImageUrl.getImageUrl(value.intervalType)
})