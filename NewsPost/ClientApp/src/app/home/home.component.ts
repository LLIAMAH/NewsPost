import { Component } from '@angular/core';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {IPostActions} from '../app-interfaces/interfaces';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  postAddInstance: IPostActions;

  constructor(private readonly modalService: NgbModal) {}
}
