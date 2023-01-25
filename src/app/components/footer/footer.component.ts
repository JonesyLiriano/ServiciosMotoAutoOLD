import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, FormGroupDirective, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Contact } from 'src/app/core/models/contact';
import { ContactService } from 'src/app/core/services/contact.service';
import { phoneNumberValidator } from 'src/app/core/validators/phone-validator';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  subscriberForm = this.fb.group({
    email: ['', [ Validators.email]],
    phone: ['', [ phoneNumberValidator]]
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

  showContactSpinner: boolean;
  constructor(private fb: UntypedFormBuilder, private toastr: ToastrService, private contactService: ContactService) { }

  ngOnInit(): void {
  }
  clearEmail()
  {
    this.email?.setValue('');
    this.email?.setErrors(null);
  }
  clearPhone()
  {
    this.phone?.setValue('');
    this.phone?.setErrors(null);
  }
  onClickSubmit(formDirective: FormGroupDirective) {
    if (this.subscriberForm.valid) {
      this.showContactSpinner = true;
      let contact = new Contact();
      Object.assign(contact, this.subscriberForm.value);
      this.contactService.sendSubscribeEmail(this.subscriberForm.value).subscribe(res => {
        if (res.status == 200) {
          this.toastr.success('En breve nuestros representantes se estaran contactando con usted.', 'Solicitud Enviada!', {
            timeOut: 3000,
            positionClass: 'toast-top-right'
          });
        } else {
          this.toastr.error('Favor intente de nuevo', 'Ha ocurrido un error.', {
            timeOut: 3000,
            positionClass: 'toast-top-right'
          });
        }
        formDirective.resetForm();
        this.subscriberForm.reset();
        this.showContactSpinner = false;
      }, (err: any) => {
        console.log(err);
        this.toastr.error('Su solicitud no ha podido ser enviada.', 'Ha ocurrido un error', {
          timeOut: 3000,
          positionClass: 'toast-top-right'
        });
        formDirective.resetForm();
        this.subscriberForm.reset();
        this.showContactSpinner = false;
      });
    } else {
      this.toastr.warning('Verifique los campos nuevamente.', 'Favor intente de nuevo', {
        timeOut: 3000,
        positionClass: 'toast-top-right'
      });
    }
  }
}
