import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, EMPTY } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SnackService {
  Snack(msg: string): Observable<{}> {
    this.snackbar.open(msg, 'Close');
    return EMPTY;
  }
  constructor(public snackbar: MatSnackBar) { }
}
