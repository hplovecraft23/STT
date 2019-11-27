import { Component, OnInit } from '@angular/core';
import { ApiClientService } from '../api-client.service';
import { CompetitionListDTO, Competition } from '../web-api-models';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  competition: CompetitionListDTO = new CompetitionListDTO();
  competitions: Competition[] = [];
  gotdata = false;
  spinner = false;
  constructor(private web: ApiClientService, private snack: MatSnackBar) {}
  public showSpinner() {
    this.spinner = true;
  }
  public hideSpinner() {
    this.spinner = false;
  }

  ngOnInit() {
    this.getList();
  }


  async getList() {
    this.showSpinner();
    await this.web.getLeagues().toPromise()
    .then(data => {
      this.competitions = data.competitions.competitions;
      this.gotdata = true;
    })
    .catch(err => {
      this.hideSpinner();
      this.snack.open('Error getting competition list: ' + err.message, 'Close');
    })
    .finally(
      () => {
        this.hideSpinner();
      });
  }
}
