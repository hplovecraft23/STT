import { Component, OnInit } from '@angular/core';
import { ApiClientService } from '../api-client.service';
import { CompetitionListDTO, Competition } from '../web-api-models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public competition: CompetitionListDTO;
  competitions: Array<Competition>;
  constructor(private web: ApiClientService) {}

  ngOnInit() {
   this.web.getLeagues().subscribe( (data) => {this.competition = data; } );
   this.competitions = this.competition.Competitions.competitions;
  }

}
