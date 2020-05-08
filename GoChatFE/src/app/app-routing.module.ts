import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [{ path: 'auth', loadChildren: () => import('./views/pages/user/auth/auth.module').then(m => m.AuthModule) }, { path: 'home', loadChildren: () => import('./views/pages/home/home.module').then(m => m.HomeModule) }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
