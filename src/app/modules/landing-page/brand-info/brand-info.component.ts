import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { NgbCarousel } from '@ng-bootstrap/ng-bootstrap';
import { Brand } from 'src/app/core/models/brand';
@Component({
  selector: 'app-brand-info',
  templateUrl: './brand-info.component.html',
  styleUrls: ['./brand-info.component.css']
})
export class BrandInfoComponent implements OnInit {
brand: Brand | undefined;
@ViewChild('myCarousel') myCarousel: NgbCarousel;
  constructor(private activatedRoute: ActivatedRoute) {
    this.brand = JSON.parse(activatedRoute.snapshot.params["info"]);
  }

  ngOnInit(): void {
    window.scroll(0,0);
  }

  imgChange(index: any) {
    this.myCarousel.select('ngb-slide-' + index);
  }
}
