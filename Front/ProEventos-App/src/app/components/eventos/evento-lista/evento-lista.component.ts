import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  constructor(
    private eventoService : EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner:NgxSpinnerService,
    private router : Router
    ) { }
  public ngOnInit(): void {
    this.carregarEventos();
  }

  modalRef?: BsModalRef;
  message?: string;



  public eventos : Evento[] = [];
  exibeImagem = false;
  public eventosFiltrados : Evento[] = [];
  private filtroListado : string = '';
  public eventoId = 0;

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
  public carregarEventos() : any {
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
  openModal(event : any,template: TemplateRef<any>,eventoId:number):void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next:(result:any) =>{
          this.toastr.success('Evento Deletado','Sucesso!')
          this.spinner.hide();
          this.carregarEventos();
      },
      error:(error:any) =>{
        this.toastr.error(`Erro ao deletar evento ${this.eventoId}! Erro:${error.message}`,'Erro');
        this.spinner.hide()},
      complete:() => {
        this.spinner.hide();
      }


    })
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }

  detalheEvento(id:number):void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
