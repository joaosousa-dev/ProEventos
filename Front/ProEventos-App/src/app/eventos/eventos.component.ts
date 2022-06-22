import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers:[EventoService]
})
export class EventosComponent implements OnInit {

  constructor(private eventoService : EventoService) { }

  public eventos : any = [];
  public eventosFiltrados : any = [];
  exibeImagem = false;
  private _filtroLista : string = '';

  public get filtroLista():string{
    return this._filtroLista;
  }
  public set filtroLista(value:string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos
  }

  filtrarEventos(filtrarPor:string):any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  ngOnInit(): void {
    this.getEventos()
  }

  public getEventos() : any {
    this.eventoService.getEventos().subscribe(
    response => {
      this.eventos = response;
      this.eventosFiltrados = response
    },
    error => console.error(error)
    )
  }

}
