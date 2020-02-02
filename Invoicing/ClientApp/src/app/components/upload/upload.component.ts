import { Component, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent{
  public progress: number;
  public message: string;
  private url: string = "api/files/upload";
  public success: boolean = true;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) { }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(this.url, formData, { reportProgress: true, observe: 'events' })
      .subscribe(
        event => {
          this.success = true;
          if (event.type === HttpEventType.UploadProgress) {
            this.progress = Math.round(100 * event.loaded / event.total);
          }
          else if (event.type === HttpEventType.Response) {
            this.message = 'Upload success.';
            this.onUploadFinished.emit(event.body);
          }
        },
        err => {
          this.success = false;
          this.progress = null;
          this.message = err.error;
          this.onUploadFinished.emit(null);
        }
      );
  }


}
