import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingPageLayoutComponent } from './landing-page-layout.component';

const routes: Routes = [
  {
    path: '', 
    component: LandingPageLayoutComponent,
    loadChildren: () => import('../../modules/landing-page/landing-page.module').then(m => m.LandingPageModule),
    data: {depth: 2}
  }
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LandingPageLayoutRoutingModule { }
