import { Component } from '@angular/core';

export class Message{
    Text: string;
    UserName: string;

    constructor(UserName: string, Text: string) {
        this.UserName = UserName; 
        this.Text = Text;       
    }
}