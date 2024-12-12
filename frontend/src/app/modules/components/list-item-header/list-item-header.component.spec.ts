import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListItemHeaderComponent } from './list-item-header.component';

describe('ListItemHeaderComponent', () => {
  let component: ListItemHeaderComponent;
  let fixture: ComponentFixture<ListItemHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListItemHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListItemHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
