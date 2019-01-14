import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { Observable, Subject,forkJoin} from 'rxjs';
import {WeatherInfo} from 'src/app/Models/weatherInfo';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  private subjectSelectedCity = new Subject<WeatherInfo>();
  private subjectCities = new Subject<string[]>();

  constructor(private http: HttpClient) { }

  searchCities(term: string) {
    if (!term) {
      this.subjectCities.next([]);
      return;
    }

    this.http.get<string[]>('https://localhost:44386/api/Weather/searchCities?term=' + term).subscribe(data => {
      this.subjectCities.next(data);
    })
  }

  getCitiesFromSearch(): Observable<string[]> {
      return this.subjectCities.asObservable();
  }

  citySelected(selected:string){
        const params = new HttpParams().set('q', selected);
    this.http.post('https://localhost:44386/api/Weather/GetWeather', { city: selected })
      .subscribe(data => {
        this.subjectSelectedCity.next(new WeatherInfo(data['city'], data['tmp'], data['wind'], data['feelLike']));
        })
  }

  getSelectedCity(): Observable<WeatherInfo> {
          return this.subjectSelectedCity.asObservable();
  }

  getDefaultCities(cities:string[]):Observable<any[]>{
    return forkJoin(
      this.http.post('https://localhost:44386/api/Weather/GetWeather', { city: cities[0]}),
      this.http.post('https://localhost:44386/api/Weather/GetWeather', { city: cities[1]}),
      this.http.post('https://localhost:44386/api/Weather/GetWeather', { city: cities[2]})
    );
  }
}
