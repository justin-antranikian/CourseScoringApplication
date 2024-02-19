import { Component, Input } from "@angular/core";
import { BracketRankComponent } from "../_subComponents/bracket-rank/bracket-rank.component";
import { IntervalTimeComponent } from "../_subComponents/interval-time/interval-time.component";

@Component({
  standalone: true,
  selector: '[app-irp-interval-result]',
  imports: [BracketRankComponent, IntervalTimeComponent],
  templateUrl: './irp-interval-result.component.html',
  styleUrls: ['./irp.component.css']
})
export class IrpIntervalResultComponent {
  @Input('irpResultByInterval')
  public irpResultByInterval: any
}