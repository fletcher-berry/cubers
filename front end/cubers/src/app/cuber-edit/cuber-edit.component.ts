import { Component, OnInit } from '@angular/core';
import {Cuber} from '../models/Cuber';
import {CuberService} from '../cuber.service';
import { ActivatedRoute, Router } from '@angular/router';
import {CuberMetadata} from '../models/CuberMetadata'

@Component({
  selector: 'app-cuber-edit',
  templateUrl: './cuber-edit.component.html',
  styleUrls: ['./cuber-edit.component.css', '../app.component.css']
})
export class CuberEditComponent implements OnInit {

  constructor(private service: CuberService, private route: ActivatedRoute, private router: Router) { }

  cuber: Cuber;
  list3x3: string;
  listOh: string;
  list4x4: string;

  ngOnInit() {
    let id = +this.route.snapshot.paramMap.get('id');
    if(id != 0){
      this.service.getCuber(id).subscribe(response => {
        this.cuber = response
        this.initializeSolveLists();
      });
    }
    else{
      this.cuber = new Cuber();
      this.cuber.id = -1;
      this.cuber.metadata = []
    }
  }

  backButtonClick(){
    this.router.navigateByUrl('cuber/' + this.cuber.id);
  }

  saveButtonClick(){
    if(!this.cuber.name){
      alert("Name is missing");
      return;
    }
    this.cuber.solves3x3 = this.stringToTimeList(this.list3x3);
    this.cuber.solvesOh = this.stringToTimeList(this.listOh);
    this.cuber.solves4x4 = this.stringToTimeList(this.list4x4);
    if(this.cuber.solves3x3 === null){
      alert("3x3 solves is not in correct format");
      return;
    }
    if(this.cuber.solvesOh === null){
      alert("OH solves is not in correct format");
      return;
    }
    if(this.cuber.solves4x4 === null){
      alert("4x4 solves is not in correct format");
      return;
    }

    this.service.updateCuber(this.cuber).subscribe(response => {
      alert('saved');
      this.cuber.id = response.id;
    });
  }

  addMetaButtonClick(){
    this.cuber.metadata.push(new CuberMetadata());
  }

  removeMetaButtonClick(index: number){
    this.cuber.metadata.splice(index, 1);
  }

  // initializes text versions of the cuber's list of solves for each event
  initializeSolveLists(): void{
    this.list3x3 = this.cuber.solves3x3.reduce((acc, curr, idx) => {
      acc += curr.toString();
      if(idx != this.cuber.solves3x3.length - 1){
        acc += ", "
      };
      return acc;
    }, "");

    this.listOh = this.cuber.solvesOh.reduce((acc, curr, idx) => {
      acc += curr.toString();
      if(idx != this.cuber.solvesOh.length - 1){
        acc += ", "
      };
      return acc;
    }, "");

    this.list4x4 = this.cuber.solves4x4.reduce((acc, curr, idx) => {
      acc += curr.toString();
      if(idx != this.cuber.solves4x4.length - 1){
        acc += ", "
      };
      return acc;
    }, "");
  }

  // parse a comma delimited list of times into an array of times
  // returns null if input is not formatted correctly
  stringToTimeList(str: string): number[]{
    if(!str || str.trim() == ""){
      return [];
    }
    var timeList = str.split(", ").map(val => {
      if (!val) {
        return null;
      }
      var time = 0;
      var seconds;
      if (val.indexOf(":") != -1) {
        var minutes = val.substring(0, val.indexOf(":"));
        if (+minutes) {
          time += 60 * +minutes;
        }
        else {
          return null;
        }
        seconds = val.substring(val.indexOf(":") + 1);
      }
      else {
        seconds = val;
      }
      if (+seconds) {
        time += +seconds;
      }
      else {
        return null;
      }
      return time;
    });
    for(let time of timeList){
      if(time === null){
        return null;
      }
    }
    return timeList;
  }

}
