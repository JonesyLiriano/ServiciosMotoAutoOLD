import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import * as AOS from 'aos';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ServiciosMotoAuto';

  constructor(private cdref: ChangeDetectorRef)
  {
    
  }
  ngOnInit() {
    AOS.init();
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
}
