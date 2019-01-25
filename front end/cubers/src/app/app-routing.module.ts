import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CuberInfoComponent} from './cuber-info/cuber-info.component';
import {CuberSummaryComponent} from './cuber-summary/cuber-summary.component';
import {CuberEditComponent} from './cuber-edit/cuber-edit.component';

const routes: Routes = [
  {path: '', component: CuberSummaryComponent},
  {path: 'cuber/:id', component: CuberInfoComponent},
  {path: 'edit', component: CuberEditComponent},
  {path: 'edit/:id', component: CuberEditComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
