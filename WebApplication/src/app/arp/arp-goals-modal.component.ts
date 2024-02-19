import { Component, inject } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';

@Component({
  standalone: true,
  templateUrl: './arp-goals-modal.component.html',
  imports: [CommonModule, NgbModule, RouterModule],
  styleUrls: []
})
export class ArpGoalsModalComponent extends ComponentBaseWithRoutes {
  public modal = inject(NgbActiveModal)
  public selectedGoal: any
}
