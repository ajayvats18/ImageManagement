import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageService } from '../../services/image.service';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.scss'],
})
export class ImageUploadComponent {
  uploadForm: FormGroup;

  constructor(private fb: FormBuilder, private imageService: ImageService) {
    this.uploadForm = this.fb.group({
      file: [null, Validators.required],
    });
  }

  onSubmit(): void {
    if (this.uploadForm.valid) {
      const file = this.uploadForm.get('file')?.value;
      this.imageService.uploadImage(file).subscribe(() => {
        alert('Image uploaded successfully!');
        this.imageService.fetchImages();
      });
    }
  }
}
