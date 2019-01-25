import { Component, OnInit } from '@angular/core';
import {CuberService} from '../cuber.service';
import {CuberSummary} from '../models/CuberSummary';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cuber-summary',
  templateUrl: './cuber-summary.component.html',
  styleUrls: ['./cuber-summary.component.css', '../app.component.css'],
  providers: [CuberService]
})
export class CuberSummaryComponent implements OnInit {

  //private service: CuberService;

  summary: CuberSummary;

  constructor(private service: CuberService, private router: Router) { }

  ngOnInit() {
    this.getCuberSummary();
  }

  addButtonClick(){
    this.router.navigateByUrl('edit');
  }

  cuberClick(index){
    let cuberId = this.summary.cubers[index].cuberId;
    this.router.navigateByUrl('cuber/' + cuberId);
  }

  cuberDelete(index){
    let cuberId = this.summary.cubers[index].cuberId;
    this.summary.cubers.splice(index, 1);
    this.service.deleteCuber(cuberId).subscribe(response => {
      this.getCuberSummary(); // necessary because the deleted cuber may still be in leaders
    });
  }

  getCuberSummary(){
    this.service.getCuberSummary().subscribe(response => {
      // if cuber has pb less than 0, set it to null
      for (let cuber of response.cubers)
      {
        if(cuber.pb3x3 < 0){
          cuber.pb3x3 = null;
        }
        if(cuber.pbOh < 0){
          cuber.pbOh = null;
        }
        if(cuber.pb4x4 < 0){
          cuber.pb4x4 = null;
        }
      }
      this.summary = response
    });
  }



}
