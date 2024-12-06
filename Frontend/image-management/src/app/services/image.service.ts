import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Image } from '../models/image.model';  // Import the Image model

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  private apiUrl = `${environment.apiUrl}/images`;
  private imagesSubject = new BehaviorSubject<Image[]>([]);
  images$ = this.imagesSubject.asObservable();

  constructor(private http: HttpClient) {}

  fetchImages(): void {
    this.http.get<Image[]>(this.apiUrl).subscribe((images) => {
      this.imagesSubject.next(images);
    });
  }

  deleteImage(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  uploadImage(formData: FormData): Observable<any> {
    return this.http.post(this.apiUrl, formData);
  }
}
