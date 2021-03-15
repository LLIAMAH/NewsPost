import {Component, Inject, OnInit} from '@angular/core';
import {IPost} from '../../../app-interfaces/interfaces';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

  public arrayOfPosts: Array<IPost>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IPost[]>(baseUrl + 'posts').subscribe(result => {
      this.arrayOfPosts = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
  }

}
