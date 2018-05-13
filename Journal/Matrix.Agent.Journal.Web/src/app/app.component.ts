import { Component, OnInit } from '@angular/core';
import { MetadataService } from './services/metadata.service';
import { Metadata } from './entities/metadata';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'matrix-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'matrix';
  metadata: Metadata = new Metadata();

  constructor(private modalService: BsModalService, private metadataService: MetadataService) {
  }

  ngOnInit() {
    this.metadataService.getMetadata().subscribe(data => {
      this.metadata = data;
    }, error => {
      console.log(error);
    });
  }

  open(modal) {
    this.modalService.show(modal);
    console.log(modal);
  }
}
