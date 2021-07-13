import { Component, OnInit } from '@angular/core';
import {ActivatedRoute } from "@angular/router";
import { Brand } from 'src/app/core/models/brand';
@Component({
  selector: 'app-brand-info',
  templateUrl: './brand-info.component.html',
  styleUrls: ['./brand-info.component.css']
})
export class BrandInfoComponent implements OnInit {
brand: Brand | undefined;
  constructor(private activatedRoute: ActivatedRoute) {
    this.brand = JSON.parse(activatedRoute.snapshot.params["info"]);
  }

  ngOnInit(): void {
  }

}
