import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from './about-us/about-us.component';
import { BrandInfoComponent } from './brand-info/brand-info.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: {depth: 2} },
  { path: 'about-us', component: AboutUsComponent, data: {depth: 2} },
  { path: 'brand/:info', component: BrandInfoComponent, data: {depth: 3} }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LandingPageRoutingModule { }
