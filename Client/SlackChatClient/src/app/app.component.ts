import { Component, AfterViewInit, ReflectiveInjector } from '@angular/core';
import { Message } from './message';
import { MessageService } from './messageService';
import { Http } from '@angular/http';
import { NgForm } from "@angular/forms/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{  
    private http;
    constructor (private _http: Http) {this.http=_http;}
  
  onSubmit(f : NgForm) {    
    var messageService = new MessageService(this.http);
    messageService.sendMessage(new Message(f.value.userName, f.value.text));
  } 
}