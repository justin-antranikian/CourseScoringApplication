import { Component, Input } from '@angular/core';
import { PaceWithTime } from '../../_core/enums/paceWithTime';

@Component({
  selector: 'app-interval-time',
  templateUrl: './interval-time.component.html',
  styleUrls: []
})
export class IntervalTimeComponent {

  @Input('paceTime')
  public paceTime: PaceWithTime
}
