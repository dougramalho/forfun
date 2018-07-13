import { Component } from '@angular/core'

@Component({
    selector: 'messages',
    template: `
        <div *ngFor="let message of messages">
            <mat-card style="margin: 8px;">
                <mat-card-title>{{message.text}}</mat-card-title>
                <mat-card-content>{{message.owner}}</mat-card-content>
            </mat-card>
        </div>
        `
})

export class MessagesComponent {
    messages = [{text: 'some text', owner: 'Tim'}, {text: 'other message', owner: "Jam"}];
}


