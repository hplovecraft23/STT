import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError} from 'rxjs';
import { CompetitionListDTO } from './web-api-models';
import { catchError } from 'rxjs/operators';
import { SnackService } from './snack.service';


@Injectable({
  providedIn: 'root'
})
export class ApiClientService {

  private apiurl = 'https://localhost:44383/';
  private list =  'Services/GetLeages';
  private import = 'import-league/';
  private ammount = 'total-players/';
  private key = 'Services/ChangeAPIKEY/';

  public getLeagues(): Observable<any> {
    const url = `${this.apiurl}${this.list}`;
    return this.http.get(url).pipe(
      catchError(_ => this.snack.Snack('Error getting competition list'))
    );
  }
  constructor(private http: HttpClient, private snack: SnackService) { }
}
