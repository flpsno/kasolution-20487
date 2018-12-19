import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { ProdutoModel } from './produto.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  token: string = null;
  produtos: any[];
  
  produtoForm: FormGroup;
  apiUrl = environment.apiUrl;
  previewBase64: string = '';
  file: File;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

  ngOnInit() {

    this.token = localStorage.getItem('token');

    this.produtoForm = this.formBuilder.group({
      id: ['0'],
      foto: [null, [Validators.required]],
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
      this.http.get(this.apiUrl + '/produtos', { headers: { 'Authorization': `bearer ${this.token}` } })
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

    // console.log(data);
    // console.log(this.file);
    
    const produto: ProdutoModel = {
      id: data.id,
      foto: this.file,
      nome: data.nome,
      tipoProdutoId: data.tipoProdutoId,
      preco: data.preco
    };

    this.enviarForm(produto);
  }

  enviarForm(produto: ProdutoModel){
    const formData = new FormData();
    // formData.append('id', produto.id.toString());
    Object.keys(produto).forEach(key => formData.append(key, produto[key]));

    this.http.post(this.apiUrl + '/produtos', formData, { headers: { 'Authorization': `bearer ${this.token}` } })
      .subscribe((data: any) => {
        // console.log(data);
        produto.id = data.id;
        this.produtos.push(produto);
      });

      this.limpar();
  }

  limpar() {
    this.produtoForm.reset();
    this.previewBase64 = '';
    this.file = null;
    this.produtoForm.patchValue({ 'id': '0' });
  }

  handleFile(file: File) {

    if (!file) {
      this.previewBase64 = '';
      this.file = null;
      return;
    }

    // console.log(file);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      // console.log(event.target.result);
      this.previewBase64 = event.target.result;
      this.file = file;
    };
    reader.readAsDataURL(file);
  }

}
// fabiano.nalin@gmail.com