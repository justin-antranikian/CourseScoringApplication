import { Component, Input, OnInit } from "@angular/core";
import { ComponentBaseWithRoutes } from "../_common/componentBaseWithRoutes";
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: 'app-irp-pizza-tracker',
  templateUrl: './irp-pizza-tracker.component.html',
  imports: [CommonModule, NgbModule],
  styleUrls: ['./irp.component.css']
})
export class IrpPizzaTrackerComponent extends ComponentBaseWithRoutes {

  @Input('interval')
  public interval: any
}