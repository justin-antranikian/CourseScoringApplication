import { Component, Input } from '@angular/core';
import { PaceWithTime } from '../../_core/paceWithTime';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-interval-time',
  templateUrl: './interval-time.component.html',
  imports: [CommonModule],
  styleUrls: []
})
export class IntervalTimeComponent {

  @Input('paceTime')
  public paceTime!: PaceWithTime
}
