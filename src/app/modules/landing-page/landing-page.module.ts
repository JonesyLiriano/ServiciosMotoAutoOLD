import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingPageRoutingModule } from './landing-page-routing.module';
import { AboutUsComponent } from './about-us/about-us.component';
import { BrandInfoComponent } from './brand-info/brand-info.component';
import { HomeComponent } from './home/home.component';
import { AngularMaterialModule } from 'src/app/shared/modules/angular-material/angular-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DragScrollModule } from 'ngx-drag-scroll';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GoogleMapsModule } from '@angular/google-maps'

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  declarations: [
    AboutUsComponent,
    BrandInfoComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    LandingPageRoutingModule,    
    AngularMaterialModule,
    FlexLayoutModule,
    DragScrollModule,
    FormsModule,
    ReactiveFormsModule,
    GoogleMapsModule,
    NgbModule
  ]
})
export class LandingPageModule { }
