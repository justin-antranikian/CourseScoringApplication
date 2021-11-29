import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IrpComponent } from './irp.component';

describe('IrpComponent', () => {
  let component: IrpComponent;
  let fixture: ComponentFixture<IrpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IrpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IrpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
