
import { ChartOptions, ChartType } from "chart.js"
import { Color, Label } from "ng2-charts"

export class ChartOptionsForIrp {

  public config: ChartOptions = {
    legend: { display: false },
  };

  public type: ChartType = 'doughnut';
  public labels: Label[] = ['Better Than', 'Worse Than'];
  public colors: Color[] = [{ backgroundColor: ['#28a745', '#e5e5e5'] }]
}