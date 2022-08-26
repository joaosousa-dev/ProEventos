import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComparerFields } from 'src/app/helpers/ComparerFields';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  form!:FormGroup

  get f():any{
    return this.form.controls;
  }

  constructor(public fb:FormBuilder) { }

  ngOnInit() {
    this.validation()
  }

  private validation():void{
    const formOptions : AbstractControlOptions={
      validators:ComparerFields.mustMatch('senha','confirmeSenha')
    }
    this.form = this.fb.group({
      primeiroNome: ['',[Validators.required]],
      ultimoNome: ['',[Validators.required]],
      email: ['',[Validators.required,Validators.email]],
      senha: ['',[Validators.required]],
      confirmeSenha:['',[Validators.required]],
      telefone: ['',[Validators.required]],
      funcao: ['',[Validators.required]],
      descricao:['',[Validators.required,Validators.maxLength(150)]],
      titulo:['',[Validators.required,Validators.min(1),Validators.max(8)]]

    },formOptions)
  }

}
