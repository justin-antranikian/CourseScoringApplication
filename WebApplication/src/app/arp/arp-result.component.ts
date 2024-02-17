import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { BracketRankComponent } from '../_subComponents/bracket-rank/bracket-rank.component';
import { RouterModule } from '@angular/router';
import { IntervalTimeComponent } from '../_subComponents/interval-time/interval-time.component';

@Component({
  standalone: true,
  selector: '[app-arp-result]',
  templateUrl: './arp-result.component.html',
  imports: [CommonModule, RouterModule, BracketRankComponent, IntervalTimeComponent],
  styleUrls: []
})
export class ArpResultComponent extends ComponentBaseWithRoutes {

  @Input('arpResultDto')
  public arpResultDto: any
}
