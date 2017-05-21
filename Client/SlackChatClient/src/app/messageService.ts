import { Injectable }              from '@angular/core';
import { Http, Response, Headers, RequestOptions }          from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Message } from './message';
  
let headers = new Headers({ 'Content-Type': 'application/json' });
let options = new RequestOptions({ headers: headers });

@Injectable()
export class MessageService {
  private slackControllerUrl = 'http://localhost:5000/';  // URL to web API
  private http;
  constructor (private _http: Http) {this.http=_http;}
  
  public getMessages(): Observable<Message[]> {
    return this.http.get(this.slackControllerUrl)
                    .map(this.extractData);
  }

  private extractData(res: Response) {
    let body = res.json();
    return body.data || { };
  }

  public sendMessage(message: Message){
      this.http
           .post(this.slackControllerUrl + 'api/slack/postMessageToSlack?userName=' + message.UserName + '&text=' + message.Text,
            null,
            { headers: new Headers({ "Content-Type": "application/json" }) })
           .map(response => response.json())
           .subscribe(json => { err => console.log(err) });
  }
}