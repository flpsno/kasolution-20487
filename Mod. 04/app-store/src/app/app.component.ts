import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';


const APIURL = 'https://nalin-api.azurewebsites.net/api/v1';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  token: string = null;
  produtos: any[];
  
  produtoForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

  ngOnInit() {

    this.token = localStorage.getItem('token');

    this.produtoForm = this.formBuilder.group({
      id: ['0'],
      nome: [null, [Validators.required]],
      tipoProdutoId: [null, [Validators.required]],
      preco: [null, [Validators.required]]
    });

    this.obterProdutos();
  }

  refreshToken(token) {
    this.token = token;
    this.obterProdutos();
  }

  obterProdutos() {
    const _token = localStorage.getItem('token');
    if (!!_token) {
      this.http.get(APIURL + '/produtos', { headers: { 'Authorization': `bearer ${this.token}` } })
        .subscribe(
          (data: any) => this.produtos = data,
          error => console.log(error)
        );
    }else{
      this.produtos = [];
    }
  }

  salvar() {
    const data = this.produtoForm.getRawValue();

    this.http.post(APIURL + '/produtos', data, { headers: { 'Authorization': `bearer ${this.token}` } })
      .subscribe((resp: any) => { });
  }

  limpar() {
    this.produtoForm.reset();
    this.produtoForm.patchValue({ 'id': '0' });
  }

}
// fabiano.nalin@gmail.com