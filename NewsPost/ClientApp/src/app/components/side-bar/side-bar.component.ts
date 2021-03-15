import {Component, Input, OnInit} from '@angular/core';
import {IPostActions} from '../../app-interfaces/interfaces';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})

export class SideBarComponent implements OnInit {

  @Input() postAdd: IPostActions;

  constructor() { }

  ngOnInit(): void {
  }

}
