import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecast, WeatherForecastService } from 'src/app/swagger-generated';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  providers: [WeatherForecastService]
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(weatherForecastService : WeatherForecastService) {
    weatherForecastService.weatherForecastGet().subscribe(response =>{
      this.forecasts = response;
    });
  }
  
}
