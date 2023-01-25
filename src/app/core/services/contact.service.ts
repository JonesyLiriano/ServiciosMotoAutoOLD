import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact';
import { ApiService } from './api.service';
const routes = {
  root: 'mail',
  subscribeMail: () => `${routes.root}/subscribeMail`,
  contactMail: () => `${routes.root}/contactMail`
};
@Injectable({
  providedIn: 'root'
})
export class ContactService {
  constructor(
    private apiService: ApiService) { }


  sendContactEmail(contact: Contact): Observable<any> {
    console.log(contact);
    return this.apiService.post(routes.contactMail(), contact);
  }

  sendSubscribeEmail(contact: Contact): Observable<any> {
    console.log(contact);
    return this.apiService.post(routes.subscribeMail(), contact);
  }

}
