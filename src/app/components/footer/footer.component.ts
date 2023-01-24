import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, FormGroupDirective, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { phoneNumberValidator } from 'src/app/core/validators/phone-validator';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  subscriberForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, phoneNumberValidator]]
  });
  get phone() {
    return this.subscriberForm.get('phone');
  }
  get email() {
    return this.subscriberForm.get('email');
  }
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
  constructor(private fb: UntypedFormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  onClickSubmit(formDirective: FormGroupDirective) {
    if (this.subscriberForm.valid) {
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
