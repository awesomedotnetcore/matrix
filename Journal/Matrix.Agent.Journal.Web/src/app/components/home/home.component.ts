import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../services/application.service';
import { LogService } from '../../services/log.service';
import { Application } from '../../entities/application';
import { Log } from '../../entities/log';

@Component({
  selector: 'matrix-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  logs: Log[];
  applications: Application[];
  application: string;
  startDate: string = new Date().toLocaleString('en-US', { hour12: false, timeZone: 'UTC', year: 'numeric', month: 'numeric', day: 'numeric', hour: "2-digit", minute: "2-digit", second: "2-digit" });
  endDate: string = new Date().toLocaleString('en-US', { hour12: false, timeZone: 'UTC', year: 'numeric', month: 'numeric', day: 'numeric', hour: "2-digit", minute: "2-digit", second: "2-digit" });
  searchTerm: string;

  constructor(private applicationService: ApplicationService, private logService: LogService) {
  }

  ngOnInit() {
    this.applicationService.getApplications().subscribe(apps => {
      this.applications = apps;
    }, error => {
      console.log(error);
    });
  }

  applicationChanged() {
    this.logService.getLogs(this.application, this.formatDate(new Date(Date.parse(this.startDate))), this.formatDate(new Date(Date.parse(this.endDate)))).subscribe(logs => {
      this.logs = logs;
    }, error => {
      console.log(error);
    });
  }

  search() {
    if (this.application) {
      if (this.searchTerm) {
        this.logService.searchLogs(this.application, this.formatDate(new Date(Date.parse(this.startDate))), this.formatDate(new Date(Date.parse(this.endDate))), this.searchTerm).subscribe(logs => {
          this.logs = logs;
        }, error => {
          console.log(error);
        });
      }
      else {
        this.logService.getLogs(this.application, this.formatDate(new Date(Date.parse(this.startDate))), this.formatDate(new Date(Date.parse(this.endDate)))).subscribe(logs => {
          this.logs = logs;
        }, error => {
          console.log(error);
        });
      }
    }
  }

  formatDate(date: Date): string {
    let result: string = "";

    let strYear = date.getFullYear() + "";
    let strMonth = (date.getMonth() + 1) + "";
    let strDay = date.getDate() + "";
    let strHour = date.getHours() + "";
    let strMinute = date.getMinutes() + "";
    let strSecond = date.getSeconds() + "";

    if (strMonth.length === 1) {
      strMonth = "0" + strMonth;
    }
    if (strDay.length === 1) {
      strDay = "0" + strDay;
    }
    if (strHour.length === 1) {
      strHour = "0" + strHour;
    }
    if (strMinute.length === 1) {
      strMinute = "0" + strMinute;
    }
    if (strSecond.length === 1) {
      strSecond = "0" + strSecond;
    }

    result = strYear + strMonth + strDay + "T" + strHour + strMinute + strSecond;

    return result;
  }
}
