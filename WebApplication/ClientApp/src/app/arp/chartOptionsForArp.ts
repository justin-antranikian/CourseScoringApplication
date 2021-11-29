import { ChartType, ChartDataSets } from "chart.js"
import { Color, Label } from "ng2-charts"

export class ChartOptionsForArp {

  private static readonly allEventsColor: string = '#28a745'
  private static readonly individualEventColor: string = '#ffc107'

  public readonly type: ChartType = 'bar'
  public readonly legend = true
  public readonly plugins = []
  public readonly colors: Color[]

  public labels: Label[]
  public data: ChartDataSets[]

  constructor() {
    const individualEventColors = new Array<string>(6).fill(ChartOptionsForArp.individualEventColor)
    const colors = [ChartOptionsForArp.allEventsColor, ...individualEventColors]
    this.colors = [{ backgroundColor: colors }]
  }

  public setLabels = (labels: string[]) => {
    this.labels = labels
  }

  public setData = (totalDistances: number[]) => {
    const dataSet = { data: totalDistances, label: 'Total Distance' }
    this.data = [dataSet]
  }
}