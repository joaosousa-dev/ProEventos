import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers:[EventoService]
})
export class EventosComponent implements OnInit {

  constructor(
    private eventoService : EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner:NgxSpinnerService
    ) { }
  public ngOnInit(): void {
    this.getEventos();
  }

  modalRef?: BsModalRef;
  message?: string;



  public eventos : Evento[] = [];
  exibeImagem = false;
  public eventosFiltrados : Evento[] = [];
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
  public getEventos() : any {
    this.spinner.show();
    this.eventoService.getEventos().subscribe({
    next : (eventos : Evento[]) => {
      this.eventos = eventos;
      this.eventosFiltrados = this.eventos;
    },
    error : (error:any) => {
      this.toastr.error(`Erro ao carregar eventos! Erro:${error.message}`,'Erro');
      this.spinner.hide()
    },
    complete: () => this.spinner.hide()
  })
  }
  openModal(template: TemplateRef<any>):void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef?.hide();
    this.toastr.success('Evento Deletado','Sucesso!')
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }

}
