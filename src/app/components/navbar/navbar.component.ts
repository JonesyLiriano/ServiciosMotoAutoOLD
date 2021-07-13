import { Component, OnInit } from '@angular/core';

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
      url: '#brands'
    },
    {
      label: 'Nosotros',
      icon: '',
      url: '/about-us'
    },
    {
      label: 'Log in',
      icon: '',
      url: ''
    },
    {
      label: 'Registrarse',
      icon: 'fas fa-sign-in-alt',
      url: ''
    }
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
