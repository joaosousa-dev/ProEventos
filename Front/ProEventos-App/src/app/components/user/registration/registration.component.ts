import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComparerFields } from 'src/app/helpers/ComparerFields';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  form!:FormGroup;

  constructor(public fb:FormBuilder) { }

  get f():any{
    return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
  }

  private validation():void{
    const formOptions : AbstractControlOptions={
      validators:ComparerFields.mustMatch('senha','confirmeSenha')
    }
    this.form = this.fb.group({
      primeiroNome: ['',[Validators.required]],
      ultimoNome: ['',[Validators.required]],
      email: ['',[Validators.required,Validators.email]],
      username: ['',[Validators.required]],
      senha: ['',[Validators.required]],
      confirmeSenha:['',[Validators.required]]
    },formOptions)
  }
}
