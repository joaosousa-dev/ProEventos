import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, MinLengthValidator, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  constructor(private fb:FormBuilder) { }

  form!: FormGroup;
  get f():any{
    return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
  }

  public validation():void{
    this.form = this.fb.group({
      tema:['',[Validators.required,Validators.minLength(4),Validators.maxLength(50)]],
      local : ['',Validators.required],
      dataEvento: ['',Validators.required],
      qtdPessoas:['1',[Validators.required,Validators.max(100000)]],
      telefone:['',[Validators.required]],
      email:['',[Validators.required,Validators.email]],
      imagemURL:['',Validators.required]
    });
  }
  public resetForm():void{
    this.form.reset();
  }

}
