import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherInfo } from '../Models/weatherInfo';
import {ApiServiceService} from 'src/app/services/api.service';
import { Observable} from 'rxjs';
import {Router} from "@angular/router";


@Injectable({
  providedIn: 'root'
})
export class DbApiService {
  defaultCities:string[] = ["Tel-Aviv","Haifa","Netanya"];
  private citiesFromDb:WeatherInfo[];

  constructor(private http: HttpClient,private apiService: ApiServiceService,private router: Router) { }

  public getDefaultCities(): Observable<any[]> {
    return this.apiService.getDefaultCities(this.defaultCities); // In practice i will send post request with cities names and make the api request from the server.
  }

  public getCitiesFromDB(name:string,password:string){
    this.http.post<WeatherInfo[]>('https://localhost:44386/api/Weather/GetCitiesForUsers',{name,password}).subscribe(cities => {
      this.citiesFromDb = cities;
      this.router.navigate(['']);
    })
  }

  public getCities():WeatherInfo[]{
    return this.citiesFromDb.slice();
  }

  public AddCityToUserList(selectedCity:WeatherInfo){
    this.http.post<any>('https://localhost:44386/api/Weather/Add', { weatherInfo:selectedCity , userInfo:"Take user details/token from local storage"}).subscribe(res=>{}); 
  }

  public saveUserDetails(username: string, password: string) {
    this.http.post<boolean>('https://localhost:44386/api/User/add', { name, password }).subscribe(res => {
      this.router.navigate(['']);
    })
  }

}
