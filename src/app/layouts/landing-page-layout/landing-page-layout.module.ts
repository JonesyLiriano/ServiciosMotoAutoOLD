import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingPageLayoutRoutingModule } from './landing-page-layout-routing.module';
import { LandingPageLayoutComponent } from './landing-page-layout.component';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';
import { FooterComponent } from 'src/app/components/footer/footer.component';
import { AngularMaterialModule } from 'src/app/shared/modules/angular-material/angular-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [LandingPageLayoutComponent, NavbarComponent, FooterComponent],
  imports: [
    CommonModule,
    LandingPageLayoutRoutingModule,
    AngularMaterialModule,
    FlexLayoutModule,    
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class LandingPageLayoutModule { }
