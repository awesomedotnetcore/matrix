import { Component, OnInit, TemplateRef } from '@angular/core';
import { ApplicationService } from '../../services/application.service';
import { Application } from '../../entities/application';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'matrix-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public appId: string;
  public appName: string;
  public appDescription: string;
  public applications: Application[];
  public searchTerm: string;
  public modal: BsModalRef;

  constructor(private applicationService: ApplicationService, private modalService: BsModalService) {
  }

  ngOnInit() {
    this.applicationService.getApplications().subscribe(apps => {
      this.applications = apps;
    }, error => {
      console.log(error);
    });
  }

  load() {
    this.applicationService.getApplications().subscribe(apps => {
      this.applications = apps;
    }, error => {
      console.log(error);
    });
  }

  open(template: TemplateRef<any>) {
    this.modal = this.modalService.show(template);
  }

  openEditDialog(template: TemplateRef<any>, id: string, name: string, description: string) {
    this.appId = id.toUpperCase();
    this.appName = name;
    this.appDescription = description;
    this.modal = this.modalService.show(template);
  }

  create() {
    this.applicationService.register(this.appName, this.appDescription).subscribe(data => {
      if (this.modal) {
        this.modal.hide();
        this.appId = "";
        this.appName = "";
        this.appDescription = "";
      }
      this.load();
    }, error => {
      console.log(error);
    });
  }

  update() {
    this.applicationService.update(this.appId, this.appName, this.appDescription).subscribe(data => {
      if (this.modal)
        this.modal.hide();
      this.load();
    }, error => {
      console.log(error);
    });
  }

  delete(id: string) {
    this.applicationService.unregister(id).subscribe(data => {
      this.load();
    }, error => {
      console.log(error);
    });
  }
}
