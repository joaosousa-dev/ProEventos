import { Component, OnInit } from '@angular/core';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers:[EventoService]
})
export class EventosComponent implements OnInit {

  constructor(private eventoService : EventoService) { }

  public eventos : Evento[] = [];
  public eventosFiltrados : Evento[] = [];
  exibeImagem = false;
  private filtroListado : string = '';

  public get filtroLista():string{
    return this.filtroListado;
  }
  public set filtroLista(value:string){
    this.filtroListado = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos
  }

  public filtrarEventos(filtrarPor:string):Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public ngOnInit(): void {
    this.getEventos();
  }

  public getEventos() : any {
    this.eventoService.getEventos().subscribe({
    next : (eventos : Evento[]) => {
      this.eventos = eventos;
      this.eventosFiltrados = this.eventos;
    },
    error : (error:any) => console.error(error)
  });
  }

}
