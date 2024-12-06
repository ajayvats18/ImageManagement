import { Component, OnInit } from '@angular/core';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.css'],
})
export class ImageListComponent implements OnInit {
  images = [];

  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
    this.imageService.images$.subscribe((images) => {
      this.images = images;
    });
    this.imageService.fetchImages();
  }

  onDelete(id: number): void {
    if (confirm('Are you sure you want to delete this image?')) {
      this.imageService.deleteImage(id).subscribe(() => {
        this.imageService.fetchImages();
      });
    }
  }

  onEdit(id: number): void {
    // Implement navigation to edit page or open edit form
    console.log('Edit image with ID:', id);
  }
}
