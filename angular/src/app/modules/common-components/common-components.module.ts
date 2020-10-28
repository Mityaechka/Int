import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PipesModule } from './../pipes/pipes.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountdownComponent } from './components/countdown/countdown.component';

@NgModule({
  declarations: [CountdownComponent],
  imports: [
    CommonModule,
    PipesModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
  ],
  exports: [CountdownComponent],
})
export class CommonComponentsModule {}
