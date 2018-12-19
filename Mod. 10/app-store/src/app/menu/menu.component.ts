import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Component({
    templateUrl: './menu.component.html',
    selector: 'app-menu'
})
export class MenuComponent implements OnInit{
    @Input() token: string;
    @Output() tokenReturn = new EventEmitter();
    loginForm: FormGroup;
    apiUrl = environment.apiUrl;

    constructor(private formBuilder: FormBuilder, private http: HttpClient) { }
    
    ngOnInit() {
        this.loginForm = this.formBuilder.group({
          email: ['admin@fansoft.com.br', [Validators.email, Validators.required]],
          senha: ['123456', [Validators.required]]
        });
      }

      login() {
        const data = this.loginForm.getRawValue();
        console.log(data);
        this.http.post(this.apiUrl + '/security', data)
          .subscribe((resp: any) => {
            console.log(resp);
            this.token = resp.token;
            window.localStorage.setItem('token', resp.token);
            this.tokenReturn.emit(this.token);
          });
    
      }

      logOff() {
        localStorage.clear();
        this.token = null;
        this.tokenReturn.emit(null);
      }
    
}
