import { Component, OnInit } from '@angular/core';
import { RestService } from '../services/rest-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  message: string = '';

  constructor(private restService: RestService) { }

  ngOnInit() {
    this.restService.getSomeData().subscribe({
      next: (data) => this.message = data,
      error: (err) => this.message = "Error from API"
    });
  }
}
