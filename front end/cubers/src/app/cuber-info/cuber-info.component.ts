import { Component, OnInit } from '@angular/core';
import {Cuber} from '../models/Cuber';
import {CuberService} from '../cuber.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-cuber-info',
  templateUrl: './cuber-info.component.html',
  styleUrls: ['./cuber-info.component.css', '../app.component.css']
})
export class CuberInfoComponent implements OnInit {

  cuber: Cuber;
  best3x3: number;
  average3x3: number;
  bestOh: number;
  averageOh: number;
  best4x4: number;
  average4x4: number;

  constructor(private service: CuberService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    let id = +this.route.snapshot.paramMap.get('id');
    this.service.getCuber(id).subscribe(response => {
      this.cuber = response;
      this.calculateStats();
    });
  }

  backButtonClick(){
    this.router.navigateByUrl('');
  }

  editButtonClick(){
    this.router.navigateByUrl('edit/' + this.cuber.id);
  }

  // finds the cuber's best solve and average for each event
  calculateStats(): void{
    let best = null;
    let sum = 0;
    this.cuber.solves3x3.forEach(time => {
      if(best === null || time < best){
        best = time;
      }
      sum += time;
    });
    if(this.cuber.solves3x3.length > 0){
      this.best3x3 = best;
      this.average3x3 = Math.round(sum / this.cuber.solves3x3.length * 100) / 100;
    }

    best = null;
    sum = 0;
    this.cuber.solvesOh.forEach(time => {
      if(best === null || time < best){
        best = time;
      }
      sum += time;
    });
    if(this.cuber.solvesOh.length > 0){
      this.bestOh = best;
      this.averageOh = Math.round(sum / this.cuber.solvesOh.length * 100) / 100;
    }

    best = null;
    sum = 0;
    this.cuber.solves4x4.forEach(time => {
      if(best === null || time < best){
        best = time;
      }
      sum += time;
    });
    if(this.cuber.solves4x4.length > 0){
      this.best4x4 = best;
      this.average4x4 = Math.round(sum / this.cuber.solves4x4.length * 100) / 100;
    }


  }

}
