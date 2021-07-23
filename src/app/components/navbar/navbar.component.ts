import { ViewportScroller } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  menuItems = [
    {
      label: 'Marcas',
      icon: '',
      url: '#brands',
      raised: false,
      color: '',
      class: ''
    },
    {
      label: 'Nosotros',
      icon: '',
      url: '/about-us',
      raised: false,
      color: '',
      class: 'item-class'
    },
    {
      label: 'Log in',
      icon: 'fas fa-sign-in-alt',
      url: '',
      raised: true,
      color: '',
      class: ''
    },
    {
      label: 'Registrarse',
      icon: '',
      url: '',
      raised: true,
      color: 'accent',
      class: ''
    }
  ];
  constructor() { }

  ngOnInit(): void {}

}
