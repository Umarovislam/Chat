import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './views/pages/user/auth/service/auth-guard.service';


const routes: Routes = [
  {path: '', loadChildren: () => import('./views/pages/home/home.module').then(m => m.HomeModule)},
  { path: 'auth', loadChildren: () => import('./views/pages/user/auth/auth.module').then(m => m.AuthModule) },
  { path: 'home', loadChildren: () => import('./views/pages/home/home.module').then(m => m.HomeModule) },
  { 
    path: 'feed', loadChildren: () => import('./views/pages/user/feed/feed.module').then(m => m.FeedModule),
   // canActivate: [AuthGuardService]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
