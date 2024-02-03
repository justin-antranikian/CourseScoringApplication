import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-bracket-rank',
  templateUrl: './bracket-rank.component.html',
  imports: [CommonModule],
  styleUrls: []
})
export class BracketRankComponent {

  @Input('rank')
  public rank!: number | null;

  @Input('total')
  public total!: number;

  @Input('indicator')
  public indicator: any;
}
