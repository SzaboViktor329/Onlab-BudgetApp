import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js/auto';
import { ThousandSeparatorPipe } from 'src/app/pipes/thousand-separator.pipe';
import { AuthService } from 'src/app/services/auth.service';
import { CategoryReportModel, ReportModel, ReportService } from 'src/app/swagger-generated';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  providers: [ReportService]
})
export class ReportsComponent implements OnInit {
  public chart: any;
  public accountId = this.authService.getSelectedAccountId();
  public availableYearsMonths : Date[] = [];
  public availableYears : Date[] = [];
  public annualReport : ReportModel = {};
  public monthlyReports : ReportModel[] = [];
  public categoryReport : CategoryReportModel[] = [];
  public categories : string[] = [];
  public amounts : number[] = [];
  public annual : boolean = true;

  public thousandSeparator : ThousandSeparatorPipe = new ThousandSeparatorPipe();

  constructor(private reportService : ReportService, public authService : AuthService){
    reportService.apiReportAvailableyearsGet(this.accountId).subscribe(response=>{
      for(var dateString of response){
        this.availableYears.push(new Date(dateString));
      }
      this.getAnnualReport(this.availableYears[0]);
      this.getMonthlyReport(this.availableYears[0]);
      this.getCategoriesReport(this.availableYears[0]);
    });
    reportService.apiReportAvailabledatesGet(this.accountId).subscribe(response=>{
      for(var dateString of response){
        this.availableYearsMonths.push(new Date(dateString));
      }
    });
  }

  annualReportSelectionChanged(dateString : string){
    this.getAnnualReport(new Date(dateString));
  }
  
  monthlyReportSelectionChanged(dateString : string){
    this.getMonthlyReport(new Date(dateString));
  }

  categoryReportSelectionChanged(dateString : string){
    this.getCategoriesReport(new Date(dateString));
  }
  
  setAnnual(annual : boolean){
    this.annual=annual;
    this.getCategoriesReport(this.availableYearsMonths[0]);
    console.log(this.annual);
  }


  ngOnInit(): void {
    this.createChart();
  }

  getAnnualReport(date: Date){
    this.reportService.apiReportAnnualGet(this.accountId,date).subscribe(response=>{
      this.annualReport=response;
      console.log(this.annualReport);
    })
  }

  getMonthlyReport(date: Date){
    this.reportService.apiReportMonthlyGet(this.accountId,date).subscribe(response=>{
      this.monthlyReports=response;
      console.log(this.monthlyReports);
    })
  }

  getCategoriesReport(date : Date){
    this.reportService.apiReportCategoriesreportGet(this.accountId,this.annual,date).subscribe(response=>{
      this.categoryReport=response;
      this.initPieChartData();
      this.chart.update();
      console.log(this.categoryReport);
    })
  }

  initPieChartData(){
    this.amounts.length=0;
    this.categories.length=0;
    for(var i =0;i<this.categoryReport.length;i++){
      this.categories.push(this.categoryReport[i].category as string);
      this.amounts.push(this.categoryReport[i].amount as number);
    }
  }

  createChart(){
    this.chart = new Chart("MyChart", {
      type: 'pie', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: this.categories,
	       datasets: [{
    label: 'My First Dataset',
    data: this.amounts,
    backgroundColor: [
      'red',
      'pink',
      'green',
			'yellow',
      'orange',
      'blue',			
    ],
    hoverOffset: 4
  }],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
      }
    });
  }
}
