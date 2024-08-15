import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileInfosFormComponent } from './profile-infos-form.component';

describe('ProfilesInfosFormComponent', () => {
  let component: ProfileInfosFormComponent;
  let fixture: ComponentFixture<ProfileInfosFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileInfosFormComponent]
    });
    fixture = TestBed.createComponent(ProfileInfosFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
