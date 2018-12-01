import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  token = '';
  produtos:any[] = [{id:1, nome: 'prod1', preco: 10}, {id:2, nome: 'prod2', preco: 20}];
  login() {
    const data = this.meuForm.getRawValue();
    console.log(data);
    this.http.post('http://localhost:5000/api/v1/security', data)
      .subscribe( (resp: any) => {
        console.log(resp);
        this.token = resp.token;
      });

  }

  meuForm: FormGroup;
  constructor(formBuilder: FormBuilder, private http: HttpClient) {
    this.meuForm = formBuilder.group({
      email: [null, [Validators.email, Validators.required]],
      senha: [null, [Validators.required]]
    });
  }

}
