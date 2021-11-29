import { Component, Input } from '@angular/core';
import { BetweenIntervalTimeIndicator } from 'src/app/_core/enums/betweenIntervalTimeIndicator';

@Component({
  selector: 'app-bracket-rank',
  templateUrl: './bracket-rank.component.html',
  styleUrls: []
})
export class BracketRankComponent {

  @Input('rank')
  public rank: number | null;

  @Input('total')
  public total: number;
    
  @Input('indicator')
  public indicator: BetweenIntervalTimeIndicator;
}
