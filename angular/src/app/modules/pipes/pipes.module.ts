import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormatTimePipe } from './formt-time.pipe';

@NgModule({
  declarations: [FormatTimePipe],
  exports: [FormatTimePipe],
  imports: [CommonModule],
})
export class PipesModule {}
