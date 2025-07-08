import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  template: `
    <div class="modal-header">
      <h4 class="modal-title">{{ title }}</h4>
      <button type="button" class="btn-close" aria-label="Close" (click)="activeModal.dismiss()"></button>
    </div>
    <div class="modal-body">
      <p>{{ message }}</p>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-outline-secondary" (click)="activeModal.dismiss()">Отмена</button>
      <button type="button" class="btn btn-primary" (click)="activeModal.close(true)">Подтвердить</button>
    </div>
  `
})
export class ConfirmDialogComponent {
  activeModal = inject(NgbActiveModal);

  @Input() title: string = '';
  @Input() message: string = '';
}
