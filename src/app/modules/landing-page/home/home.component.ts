import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BRANDS } from 'src/app/core/mocks/brands';
import { Brand } from 'src/app/core/models/brand';
import { phoneNumberValidator } from 'src/app/core/validators/phone-validator';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  marker: any = {
    position: {
      lat: 18.472782344068744,
      lng: -69.95290504335084,
    },
    label: {
      color: 'red',
      text: 'Servicios MotoAuto',
    },
    title: 'Servicios MotoAuto',
    options: { animation: google.maps.Animation.BOUNCE }
  };
  centerMap = {
    lat: 18.472782344068744,
    lng: -69.95290504335084, 
  }
  optionsMap: google.maps.MapOptions = {
    mapTypeId: 'roadmap',
    zoomControl: true,
    scrollwheel: false,
    disableDoubleClickZoom: false,
    maxZoom: 100,
    minZoom: 8,
  }
  brandsList: Brand[] = [];
  showBrandSpinner: boolean | undefined;
  showContactSpinner: boolean | undefined;
  contactForm = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, phoneNumberValidator]],
    message: ['', Validators.required]
  });
  get name() {
    return this.contactForm.get('name');
  }
  get message() {
    return this.contactForm.get('message');
  }
  get phone() {
    return this.contactForm.get('phone');
  }
  get email() {
    return this.contactForm.get('email');
  }
  images = [
    './../../../assets/carrousel/moto/1.jpg',
     './../../../assets/carrousel/bici/1.jpg',     
     './../../../assets/carrousel/moto/2.jpg',
     './../../../assets/carrousel/bici/2.jpg',
     './../../../assets/carrousel/moto/3.jpg',     
     './../../../assets/carrousel/bici/3.jpg',
     './../../../assets/carrousel/moto/4.jpg',
     './../../../assets/carrousel/bici/4.jpg',
     './../../../assets/carrousel/moto/5.jpg',
    './../../../assets/carrousel/bici/5.jpg',
  ];
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private router: Router) {      
    }

  ngOnInit(): void {
    this.getBrands();
  }

  getBrands() {
    this.showBrandSpinner = true;
    this.brandsList = BRANDS;
    this.showBrandSpinner = false;
  }

  navegateBrandInfo(brand: Brand){
    this.router.navigate(['/brand',JSON.stringify(brand)]​);
  }

  onClickSubmit(formDirective: FormGroupDirective) {
    if (this.contactForm.valid) {
      //this.showContactSpinner = true;
      // this.httpRequestsService.post('https://us-central1-okonomi-bfa7f.cloudfunctions.net/contactEmail',
      //   this.contactForm.value).subscribe((res: { message: string; }) => {
      //     if (res.message === 'success') {
      //       this.toastr.success('En breve nuestros representantes se estaran contactando con usted.', 'Solicitud Enviada!', {
      //         timeOut: 3000,
      //         positionClass: 'toast-top-right'
      //       });
      //     } else {
      //       this.toastr.error('Favor intente de nuevo', 'Ha ocurrido un error.', {
      //         timeOut: 3000,
      //         positionClass: 'toast-top-right'
      //       });
      //     }
      //     formDirective.resetForm();
      //     this.contactForm.reset();
      //     this.showContactSpinner = false;
      //   }, (err: any) => {
      //     console.log(err);
      //     this.toastr.error('Su solicitud no ha podido ser enviada.', 'Ha ocurrido un error', {
      //       timeOut: 3000,
      //       positionClass: 'toast-top-right'
      //     });
      //     formDirective.resetForm();
      //     this.contactForm.reset();
      //     this.showContactSpinner = false;
      //   });
    } else {
      this.toastr.warning('Verifique los campos nuevamente.', 'Favor intente de nuevo', {
        timeOut: 3000,
        positionClass: 'toast-top-right'
      });
    }
  }
}
